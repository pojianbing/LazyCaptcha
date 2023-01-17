using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator.Code
{
    public class DefaultCaptchaCodeGenerator : ICaptchaCodeGenerator
    {
        private CaptchaType _captchaType = CaptchaType.DEFAULT;

        private static readonly Dictionary<CaptchaType, List<char>> TypeCharactersMap = new Dictionary<CaptchaType, List<char>>
        {
            { CaptchaType.DEFAULT , Characters.DEFAULT },
            { CaptchaType.CHINESE , Characters.CHINESE },
            { CaptchaType.NUMBER , Characters.NUMBER },
            { CaptchaType.NUMBER_ZH_CN , Characters.NUMBER_ZH_CN },
            { CaptchaType.NUMBER_ZH_HK , Characters.NUMBER_ZH_HK },
            { CaptchaType.WORD_NUMBER_LOWER , Characters.WORD_NUMBER_LOWER },
            { CaptchaType.WORD_NUMBER_UPPER , Characters.WORD_NUMBER_UPPER },
            { CaptchaType.WORD , Characters.WORD },
            { CaptchaType.WORD_LOWER , Characters.WORD_LOWER },
            { CaptchaType.WORD_UPPER , Characters.WORD_UPPER },
        };

        private static readonly Random random = new Random();

        private static readonly EvaluationEngine EvaluationEngine = new EvaluationEngine();

        /// <summary>
        /// 中文操作符
        /// </summary>
        private static Dictionary<char,char> OPERATOR_MAP = new Dictionary<char, char>
        { 
             { '+', '加' },  { '-', '减' }, { 'x', '乘' }
        };

        public DefaultCaptchaCodeGenerator() : this(CaptchaType.DEFAULT)
        {

        }

        public DefaultCaptchaCodeGenerator(CaptchaType captchaType)
        {
            this._captchaType = captchaType;
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="length">长度（ARITHMETIC, ARITHMETIC_ZH 长度代表乘数个数）</param>
        /// <returns>（渲染文本，code）</returns>
        public (string renderText, string code) Generate(int length)
        {
            if (this._captchaType == CaptchaType.ARITHMETIC)
            {
                return GenerateaArithmetic(length);
            }
            else if (this._captchaType == CaptchaType.ARITHMETIC_ZH)
            {
                return GenerateaArithmeticZh(length);
            }
            else if (this._captchaType == CaptchaType.NUMBER_ZH_CN)
            {
                return GenerateaNumberZH(length, false);
            }
            else if (this._captchaType == CaptchaType.NUMBER_ZH_HK)
            {
                return GenerateaNumberZH(length, true);
            }
            else
            {
                var code = Pick(TypeCharactersMap[this._captchaType], length);
                return (code, code);
            }
        }

        private (string renderText, string code) GenerateaNumberZH(int length, bool isHk)
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var characters = isHk ? Characters.NUMBER_ZH_HK : Characters.NUMBER_ZH_CN;

            for (int i = 0; i < length; i++)
            {
                var num = random.Next(characters.Count);
                sb1.Append(characters[num]);
                sb2.Append(Characters.NUMBER[num]);
            }

            return (sb1.ToString(), sb2.ToString());
        }

        /// <summary>
        /// 生成算术表达式组成部分
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private (List<int> operands, List<char> operators) GenerateaArithmeticParts(int length)
        {
            // 生成操作数
            var operands = new List<int>();
            Enumerable.Range(0, length).ToList().ForEach(x => operands.Add(random.Next(10)));

            // 生成操作符
            var operators = new List<char>();
            Enumerable.Range(0, length - 1).ToList().ForEach(x =>
            {
                switch (random.Next(3))
                {
                    case 0:
                        operators.Add('+');
                        break;
                    case 1:
                        operators.Add('-');
                        break;
                    case 2:
                        operators.Add('x');
                        break;
                }
            });

            // fix: 在数字运算的时候出现减法，建议结果不要出现负数 https://gitee.com/pojianbing/lazy-captcha/issues/I6ATSJ
            // 多操作数比较复杂，目前仅修复两个操作数的情况
            if (length == 2 && operators[0] == '-')
            {
                var max = Math.Max(operands[0], operands[1]);
                var min = Math.Min(operands[0], operands[1]);
                operands[0] = max;
                operands[1] = min;
            }

            return (operands, operators);
        }

        /// <summary>
        /// 生成阿拉伯算术表达式
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private (string renderText, string code) GenerateaArithmetic(int length)
        {
            (var operands, var operators) = GenerateaArithmeticParts(length);

            // 生成表达式
            var sb = new StringBuilder();
            sb.Append(operands[0]);
            for (var i = 0; i < length - 1; i++)
            {
                sb.Append(operators[i]);
                sb.Append(operands[i + 1]);
            }

            var result = ((int)EvaluationEngine.Evaluate(sb.ToString())).ToString();
            return (sb.Append("=?").ToString(), result);
        }

        /// <summary>
        /// 生成汉字算术表达式
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private (string renderText, string code) GenerateaArithmeticZh(int length)
        {
            (var operands, var operators) = GenerateaArithmeticParts(length);

            // 生成表达式
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            sb1.Append(operands[0]);
            sb2.Append(Characters.NUMBER_ZH_CN[operands[0]]);
            for (var i = 0; i < length - 1; i++)
            {
                // 操作符
                sb1.Append(operators[i]);
                sb2.Append(OPERATOR_MAP[operators[i]]);
                // 操作符
                sb1.Append(operands[i + 1]);
                sb2.Append(Characters.NUMBER_ZH_CN[operands[i + 1]]);
            }

            var result = ((int)EvaluationEngine.Evaluate(sb1.ToString())).ToString();
            return (sb2.Append("=?").ToString(), result);
        }

        /// <summary>
        /// 随机挑选字符
        /// </summary>
        /// <param name="characters">字符列表</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        private string Pick(List<char> characters, int count)
        {
            var result = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                result.Append(characters[random.Next(characters.Count)]);
            }

            return result.ToString();
        }
    }
}

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
            var characters = isHk? Characters.NUMBER_ZH_HK : Characters.NUMBER_ZH_CN;

            for (int i = 0; i < length; i++)
            {
                var num = random.Next(characters.Count);
                sb1.Append(characters[num]);
                sb2.Append(Characters.NUMBER[num]);
            }

            return (sb1.ToString(), sb2.ToString());
        }

        private (string renderText, string code) GenerateaArithmetic(int length)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                sb.Append(random.Next(10));
                if (i < length - 1)
                {
                    int type = random.Next(1, 4);
                    if (type == 1)
                    {
                        sb.Append("+");
                    }
                    else if (type == 2)
                    {
                        sb.Append("-");
                    }
                    else if (type == 3)
                    {
                        sb.Append("x");
                    }
                }
            }

            var result = ((int)EvaluationEngine.Evaluate(sb.ToString())).ToString();

            sb.Append("=?");

            return (sb.ToString(), result);
        }

        private (string renderText, string code) GenerateaArithmeticZh(int length)
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var operand = random.Next(10);
                sb1.Append(operand);
                sb2.Append(Characters.NUMBER_ZH_CN[operand]);

                if (i < length - 1)
                {
                    int type = random.Next(1, 4);
                    if (type == 1)
                    {
                        sb1.Append("+");
                        sb2.Append("加");
                    }
                    else if (type == 2)
                    {
                        sb1.Append("-");
                        sb2.Append("减");
                    }
                    else if (type == 3)
                    {
                        sb1.Append("x");
                        sb2.Append("乘");
                    }
                }
            }

            var result = ((int)EvaluationEngine.Evaluate(sb1.ToString())).ToString();

            sb2.Append("=?");

            return (sb2.ToString(), result);
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

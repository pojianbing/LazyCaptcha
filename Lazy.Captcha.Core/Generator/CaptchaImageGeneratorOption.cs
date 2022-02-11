using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core.Generator
{
    public class CaptchaImageGeneratorOption
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public CaptchaType CaptchaType { get; set; } = CaptchaType.DEFAULT;
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;
        /// <summary>
        /// FontFamily
        /// </summary>
        public FontFamily FontFamily { get; set;}
        /// <summary>
        /// FontStyle
        /// </summary>
        public FontStyle FontStyle { get; set;} = FontStyle.Regular;
        /// <summary>
        /// 字体大小
        /// </summary>
        public float FontSize { get; set; } = 28;

        /// <summary>
        /// 验证码的宽
        /// </summary>
        public int Width { get; set; } = 130;
        /// <summary>
        /// 验证码高
        /// </summary>
        public int Height { get; set; } = 48;
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int Length { get; set; } = 4;
        /// <summary>
        /// 是否绘制气泡
        /// </summary>
        public bool DrawBubble { get; set; } = true;
        /// <summary>
        /// 气泡数量
        /// </summary>
        public int BubbleCount { get; set; } = 3;
        /// <summary>
        /// 气泡边沿厚度
        /// </summary>
        public float BubbleThickness { get; set; } = 1;
        /// <summary>
        /// 是否绘制干扰线
        /// </summary>
        public bool DrawInterferenceLine { get; set; } = true;
        /// <summary>
        /// 干扰线数量
        /// </summary>
        public int InterferenceLineCount { get; set; } = 1;
        /// <summary>
        /// 中文字符池
        /// </summary>
        public List<char> ChineseTexts { get; set; } = new List<char> { '的', '一', '国', '在', '人', '了', '有', '中', '是', '年', '和', '大', '业', '不', '为', '发', '会', '工', '经', '上', '地', '市', '要', '个', '产', '这', '出', '行', '作', '生', '家', '以', '成', '到', '日', '民', '来', '我', '部', '对', '进', '多', '全', '建', '他', '公', '开', '们', '场', '展', '时', '理', '新', '方', '主', '企', '资', '实', '学', '报', '制', '政', '济', '用', '同', '于', '法', '高', '长', '现', '本', '月', '定', '化', '加', '动', '合', '品', '重', '关', '机', '分', '力', '自', '外', '者', '区', '能', '设', '后', '就', '等', '体', '下', '万', '元', '社', '过', '前', '面', '农', '也', '得', '与', '说', '之', '员', '而', '务', '利', '电', '文', '事', '可', '种', '总', '改', '三', '各', '好', '金', '第', '司', '其', '从', '平', '代', '当', '天', '水', '省', '提', '商', '十', '管', '内', '小', '技', '位', '目', '起', '海', '所', '立', '已', '通', '入', '量', '子', '问', '度', '北', '保', '心', '还', '科', '委', '都', '术', '使', '明', '着', '次', '将', '增', '基', '名', '向', '门', '应', '里', '美', '由', '规', '今', '题', '记', '点', '计', '去', '强', '两', '些', '表', '系', '办', '教', '正', '条', '最', '达', '特', '革', '收', '二', '期', '并', '程', '厂', '如', '道', '际', '及', '西', '口', '京', '华', '任', '调', '性', '导', '组', '东', '路', '活', '广', '意', '比', '投', '决', '交', '统', '党', '南', '安', '此', '领', '结', '营', '项', '情', '解', '议', '义', '山', '先', '车', '然', '价', '放', '世', '间', '因', '共', '院', '步', '物', '界', '集', '把', '持', '无', '但', '城', '相', '书', '村', '求', '治', '取', '原', '处', '府', '研', '质', '信', '四', '运', '县', '军', '件', '育', '局', '干', '队', '团', '又', '造', '形', '级', '标', '联', '专', '少', '费', '效', '据', '手', '施', '权', '江', '近', '深', '更', '认', '果', '格', '几', '看', '没', '职', '服', '台', '式', '益', '想', '数', '单', '样', '只', '被', '亿', '老', '受', '优', '常', '销', '志', '战', '流', '很', '接', '乡', '头', '给', '至', '难', '观', '指', '创', '证', '织', '论', '别', '五', '协', '变', '风', '批', '见', '究', '支', '那', '查', '张', '精', '每', '林', '转', '划', '准', '做', '需', '传', '争', '税', '构', '具', '百', '或', '才', '积', '势', '举', '必', '型', '易', '视', '快', '李', '参', '回', '引', '镇', '首', '推', '思', '完', '消', '值', '该', '走', '装', '众', '责', '备', '州', '供', '包', '副', '极', '整', '确', '知', '贸', '己', '环', '话', '反', '身', '选', '亚', '么', '带', '采', '王', '策', '真', '女', '谈', '严', '斯', '况', '色', '打', '德', '告', '仅', '它', '气', '料', '神', '率', '识', '劳', '境', '源', '青', '护', '列', '兴', '许', '户', '马', '港', '则', '节', '款', '拉', '直', '案', '股', '光', '较', '河', '花', '根', '布', '线', '土', '克', '再', '群', '医', '清', '速', '律', '她', '族', '历', '非', '感', '占', '续', '师', '何', '影', '功', '负', '验', '望', '财', '类', '货', '约', '艺', '售', '连', '纪', '按', '讯', '史', '示', '象', '养', '获', '石', '食', '抓', '富', '模', '始', '住', '赛', '客', '越', '闻', '央', '席', '坚' };
    
        /// <summary>
        /// 字体
        /// </summary>
        public Font Font
        {
            get
            {
                if (this.FontFamily == null)
                {
                    var fontFamily = this.IsChineseIn() ? DefaultFonts.instance.Kaiti : DefaultFonts.instance.Epilog;
                    return new Font(fontFamily, this.FontSize, this.FontStyle);
                }
                else
                {
                    var fontFamily = this.IsChineseIn() ? DefaultFonts.instance.Kaiti : DefaultFonts.instance.Epilog;
                    return new Font(this.FontFamily, this.FontSize, this.FontStyle);
                }
            }
        }

        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <returns></returns>
        private bool IsChineseIn()
        {
            return this.CaptchaType == CaptchaType.CHINESE ||
                   this.CaptchaType == CaptchaType.NUMBER_ZH_CN ||
                   this.CaptchaType == CaptchaType.NUMBER_ZH_HK ||
                   this.CaptchaType == CaptchaType.NUMBER_ZH_HK;
        }
    }
}

namespace Sample.Winfrom.Models
{
    /// <summary>
    /// 选项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Option<T>
    {
        /// <summary>
        /// 选项文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 选项值
        /// </summary>
        public T Value { get; set; }
    }
}

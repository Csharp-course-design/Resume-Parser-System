namespace Function.Factory
{
    public interface IFactory
    {
        /// <summary>
        /// 获取Models类
        /// </summary>
        /// <param name="Json">Json文件内容</param>
        /// <returns>Models类</returns>
        abstract public static Object Get(string Json);
    }
}

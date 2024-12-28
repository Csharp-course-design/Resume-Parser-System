namespace Function
{
    public class Base64Helper
    {

        /// <summary>
        /// 文件转base64
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>base64字符串</returns>
        public static string FileToBase64String(string path)
        {
            FileStream fsForRead = new FileStream(path, FileMode.Open);//文件路径
            string base64Str = "";
            try
            {
                //读写指针移到距开头10个字节处
                fsForRead.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[fsForRead.Length];
                int log = Convert.ToInt32(fsForRead.Length);
                //从文件中读取10个字节放到数组bs中
                fsForRead.Read(bs, 0, log);
                base64Str = Convert.ToBase64String(bs);
                return base64Str;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
                return base64Str;
            }
            finally
            {
                fsForRead.Close();
            }
        }

        /// <summary>
        /// Base64字符串转文件并保存
        /// </summary>
        /// <param name="base64String">base64字符串</param>
        /// <param name="path">保存的文件名</param>
        /// <returns>是否转换并保存成功</returns>
        public static bool Base64StringToFile(string base64String, string path)
        {
            bool opResult = false;
            try
            {
                string strbase64 = base64String.Trim().Substring(base64String.IndexOf(",") + 1);   //将‘，’以前的多余字符串删除
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strbase64));
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] b = stream.ToArray();
                fs.Write(b, 0, b.Length);
                fs.Close();

                opResult = true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return opResult;
        }

    }
}

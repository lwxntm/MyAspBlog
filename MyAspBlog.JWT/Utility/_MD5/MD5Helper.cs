using System;
using System.Security.Cryptography;
using System.Text;

namespace MyAspBlog.JWT.Utility._MD5
{
    public static class MD5Helper
    {
        public static string MD5Encrypt32(string passwd)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X"));
                }
                return sBuilder.ToString();
            }
        }
    }
}

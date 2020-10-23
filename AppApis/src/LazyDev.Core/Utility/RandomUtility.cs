using System;
using System.Text;

namespace LazyDev.Core.Utility
{
    public class RandomUtility
    {
        private const string Str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public static string RandDomStr(int length = 6)
        {
            var maxlength = Str.Length;
            var strArray = Str.ToCharArray();

            var sb =new StringBuilder(length);
            for (var i = 0; i < 6; i++)
            {
                var index = Random.Next(0, maxlength);
                sb.Append(strArray[index]);
            }
           

            return sb.ToString();
        }
    }
}

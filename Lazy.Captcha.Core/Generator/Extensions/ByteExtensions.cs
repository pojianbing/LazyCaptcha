using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public static class ByteExtensions
    {
        public static Stream ToStream(this byte[] bytes)
        { 
            return new MemoryStream(bytes);
        }

        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}

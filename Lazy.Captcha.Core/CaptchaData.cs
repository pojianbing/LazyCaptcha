using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.Captcha.Core
{
    public class CaptchaData
    {
        public CaptchaData(string id, string code, byte[] bytes)
        {
            Id = id;
            Code = code;
            Bytes = bytes;
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public byte[] Bytes { get; set; }
        public string Base64 => Convert.ToBase64String(Bytes);
    }
}

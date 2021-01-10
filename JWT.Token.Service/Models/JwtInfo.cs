using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Token.Service.Models
{
    public class JwtInfo
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

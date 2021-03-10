using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ErrorsModel
{
    public class InvalidData
    {
        public string Invalid { get; set; }

        public bool ShowCaptcha { get; set; } = false;
    }
}

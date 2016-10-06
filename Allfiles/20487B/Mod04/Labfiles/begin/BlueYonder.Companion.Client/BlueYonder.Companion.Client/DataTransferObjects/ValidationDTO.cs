using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueYonder.Companion.Client.DataTransferObjects
{
    public class ValidationDTO
    {
        public string Message { get; set; }
        public Dictionary<string, List<string>> ModelState { get; set; }
    }
}

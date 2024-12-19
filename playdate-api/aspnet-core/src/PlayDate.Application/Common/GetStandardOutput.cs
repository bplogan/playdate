using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Common
{
    public class GetStandardOutput
    {
        public string ItemId { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool Success { get { return Errors.Count == 0; } }       
    }
}

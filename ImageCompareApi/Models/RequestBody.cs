using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCompareApi.Models
{
    public class RequestBody
    {
        public string frontImage { get; set; }
        public string backImage { get; set; }
    }
}

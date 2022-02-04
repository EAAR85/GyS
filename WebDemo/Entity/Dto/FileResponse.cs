using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Common
{
    public class FileResponse
    {
        public FileResponse(String value) {
            this.value = value;
        }
        public String value { get; set; }
    }
}

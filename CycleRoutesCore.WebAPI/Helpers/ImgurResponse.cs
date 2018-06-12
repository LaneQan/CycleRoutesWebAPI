using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CycleRoutesCore.WebAPI.Helpers
{
    public class ImgurResponse
    {
        public ImgurData Data { get; set; }
    }

    public class ImgurData
    {
        public string Link { get; set; }
    }
}

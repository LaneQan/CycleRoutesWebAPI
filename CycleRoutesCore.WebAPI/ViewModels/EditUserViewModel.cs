using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CycleRoutesCore.WebAPI.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
    }
}

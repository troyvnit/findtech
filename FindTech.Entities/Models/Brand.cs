using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class Brand : Entity
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public int Priority { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}

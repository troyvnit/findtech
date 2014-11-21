﻿using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class DeviceColor : Entity
    {
        public int DeviceColorId { get; set; }
        public string DeviceColorName { get; set; }
        public string Color { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public ICollection<Image> Images { get; set; } 
    }
}
using System;
using System.Collections.Generic;
using FindTech.Entities.Models.Enums;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class Device : Entity
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public MarketStatus MarketStatus { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public int ViewCount { get; set; }
        public BoxSize BoxSize { get; set; }
        public int Priority { get; set; }
        public ICollection<BenchmarkDetail> BenchmarkDetails { get; set; }
        public ICollection<DeviceColor> DeviceColors { get; set; } 
    }
}

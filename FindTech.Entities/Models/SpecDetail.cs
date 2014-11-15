﻿using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class SpecDetail : Entity
    {
        public int SpecDetailId { get; set; }
        public string Value { get; set; }
        public bool HighLight { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int SpecId { get; set; }
        public Spec Spec { get; set; }
    }
}

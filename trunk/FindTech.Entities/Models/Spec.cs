﻿using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class Spec : Entity
    {
        public int SpecId { get; set; }
        public string SpecName { get; set; }
        public int Priority { get; set; }
        public bool? IsMain { get; set; }
        public int SpecGroupId { get; set; }
        public SpecGroup SpecGroup { get; set; }
    }
}

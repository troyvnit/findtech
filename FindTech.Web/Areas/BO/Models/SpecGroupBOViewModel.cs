﻿using System;
using FindTech.Entities.Models.Enums;

namespace FindTech.Web.Areas.BO.Models
{
    public class SpecGroupBOViewModel
    {
        public int SpecGroupId { get; set; }
        public string SpecGroupName { get; set; }
        public int Priority { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class DistrictModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int ProvinceID { set; get; }
        public string Type { get; set; }
    }
}
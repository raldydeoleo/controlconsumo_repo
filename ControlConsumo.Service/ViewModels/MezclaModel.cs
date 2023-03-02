﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class MezclaModel
    {
        public String MANDT { get; set; }
        public String IDPROCESS { get; set; }
        public String IDEQUIPO { get; set; }
        public Int16 SECENTRADA { get; set; }
        public String ZENTRADADATE { get; set; }
        public Int16 SECSALIDA { get; set; }
        public String ZSALIDADATE { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace NovaRules.BRE.Entities
{
    public class WhenFragment
    {
        public WhenFragment()
        {

        }

        public int Section { get; set; }

        public int DataCode { get; set; }

        public int MyProperty { get; set; }

        public SubsectionType DataType { get; set; }

    }
}

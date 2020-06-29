using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace NovaRules.BRE.Entities
{
    public class BaseFragment
    {
        public BaseFragment()
        {

        }

        public string Key { get; set; }

        public string KeyDataType { get; set; } = "string";

        public string Attribute { get; set; }

        public string AttributeDataType { get; set; } = "string";

        public string Value { get; set; }

        public string ValueDataType { get; set; } = "string";

    }
}

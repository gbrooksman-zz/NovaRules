using System;
using System.Collections.Generic;
using System.Text;

namespace NovaRules.BRE.Entities
{
    public class XRule
    {
        public XRule()
        {

        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public string Tag { get; set; }

        public List<LeftFragment> LeftItems { get; set; }

        public List<RightFragment> RightItems { get; set; }

    }
}

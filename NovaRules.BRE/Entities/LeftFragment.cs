using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace NovaRules.BRE.Entities
{
    public class LeftFragment : BaseFragment
    {
        public LeftFragment()
        {

        }

        public bool LinkAsOr { get; set; }
    }
}

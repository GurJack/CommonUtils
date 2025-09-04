using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Settings.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplaySettingsAttribute : Attribute
    {
        public int Order { get; set; } = int.MaxValue;
        public string Category { get; set; } = "General";
        public string Description { get; set; }
        public string DisplayName { get; set; }
    }
}

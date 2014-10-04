using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Attributes;

namespace Library.Enums
{
    public enum GameItems
    {
        [Description("R")]
        Rock,
        [Description("P")]
        Paper,
        [Description("S")]
        Scissor
    }
}

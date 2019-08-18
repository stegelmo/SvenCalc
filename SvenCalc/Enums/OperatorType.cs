using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SvenCalc.Enums
{
    public enum OperatorType
    {
        [Display(Name = "+ Addera")]
        Addition,
        [Display(Name = "* Multiplicera")]
        Multiplication,
        [Display(Name = "/ Dividera")]
        Division,
        [Display(Name = "- Subtrahera")]
        Subtraction
    }
}

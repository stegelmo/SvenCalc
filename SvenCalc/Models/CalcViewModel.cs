using SvenCalc.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SvenCalc.Models
{
    [Serializable]
    public class CalcViewModel
    {
        [Display(Name = "Tal 1")]
        [Required(ErrorMessage = "Var god ange ett tal")]
        public double? NumberA { get; set; }

        [Display(Name = "Operator")]
        [Required(ErrorMessage = "Var god välj en operator")]
        public OperatorType OperatorType { get; set; } = OperatorType.Addition;

        [Display(Name = "Tal 2")]
        [Required(ErrorMessage = "Var god ange ett tal")]
        public double? NumberB { get; set; }

        [Display(Name = "Resultat")]
        public double? Result { get; set; }

        public IEnumerable<OperatorType> ValidOperators{ set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiariosModel
    {
        public long IdCLiente { get; set; }

        public long Id { get; set; }

        [RegularExpression(@"^[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}", ErrorMessage = "Digite um CPF válido")]
        public string CPF { get; set; }

        public string Nome { get; set; }
    }
}
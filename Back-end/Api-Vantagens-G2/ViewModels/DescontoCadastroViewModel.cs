using System;
using System.ComponentModel.DataAnnotations;

namespace SenaiRH_G2.ViewModels
{
    public class DescontoCadastroViewModel
    {

        public int IdDesconto { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public string NomeDesconto { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public string DescricaoDesconto { get; set; }

        public string CaminhoImagemDesconto { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public DateTime ValidadeDesconto { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public int ValorDesconto { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public string NumeroCupom { get; set; }
        public decimal MediaAvaliacaoDesconto { get; set; }

    }
}

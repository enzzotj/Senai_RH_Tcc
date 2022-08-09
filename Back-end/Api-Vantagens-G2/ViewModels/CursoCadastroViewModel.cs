using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.ViewModels
{
    public class CursoCadastroViewModel
    {
        public int IdCurso { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public string NomeCurso { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public string DescricaoCurso { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public string SiteCurso { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public bool ModalidadeCurso { get; set; }

        public string CaminhoImagemCurso { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public int CargaHoraria { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public DateTime DataFinalizacao { get; set; }

        public decimal MediaAvaliacaoCurso { get; set; }


        public byte? IdSituacaoInscricao { get; set; }

        [Required(ErrorMessage = "Todos os campos devem ser preenchidos !")]
        public int? ValorCurso { get; set; }


    }
}

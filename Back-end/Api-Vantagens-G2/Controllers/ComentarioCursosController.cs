using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using SenaiRH_G2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioCursosController : ControllerBase
    {

        private IComentarioCursoRepository _comentarioCurso { get; set; }

        public ComentarioCursosController(IComentarioCursoRepository repo)
        {
            _comentarioCurso = repo;
        }

        /// <summary>
        /// Listar todos s comentarios dos cursos
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult ListarComenatarioCurso()
        {
            try
            {
                return Ok(_comentarioCurso.ListarComenatarioCurso());
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


        /// <summary>
        /// Listar um comentario pelo seu id
        /// </summary>
        /// <param name="id">id Comentario</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult ListarComentarioPorId(int id)
        {
            if (_comentarioCurso.ListarComentarioPorId(id) == null)
            {
                return BadRequest(new
                {
                    mensagem = "Id nao existente!!"
                });
            }
            return Ok(_comentarioCurso.ListarComentarioPorId(id));
        }


        /// <summary>
        /// Listar um comentario pelo seu idcurso
        /// </summary>
        /// <param name="idCurso">id Comentario</param>
        /// <returns></returns>
        [HttpGet("Comentario/{idCurso}")]
        public IActionResult ListarComentarioPorIdCurso(int idCurso)
        {
            try
            {
                return Ok(_comentarioCurso.ListarComentarioPorIdCurso(idCurso));
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deletar um comentario pelo seu id
        /// </summary>
        /// <param name="id">Id do cometario</param>
        /// <returns></returns>
        [HttpDelete("deletar/{id}")]
        public IActionResult ExcluirComentarioCurso(int id)
        {
            if (_comentarioCurso.ListarComentarioPorId(id)==null)
            {
                return BadRequest(new { menssagem = "Esse id nao existe" });
            }

            _comentarioCurso.ExcluirComentarioCurso(id);
            return StatusCode(204);
        }


        /// <summary>
        /// Cadastrar um novo comentario
        /// </summary>
        /// <param name="NovoComentario">Dados do novo comentario</param>
        /// <param name="idcurso">id do curso cujo comentario faz parte </param>
        /// <param name="idUsuario">Id do usuario que cadastrou esse comentario</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CadastrarComentarioCurso(Comentariocurso NovoComentario, int idcurso, int idUsuario)
        {
            try
            {
                if (_comentarioCurso.ListarComentarioPorId(Convert.ToInt16(NovoComentario.IdComentarioCurso))!=null)
                {
                    return BadRequest(new
                    {
                        mensagem = "ja existe um comentario com esse id"
                    });
                }

                if (NovoComentario.IdCurso <= 0 ||  NovoComentario.IdUsuario <= 0 ||  NovoComentario.AvaliacaoComentario == 0 || NovoComentario.ComentarioCurso1 == null  )     
                {
                    return BadRequest(new
                    {
                        mensagem = "Algum dado nao foi preenchido ou nao foi informado corretamente"
                    });
                }

                _comentarioCurso.CadastrarComentarioCurso(NovoComentario);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Listar todos os comentario de um unico usuario 
        /// </summary>
        /// <param name="idUsuario">id do usuario a ser buscado</param>
        /// <returns></returns>
        /// 

        [HttpGet("Comentario/Usuario/{idUsuario}")]
        public IActionResult ListarComenatarioCursoPorUsuario(int idUsuario)
        {
            try
            {
                return Ok(_comentarioCurso.ListarComenatarioCursoPorUsuario(idUsuario));
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }






        }    
    }
}

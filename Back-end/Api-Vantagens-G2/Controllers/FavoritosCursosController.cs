using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosCursosController : ControllerBase
    {

        private IFavoritosCursoRepository _favoritoCurso { get; set; }

        public FavoritosCursosController(IFavoritosCursoRepository repo)
        {
            _favoritoCurso = repo;
        }

        /// <summary>
        /// Listar todos os cursos favoritos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {
                return Ok(_favoritoCurso.ListarTodos());
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


        /// <summary>
        /// Buscar um curso nos favoritos pelo seu id
        /// </summary>
        /// <param name="Id">d do curso favorito</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public IActionResult BuscarCursoFavoritoPorId(int Id)
        {
            if (_favoritoCurso.BuscarCursoFavoritoPorId(Id) == null)
            {
                return BadRequest(new
                {
                    mensagem = "Id nao existente!!"
                });
            }
            return Ok(_favoritoCurso.BuscarCursoFavoritoPorId(Id));
        }

        /// <summary>
        /// Excluir um curso dos favoritoss
        /// </summary>
        /// <param name="Id">Id do curso favorito</param>
        /// <returns></returns>
        [HttpDelete("deletar/{Id}")]
        public IActionResult ExcluirComentarioCurso(int Id)
        {
            if (_favoritoCurso.BuscarCursoFavoritoPorId(Id) == null)
            {
                return BadRequest(new { menssagem = "Esse id nao existe" });
            }

            _favoritoCurso.ExcluirFavoritos(Id);
            return StatusCode(204);
        }

        /// <summary>
        /// Listar um comentario pelo seu idcurso
        /// </summary>
        /// <param name="IdUsuario">id Usuario</param>
        /// <returns></returns>
        [HttpGet("Favorito/{IdUsuario}")]
        public IActionResult ListarPorIdFavoritoCurso(int IdUsuario)
        {
            try
            {
                return Ok(_favoritoCurso.ListarPorIdFavoritoCurso(IdUsuario));
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


        /// <summary>
        /// Adcionar um novo curso aos favoritos
        /// </summary>
        /// <param name="NovoFavorito">Dados obrigatorios para cadastro</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AdcionarFavoritos(Cursofavorito NovoFavorito)
        {
            try
            {
                if (_favoritoCurso.BuscarCursoFavoritoPorId(Convert.ToInt16(NovoFavorito.IdCursoFavorito)) != null)
                {
                    return BadRequest(new
                    {
                        mensagem = "ja existe um comentario com esse id"
                    });
                }

                if (NovoFavorito.IdCurso <=0 || NovoFavorito.IdUsuario <= 0)
                {
                    return BadRequest(new
                    {
                        mensagem = "Algum dado nao foi preenchido ou nao foi informado corretamente"
                    });
                }

                _favoritoCurso.AdcionarFavoritos(NovoFavorito);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

    }
}

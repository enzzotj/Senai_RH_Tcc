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
    public class BairrosController : ControllerBase
    {

        private IBairroRepository _bairroRepository { get; set; }

        public BairrosController(IBairroRepository repo)
        {
            _bairroRepository = repo;
        }

        /// <summary>
        /// Listar todos os bairros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {

                List<Bairro> listarBairro = _bairroRepository.ListarTodos();
                if (listarBairro.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma Bairro cadastrada no sistema!"
                    });
                }
                return Ok(listarBairro);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }


        /// <summary>
        /// Listar um bairro pelo seu id
        /// </summary>
        /// <param name="NomeBairro">Nome do bairro a ser buscado </param>
        /// <returns></returns>
        [HttpGet("BuscarBairro/{NomeBairro}")]
        public IActionResult ListarBairro(string NomeBairro)
        {
            try
            {
                return Ok(_bairroRepository.ListarBairro(NomeBairro));
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }



        /// <summary>
        /// Buscar um bairro pelo seu id 
        /// </summary>
        /// <param name="id">Id do bairro a ser buscado</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_bairroRepository.BuscarPorId(id));
        }

        /// <summary>
        /// Excluir um bairro 
        /// </summary>
        /// <param name="id">Id do bairro a ser excluido</param>
        [HttpDelete("Deletar/{id}")]
        public IActionResult ExcluirBairro(int id)
        {
            try
            {
                if (id != 0)
                {
                    _bairroRepository.ExcluirBairro(id);
                    return StatusCode(204);
                }

                return NotFound();
            }
            catch (Exception execp)
            {
                return BadRequest(execp);
            }

        }

        /// <summary>
        /// Cadastrar um novo bairro 
        /// </summary>
        /// <param name="novoBairro">Dados do bairro a ser cadastrado</param>
        [HttpPost("Cadastrar")]
        public IActionResult CadastrarBairro(Bairro novoBairro)
        {

            try
            {

                if (novoBairro == null)
                {
                    return BadRequest("Todos os campos do usuario devem ser preenchidos !");
                }
                else
                {
                    _bairroRepository.CadastrarBairro(novoBairro);
                    return StatusCode(201);
                }
            }
            catch (Exception exp)
            {

                return BadRequest(exp);
            }

        }

    }
}

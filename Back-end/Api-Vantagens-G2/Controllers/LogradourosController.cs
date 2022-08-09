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
    public class LogradourosController : ControllerBase
    {

        private ILogradouroRepository _logradouroRepository { get; set; }

        public LogradourosController(ILogradouroRepository repo)
        {
            _logradouroRepository = repo;
        }

        /// <summary>
        /// Listar todos os logradouros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {

                List<Logradouro> listarLogradouro = _logradouroRepository.ListarTodos();
                if (listarLogradouro.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma Rua cadastrada no sistema!"
                    });
                }
                return Ok(listarLogradouro);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }


        [HttpGet("BuscarLogradouro/{NomeLogradouro}")]
        public IActionResult ListarLogradouro(string NomeLogradouro)
        {
            try
            {
                return Ok(_logradouroRepository.ListarLogradouro(NomeLogradouro));
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }



        /// <summary>
        /// Buscar um logradouro pelo seu id
        /// </summary>
        /// <param name="id">Id do logradouro a ser buscado</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_logradouroRepository.BuscarPorId(id));
        }


        /// <summary>
        /// Excluir um logradouro 
        /// </summary>
        /// <param name="id">Id do logradouro a ser excluido</param>
        [HttpDelete("Deletar/{id}")]
        public IActionResult ExcluirLogradouro(int id)
        {
            try
            {
                if (id != 0)
                {
                    _logradouroRepository.ExcluirLogradouro(id);
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
        /// Cadastrar um logradouro
        /// </summary>
        /// <param name="novoLogradouro">Dados do logradouro a ser cadatrado</param>
        [HttpPost("Cadastrar")]
        public IActionResult CadastrarLogradouro(Logradouro novoLogradouro)
        {

            try
            {

                if (novoLogradouro == null)
                {
                    return BadRequest("Todos os campos do usuario devem ser preenchidos !");
                }
                else
                {
                    _logradouroRepository.CadastrarLogradouro(novoLogradouro);
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

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
    public class CidadesController : ControllerBase
    {

        private ICidadeRepository _cidadeRepository { get; set; }

        public CidadesController(ICidadeRepository repo)
        {
            _cidadeRepository = repo;
        }

        /// <summary>
        /// Listar todas as cidades
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {

                List<Cidade> listarCidade = _cidadeRepository.ListarTodos();
                if (listarCidade.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma Cidade cadastrada no sistema!"
                    });
                }
                return Ok(listarCidade);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Buscar uma cidade pelo seu id 
        /// </summary>
        /// <param name="id">id do bairro a ser buscado</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_cidadeRepository.BuscarPorId(id));
        }

        [HttpGet("BuscarCidade/{NomeCidade}")]
        public IActionResult ListarCidade(string NomeCidade)
        {
            try
            {
                return Ok(_cidadeRepository.ListarCidade(NomeCidade));
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


        /// <summary>
        /// Excluir uma cidade
        /// </summary>
        /// <param name="id">Id da cidade a ser excluida</param>
        [HttpDelete("Deletar/{id}")]
        public IActionResult ExcluirCidade(int id)
        {
            try
            {
                if (id != 0)
                {
                    _cidadeRepository.ExcluirCidade(id);
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
        /// Cadastrar uma nova cidade
        /// </summary>
        /// <param name="novoCidade">Dados da nova cidade a ser cadastrada</param>
        [HttpPost("Cadastrar")]
        public IActionResult CadastrarCidade(Cidade novoCidade)
        {

            try
            {

                if (novoCidade == null)
                {
                    return BadRequest("Todos os campos do usuario devem ser preenchidos !");
                }
                else
                {
                    _cidadeRepository.CadastrarCidade(novoCidade);
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

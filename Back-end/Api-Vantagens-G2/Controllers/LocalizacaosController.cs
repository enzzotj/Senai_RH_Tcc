using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using SenaiRH_G2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaosController : ControllerBase
    {

        private ILocalizacaoRepository _localizacaoRepository { get; set; }

        public LocalizacaosController(ILocalizacaoRepository repo)
        {
            _localizacaoRepository = repo;
        }


        /// <summary>
        /// Lstar todase as localizacoes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {

                List<Localizacao> listarLocalizacao = _localizacaoRepository.ListarTodos();
                if (listarLocalizacao.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma Localizacao cadastrada no sistema!"
                    });
                }
                return Ok(listarLocalizacao);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }


        /// <summary>
        /// Buscar uma localizacao pelo seu id
        /// </summary>
        /// <param name="id">Id da localizacao</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_localizacaoRepository.BuscarPorId(id));
        }

        /// <summary>
        /// Excluir uma localizacao 
        /// </summary>
        /// <param name="id">Id da localizacao a ser excluida</param>
        [HttpDelete("Deletar/{id}")]
        public IActionResult ExcluirLocalizacao(int id)
        {
            try
            {
                if (id != 0)
                {
                    _localizacaoRepository.ExcluirLocalizacao(id);
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
        /// Cadastrar um nova localizacao
        /// </summary>
        /// <param name="novoLocalizacao">Dados da localizacao a ser cadastrada</param>
        [HttpPost("Cadastrar")]
        public IActionResult CadastrarLocalizacao(LocalizacaoViewModel novoLocalizacao)
        {

            try
            {

                if (novoLocalizacao == null)
                {
                    return BadRequest("Todos os campos do usuario devem ser preenchidos !");
                }
                else
                {
                    _localizacaoRepository.CadastrarLocalizacao(novoLocalizacao);
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

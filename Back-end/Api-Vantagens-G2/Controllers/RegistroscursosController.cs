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
    public class RegistroscursosController : ControllerBase
    {

        private IRegistrocursoRepository _registrocursoRepository { get; set; }

        public RegistroscursosController(IRegistrocursoRepository repo)
        {
            _registrocursoRepository = repo;
        }


        /// <summary>
        /// Listar todos os registros de desconto
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {

                List<Registrocurso> listaRegistro = _registrocursoRepository.ListarTodos();
                if (listaRegistro.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma regisro cadastrada no sistema!"
                    });
                }
                return Ok(listaRegistro);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Listar um registro cursos pelo seu idSituacaoAtividade
        /// </summary>
        /// <param name="IdSituacaoAtividade">id Situação Atividade</param>
        /// <returns></returns>
        [HttpGet("RegistroCursos/{IdSituacaoAtividade}")]
        public IActionResult ListarRegistroCursoPorIdSituação(int IdSituacaoAtividade)
        {
            try
            {
                return Ok(_registrocursoRepository.ListarRegistroCursoPorIdSituação(IdSituacaoAtividade));
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


        [HttpGet("RegistroCursos/IdUsuario/{IdUsuario}")]
        public IActionResult ListarRegistrocursoPorUsuario(int IdUsuario)
        {
            try
            {
                return Ok(_registrocursoRepository.ListarRegistrocursoPorUsuario(IdUsuario));
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }



        /// <summary>
        /// Buscar um registro de cursos pelo seu id
        /// </summary>
        /// <param name="id">Id do registro a ser encontrado</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_registrocursoRepository.BuscarPorId(id));
        }


        /// <summary>
        /// Deletar um registro 
        /// </summary>
        /// <param name="id">Id do registro a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("Deletar/{id}")]
        public IActionResult ExcluirRegistrocurso(int id)
        {
            try
            {
                if (_registrocursoRepository.BuscarPorId(id)!= null)
                {
                    _registrocursoRepository.ExcluirRegistrocurso(id);
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
        /// Cadastrar um novo registro curso
        /// </summary>
        /// <param name="novoRegistrocurso"></param>
        /// <returns></returns>
        [HttpPost("Cadastrar")]
        public IActionResult CadastrarRegistrodesconto(RegistroCursoCadastrarViewModel novoRegistrocurso)
        {

            try
            {

                if (novoRegistrocurso == null)
                {
                    return BadRequest("Todos os campos do usuario devem ser preenchidos !");
                }
                else
                {
                    _registrocursoRepository.CadastrarRegistrocurso(novoRegistrocurso);
                    return StatusCode(201);
                }
            }
            catch (Exception erro)
            {
                if (novoRegistrocurso != null)
                {
                    return BadRequest("Saldo Insuficiente");
                }
                return BadRequest(erro);
            }

        }


        /// <summary>
        /// Atualizar situação
        /// </summary>
        /// <param name="idRegistroCurso">Id do curso registrado</param>
        [HttpPut("{idRegistroCurso}")]
        public IActionResult AtualizarSituacao(int idRegistroCurso)
        {
            try
            {
                _registrocursoRepository.AtualizarSituacao(Convert.ToInt16(idRegistroCurso));
                return StatusCode(200, new
                {
                    mensagem = "Dados atualizados!"
                });
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


        /// <summary>
        /// Enviar Comunicado sobre o registro d curso atraves do email
        /// </summary>
        /// <param name="email"></param>
        [HttpPost("EnviaEmailDescricao/{email}")]
        public IActionResult EnviaEmailDescricao(string email)
        {
            try
            {
                _registrocursoRepository.EnviaEmailDescricao(email);
                return Ok(new
                {
                    Mensagem = "Email enviado com sucesso"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }


    }
}

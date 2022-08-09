using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using SenaiRH_G2.Utils;
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
    public class EmpresasController : ControllerBase
    {
        private IEmpresaRepository _empresaRepository { get; set; }

        public EmpresasController(IEmpresaRepository repo)
        {
            _empresaRepository = repo;
        }

        /// <summary>
        /// Listar todas as empresas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {

                List<Empresa> listarEmpresa = _empresaRepository.ListarTodos();
                if (listarEmpresa.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma consulta cadastrada no sistema!"
                    });
                }
                return Ok(listarEmpresa);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }


        /// <summary>
        /// Buscar um empresa pelo seu id 
        /// </summary>
        /// <param name="id">id da empresa a ser buscada </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_empresaRepository.BuscarPorId(id));
        }


        /// <summary>
        /// Excluir uma empresa 
        /// </summary>
        /// <param name="id">Id da empresa a ser excluida</param>
        [HttpDelete("Deletar/{id}")]
        public IActionResult ExcluirEmpresa(int id)
        {
            try
            {
                if (id != 0)
                {
                    _empresaRepository.ExcluirEmpresa(id);
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
        /// Cadastrar um nova empresa
        /// </summary>
        /// <param name="novoEmpresa">Dados da empresa a ser cadastrada</param>
        /// <param name="fotoEmpresa">foto da empresa a ser cadastrada</param>
        [HttpPost("Cadastrar")]
        public IActionResult CadastrarCurso([FromForm] EmpresaCadastroViewModel novoEmpresa, IFormFile fotoEmpresa)
        {

            try
            {
                if (fotoEmpresa == null)
                {
                    novoEmpresa.CaminhoImagemEmpresa = "imagem-padrao.png";
                }
                else
                {
                    #region Upload da Imagem com extensões permitidas apenas
                    string[] extensoesPermitidas = { "jpg", "png", "jpeg" };
                    string uploadResultado = Upload.EnviarFoto(fotoEmpresa).ToString();

                    if (uploadResultado == "")
                    {
                        return BadRequest("Arquivo não encontrado !");
                    }
                    if (uploadResultado == "Extensão não permitida")
                    {
                        return BadRequest("Extensão do arquivo não permitida");
                    }

                    novoEmpresa.CaminhoImagemEmpresa = uploadResultado;
                    #endregion
                }



                if (novoEmpresa == null)
                {
                    return BadRequest("Todos os campos do usuario devem ser preenchidos !");
                }
                else
                {
                    _empresaRepository.CadastrarEmpresa(novoEmpresa);
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

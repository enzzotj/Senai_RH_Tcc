using SenaiRH_G2.Domains;
using SenaiRH_G2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IEmpresaRepository
    {

        /// <summary>
        /// Listar todas as empresas 
        /// </summary>
        /// <returns></returns>
        List<Empresa> ListarTodos();
        /// <summary>
        /// Excluir uma empresa atraves de seu id
        /// </summary>
        /// <param name="id">id da empresa a ser excluida </param>
        void ExcluirEmpresa(int id);
        /// <summary>
        /// Buscar uma empresa atraves de seu id
        /// </summary>
        /// <param name="id">id da empresa a ser buscada </param>
        /// <returns></returns>
        Empresa BuscarPorId(int id);
        /// <summary>
        /// Cadastrar uma nova empresa 
        /// </summary>
        /// <param name="novoEmpresa">Dados da empresa a ser cadastrada</param>
        void CadastrarEmpresa(EmpresaCadastroViewModel novoEmpresa);

    }
}

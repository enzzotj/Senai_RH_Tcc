using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface ILogradouroRepository
    {

        /// <summary>
        /// Listar todos os logradouros
        /// </summary>
        /// <returns></returns>
        List<Logradouro> ListarTodos();
        /// <summary>
        /// Excluir um logradouro pelo seu id
        /// </summary>
        /// <param name="id">id do logradouro a ser excluido</param>
        void ExcluirLogradouro(int id);
        /// <summary>
        /// Buscar um logradouro pelo seu id
        /// </summary>
        /// <param name="id">id do logradouro a ser busscado</param>
        /// <returns></returns>
        Logradouro BuscarPorId(int id);
        /// <summary>
        /// Cadastrar um logradouro 
        /// </summary>
        /// <param name="novoLogradouro">Dados do logradouro a ser cadastrado</param>
        void CadastrarLogradouro(Logradouro novoLogradouro);

        List<Logradouro> ListarLogradouro(string logradouro);

    }
}

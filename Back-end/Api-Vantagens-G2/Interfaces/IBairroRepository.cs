using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IBairroRepository
    {
        /// <summary>
        /// Listar todos os bairros 
        /// </summary>
        /// <returns></returns>
        List<Bairro> ListarTodos();
        /// <summary>
        /// Excluir um bairro pelo seu id
        /// </summary>
        /// <param name="id">id do bairro a ser excluido</param>
        void ExcluirBairro(int id);

        /// <summary>
        /// Buscar um bairro pelo seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Bairro BuscarPorId(int id);
        /// <summary>
        /// cadastrar um novo bairro 
        /// </summary>
        /// <param name="novoBairro">dados de cadastro de um bairro</param>
        void CadastrarBairro(Bairro novoBairro);

        /// <summary>
        /// Listar um bairro pelo seu id
        /// </summary>
        /// <param name="bairro">Nome do bairro a ser buscado </param>
        /// <returns></returns>
        List<Bairro> ListarBairro(string bairro);

    }
}

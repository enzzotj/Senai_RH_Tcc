using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface ICidadeRepository
    {
        /// <summary>
        /// Listar todos as cidades 
        /// </summary>
        /// <returns></returns>
        List<Cidade> ListarTodos();
        /// <summary>
        /// Excluir uma cidade pelo seu id
        /// </summary>
        /// <param name="id">id da cidade a ser excluida</param>
        void ExcluirCidade(int id);
        /// <summary>
        /// Buscar uma cidade pelo seu id
        /// </summary>
        /// <param name="id">id da cidade a ser buscada</param>
        /// <returns></returns>
        Cidade BuscarPorId(int id);
        /// <summary>
        /// Cadastrar uma nova cidade 
        /// </summary>
        /// <param name="novoCidade">dados da cidade a ser cadastrada</param>
        void CadastrarCidade(Cidade novoCidade);

        List<Cidade> ListarCidade(string cidade);

    }
}

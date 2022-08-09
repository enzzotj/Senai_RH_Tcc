using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IEstadoRepository
    {
        /// <summary>
        /// Listar todos os estados 
        /// </summary>
        /// <returns></returns>
        List<Estado> ListarTodos();
        /// <summary>
        /// Excluir um estado pelo seu id 
        /// </summary>
        /// <param name="id">id do estado a ser excluido</param>
        void ExcluirEstado(int id);
        /// <summary>
        /// Buscar um estado pelo seu id 
        /// </summary>
        /// <param name="id">id do estado a ser buscado</param>
        /// <returns></returns>
        Estado BuscarPorId(int id);
        /// <summary>
        /// Cadastrar um novo estado
        /// </summary>
        /// <param name="novoEstado">Dados do estado a ser cadastrado</param>
        void CadastrarEstado(Estado novoEstado);
        /// <summary>
        /// Listar um estado pelo seu Nome
        /// </summary>
        /// <param name="estado">nome do estado as ser buscado</param>
        /// <returns></returns>
        List<Estado> ListarEstado(string estado);

    }
}

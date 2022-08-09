using SenaiRH_G2.Domains;
using SenaiRH_G2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IRegistrodescontoRepository
    {
        /// <summary>
        /// Listar Todos os registros de desconto
        /// </summary>
        /// <returns></returns>
        List<Registrodesconto> ListarTodos();
        /// <summary>
        /// Excluir um registro de um desconto pelo seu id
        /// </summary>
        /// <param name="id">id do registro a ser excluido</param>
        void ExcluirRegistrodesconto(int id);
        /// <summary>
        /// Buscar um registro de desconto pelo seu id
        /// </summary>
        /// <param name="id">id do registro a ser buscado</param>
        /// <returns></returns>
        Registrodesconto BuscarPorId(int id);
        /// <summary>
        /// Cadastrar um registro desconto 
        /// </summary>
        /// <param name="novoRegistrodesconto">Dados do registro a ser cadastrado</param>
        void CadastrarRegistrodesconto(RegistroDescontoCadastrarViewModel novoRegistrodesconto);

        List<Registrodesconto> ListarRegistrodescontoPorUsuario(int id);

    }
}

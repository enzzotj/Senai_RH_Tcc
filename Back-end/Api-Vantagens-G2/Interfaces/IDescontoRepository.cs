using SenaiRH_G2.Domains;
using SenaiRH_G2.ViewModels;
using System.Collections.Generic;

namespace SenaiRH_G2.Interfaces
{
    public interface IDescontoRepository
    {
        /// <summary>
        /// LIstar todos s descontos 
        /// </summary>
        /// <returns></returns>
        List<Desconto> ListarTodos();
        /// <summary>
        /// Excluir um desconto pelo seu id
        /// </summary>
        /// <param name="idDesconto">id do desconto a ser excluido</param>
        void ExcluirDesconto(int idDesconto);
        /// <summary>
        /// Buscar um desconto pelo seu id 
        /// </summary>
        /// <param name="id">id do desconto a ser buscado</param>
        /// <returns></returns>
        Desconto BuscarPorId(int id);
        /// <summary>
        /// Cadastrar um novo Desconto 
        /// </summary>
        /// <param name="novoDesconto">Dados do desconto a ser cadastrado</param>
        void CadastrarDesconto(DescontoCadastroViewModel novoDesconto);
        
    }
}

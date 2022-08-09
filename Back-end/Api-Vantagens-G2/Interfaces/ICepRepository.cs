using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface ICepRepository
    {
        /// <summary>
        /// Listar todos os ceps
        /// </summary>
        /// <returns></returns>
        List<Cep> ListarTodos();
        /// <summary>
        /// Excluir um cep pelo seu id
        /// </summary>
        /// <param name="id">id do cep a ser excluido</param>
        void ExcluirCep(int id);
        /// <summary>
        /// Buscar um cep pelo seu id 
        /// </summary>
        /// <param name="id">id do cep a ser buscado</param>
        /// <returns></returns>
        Cep BuscarPorId(int id);
        /// <summary>
        /// Cadastrar um novo cep
        /// </summary>
        /// <param name="novoCep">Dados do cep a ser cadastrado</param>
        void CadastrarCep(Cep novoCep);

        /// <summary>
        /// Listar um cep pelo seu numero
        /// </summary>
        /// <param name="cep">numero do cep a ser buscado</param>
        /// <returns></returns>
        List<Cep> ListarCep(string cep);

    }
}

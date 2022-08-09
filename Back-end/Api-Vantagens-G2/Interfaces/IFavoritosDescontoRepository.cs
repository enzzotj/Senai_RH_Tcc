using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IFavoritosDescontoRepository
    {

        /// <summary>
        /// Listar todos os favoritos Desconto 
        /// </summary>
        /// <returns></returns>
        List<Descontofavorito> ListarTodos();
        /// <summary>
        /// Cadastrar um novo favorito desconto
        /// </summary>
        /// <param name="Novofavorito">Dados do favorito a ser cadastrado</param>

        void AdcionarFavoritos(Descontofavorito Novofavorito);
        /// <summary>
        /// Excluir um favorito pelo seu id 
        /// </summary>
        /// <param name="Id">id do favorito a ser excluido </param>
        void ExcluirFavoritos(int Id);
        /// <summary>
        /// Buscar um favorito desconto pelo seu id
        /// </summary>
        /// <param name="Id">id do favorito a ser buscado</param>
        /// <returns></returns>
        Descontofavorito BuscarDescontoFavoritoPorId(int Id);
        List<Descontofavorito> ListarPorIdFavoritoDesconto(int Id);
    }
}

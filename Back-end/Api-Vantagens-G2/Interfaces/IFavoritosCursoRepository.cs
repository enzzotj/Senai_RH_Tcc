using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IFavoritosCursoRepository
    {
        /// <summary>
        /// Lstar todos os Favoritos curso
        /// </summary>
        /// <returns></returns>
        List<Cursofavorito> ListarTodos();
        /// <summary>
        /// Cadastrar um favoritos curso
        /// </summary>
        /// <param name="Novofavorito">Dados do do curso a ser favoritado</param>

        void AdcionarFavoritos(Cursofavorito Novofavorito);
        /// <summary>
        /// Excluir um favorito curso atraves de seu id
        /// </summary>
        /// <param name="Id">id do favorito a ser excluido</param>
        void ExcluirFavoritos(int Id);
        /// <summary>
        /// Buscar um favorito curso atraves de seu id
        /// </summary>
        /// <param name="Id">id do favorito a ser buscado</param>
        /// <returns></returns>
        Cursofavorito BuscarCursoFavoritoPorId(int Id);
        List<Cursofavorito> ListarPorIdFavoritoCurso(int Id);


    }
}

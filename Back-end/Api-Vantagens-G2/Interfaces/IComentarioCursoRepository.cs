using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IComentarioCursoRepository
    {
        /// <summary>
        /// Listar todos os comentarios de um curso
        /// </summary>
        /// <returns></returns>
        List<Comentariocurso> ListarComenatarioCurso();
        /// <summary>
        /// Buscar um comentario de um curso atraves de seu id
        /// </summary>
        /// <param name="Id">id do cometario a ser buscado </param>
        /// <returns></returns>
        Comentariocurso ListarComentarioPorId(int Id);
        /// <summary>
        /// Buscar um comentario de um curso atravaes de um id do curso
        /// </summary>
        /// <param name="Id">id do curso a ser buscado</param>
        /// <returns></returns>
        List<Comentariocurso> ListarComentarioPorIdCurso(int Id);
        /// <summary>
        /// Excluir um comentario de um curso atraves do id
        /// </summary>
        /// <param name="Id">id do comentario a ser excluido</param>
        void ExcluirComentarioCurso(int Id);
        /// <summary>
        /// Cadastrar um comentario de um curso
        /// </summary>
        /// <param name="NovoComentario">Dados de um comentario a ser cadastrado</param>
        void CadastrarComentarioCurso(Comentariocurso NovoComentario);

        /// <summary>
        /// Listar todos os comentario de um usuario apenas
        /// </summary>
        /// <param name="id">id do usuario a ser buscada</param>
        /// <returns></returns>
        List<Comentariocurso> ListarComenatarioCursoPorUsuario(int id);


    }
}

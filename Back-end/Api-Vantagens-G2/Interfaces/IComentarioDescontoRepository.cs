using SenaiRH_G2.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IComentarioDescontoRepository
    {
        /// <summary>
        /// Listar todos os comentario de desconto
        /// </summary>
        /// <returns></returns>
        List<Comentariodesconto> ListarComenatarioDesconto();
        /// <summary>
        /// Cadastrar um novo comentario do desconto
        /// </summary>
        /// <param name="NovoComentario">dados do cometario a ser cadastrado</param>
        void CadastrarComentarioDesconto(Comentariodesconto NovoComentario);
        /// <summary>
        /// Buscar um comentario desconto pelo seu idDesconto
        /// </summary>
        /// <param name="Id">id do desconto a ser buscado</param>
        /// <returns></returns>
        List<Comentariodesconto> ListarComentarioPorIdDesconto(int Id);

        /// <summary>
        /// Buscar um comentario desconto pelo seu id
        /// </summary>
        /// <param name="Id">id do comentario a ser buscado</param>
        /// <returns></returns>
        Comentariodesconto ListarComentarioPorId(int Id);
        /// <summary>
        /// Excluir um comentario desconto
        /// </summary>
        /// <param name="Id">id do comentario a ser excluido</param>
        void ExcluirComentarioDesconto(int Id);
        /// <summary>
        /// Buscar os comentarios descnto pelo id usuario
        /// </summary>
        /// <param name="id">Id do usuario a ser buscado</param>
        /// <returns></returns>
        List<Comentariodesconto> ListarComenatarioDescontoPorUsuario(int id);
    }
}

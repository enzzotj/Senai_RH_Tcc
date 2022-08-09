using SenaiRH_G2.Domains;
using SenaiRH_G2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface IRegistrocursoRepository
    {
        /// <summary>
        /// Listar todos os registros de curso 
        /// </summary>
        /// <returns></returns>
        List<Registrocurso> ListarTodos();
        /// <summary>
        /// Excluir um registro de um curso pelo seu id
        /// </summary>
        /// <param name="id">id do registro a ser cadastrado</param>
        void ExcluirRegistrocurso(int id);
        /// <summary>
        /// Buscar um registro de um curso pelo seu id
        /// </summary>
        /// <param name="id">id do registro a ser buscado</param>
        /// <returns></returns>
        Registrocurso BuscarPorId(int id);
        /// <summary>
        /// Cadastrar um registro de curso
        /// </summary>
        /// <param name="novoRegistrocurso">Dados do registro a ser cadastrado</param>
        void CadastrarRegistrocurso(RegistroCursoCadastrarViewModel novoRegistrocurso);
        /// <summary>
        /// Listar registro atraves de seu idSituação
        /// </summary>
        /// <param name="Id">id Siuacao a ser buscado</param>
        /// <returns></returns>
        List<Registrocurso> ListarRegistroCursoPorIdSituação(int Id);

        List<Registrocurso> ListarRegistrocursoPorUsuario(int id);
        /// <summary>
        /// Atualizar a situação do registro 
        /// </summary>
        /// <param name="idRegistroCurso">id do registro do curso a ser alterado</param>
        void AtualizarSituacao(int idRegistroCurso);
        /// <summary>
        /// Enviar umemail de confirmação de cadastro em um curso
        /// </summary>
        /// <param name="email">Email do usuario a ser direcionado o email</param>
        void EnviaEmailDescricao(string email);

    }
}

using SenaiRH_G2.Domains;
using SenaiRH_G2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface ICursoRepository
    {
        /// <summary>
        /// Listar todos os cursos
        /// </summary>
        /// <returns></returns>
        List<Curso> ListarTodos();
        /// <summary>
        /// Excluir um curso pelo seu id 
        /// </summary>
        /// <param name="id">id do curso a sex excludo</param>
        void ExcluirCurso(int id);
        /// <summary>
        /// Buscar um curso pelo seu id
        /// </summary>
        /// <param name="id">id do curso a ser buscado</param>
        /// <returns></returns>
        Curso BuscarPorId(int id);

        /// <summary>
        /// Cadastrar um curso 
        /// </summary>
        /// <param name="novoCurso">dados dos cursos a ser cadastrado</param>
        void CadastrarCurso(CursoCadastroViewModel novoCurso);
    }
}

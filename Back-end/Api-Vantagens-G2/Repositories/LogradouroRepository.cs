using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Repositories
{
    public class LogradouroRepository : ILogradouroRepository
    {

        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Buscar um logradouro pelo seu id
        /// </summary>
        /// <param name="id">Id do logradouro a ser buscado</param>
        /// <returns></returns>
        public Logradouro BuscarPorId(int id)
        {
            return ctx.Logradouros.FirstOrDefault(c => c.IdLogradouro == id);
        }

        /// <summary>
        /// Cadastrar um logradouro
        /// </summary>
        /// <param name="novoLogradouro">Dados do logradouro a ser cadatrado</param>
        public void CadastrarLogradouro(Logradouro novoLogradouro)
        {
            Logradouro logradouro = new Logradouro()
            {
                NomeLogradouro = novoLogradouro.NomeLogradouro
            };

            ctx.Logradouros.Add(logradouro);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Excluir um logradouro 
        /// </summary>
        /// <param name="id">Id do logradouro a ser excluido</param>
        public void ExcluirLogradouro(int id)
        {
            Logradouro buscarPorId = ctx.Logradouros.FirstOrDefault(c => c.IdLogradouro == id);
            ctx.Logradouros.Remove(buscarPorId);
            ctx.SaveChanges();
        }

        public List<Logradouro> ListarLogradouro(string logradouro)
        {
            List<Logradouro> listarLogradouro = new();
            foreach (var buscarlogradouro in ctx.Logradouros.Select(p => new Logradouro()
            {
                IdLogradouro = p.IdLogradouro,
                NomeLogradouro = p.NomeLogradouro
            }).ToList())
            {
                if (buscarlogradouro.NomeLogradouro == logradouro)
                {
                    listarLogradouro.Add(buscarlogradouro);
                }
            }
            return listarLogradouro;
        }


        /// <summary>
        /// Listar todos os logradouros
        /// </summary>
        /// <returns></returns>
        public List<Logradouro> ListarTodos()
        {
            return ctx.Logradouros.Select(p => new Logradouro
            {

                IdLogradouro = p.IdLogradouro,
                NomeLogradouro = p.NomeLogradouro

            }).ToList();
        }
    }
}

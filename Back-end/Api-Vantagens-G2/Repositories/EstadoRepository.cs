using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {

        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Buscar ym estado pelo seu id
        /// </summary>
        /// <param name="id">Id do estado a e buscado</param>
        /// <returns></returns>
        public Estado BuscarPorId(int id)
        {
            return ctx.Estados.FirstOrDefault(c => c.IdEstado == id);
        }

        /// <summary>
        /// Cadasrar u novo estado
        /// </summary>
        /// <param name="novoEstado">Dados do id a ser buscado</param>
        public void CadastrarEstado(Estado novoEstado)
        {
            Estado estado = new Estado()
            {
                NomeEstado = novoEstado.NomeEstado
            };

            ctx.Estados.Add(estado);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Excluir um estado
        /// </summary>
        /// <param name="id">Id do estado a ser excludo</param>
        public void ExcluirEstado(int id)
        {
            Estado buscarPorId = ctx.Estados.FirstOrDefault(c => c.IdEstado == id);
            ctx.Estados.Remove(buscarPorId);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Buscar um estado pelo sesu nome
        /// </summary>
        /// <param name="estado">Nome do estado a ser buscado</param>
        /// <returns></returns>
        public List<Estado> ListarEstado(string estado)
        {
            List<Estado> listarEstado = new();
            foreach (var buscarestado in ctx.Estados.Select(p => new Estado()
            {
                IdEstado = p.IdEstado,
                NomeEstado = p.NomeEstado
            }).ToList())
            {
                if (buscarestado.NomeEstado == estado)
                {
                    listarEstado.Add(buscarestado);
                }
            }
            return listarEstado;
        }

        /// <summary>
        /// Listar todos estados
        /// </summary>
        /// <returns></returns>
        public List<Estado> ListarTodos()
        {
            return ctx.Estados.Select(p => new Estado
            {

                IdEstado = p.IdEstado,
                NomeEstado = p.NomeEstado

            }).ToList();
        }
    }
}

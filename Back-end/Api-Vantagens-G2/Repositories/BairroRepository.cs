using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        /// <summary>
        /// Instancando um contexto
        /// </summary>
        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Buscar um bairro pelo seu id 
        /// </summary>
        /// <param name="id">Id do bairro a ser buscado</param>
        /// <returns></returns>
        public Bairro BuscarPorId(int id)
        {
            //Buscando um bairro pelo id passado
            return ctx.Bairros.FirstOrDefault(c => c.IdBairro == id);
        }

        /// <summary>
        /// Cadastrar um novo bairro 
        /// </summary>
        /// <param name="novoBairro">Dados do bairro a ser cadastrado</param>
        public void CadastrarBairro(Bairro novoBairro)
        {
            Bairro bairro = new Bairro()
            {
                //passando os atributos para cadastrar 
                NomeBairro = novoBairro.NomeBairro
            };
            //Cadastrando um bairro
            ctx.Bairros.Add(bairro);
            //Salvando o cadastro
            ctx.SaveChanges();
        }

        /// <summary>
        /// Excluir um bairro 
        /// </summary>
        /// <param name="id">Id do bairro a ser excluido</param>
        public void ExcluirBairro(int id)
        {
            //Buscado um bairro pelo seu id 
            Bairro buscarPorId = ctx.Bairros.FirstOrDefault(c => c.IdBairro == id);
            //Excluido
            ctx.Bairros.Remove(buscarPorId);
            //Salvando a alteração
            ctx.SaveChanges();
        }

        /// <summary>
        /// Listar um bairro pelo seu id
        /// </summary>
        /// <param name="bairro">Nome do bairro a ser buscado </param>
        /// <returns></returns>
        public List<Bairro> ListarBairro(string bairro)
        {
            List<Bairro> listarBairro = new();
            foreach (var buscarbairro in ctx.Bairros.Select(p => new Bairro()
            {
                IdBairro = p.IdBairro,
                NomeBairro = p.NomeBairro
            }).ToList())
            {
                if (buscarbairro.NomeBairro == bairro)
                {
                    listarBairro.Add(buscarbairro);
                }
            }
            return listarBairro;
        }

        /// <summary>
        /// Listar todos os bairros
        /// </summary>
        /// <returns></returns>
        public List<Bairro> ListarTodos()
        {
            //Listar todos os bairros , com os atributos idBairro e nomeBairro
            return ctx.Bairros.Select(p => new Bairro
            {
                IdBairro = p.IdBairro,
                NomeBairro = p.NomeBairro

            }).ToList();
        }
    }
}

using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        /// <summary>
        /// Instanciando um contexto
        /// </summary>
        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Buscar uma cidade pelo seu id 
        /// </summary>
        /// <param name="id">id do bairro a ser buscado</param>
        /// <returns></returns>
        public Cidade BuscarPorId(int id)
        {
            //Buscando uma cidade pelo id passado
            return ctx.Cidades.FirstOrDefault(c => c.IdCidade == id);
        }

        /// <summary>
        /// Cadastrar uma nova cidade
        /// </summary>
        /// <param name="novoCidade">Dados da nova cidade a ser cadastrada</param>
        public void CadastrarCidade(Cidade novoCidade)
        {
            Cidade cidade = new Cidade()
            {
                //passando os atributos para cadastrar 
                NomeCidade = novoCidade.NomeCidade
            };

            //Cadasntrando uma cidade
            ctx.Cidades.Add(cidade);
            //Salvando o cadastro 
            ctx.SaveChanges();
        }

        /// <summary>
        /// Excluir uma cidade
        /// </summary>
        /// <param name="id">Id da cidade a ser excluida</param>
        public void ExcluirCidade(int id)
        {
            //Buscando uma cidade pelo id passado
            Cidade buscarPorId = ctx.Cidades.FirstOrDefault(c => c.IdCidade == id);
            //Excluir a cidade buacada
            ctx.Cidades.Remove(buscarPorId);
            //Salvando a exclusao
            ctx.SaveChanges();
        }

        public List<Cidade> ListarCidade(string cidade)
        {
            List<Cidade> listarCidade = new();
            foreach (var buscarcidade in ctx.Cidades.Select(p => new Cidade()
            {
                IdCidade = p.IdCidade,
                NomeCidade = p.NomeCidade
            }).ToList())
            {
                if (buscarcidade.NomeCidade == cidade)
                {
                    listarCidade.Add(buscarcidade);
                }
            }
            return listarCidade;
        }

        /// <summary>
        /// Listar todas as cidades
        /// </summary>
        /// <returns></returns>
        public List<Cidade> ListarTodos()
        {
            //Listar todas as cidades, com os atributos, idCidade, nomeCidade
            return ctx.Cidades.Select(p => new Cidade
            {
                IdCidade = p.IdCidade,
                NomeCidade = p.NomeCidade

            }).ToList();
        }
    }
}

using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Repositories
{
    public class CepRepository : ICepRepository
    {
        /// <summary>
        /// Instacando um contexto
        /// </summary>
        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Buscar um cep pelo seu id 
        /// </summary>
        /// <param name="id">Id do cep a ser buscado</param>
        /// <returns></returns>
        public Cep BuscarPorId(int id)
        {
            //Buscando um cep pelo id passado
            return ctx.Ceps.FirstOrDefault(c => c.IdCep == id);
        }

        /// <summary>
        /// Cadastrar um novo cep
        /// </summary>
        /// <param name="novoCep">Dados do cep a ser cadastrado</param>
        public void CadastrarCep(Cep novoCep)
        {
            Cep cep = new Cep()
            {
                //Passando os atributos para cadastrar
                Cep1 = novoCep.Cep1
            };

            //Cadastrando um cep
            ctx.Ceps.Add(cep);
            //Salvando o cadastro
            ctx.SaveChanges();
        }

        /// <summary>
        /// Escluir um cep
        /// </summary>
        /// <param name="id">Id do cep a ser excluido</param>
        public void ExcluirCep(int id)
        {
            //Buscando o cep pelo id passado 
            Cep buscarPorId = ctx.Ceps.FirstOrDefault(c => c.IdCep == id);
            //Excluindo o cep buscado
            ctx.Ceps.Remove(buscarPorId);
            //Salvando as alterações
            ctx.SaveChanges();
        }

        /// <summary>
        /// Listar um cep pelo seu numero
        /// </summary>
        /// <param name="cep">numero do cep a ser buscado</param>
        /// <returns></returns>
        public List<Cep> ListarCep(string cep)
        {
            List<Cep> listarCep = new();
            foreach (var buscarcep in ctx.Ceps.Select(p => new Cep()
            {
               IdCep = p.IdCep,
               Cep1 = p.Cep1
            }).ToList())
            {
                if (buscarcep.Cep1 == cep)
                {
                    listarCep.Add(buscarcep);
                }
            }
            return listarCep;
        }

        /// <summary>
        /// Listar todos os cep
        /// </summary>
        /// <returns></returns>
        public List<Cep> ListarTodos()
        {
            //Listado todos os cep, com os atributos, IdCep e Cep1(numero do cep)
            return ctx.Ceps.Select(p => new Cep
            {
                //Atributos passados
                IdCep = p.IdCep,
                Cep1 = p.Cep1

            }).ToList();
        }
    }
}

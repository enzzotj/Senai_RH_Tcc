using Microsoft.EntityFrameworkCore;
using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using SenaiRH_G2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SenaiRH_G2.Repositories
{
    public class RegistrodescontoRepository : IRegistrodescontoRepository
    {

        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Buscar um Registro de um descontro atraves de seu id
        /// </summary>
        /// <param name="id">Id do registro desconto a ser buscado</param>
        /// <returns></returns>
        public Registrodesconto BuscarPorId(int id)
        {
            return ctx.Registrodescontos.FirstOrDefault(c => c.IdRegistroDesconto == id);
        }

        /// <summary>
        /// Cadastrar um registro de desconto
        /// </summary>
        /// <param name="novoRegistrodesconto">Dados do registro desconto a ser cadastrado</param>
        public void CadastrarRegistrodesconto(RegistroDescontoCadastrarViewModel novoRegistrodesconto)
        {
            Usuario usuario = new Usuario();
            Desconto desconto = new Desconto();
            Registrodesconto registrodesconto = new Registrodesconto();
            registrodesconto.IdUsuario = novoRegistrodesconto.IdUsuario;
            registrodesconto.IdDesconto = novoRegistrodesconto.IdDesconto;

            usuario.IdUsuario = registrodesconto.IdUsuario;
            desconto.IdDesconto = registrodesconto.IdDesconto;

            Usuario buscarUsuario = ctx.Usuarios.FirstOrDefault(c => c.IdUsuario == usuario.IdUsuario);
            Desconto buscarDesconto = ctx.Descontos.FirstOrDefault(c => c.IdDesconto == desconto.IdDesconto);


            if (buscarUsuario.SaldoMoeda >= buscarDesconto.ValorDesconto)
            {

                buscarUsuario.SaldoMoeda -= buscarDesconto.ValorDesconto;


                ctx.Usuarios.Update(buscarUsuario);
                ctx.Registrodescontos.Add(registrodesconto);
                ctx.SaveChanges();
            }

        }


        /// <summary>
        /// Excluir um registro de um desconto
        /// </summary>
        /// <param name="id">Id do registro desconto a ser excluido</param>
        public void ExcluirRegistrodesconto(int id)
        {
            Registrodesconto buscarPorId = ctx.Registrodescontos.FirstOrDefault(c => c.IdRegistroDesconto == id);
            ctx.Registrodescontos.Remove(buscarPorId);
            ctx.SaveChanges();
        }

        public List<Registrodesconto> ListarRegistrodescontoPorUsuario(int id)
        {
            List<Registrodesconto> registrodesconto = new();
            foreach (var registro in ctx.Registrodescontos.Select(p => new Registrodesconto
            {
                IdRegistroDesconto = p.IdRegistroDesconto,
                IdDesconto = p.IdDesconto,
                IdUsuario = p.IdUsuario,
                IdDescontoNavigation = new Desconto
                {
                    IdDesconto = p.IdDesconto,
                    IdEmpresa = p.IdDescontoNavigation.IdEmpresa,
                    NomeDesconto = p.IdDescontoNavigation.NomeDesconto,
                    DescricaoDesconto = p.IdDescontoNavigation.DescricaoDesconto,
                    CaminhoImagemDesconto = p.IdDescontoNavigation.CaminhoImagemDesconto,
                    ValidadeDesconto = p.IdDescontoNavigation.ValidadeDesconto,
                    ValorDesconto = p.IdDescontoNavigation.ValorDesconto,
                    NumeroCupom = p.IdDescontoNavigation.NumeroCupom,
                    MediaAvaliacaoDesconto = p.IdDescontoNavigation.MediaAvaliacaoDesconto
                },
                IdUsuarioNavigation = new Usuario
                {
                    IdUsuario = p.IdUsuarioNavigation.IdUsuario,
                    Nome = p.IdUsuarioNavigation.Nome,
                    Cpf = p.IdUsuarioNavigation.Cpf,
                    Email = p.IdUsuarioNavigation.Email,
                    IdCargoNavigation = new Cargo
                    {
                        IdCargo = p.IdUsuarioNavigation.IdCargoNavigation.IdCargo,
                        NomeCargo = p.IdUsuarioNavigation.IdCargoNavigation.NomeCargo
                    },
                }
            }).ToList())
            {
                if (registro.IdUsuario == id)
                {
                    registrodesconto.Add(registro);
                }
            }
            return registrodesconto;
        }


        /// <summary>
        /// Listar todos os registros de desconto
        /// </summary>
        /// <returns></returns>
        public List<Registrodesconto> ListarTodos()
        {
            return ctx.Registrodescontos.Select(p => new Registrodesconto
            {

                IdRegistroDesconto = p.IdRegistroDesconto,
                IdDesconto = p.IdDesconto,
                IdUsuario = p.IdUsuario,
                IdUsuarioNavigation = new Usuario()
                {
                    IdUsuario = p.IdUsuarioNavigation.IdUsuario,
                    Nome = p.IdUsuarioNavigation.Nome,
                    SaldoMoeda = p.IdUsuarioNavigation.SaldoMoeda
                },
                IdDescontoNavigation = new Desconto()
                {
                    IdDesconto = p.IdDescontoNavigation.IdDesconto,
                    NomeDesconto = p.IdDescontoNavigation.NomeDesconto,
                    ValorDesconto = p.IdDescontoNavigation.ValorDesconto

                }

            }).ToList();
        }

        }

    
}

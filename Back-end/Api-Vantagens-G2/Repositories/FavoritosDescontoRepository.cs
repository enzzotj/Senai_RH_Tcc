using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Repositories
{
    public class FavoritosDescontoRepository : IFavoritosDescontoRepository
    {

        senaiRhContext ctx = new senaiRhContext();


        /// <summary>
        /// Adcionar um desconto dos favoritos
        /// </summary>
        /// <param name="Novofavorito"></param>
        public void AdcionarFavoritos(Descontofavorito Novofavorito)
        {
            Descontofavorito desconto = new Descontofavorito()
            {
                IdDesconto = Novofavorito.IdDesconto,
                IdUsuario = Novofavorito.IdUsuario
            };
            ctx.Descontofavoritos.Add(desconto);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Buscar um favorito desconto pelo seu id
        /// </summary>
        /// <param name="Id">id do desconto favorito</param>
        /// <returns></returns>
        public Descontofavorito BuscarDescontoFavoritoPorId(int Id)
        {
            return ctx.Descontofavoritos.FirstOrDefault(c => c.IdDescontoFavorito == Id);
        }

        /// <summary>
        /// Excluir um desconto dos favoritos 
        /// </summary>
        /// <param name="Id">Id do desconto favorito</param>
        public void ExcluirFavoritos(int Id)
        {
            ctx.Descontofavoritos.Remove(BuscarDescontoFavoritoPorId(Id));
            ctx.SaveChanges();
        }

        public List<Descontofavorito> ListarPorIdFavoritoDesconto(int Id)
        {
            List<Descontofavorito> descontofavoritos = new();

            foreach (var favorito in ctx.Descontofavoritos.Select(p => new Descontofavorito()
            {
                IdDescontoFavorito = p.IdDescontoFavorito,
                IdDesconto = p.IdDesconto,
                IdUsuario = p.IdUsuario,
                IdUsuarioNavigation = new Usuario
                {
                    Nome = p.IdUsuarioNavigation.Nome,
                    Email = p.IdUsuarioNavigation.Email,
                    Cpf = p.IdUsuarioNavigation.Cpf,
                    SaldoMoeda = p.IdUsuarioNavigation.SaldoMoeda
                },
                IdDescontoNavigation = new Desconto
                {
                    IdDesconto = p.IdDescontoNavigation.IdDesconto,
                    IdEmpresa = p.IdDescontoNavigation.IdEmpresa,
                    NomeDesconto = p.IdDescontoNavigation.NomeDesconto,
                    DescricaoDesconto = p.IdDescontoNavigation.DescricaoDesconto,
                    CaminhoImagemDesconto = p.IdDescontoNavigation.CaminhoImagemDesconto,
                    ValidadeDesconto = p.IdDescontoNavigation.ValidadeDesconto,
                    ValorDesconto = p.IdDescontoNavigation.ValorDesconto,
                    NumeroCupom = p.IdDescontoNavigation.NumeroCupom,
                    MediaAvaliacaoDesconto = p.IdDescontoNavigation.MediaAvaliacaoDesconto,
                    IdEmpresaNavigation = new Empresa()
                    {

                        NomeEmpresa = p.IdDescontoNavigation.IdEmpresaNavigation.NomeEmpresa,
                        EmailEmpresa = p.IdDescontoNavigation.IdEmpresaNavigation.EmailEmpresa,
                        TelefoneEmpresa = p.IdDescontoNavigation.IdEmpresaNavigation.TelefoneEmpresa,
                        IdLocalizacaoNavigation = new Localizacao()
                        {
                            Numero = p.IdDescontoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.Numero,
                            IdCep = p.IdDescontoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCep,
                            IdCepNavigation = new Cep()
                            {
                                IdCep = p.IdDescontoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCepNavigation.IdCep,
                                Cep1 = p.IdDescontoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCepNavigation.Cep1
                            },
                            IdLogradouroNavigation = new Logradouro()
                            {
                                NomeLogradouro = p.IdDescontoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdLogradouroNavigation.NomeLogradouro
                            },
                            IdEstadoNavigation = new Estado()
                            {
                                NomeEstado = p.IdDescontoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdEstadoNavigation.NomeEstado
                            }

                        }


                    }
                }
            }).ToList())
            {
                if (favorito.IdUsuario == Id)
                {
                    descontofavoritos.Add(favorito);
                }
            }

            return descontofavoritos;
        }

        /// <summary>
        /// Listar os descontos favoritos de um determinado usuario
        /// </summary>
        /// <returns></returns>
        public List<Descontofavorito> ListarTodos()
        {
            return ctx.Descontofavoritos.Select(f => new Descontofavorito
            {
                IdDescontoFavorito = f.IdDescontoFavorito,
                IdDesconto = f.IdDesconto,
                IdUsuario = f.IdUsuario,
                IdDescontoNavigation = new Desconto
                {

                    IdEmpresa = f.IdDescontoNavigation.IdEmpresa,
                    NomeDesconto = f.IdDescontoNavigation.NomeDesconto,
                    DescricaoDesconto = f.IdDescontoNavigation.DescricaoDesconto,
                    CaminhoImagemDesconto = f.IdDescontoNavigation.CaminhoImagemDesconto,
                    ValidadeDesconto = f.IdDescontoNavigation.ValidadeDesconto,
                    ValorDesconto = f.IdDescontoNavigation.ValorDesconto,
                    NumeroCupom = f.IdDescontoNavigation.NumeroCupom,
                    MediaAvaliacaoDesconto = f.IdDescontoNavigation.MediaAvaliacaoDesconto,
                },
                IdUsuarioNavigation = new Usuario
                {
                    Nome = f.IdUsuarioNavigation.Nome,
                    Email = f.IdUsuarioNavigation.Email,
                    Cpf = f.IdUsuarioNavigation.Cpf,
                    SaldoMoeda = f.IdUsuarioNavigation.SaldoMoeda
                }
            }).ToList();
        }
    }
}


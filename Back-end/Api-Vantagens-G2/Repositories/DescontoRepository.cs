using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using SenaiRH_G2.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SenaiRH_G2.Repositories
{
    public class DescontoRepository : IDescontoRepository
    {

        /// <summary>
        /// Instanciando um contexto
        /// </summary>
        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Bsucar um desconto pelo seu id
        /// </summary>
        /// <param name="id">id do desconto a ser buscado</param>
        /// <returns></returns>
        public Desconto BuscarPorId(int id)
        {
            //Buscando um desconto pelo id passado
            return ctx.Descontos
                .Select(p => new Desconto
                {
                    IdDesconto = p.IdDesconto,
                    IdEmpresa = p.IdEmpresa,
                    NomeDesconto = p.NomeDesconto,
                    DescricaoDesconto = p.DescricaoDesconto,
                    CaminhoImagemDesconto = p.CaminhoImagemDesconto,
                    ValidadeDesconto = p.ValidadeDesconto,
                    ValorDesconto = p.ValorDesconto,
                    NumeroCupom = p.NumeroCupom,
                    MediaAvaliacaoDesconto = p.MediaAvaliacaoDesconto,
                    IdEmpresaNavigation = new Empresa()
                    {

                        NomeEmpresa = p.IdEmpresaNavigation.NomeEmpresa,
                        EmailEmpresa = p.IdEmpresaNavigation.EmailEmpresa,
                        TelefoneEmpresa = p.IdEmpresaNavigation.TelefoneEmpresa,
                        IdLocalizacaoNavigation = new Localizacao()
                        {
                            Numero = p.IdEmpresaNavigation.IdLocalizacaoNavigation.Numero,
                            IdCep = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCep,
                            IdCepNavigation = new Cep()
                            {
                                IdCep = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCepNavigation.IdCep,
                                Cep1 = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCepNavigation.Cep1
                            },
                            IdLogradouroNavigation = new Logradouro()
                            {
                                NomeLogradouro = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdLogradouroNavigation.NomeLogradouro
                            },
                            IdEstadoNavigation = new Estado()
                            {
                                NomeEstado = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdEstadoNavigation.NomeEstado
                            }
                        }
                    }
                })    
                .FirstOrDefault(c => c.IdDesconto == id);
        }


        /// <summary>
        /// Cadastrar um novo desconto
        /// </summary>
        /// <param name="novoDesconto">dados do desconto a ser cadastrado</param>
        public void CadastrarDesconto(DescontoCadastroViewModel novoDesconto)
        {
            //Instanciando um desconto
            Desconto desconto = new Desconto()
            {
                //Passando os atributos para cadastro
                IdEmpresa = novoDesconto.IdEmpresa,
                NomeDesconto = novoDesconto.NomeDesconto,
                DescricaoDesconto = novoDesconto.DescricaoDesconto,
                CaminhoImagemDesconto = novoDesconto.CaminhoImagemDesconto,
                ValidadeDesconto = novoDesconto.ValidadeDesconto,
                ValorDesconto = novoDesconto.ValorDesconto,
                NumeroCupom = novoDesconto.NumeroCupom,
                MediaAvaliacaoDesconto = novoDesconto.MediaAvaliacaoDesconto = 0
            };
            //Cadastrando um desconto 
            ctx.Descontos.Add(desconto);
            //Salvando o cadastro
            ctx.SaveChanges();
        }

        /// <summary>
        /// Excluir um desconto 
        /// </summary>
        /// <param name="idDesconto">Id do desconto a ser excluido</param>


        public void ExcluirDesconto(int idDesconto)
        {
            //Buscando um desconto atraves do idDesconto passado
            Desconto desconto = ctx.Descontos.FirstOrDefault(c => c.IdDesconto == idDesconto);

            //Excluindo os comentarios relacionado a esse desconto
            foreach (var comentario in ctx.Comentariodescontos)
            {
                if(comentario.IdDesconto == desconto.IdDesconto)
                {
                    ctx.Comentariodescontos.Remove(comentario);
                }
            }
            foreach (var registro in ctx.Registrodescontos)
            {
                if (registro.IdDesconto == desconto.IdDesconto)
                {
                    ctx.Registrodescontos.Remove(registro);
                }
            }
            foreach (var favoritos in ctx.Descontofavoritos)
            {
                if (favoritos.IdDesconto == desconto.IdDesconto)
                {
                    ctx.Descontofavoritos.Remove(favoritos);
                }
            }
            ctx.Descontos.Remove(desconto);
            ctx.SaveChanges();
        }



        /// <summary>
        /// Listar todos os descontos
        /// </summary>
        /// <returns></returns>
        public List<Desconto> ListarTodos()
        {
            return ctx.Descontos
                    .Select(p => new Desconto
                    {
                        IdDesconto = p.IdDesconto,
                        IdEmpresa = p.IdEmpresa,
                        NomeDesconto = p.NomeDesconto,
                        DescricaoDesconto = p.DescricaoDesconto,
                        CaminhoImagemDesconto = p.CaminhoImagemDesconto,
                        ValidadeDesconto = p.ValidadeDesconto,
                        ValorDesconto = p.ValorDesconto,
                        NumeroCupom = p.NumeroCupom,
                        MediaAvaliacaoDesconto = p.MediaAvaliacaoDesconto,
                        IdEmpresaNavigation = new Empresa()
                        {

                            NomeEmpresa = p.IdEmpresaNavigation.NomeEmpresa,
                            EmailEmpresa = p.IdEmpresaNavigation.EmailEmpresa,
                            TelefoneEmpresa = p.IdEmpresaNavigation.TelefoneEmpresa,
                            IdLocalizacaoNavigation = new Localizacao()
                            {
                                Numero = p.IdEmpresaNavigation.IdLocalizacaoNavigation.Numero,
                                IdCep = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCep,
                                IdCepNavigation = new Cep()
                                {
                                    IdCep = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCepNavigation.IdCep,
                                    Cep1 = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCepNavigation.Cep1
                                },
                                IdLogradouroNavigation = new Logradouro()
                                {
                                    NomeLogradouro = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdLogradouroNavigation.NomeLogradouro
                                },
                                IdEstadoNavigation = new Estado()
                                {
                                    NomeEstado = p.IdEmpresaNavigation.IdLocalizacaoNavigation.IdEstadoNavigation.NomeEstado
                                }

                            }


                        }


                    })
                .ToList();
        }
    }
}

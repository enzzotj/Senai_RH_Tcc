using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Repositories
{
    public class FavoritosCursoRepository : IFavoritosCursoRepository
    {
        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Adcionar um curso dos favoritos
        /// </summary>
        /// <param name="Novofavorito"></param>
        public void AdcionarFavoritos(Cursofavorito Novofavorito)
        {
            Cursofavorito curso = new Cursofavorito()
            {
                IdCurso = Novofavorito.IdCurso,
                IdUsuario = Novofavorito.IdUsuario
            };
            ctx.Cursofavoritos.Add(curso);
            ctx.SaveChanges();

        }

        /// <summary>
        /// Buscar um favorito curso pelo seu id
        /// </summary>
        /// <param name="Id">id do curso favorito</param>
        /// <returns></returns>
        public Cursofavorito BuscarCursoFavoritoPorId(int Id)
        {
            return ctx.Cursofavoritos.FirstOrDefault(c => c.IdCursoFavorito == Id);
        }


        /// <summary>
        /// Excluir um curso dos favoritos 
        /// </summary>
        /// <param name="Id">Id do curso favorito</param>
        public void ExcluirFavoritos(int Id)
        {
            ctx.Cursofavoritos.Remove(BuscarCursoFavoritoPorId(Id));
            ctx.SaveChanges();
        }

        public List<Cursofavorito> ListarPorIdFavoritoCurso(int Id)
        {
            List<Cursofavorito> cursofavoritos = new();

            foreach (var favorito in ctx.Cursofavoritos.Select(p => new Cursofavorito
            {
                IdCursoFavorito = p.IdCursoFavorito,
                IdCurso = p.IdCurso,
                IdUsuario = p.IdUsuario,
                IdUsuarioNavigation = new Usuario
                {
                    Nome = p.IdUsuarioNavigation.Nome,
                    Email = p.IdUsuarioNavigation.Email,
                    Cpf = p.IdUsuarioNavigation.Cpf,
                    SaldoMoeda = p.IdUsuarioNavigation.SaldoMoeda
                },
                IdCursoNavigation = new Curso
                {

                    IdCurso = p.IdCursoNavigation.IdCurso,
                    IdEmpresa = p.IdCursoNavigation.IdEmpresa,
                    NomeCurso = p.IdCursoNavigation.NomeCurso,
                    DescricaoCurso = p.IdCursoNavigation.DescricaoCurso,
                    SiteCurso = p.IdCursoNavigation.SiteCurso,
                    ModalidadeCurso = p.IdCursoNavigation.ModalidadeCurso,
                    CaminhoImagemCurso = p.IdCursoNavigation.CaminhoImagemCurso,
                    CargaHoraria = p.IdCursoNavigation.CargaHoraria,
                    DataFinalizacao = p.IdCursoNavigation.DataFinalizacao,
                    MediaAvaliacaoCurso = p.IdCursoNavigation.MediaAvaliacaoCurso,
                    ValorCurso = p.IdCursoNavigation.ValorCurso,
                    IdSituacaoInscricao = p.IdCursoNavigation.IdSituacaoInscricao,
                    IdSituacaoInscricaoNavigation = new Situacaoatividade()
                    {
                        NomeSituacaoAtividade = p.IdCursoNavigation.IdSituacaoInscricaoNavigation.NomeSituacaoAtividade,
                        IdSituacaoAtividade = p.IdCursoNavigation.IdSituacaoInscricaoNavigation.IdSituacaoAtividade,
                    },
                    IdEmpresaNavigation = new Empresa()
                    {

                        NomeEmpresa = p.IdCursoNavigation.IdEmpresaNavigation.NomeEmpresa,
                        EmailEmpresa = p.IdCursoNavigation.IdEmpresaNavigation.EmailEmpresa,
                        TelefoneEmpresa = p.IdCursoNavigation.IdEmpresaNavigation.TelefoneEmpresa,
                        IdLocalizacaoNavigation = new Localizacao()
                        {
                            Numero = p.IdCursoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.Numero,
                            IdCep = p.IdCursoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCep,
                            IdCepNavigation = new Cep()
                            {
                                IdCep = p.IdCursoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCepNavigation.IdCep,
                                Cep1 = p.IdCursoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdCepNavigation.Cep1
                            },
                            IdLogradouroNavigation = new Logradouro()
                            {
                                NomeLogradouro = p.IdCursoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdLogradouroNavigation.NomeLogradouro
                            },
                            IdEstadoNavigation = new Estado()
                            {
                                NomeEstado = p.IdCursoNavigation.IdEmpresaNavigation.IdLocalizacaoNavigation.IdEstadoNavigation.NomeEstado
                            },

                        }
                    },
                }
            }).ToList())
            {
                if (favorito.IdUsuario == Id)
                {
                    cursofavoritos.Add(favorito);
                }
            }

            return cursofavoritos;
        }


        /// <summary>
        /// Listar os cursos favoritos de um determinado usuario
        /// </summary>
        /// <returns></returns>
        public List<Cursofavorito> ListarTodos()
        {
            return ctx.Cursofavoritos.Select(f => new Cursofavorito { 
                                                IdCursoFavorito =f.IdCursoFavorito,
                                                IdCurso = f.IdCurso,
                                                IdUsuario = f.IdUsuario,
                                                IdCursoNavigation = new Curso {
                                                    
                                                    IdEmpresa = f.IdCursoNavigation.IdEmpresa,
                                                    NomeCurso = f.IdCursoNavigation.NomeCurso,
                                                    DescricaoCurso = f.IdCursoNavigation.DescricaoCurso,
                                                    SiteCurso = f.IdCursoNavigation.SiteCurso,
                                                    ModalidadeCurso = f.IdCursoNavigation.ModalidadeCurso,
                                                    CaminhoImagemCurso = f.IdCursoNavigation.CaminhoImagemCurso,
                                                    CargaHoraria = f.IdCursoNavigation.CargaHoraria,
                                                    DataFinalizacao = f.IdCursoNavigation.DataFinalizacao,
                                                    MediaAvaliacaoCurso = f.IdCursoNavigation.MediaAvaliacaoCurso
                                                },
                                                IdUsuarioNavigation = new Usuario { 
                                                    Nome = f.IdUsuarioNavigation.Nome,
                                                    Email = f.IdUsuarioNavigation.Email,
                                                    Cpf = f.IdUsuarioNavigation.Cpf,
                                                    SaldoMoeda = f.IdUsuarioNavigation.SaldoMoeda
                                                }
                                            }).ToList();
        }
    }
}

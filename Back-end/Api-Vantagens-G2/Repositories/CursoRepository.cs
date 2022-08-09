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
    public class CursoRepository : ICursoRepository
    {
        //Instanciando um contexto 
        senaiRhContext ctx = new senaiRhContext();


        /// <summary>
        /// Buscar um curso pelo seu id 
        /// </summary>
        /// <param name="id">id do curso a ser buscado</param>
        /// <returns></returns>
        public Curso BuscarPorId(int id)
        {
            //Buscando um curso pelo id passado 
            return ctx.Cursos
                .Select(p => new Curso
                {
                    IdCurso = p.IdCurso,
                    IdEmpresa = p.IdEmpresa,
                    NomeCurso = p.NomeCurso,
                    DescricaoCurso = p.DescricaoCurso,
                    SiteCurso = p.SiteCurso,
                    ModalidadeCurso = p.ModalidadeCurso,
                    CaminhoImagemCurso = p.CaminhoImagemCurso,
                    CargaHoraria = p.CargaHoraria,
                    DataFinalizacao = p.DataFinalizacao,
                    MediaAvaliacaoCurso = p.MediaAvaliacaoCurso,
                    ValorCurso = p.ValorCurso,
                    IdSituacaoInscricao = p.IdSituacaoInscricao,
                    IdSituacaoInscricaoNavigation = new Situacaoatividade()
                    {
                        NomeSituacaoAtividade = p.IdSituacaoInscricaoNavigation.NomeSituacaoAtividade,
                        IdSituacaoAtividade = p.IdSituacaoInscricaoNavigation.IdSituacaoAtividade,
                    },
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
                .FirstOrDefault(c => c.IdCurso == id);
        }


        /// <summary>
        /// Cadastrar um novo curso
        /// </summary>
        /// <param name="novoCurso">dados desse novo curso a ser cadastrado</param>
        public void CadastrarCurso(CursoCadastroViewModel novoCurso)
        {
            //Definindo o valor do atrbuto Idsituacao
            novoCurso.IdSituacaoInscricao = 1;
            //Indtanciando um curso
            Curso curso = new Curso()
            {
                //passando os atribudos para cadastrar
                IdEmpresa = novoCurso.IdEmpresa,
                NomeCurso = novoCurso.NomeCurso,
                DescricaoCurso = novoCurso.DescricaoCurso,
                SiteCurso = novoCurso.SiteCurso,
                ModalidadeCurso = novoCurso.ModalidadeCurso,
                CaminhoImagemCurso = novoCurso.CaminhoImagemCurso,
                CargaHoraria = novoCurso.CargaHoraria,
                DataFinalizacao = novoCurso.DataFinalizacao,
                MediaAvaliacaoCurso = novoCurso.MediaAvaliacaoCurso = 0,
                IdSituacaoInscricao = (byte)novoCurso.IdSituacaoInscricao,
                ValorCurso = (int)novoCurso.ValorCurso
            };
            //cadastrando um curso 
            ctx.Cursos.Add(curso);
            //salvando o cadastro
            ctx.SaveChanges();
            
        }

        /// <summary>
        /// Excluir um curso 
        /// </summary>
        /// <param name="id">id do curso a ser excluido</param>
        public void ExcluirCurso(int id)
        {
            //buscando um curso pelo id passado
            Curso curso = ctx.Cursos.FirstOrDefault(c => c.IdCurso == id);

            //Excluindo os comentarios relacionado a esse curso
            foreach (var comentario in ctx.Comentariocursos)
            {
                if (comentario.IdCurso == curso.IdCurso)
                {
                    ctx.Comentariocursos.Remove(comentario);
                }
            }
            //Excluindo os registros relacionad a esse curso
            foreach (var registro in ctx.Registrocursos)
            {
                if (registro.IdCurso == curso.IdCurso)
                {
                    ctx.Registrocursos.Remove(registro);
                }
            }
            //Excluindo os favoritos relaconado a esse curso
            foreach (var favoritos in ctx.Cursofavoritos)
            {
                if (favoritos.IdCurso == curso.IdCurso)
                {
                    ctx.Cursofavoritos.Remove(favoritos);
                }
            }
            //Excluindo o curso buscado
            ctx.Cursos.Remove(curso);
            //Salvado a exclusão
            ctx.SaveChanges();
        }

        /// <summary>
        /// Listar todos os cursos
        /// </summary>
        /// <returns></returns>
        public List<Curso> ListarTodos()
        {
            //Listando os cursos usando um select para selecionar quais atributos iram listar
            return ctx.Cursos
                    .Select(p => new Curso
                    {                     
                    IdCurso = p.IdCurso,
                    IdEmpresa = p.IdEmpresa,
                    NomeCurso = p.NomeCurso,
                    DescricaoCurso = p.DescricaoCurso,
                    SiteCurso = p.SiteCurso,
                    ModalidadeCurso = p.ModalidadeCurso,
                    CaminhoImagemCurso = p.CaminhoImagemCurso,
                    CargaHoraria = p.CargaHoraria,
                    DataFinalizacao = p.DataFinalizacao,
                    MediaAvaliacaoCurso = p.MediaAvaliacaoCurso,
                    ValorCurso = p.ValorCurso,
                    IdSituacaoInscricao = p.IdSituacaoInscricao,
                    IdSituacaoInscricaoNavigation = new Situacaoatividade()
                    {
                        NomeSituacaoAtividade = p.IdSituacaoInscricaoNavigation.NomeSituacaoAtividade,
                        IdSituacaoAtividade = p.IdSituacaoInscricaoNavigation.IdSituacaoAtividade,
                    },
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

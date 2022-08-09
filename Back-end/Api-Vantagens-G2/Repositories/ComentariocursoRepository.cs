using senai_gp3_webApi.SentimentAnalisys;
using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Repositories
{
    public class ComentarioCursoRepository : IComentarioCursoRepository
    {
        /// <summary>
        /// Instanciando um contexto
        /// </summary>
        senaiRhContext ctx = new senaiRhContext();


        /// <summary>
        /// Listar comentarios pelo seu id 
        /// </summary>
        /// <param name="Id">Id comentario</param>
        /// <returns></returns>
        public Comentariocurso ListarComentarioPorId(int Id)
        {
            //Buscando um comentari curso pelo id passado
            return ctx.Comentariocursos.
                Select(p => new Comentariocurso
                {
                    IdComentarioCurso = p.IdComentarioCurso,
                    IdCurso = p.IdCurso,
                    IdUsuario = p.IdUsuario,
                    AvaliacaoComentario = p.AvaliacaoComentario,
                    ComentarioCurso1 = p.ComentarioCurso1,
                    Positivo = p.Positivo,
                    Neutro = p.Neutro,
                    Negativo = p.Negativo,
                    IdUsuarioNavigation = new Usuario
                    {
                        Nome = p.IdUsuarioNavigation.Nome
                    }

                })
                .FirstOrDefault(c => c.IdComentarioCurso == Id);
        }

        /// <summary>
        /// Cadastrar um novo comentario
        /// </summary>
        /// <param name="NovoComentario">Dados no novo comentario</param>
        public void CadastrarComentarioCurso(Comentariocurso NovoComentario)
        {
            //Instanciando um curso
            Curso curso = new Curso();

            //Intanciando um comentarioCurso
            Comentariocurso comentariocurso = new Comentariocurso();

            SentimentAnalysis sentimentAnalisys = new();

            // Pega o score da IA
            var analiseSentimento = sentimentAnalisys.AnalisarTexto(NovoComentario.ComentarioCurso1);

            // Atribui os scores
            NovoComentario.Positivo = Convert.ToDecimal(analiseSentimento.ConfidenceScores.Positive);
            NovoComentario.Negativo = Convert.ToDecimal(analiseSentimento.ConfidenceScores.Negative);
            NovoComentario.Neutro = Convert.ToDecimal(analiseSentimento.ConfidenceScores.Neutral);

            //Definindo os valores dos atributos
            comentariocurso.IdUsuario = NovoComentario.IdUsuario;
            comentariocurso.IdCurso = NovoComentario.IdCurso;
            comentariocurso.ComentarioCurso1 = NovoComentario.ComentarioCurso1;
            comentariocurso.AvaliacaoComentario = NovoComentario.AvaliacaoComentario;
            curso.IdCurso = NovoComentario.IdCurso;

            //Buscando um curso atraves do id curso
            Curso buscarMediaCurso = ctx.Cursos.FirstOrDefault(c => c.IdCurso == curso.IdCurso);

            //Verificação se a media for igual a 0 ele vai entrar no if
            if (buscarMediaCurso.MediaAvaliacaoCurso == 0)
            {
                //A soma da mediaAvaliaçãoCurso mais AvaliacaoCurso  
                buscarMediaCurso.MediaAvaliacaoCurso += NovoComentario.AvaliacaoComentario;
                //Alterando o valor da media
                ctx.Cursos.Update(buscarMediaCurso);
                //Cadastrando um novo comentario ao curso
                ctx.Comentariocursos.Add(NovoComentario);
                //Salvando o cadastro
                ctx.SaveChanges();
            }
            //caso a media for diferente que 0 ele entrara no else
            else
            {
                //Definindo o valor da media
                buscarMediaCurso.MediaAvaliacaoCurso = (buscarMediaCurso.MediaAvaliacaoCurso + NovoComentario.AvaliacaoComentario) / 2;
                //Alterando esse valor da media
                ctx.Cursos.Update(buscarMediaCurso);
                //Cadastrando um comentario
                ctx.Comentariocursos.Add(NovoComentario);
                //Salvando o cadastro 
                ctx.SaveChanges();
            }
        }



        /// <summary>
        /// Listar todos os comentarios 
        /// </summary>
        /// <returns></returns>
        public List<Comentariocurso> ListarComenatarioCurso()
        {
            //Listar todos os comentarios do curso , colocando um select com aapenas oa atrubutos que devem ser listados
            return ctx.Comentariocursos
                                .Select(p => new Comentariocurso
                                {
                                    IdComentarioCurso = p.IdComentarioCurso,
                                    IdCurso = p.IdCurso,
                                    IdUsuario = p.IdUsuario,
                                    AvaliacaoComentario = p.AvaliacaoComentario,
                                    ComentarioCurso1 = p.ComentarioCurso1,
                                    Positivo = p.Positivo,
                                    Neutro = p.Neutro,
                                    Negativo = p.Negativo,
                                    IdUsuarioNavigation = new Usuario
                                    {
                                        Nome = p.IdUsuarioNavigation.Nome
                                    }

                                })
                            .ToList();
        }



        /// <summary>
        /// Excluir um comentario 
        /// </summary>
        /// <param name="Id">Id do comentario</param>
        public void ExcluirComentarioCurso(int Id)
        {
            //Excluir um comentario pelo id passado
            ctx.Comentariocursos.Remove(ListarComentarioPorId(Id));
            //Salvando a exclusao
            ctx.SaveChanges();
        }

        /// <summary>
        /// Buscaar um comentario pelo seu id do curso
        /// </summary>
        /// <param name="Id">id do curso a ser buscado</param>
        /// <returns></returns>
        public List<Comentariocurso> ListarComentarioPorIdCurso(int Id)
        {
            //Lista de comentario do curso 
            List<Comentariocurso> comentarioCurso = new();
            //Repetição dos comentarios que tem no curso
            foreach (var comentario in ctx.Comentariocursos.Select(p => new Comentariocurso
            {
                IdComentarioCurso = p.IdComentarioCurso,
                IdCurso = p.IdCurso,
                IdUsuario = p.IdUsuario,
                ComentarioCurso1 = p.ComentarioCurso1,
                AvaliacaoComentario = p.AvaliacaoComentario,
                Positivo = p.Positivo,
                Neutro = p.Neutro,
                Negativo = p.Negativo,
                IdUsuarioNavigation = new Usuario
                {
                    IdUsuario = p.IdUsuarioNavigation.IdUsuario,
                    Nome = p.IdUsuarioNavigation.Nome,
                }
            }).ToList())
            {
                //Verificando se o id passado é igual a um id curso
                if (comentario.IdCurso == Id)
                {
                    comentarioCurso.Add(comentario);
                }
            }

            //retornando a lista de comentarioCurso 
            return comentarioCurso;
        }

        public List<Comentariocurso> ListarComenatarioCursoPorUsuario(int id)
        {
            return ctx.Comentariocursos
                                .Select(p => new Comentariocurso
                                {
                                    IdComentarioCurso = p.IdComentarioCurso,
                                    IdCurso = p.IdCurso,
                                    IdUsuario = p.IdUsuario,
                                    AvaliacaoComentario = p.AvaliacaoComentario,
                                    ComentarioCurso1 = p.ComentarioCurso1,
                                    Negativo = p.Negativo,
                                    Neutro = p.Neutro,
                                    Positivo = p.Positivo,
                                    IdUsuarioNavigation = new Usuario
                                    {
                                        Nome = p.IdUsuarioNavigation.Nome
                                    }

                                })
                                .Where(c => c.IdUsuario == id)
                            .ToList();
        }
    }
}

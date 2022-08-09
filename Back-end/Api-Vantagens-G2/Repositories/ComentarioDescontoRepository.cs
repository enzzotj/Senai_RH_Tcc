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
    public class ComentarioDescontoRepository : IComentarioDescontoRepository
    {
        senaiRhContext ctx = new senaiRhContext();


        /// <summary>
        /// Cadastrar um novo comentario
        /// </summary>
        /// <param name="NovoComentario">Dados no novo comentario</param>
        public void CadastrarComentarioDesconto(Comentariodesconto NovoComentario)
        {
            //Instaciando um desconto
            Desconto desconto = new Desconto();

            SentimentAnalysis sentimentAnalisys = new();

            // Pega o score da IA
            var analiseSentimento = sentimentAnalisys.AnalisarTexto(NovoComentario.ComentarioDesconto1);

            // Atribui os scores
            NovoComentario.Positivo = Convert.ToDecimal(analiseSentimento.ConfidenceScores.Positive);
            NovoComentario.Negativo = Convert.ToDecimal(analiseSentimento.ConfidenceScores.Negative);
            NovoComentario.Neutro = Convert.ToDecimal(analiseSentimento.ConfidenceScores.Neutral);

            //Insaciando um comentarioDesconto
            Comentariodesconto comentariodesconto = new Comentariodesconto();

            //Definindo valoresa aos atributos
            comentariodesconto.IdUsuario = NovoComentario.IdUsuario;
            comentariodesconto.IdDesconto = NovoComentario.IdDesconto;
            comentariodesconto.ComentarioDesconto1 = NovoComentario.ComentarioDesconto1;
            comentariodesconto.AvaliacaoDesconto = NovoComentario.AvaliacaoDesconto;
            desconto.IdDesconto = NovoComentario.IdDesconto;

            //Buscando um curso atraves de seu id
            Desconto buscarMediaDesconto = ctx.Descontos.FirstOrDefault(c => c.IdDesconto == desconto.IdDesconto);

            //Verificação se a media for igual a 0 ele vai entrar no if
            if (buscarMediaDesconto.MediaAvaliacaoDesconto == 0)
            {
                //A soma da mediaAvaliaçãoDesconto mais AvaliacaoDesconto
                buscarMediaDesconto.MediaAvaliacaoDesconto += NovoComentario.AvaliacaoDesconto;
                //Alterando o valor da media
                ctx.Descontos.Update(buscarMediaDesconto);
                //Cadastrando um novo comentario ao curso
                ctx.Comentariodescontos.Add(NovoComentario);
                //Salvando o cadastro
                ctx.SaveChanges();
            }
            //caso a media for diferente que 0 ele entrara no else
            else
            {
                //Definindo o valor da media
                buscarMediaDesconto.MediaAvaliacaoDesconto = (buscarMediaDesconto.MediaAvaliacaoDesconto + NovoComentario.AvaliacaoDesconto) / 2;
                //Alterando esse valor da media
                ctx.Descontos.Update(buscarMediaDesconto);
                //Cadastrando um comentario
                ctx.Comentariodescontos.Add(NovoComentario);
                //Salvando o cadastro 
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Excluir um comentario 
        /// </summary>
        /// <param name="Id">Id do comentario</param>
        public void ExcluirComentarioDesconto(int Id)
        {
            //Excluir um comentario pelo id passado
            ctx.Comentariodescontos.Remove(ListarComentarioPorId(Id));
            //Salvando a exclusao
            ctx.SaveChanges();
        }



        /// <summary>
        /// Listar todos os comentarios
        /// </summary>
        /// <returns></returns>
        public List<Comentariodesconto> ListarComenatarioDesconto()
        {
            //Listar todos os comentarios do desconto , colocando um select com aapenas oa atrubutos que devem ser listados
            return ctx.Comentariodescontos
                                .Select(p => new Comentariodesconto
                                {
                                    IdComentarioDesconto = p.IdComentarioDesconto,
                                    IdDesconto = p.IdDesconto,
                                    IdUsuario = p.IdUsuario,
                                    AvaliacaoDesconto = p.AvaliacaoDesconto,
                                    ComentarioDesconto1 = p.ComentarioDesconto1,
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
        /// Listar comentarios de um unico usuario 
        /// </summary>
        /// <param name="id">id do usuario a ser buscado</param>
        /// <returns></returns>
        public List<Comentariodesconto> ListarComenatarioDescontoPorUsuario(int id)
        {
            return ctx.Comentariodescontos
                                .Select(p => new Comentariodesconto
                                {
                                    IdComentarioDesconto = p.IdComentarioDesconto,
                                    IdDesconto = p.IdDesconto,
                                    IdUsuario = p.IdUsuario,
                                    AvaliacaoDesconto = p.AvaliacaoDesconto,
                                    ComentarioDesconto1 = p.ComentarioDesconto1,
                                    Positivo = p.Positivo,
                                    Neutro = p.Neutro,
                                    Negativo = p.Negativo,
                                    IdUsuarioNavigation = new Usuario
                                    {
                                        Nome = p.IdUsuarioNavigation.Nome
                                    }

                                })
                                .Where(d => d.IdUsuario == id)
                            .ToList();
        }

        /// <summary>
        /// Listar comentarios pelo seu id 
        /// </summary>
        /// <param name="Id">id comentario</param>
        /// <returns></returns>

        public Comentariodesconto ListarComentarioPorId(int Id)
        {
            //Buscando um comenario do curso atraves de seu id passado
            return ctx.Comentariodescontos.FirstOrDefault(c => c.IdComentarioDesconto == Id);
        }

        public List<Comentariodesconto> ListarComentarioPorIdDesconto(int Id)
        {
            //Lista de comentario do desconto
            List<Comentariodesconto> comentariodescontos = new();
            //Repetição dos comentarios que tem no Desconto
            foreach (var comentario in ctx.Comentariodescontos.Select(p => new Comentariodesconto
            {
                IdComentarioDesconto = p.IdComentarioDesconto,
                IdDesconto = p.IdDesconto,
                IdUsuario = p.IdUsuario,
                ComentarioDesconto1 = p.ComentarioDesconto1,
                AvaliacaoDesconto = p.AvaliacaoDesconto,
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
                //Verificando se o id passado é igual a um idDesconto
                if (comentario.IdDesconto == Id)
                {
                    comentariodescontos.Add(comentario);
                }
            }
            //retornando a lista de comentarioDesconto
            return comentariodescontos;

        }
    }
}

using Microsoft.EntityFrameworkCore;
using SenaiRH_G2.Contexts;
using SenaiRH_G2.Domains;
using SenaiRH_G2.Interfaces;
using SenaiRH_G2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace SenaiRH_G2.Repositories
{
    public class RegistrocursoRepository : IRegistrocursoRepository
    {

        senaiRhContext ctx = new senaiRhContext();


        /// <summary>
        /// Buscar um registro de cursos pelo seu id
        /// </summary>
        /// <param name="id">Id do registro a ser encontrado</param>
        /// <returns></returns>
        public Registrocurso BuscarPorId(int id)
        {
            return ctx.Registrocursos.FirstOrDefault(c => c.IdRegistroCurso == id);
        }



        /// <summary>
        /// Cadastrar um novo registro curso
        /// </summary>
        /// <param name="novoRegistrocurso"></param>
        /// <returns></returns>
        public void CadastrarRegistrocurso(RegistroCursoCadastrarViewModel novoRegistrocurso)
        {
            Usuario usuario = new Usuario();
            Curso curso = new Curso();
            Registrocurso registrocurso = new Registrocurso();
            registrocurso.IdUsuario = novoRegistrocurso.IdUsuario;
            registrocurso.IdCurso = novoRegistrocurso.IdCurso;
            novoRegistrocurso.IdSituacaoAtividade = 2;
            registrocurso.IdSituacaoAtividade = (byte)novoRegistrocurso.IdSituacaoAtividade;

            usuario.IdUsuario = registrocurso.IdUsuario;
            curso.IdCurso = registrocurso.IdCurso;

            Usuario buscarUsuario = ctx.Usuarios.FirstOrDefault(c => c.IdUsuario == usuario.IdUsuario);
            Curso buscarCurso = ctx.Cursos.FirstOrDefault(c => c.IdCurso == curso.IdCurso);

            if (buscarUsuario.SaldoMoeda >= (int)buscarCurso.ValorCurso)
            {

                buscarUsuario.SaldoMoeda -= (int)buscarCurso.ValorCurso;

                ctx.Usuarios.Update(buscarUsuario);
                ctx.Registrocursos.Add(registrocurso);
                ctx.SaveChanges();
            }

        }


        /// <summary>
        /// Deletar um registro 
        /// </summary>
        /// <param name="id">Id do registro a ser deletado</param>
        /// <returns></returns>
        public void ExcluirRegistrocurso(int id)
        {
            Registrocurso buscarPorId = ctx.Registrocursos.FirstOrDefault(c => c.IdRegistroCurso == id);
            ctx.Registrocursos.Remove(buscarPorId);
            ctx.SaveChanges();
        }


        /// <summary>
        /// Listar todos os registros de desconto
        /// </summary>
        /// <returns></returns>
        public List<Registrocurso> ListarTodos()
        {
            return ctx.Registrocursos.Select(p => new Registrocurso
            {

                IdRegistroCurso = p.IdRegistroCurso,
                IdCurso = p.IdCurso,
                IdUsuario = p.IdUsuario,
                IdSituacaoAtividade = p.IdSituacaoAtividade,
                IdUsuarioNavigation = new Usuario()
                {
                    IdUsuario = p.IdUsuarioNavigation.IdUsuario,
                    Nome = p.IdUsuarioNavigation.Nome,
                    SaldoMoeda = p.IdUsuarioNavigation.SaldoMoeda,
                    Email = p.IdUsuarioNavigation.Email,
                    Cpf = p.IdUsuarioNavigation.Cpf,
                    IdCargoNavigation = new Cargo()
                    {
                        IdCargo = p.IdUsuarioNavigation.IdCargoNavigation.IdCargo,
                        NomeCargo = p.IdUsuarioNavigation.IdCargoNavigation.NomeCargo
                    }
                },
                IdCursoNavigation = new Curso()
                {
                    IdCurso = p.IdCursoNavigation.IdCurso,
                    NomeCurso = p.IdCursoNavigation.NomeCurso,
                    ValorCurso = p.IdCursoNavigation.ValorCurso,
                    SiteCurso = p.IdCursoNavigation.SiteCurso,
                },
                IdSituacaoAtividadeNavigation = new Situacaoatividade()
                {
                    IdSituacaoAtividade = p.IdSituacaoAtividadeNavigation.IdSituacaoAtividade,
                    NomeSituacaoAtividade = p.IdSituacaoAtividadeNavigation.NomeSituacaoAtividade
                }

            }).ToList();
        }

        /// <summary>
        /// Atualizar situação
        /// </summary>
        /// <param name="idRegistroCurso">Id do curso registrado</param>
        public void AtualizarSituacao(int idRegistroCurso)
        {

            Registrocurso buscarPoId = ctx.Registrocursos.FirstOrDefault(c => c.IdRegistroCurso == idRegistroCurso);
            buscarPoId.IdSituacaoAtividade = 1;
            ctx.Registrocursos.Update(buscarPoId);
            ctx.SaveChanges();


        }

        /// <summary>
        /// Enviar Comunicado sobre o registro d curso atraves do email
        /// </summary>
        /// <param name="email"></param>
        public void EnviaEmailDescricao(string email)
        {
            Usuario user = ctx.Usuarios.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("SenaiRHteste", "senairhteste@gmail.com"));
                message.To.Add(MailboxAddress.Parse(user.Email));
                message.Subject = "Seu Cadastro do Curso foi Realizado";
                message.Body = new TextPart("plain")
                {
                    Text = @"Olá, " + user.Nome + ". Parabens seus cadastro foi realizado com sucesso. Em breve, você receberá mais notificações sobre o curso." +
                    "Obrigado por comprar nossos cursos."
                };

                SmtpClient client = new SmtpClient();

                try
                {

                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("senairhteste@gmail.com", "SesiSenai@132");
                    client.Send(message);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();

                }
            }

        }

        public List<Registrocurso> ListarRegistroCursoPorIdSituação(int Id)
        {
            List<Registrocurso> registrocursos = new();
            foreach (var registro in ctx.Registrocursos.Select(p => new Registrocurso
            {
                IdRegistroCurso = p.IdRegistroCurso,
                IdSituacaoAtividade = p.IdSituacaoAtividade,
                IdCurso = p.IdCurso,
                IdUsuario = p.IdUsuario,
                IdCursoNavigation = new Curso
                {
                    IdCurso = p.IdCurso,
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
                if(registro.IdSituacaoAtividade == Id)
                {
                    registrocursos.Add(registro);
                }
                
            }
            return registrocursos;
        }

        public List<Registrocurso> ListarRegistrocursoPorUsuario(int id)
        {
            List<Registrocurso> registrocursos = new();
            foreach (var registro in ctx.Registrocursos.Select(p => new Registrocurso
            {
                IdRegistroCurso = p.IdRegistroCurso,
                IdSituacaoAtividade = p.IdSituacaoAtividade,
                IdCurso = p.IdCurso,
                IdUsuario = p.IdUsuario,
                IdCursoNavigation = new Curso
                {
                    IdCurso = p.IdCursoNavigation.IdCurso,
                    NomeCurso = p.IdCursoNavigation.NomeCurso,
                    SiteCurso = p.IdCursoNavigation.SiteCurso
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
                    registrocursos.Add(registro);
                }
            }
            return registrocursos;
        }
    }
}

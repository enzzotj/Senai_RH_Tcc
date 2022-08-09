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
    public class EmpresaRepository : IEmpresaRepository
    {

        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Buscar um empresa pelo seu id 
        /// </summary>
        /// <param name="id">id da empresa a ser buscada </param>
        /// <returns></returns>
        public Empresa BuscarPorId(int id)
        {
            return ctx.Empresas.FirstOrDefault(c => c.IdEmpresa == id);
        }

        /// <summary>
        /// Cadastrar um nova empresa
        /// </summary>
        /// <param name="novoEmpresa">Dados da empresa a ser cadastrada</param>
        public void CadastrarEmpresa(EmpresaCadastroViewModel novoEmpresa)
        {
            Localizacao localizacao = ctx.Localizacaos.FirstOrDefault(l => l.IdLogradouroNavigation.NomeLogradouro == novoEmpresa.Logradouro && l.Numero == novoEmpresa.Numero);

            if (localizacao == null)
            {
                LocalizacaoViewModel novaLocalizacao = new()
                {
                    Bairro = novoEmpresa.Bairro,
                    Cep = novoEmpresa.Cep,
                    Logradouro = novoEmpresa.Logradouro,
                    Numero = novoEmpresa.Numero,
                    Estado = novoEmpresa.Estado,
                    Cidade = novoEmpresa.Cidade
                };

                LocalizacaoRepository localizacaoRepository = new();
                localizacaoRepository.CadastrarLocalizacao(novaLocalizacao);
            }

            int idLocalizacaoCadastrada = ctx.Localizacaos.FirstOrDefault(l => l.IdLogradouroNavigation.NomeLogradouro == novoEmpresa.Logradouro && l.Numero == novoEmpresa.Numero).IdLocalizacao;

            Empresa empresa = new()
            {

                IdLocalizacao = idLocalizacaoCadastrada,
                NomeEmpresa = novoEmpresa.NomeEmpresa,
                EmailEmpresa = novoEmpresa.EmailEmpresa,
                TelefoneEmpresa = novoEmpresa.TelefoneEmpresa,
                CaminhoImagemEmpresa = novoEmpresa.CaminhoImagemEmpresa,

            };

            ctx.Empresas.Add(empresa);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Excluir uma empresa 
        /// </summary>
        /// <param name="id">Id da empresa a ser excluida</param>
        public void ExcluirEmpresa(int id)
        {
            Empresa buscarPorId = ctx.Empresas.FirstOrDefault(c => c.IdEmpresa == id);
            ctx.Empresas.Remove(buscarPorId);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Listar todas as empresas
        /// </summary>
        /// <returns></returns>
        public List<Empresa> ListarTodos()
        {
            return ctx.Empresas
                    .Select(p => new Empresa
                    {
                        IdEmpresa = p.IdEmpresa,
                        IdLocalizacao = p.IdLocalizacao,
                        NomeEmpresa = p.NomeEmpresa,
                        EmailEmpresa = p.EmailEmpresa,
                        TelefoneEmpresa = p.TelefoneEmpresa,
                        CaminhoImagemEmpresa = p.CaminhoImagemEmpresa,
                        IdLocalizacaoNavigation = new Localizacao()
                        {
                            Numero = p.IdLocalizacaoNavigation.Numero,
                            IdLogradouroNavigation = new Logradouro()
                            {
                                NomeLogradouro = p.IdLocalizacaoNavigation.IdLogradouroNavigation.NomeLogradouro
                            },
                            IdEstadoNavigation = new Estado()
                            {
                                NomeEstado = p.IdLocalizacaoNavigation.IdEstadoNavigation.NomeEstado
                            }

                        }

                    })
                .ToList();

        }
    }
}

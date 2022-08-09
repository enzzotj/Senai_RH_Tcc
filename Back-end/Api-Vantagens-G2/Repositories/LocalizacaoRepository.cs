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
    public class LocalizacaoRepository : ILocalizacaoRepository
    {

        senaiRhContext ctx = new senaiRhContext();

        /// <summary>
        /// Buscar uma localizacao pelo seu id
        /// </summary>
        /// <param name="id">Id da localizacao</param>
        /// <returns></returns>
        public Localizacao BuscarPorId(int id)
        {
            return ctx.Localizacaos.FirstOrDefault(c => c.IdLocalizacao== id);
        }

        /// <summary>
        /// Cadastrar um nova localizacao
        /// </summary>
        /// <param name="novoLocalizacao">Dados da localizacao a ser cadastrada</param>
        public void CadastrarLocalizacao(LocalizacaoViewModel novoLocalizacao)
        {

            // Cadastra os dados que vieram da localização
            if (ctx.Ceps.FirstOrDefault(c => c.Cep1 == novoLocalizacao.Cep) == null)
            {
                CepRepository cepRepository = new();
                Cep cepDomain = new();
                cepDomain.Cep1 = novoLocalizacao.Cep;
                cepRepository.CadastrarCep(cepDomain);
            }



            if(ctx.Estados.FirstOrDefault(e => e.NomeEstado == novoLocalizacao.Estado) == null)
            {
                EstadoRepository estadoRepository = new();
                Estado estadoDomain = new();
                estadoDomain.NomeEstado = novoLocalizacao.Estado;
                estadoRepository.CadastrarEstado(estadoDomain);
            }

            if(ctx.Cidades.FirstOrDefault(c => c.NomeCidade == novoLocalizacao.Cidade) == null)
            {
                CidadeRepository cidadeRepository = new();
                Cidade cidadeDomain = new();
                cidadeDomain.NomeCidade = novoLocalizacao.Cidade;
                cidadeRepository.CadastrarCidade(cidadeDomain);
            }

            if(ctx.Bairros.FirstOrDefault(b => b.NomeBairro == novoLocalizacao.Bairro) == null)
            {
                BairroRepository bairroRepository = new();
                Bairro bairroDomain = new();
                bairroDomain.NomeBairro = novoLocalizacao.Bairro;
                bairroRepository.CadastrarBairro(bairroDomain);
            }

            if (ctx.Logradouros.FirstOrDefault(l => l.NomeLogradouro == novoLocalizacao.Logradouro) == null)
            {
                LogradouroRepository logradouroRepository = new();
                Logradouro logradouroDomain = new();
                logradouroDomain.NomeLogradouro = novoLocalizacao.Logradouro;
                logradouroRepository.CadastrarLogradouro(logradouroDomain);
            }


            // Pega os dados que acabaram de ser cadastrador
            int idCep = ctx.Ceps.FirstOrDefault(c => c.Cep1 == novoLocalizacao.Cep).IdCep;
            int idEstado = ctx.Estados.FirstOrDefault(e => e.NomeEstado == novoLocalizacao.Estado).IdEstado;
            int idCidade = ctx.Cidades.FirstOrDefault(c => c.NomeCidade == novoLocalizacao.Cidade).IdCidade;
            int idBairro = ctx.Bairros.FirstOrDefault(b => b.NomeBairro == novoLocalizacao.Bairro).IdBairro;
            int idLogradouro = ctx.Logradouros.FirstOrDefault(l => l.NomeLogradouro == novoLocalizacao.Logradouro).IdLogradouro;

            // Cadastar uma localização com os id's já armazenados

            Localizacao localizacao = new()
            {
                IdCidade = Convert.ToByte(idCidade),
                IdLogradouro = idLogradouro,
                IdBairro = idBairro,
                IdCep = Convert.ToByte(idCep),
                IdEstado = Convert.ToByte(idEstado),
                Numero = novoLocalizacao.Numero
            };

            ctx.Localizacaos.Add(localizacao);
            ctx.SaveChanges();


        }


        /// <summary>
        /// Excluir uma localizacao 
        /// </summary>
        /// <param name="id">Id da localizacao a ser excluida</param>
        public void ExcluirLocalizacao(int id)
        {
            Localizacao buscarPorId = ctx.Localizacaos.FirstOrDefault(c => c.IdLocalizacao == id);
            ctx.Localizacaos.Remove(buscarPorId);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Lstar todase as localizacoes
        /// </summary>
        /// <returns></returns>
        public List<Localizacao> ListarTodos()
        {
            return ctx.Localizacaos.Select(p => new Localizacao
            {

                IdLocalizacao = p.IdLocalizacao,
                IdCep = p.IdCep,
                IdBairro = p.IdBairro,
                IdLogradouro = p.IdLogradouro,
                IdCidade = p.IdCidade,
                IdEstado = p.IdEstado,
                Numero = p.Numero,
                IdCepNavigation = new Cep()
                {
                    Cep1 = p.IdCepNavigation.Cep1
                },
                IdBairroNavigation = new Bairro()
                {
                    NomeBairro = p.IdBairroNavigation.NomeBairro
                },
                IdLogradouroNavigation = new Logradouro()
                {
                    NomeLogradouro = p.IdLogradouroNavigation.NomeLogradouro
                },
                IdCidadeNavigation = new Cidade()
                {
                    NomeCidade = p.IdCidadeNavigation.NomeCidade
                },
                IdEstadoNavigation = new Estado()
                {
                    NomeEstado = p.IdEstadoNavigation.NomeEstado
                }

            }).ToList();
        }
    }
}

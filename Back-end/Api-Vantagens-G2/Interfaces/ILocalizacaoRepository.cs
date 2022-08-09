using SenaiRH_G2.Domains;
using SenaiRH_G2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiRH_G2.Interfaces
{
    public interface ILocalizacaoRepository
    {
        /// <summary>
        /// Listar todos as localizações
        /// </summary>
        /// <returns></returns>
        List<Localizacao> ListarTodos();
        /// <summary>
        /// Excluir uma localização pelo seu id
        /// </summary>
        /// <param name="id">id da localização a ser excluida</param>
        void ExcluirLocalizacao(int id);
        /// <summary>
        /// Buscar uma localização pelo seu id
        /// </summary>
        /// <param name="id">id da localização a ser buscada</param>
        /// <returns></returns>
        Localizacao BuscarPorId(int id);
        /// <summary>
        /// Cadastrar uma localização 
        /// </summary>
        /// <param name="novoLocalizacao">Dados da localização a ser cadastrada</param>
        void CadastrarLocalizacao(LocalizacaoViewModel novoLocalizacao);

    }
}

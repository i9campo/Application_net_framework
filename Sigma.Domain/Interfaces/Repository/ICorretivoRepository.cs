using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository._Base;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;

namespace Sigma.Domain.Interfaces.Repository
{
    public interface ICorretivoRepository : IRepository<Corretivo>
    {
        IEnumerable<CorretivoView> GetCorretivoGrid(Guid iDPropriedade, Guid iDSafra);

        /// <summary>
        /// <para> Retorna uma lista de corretivos a partir do ID da propriedade e o ID da Safra. </para>
        /// <para> Também será necessário os dados do corretivo como prnt, perCaO, perMgO, perPe2O5, perK2O, s, corretivo e ca.  </para>
        /// <para> O Retorno da lista contém os calculos de V, P e S. </para>
        /// </summary>
        /// <param name="IDPropriedade"></param>
        /// <param name="IDSafra"></param>
        /// <param name="corretivo"></param>
        /// <param name="prnt"></param>
        /// <param name="perCaO"></param>
        /// <param name="perMgO"></param>
        /// <param name="perP2O5"></param>
        /// <param name="perK2O"></param>
        /// <param name="S"></param>
        /// <param name="perCa"></param>
        /// <returns></returns>
        IEnumerable<CorretivoView> GetListAlteracaoCorretivo(Guid IDPropriedade, Guid IDSafra, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string S, string perCa);
        bool SetDivideDose(SetDivideDoseCorretivo obj);

        /// <summary> </summary>
        /// <param name="IDAreaServico"></param>
        /// <param name="IDGrid"></param>
        /// <param name="opcao"></param>
        /// <param name="chk"></param>
        /// <returns></returns>
        bool UpdateMarcado(Guid IDAreaServico, Guid IDGrid, int opcao, bool chk);



        IEnumerable<CorretivoView> GetListCorretivo(Guid IDAreaServico, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string S, string perCa);

        IEnumerable<CorretivoView> GetOptionListaCompra(String iDArea, Guid iDSafra);
        IEnumerable<string> GetRelatorioListParcialCorretivos(String IDArea, String IDSafra, String CorretivoSelecionado, String IDCorretivoSelecionado, String IDPropriedade, String numServico, String opcaoPerfil, String opcaoCorretivo, String opcaoFertilizante, String Fertilizante, String IDFertilizante, String Tipo);

        IEnumerable<CorretivoView> GetListCorretivoCompra(String IDArea, Guid IDSafra, string optionCorretivo, string optionPerfil, string Tipo, string numServico);
        #region Recomendação alteração de corretivo.

        /// <summary>
        /// <para>Este método retorna uma lista de corretivos referente ao ID da área e o ID da safra. </para>
        /// <para>IDArea não é obrigatório</para>
        /// <para>Type: 0 não adiciona a área na consulta, 1 adiciona a área na consulta. </para>
        /// </summary>
        /// <param name="IDArea"></param>
        /// <param name="IDPropriedade"></param>
        /// <param name="IDSafra"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        IEnumerable<CorretivoView> GetAllCorretivoSelectAlteracao(Guid? IDArea, Guid IDPropriedade, Guid IDSafra, int Type);

        /// <summary>
        /// <para>Método utilizado para carregar a lista do novo corretivo.  </para>
        /// </summary>
        /// <param name="IDPropriedade"></param>
        /// <param name="IDSafra"></param>
        /// <param name="corretivo"></param>
        /// <param name="prnt"></param>
        /// <param name="perCaO"></param>
        /// <param name="perMgO"></param>
        /// <param name="perP2O5"></param>
        /// <param name="perK2O"></param>
        /// <param name="s"></param>
        /// <param name="perCa"></param>
        /// <param name="newCorretivo"></param>
        /// <param name="newPrnt"></param>
        /// <param name="newPerCaO"></param>
        /// <param name="newPerMgO"></param>
        /// <param name="newPerP2O5"></param>
        /// <param name="newPerK2O"></param>
        /// <param name="newS"></param>
        /// <param name="newPerCa"></param>
        /// <returns></returns>
        IEnumerable<CorretivoView> GetListAlteracaoCorretivoNew(string IDAreaServico, string eficiencia, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string s, string perCa, string neweficiencia, string newCorretivo, string newprnt, string newpercao, string newpermgo, string newperp2o5, string newperk2o, string newpers, string newperca, string dosefixa);

        #endregion
        #region Recomendação tela de correção/ análise/ perfil/ ciclos.

        /// <param name="IDAreaServico">Retorna uma lista de corretivos a partir do ID da Área Serviço.</param>
        /// <param name="IDGrid">Retorna uma lista de corretivos a partir do ID do Grid.</param>
        /// <param name="IDPropriedade">Retorna uma lista de corretivos a partir do ID da Propriedade.</param>
        /// <param name="IDSafra">Retorna uma lista de corretivos a partir do IDSafra. </param>
        /// <param name="opcao">Retorna uma lista de corretivos a partir da opção. </param>
        /// <param name="perfil">Retorna uma lista de corretivos do tipo perfil, se o dado for igual à 1. </param>
        /// <param name="corretivo">Retorna uma lista de corretivos do tipo corretivo, se o dado for igual a 1, se ele for igual a zero retorna fertilizante. </param>
        /// <returns>Retorna uma lista de corretivo de uma forma genérica</returns>
        IEnumerable<Corretivo> GetListCorretivo(Guid? IDAreaServico, Guid? IDGrid, Guid? IDPropriedade, Guid? IDSafra, int opcao, int perfil, int corretivo);

        /// <summary>
        /// <para>Retorna a quantidade de registros a partir de uma área ou a partir das zonas de manejo. </para>
        /// <para>O Type vai definir o tipo de retorno. </para>
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        int GetCountRegistros(Guid objID, int Type);

        /// <param name="IDAreaServico"></param>
        /// <param name="IDGrid"></param>
        /// <param name="Profundidade">(PERFIL =	"0" para profundidade de 00-20 e "1" para profundidade de 20-40)</param>
        /// <param name="unid">(UND = "0" Análise padrão E "1" em Kg/Ha)</param>
        /// <param name="tipo">(TIPO = "0" Media por Area com IDGrid nulo, "1" Media por ZM informando o IDGrid, "2" Media por Area com IDGrid nulo ou não, "3" Media por Area Ponderada pela ZM e "4" Lista da média por ZM)</param>
        /// <param name="RetornoP">(RETORNOP = "0" retorna o PMelh e o PRes e "1" retorna P independente do tipo)</param>
        /// <param name="Opcao">(Opção selecionada pelo usuário)</param>
        /// <returns></returns>
        ResultadoCorrecao RetornaResultadoCorrecao(Guid IDAreaServico, Guid? IDGrid, String Profundidade, int unid, int tipo, int RetornoP,  string Corretivo, int Opcao);

        /// <summary> Função utilizada para carregar a média do corretivo. </summary>
        /// <param name="IDAreaServico">ID referente a área serviço.</param>
        /// <param name="IDGrid">ID referente ao Grid mas não é obrigatório. </param>
        /// <param name="profundidade"> Profundidade 00 - 20 ou 20 - 40 </param>
        /// <param name="tipo"> </param>
        /// <param name="opcao">Opção selecionada</param>
        /// <param name="perfil">Buscar por perfil. </param>
        /// <returns>Retorna a média do corretivo. </returns>
        CorretivoView GetMediaCorretivo(Guid IDAreaServico, Guid? IDGrid, string profundidade, int tipo, int opcao, int perfil);


        CorretivoView GetOpcaoValida(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico);

        /// <summary>
        /// <para>Método utilizado para atualizar a lista de corretivos válido</para>
        /// <para>Onde será necessario passar três parâmetros que é, ID do Grid, Opção e a função sé e marcado ou desmarcado. </para>
        /// </summary>
        /// <param name="IDGrid"></param>
        /// <param name="Opcao"></param>
        /// <param name="MarcarOrDesmarcar"></param>
        /// <returns></returns>
        bool UpdateCorretivoOpcaoMaracada(Guid objID, int opcao, int perfil, bool MarcarOrDesmarcar);
        IEnumerable<CorretivoView> UpdateListCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, String IDPropriedade, String IDSafra, double S, double Ca);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="ID"></param>
        /// <param name="opcao"></param>
        /// <param name="perfil"></param>
        /// <param name="marcar"></param>
        /// <returns></returns>
        bool UpdateOpcaoMarcada(Guid objID, bool MarcarOrDesmarcar);

        /// <summary>
        /// <para>Método utilizado para verificar sé o elemento registro através do objID e a opção é uma opção marcada ou não. </para>
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="opcao"></param>
        /// <returns></returns>
        CorretivoView GetOpcaoMarcadaByOpcao(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico);

        /// <summary>
        /// <para>Método utilizado para retornar a média do grid alterado. </para>
        /// </summary>
        /// <param name="IDAreaServico"></param>
        /// <param name="IDGrid"></param>
        /// <returns></returns>
        CorretivoView MediaGridAlterado(Guid IDAreaServico, Guid IDGrid);
        int PostCorretivo(double prnt, double perCaO, double mgo, double p2O5, double k2O5, string corretivoalt, string corretivoant, double eficiencia, double s, double ca);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDAreaServico"></param>
        /// <param name="Corretivo"></param>
        /// <returns></returns>
        List<CorretivoView> CorretivoFinal(Guid IDAreaServico, List<CorretivoView> Corretivo);
        IEnumerable<CorretivoView> GetListOpcao(Guid iDAreaServico);
        IEnumerable<CorretivoView> ReplicarCorretivo(IEnumerable<CorretivoView> obj);

        /// <summary>
        /// <para>Método utilizado para atualizar o estado da correção para válido ou sem opção. </para>
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="Opcao"></param>
        /// <param name="MarcarOrDesmarcar"></param>
        /// <param name="type"></param>
        /// <returns>Retorna um resultado boolean. </returns>
        bool UpdateOptionChecked(Guid objID, int Opcao, bool MarcarOrDesmarcar, int type);

        bool DeleteAllCorretivoByOption(Guid objID, int Option, int Type);


        /// <summary>
        /// <para>Necessário passar como parâmetro o ID da Área Serviço. </para>
        /// </summary>
        /// <param name="IDAreaServico"></param>
        /// <returns>Retorna uma lista de opções contendo informações da opção que contém corretivo ou não. </returns>
        IEnumerable<Options> GetListOpcaoCorretivo(Guid objID, int Type, int perfil);
        /// <summary> Este método será utilizado para verificar a existencia de analises solo 20-40 em toda á área. </summary>
        bool CheckedAnaliseSolo2040(Guid IDAreaServico);
        #endregion
    }
}

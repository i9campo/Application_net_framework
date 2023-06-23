using SharpDX.Direct3D9;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repository;
using Sigma.Domain.ViewTables;
using Sigma.Infra.Data.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigma.Infra.Data.Repositories
{
    public class CorretivoRepository : RepositoryBase<Corretivo>, ICorretivoRepository
    {
        public IEnumerable<CorretivoView> GetCorretivoGrid(Guid IDPropriedade, Guid IDSafra)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT a.nome as area, g.descricao as Grid, c.* FROM Corretivo c INNER JOIN AreaServico asv ON asv.objID = c.IDAreaServico");
            query.AppendLine("INNER JOIN Area a ON a.objID =  asv.IDArea INNER JOIN Propriedade p ON p.objID = a.IDPropriedade INNER JOIN Grid g ON g.objID = c.IDGrid");
            query.AppendLine("WHERE  (c.opcaoMarcado = 1) AND (asv.IDSafra = '" + IDSafra + "') AND (p.objID = '" + IDPropriedade + "')");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        public IEnumerable<CorretivoView> GetListAlteracaoCorretivo(Guid IDPropriedade, Guid IDSafra, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string S, string perCa)
        {
            prnt = prnt.Replace(',', '.');
            perCaO = perCaO.Replace(',', '.');
            perMgO = perMgO.Replace(',', '.');
            perP2O5 = perP2O5.Replace(',', '.');
            perK2O = perK2O.Replace(',', '.');
            S = S.Replace(',', '.');
            perCa = perCa.Replace(',', '.');

            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT                  ");
            query.AppendLine("      objID          ,   ");
            query.AppendLine("      IDGrid         ,   ");
            query.AppendLine("      IDAreaServico  ,   ");
            query.AppendLine("      Area           ,   ");
            query.AppendLine("      Eficiencia     ,   ");
            query.AppendLine("      ZM             ,   ");
            query.AppendLine("      TamanhoZM      ,   ");
            query.AppendLine("      KgHa           ,   ");
            query.AppendLine("      V              ,   ");
            query.AppendLine("      Ca             ,   ");
            query.AppendLine("      Mg             ,   ");
            query.AppendLine("      K              ,   ");
            query.AppendLine("      P              ,   ");
            query.AppendLine("      S                  ");
            query.AppendLine("FROM fGetListAlteracaoCorretivo('" + IDSafra + "','" + IDPropriedade + "','" + corretivo + "', '" + prnt + "', '" + perCaO + "',   '" + perMgO + "',  '" + perP2O5 + "', '" + perK2O + "', '" + S + "', '" + perCa + "') ORDER BY Area, ZM");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        public IEnumerable<CorretivoView> GetOptionListaCompra(String IDArea, Guid IDSafra)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM  fGetOptionsListaCompra('" + IDArea + "', '" + IDSafra + "')");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        public IEnumerable<CorretivoView> GetListCorretivoCompra(String IDArea, Guid IDSafra, string optionCorretivo, string optionPerfil, string Tipo, string numServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT * FROM  fGetAllProdutosListaCompra('" + IDArea + "', '" + IDSafra + "',  '" + numServico + "', '" + optionPerfil + "' , '" + optionCorretivo + "',  " + Tipo + ")");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        public IEnumerable<string> GetRelatorioListParcialCorretivos(String IDArea, String IDSafra, String CorretivoSelecionado, String IDCorretivoSelecionado, String IDPropriedade, String numServico, String opcaoPerfil, String opcaoCorretivo, String opcaoFertilizante, String Fertilizante, String IDFertilizante, String Tipo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("declare @teste as NVARCHAR(MAX), @Comando as NVARCHAR(MAX), @Total as  NVARCHAR(MAX)");
            if (Tipo == "C")
            {
                query.AppendLine("set @teste = stuff((select distinct ',' + quotename(Corretivo) from fGetRelatorioListParcialCorretivos('" + @IDSafra + "', '" + @IDPropriedade + "', '" + @IDArea + "', '" + @numServico + "', '" + @CorretivoSelecionado + "', '" + @IDCorretivoSelecionado + "', '" + @opcaoPerfil + "', '" + @opcaoCorretivo + "', '" + @opcaoFertilizante + "', '" + @Fertilizante + "', '" + @IDFertilizante + "') for xml path('')), 1, 1, '')");
                query.AppendLine("set @Total = stuff((select distinct 'ROUND(SUM([' + Corretivo + ']),3) as [' + Corretivo + '], ' from fGetRelatorioListParcialCorretivos('" + @IDSafra + "', '" + @IDPropriedade + "', '" + @IDArea + "', '" + @numServico + "', '" + @CorretivoSelecionado + "', '" + @IDCorretivoSelecionado + "', '" + @opcaoPerfil + "', '" + @opcaoCorretivo + "', '" + @opcaoFertilizante + "', '" + @Fertilizante + "', '" + @IDFertilizante + "') for xml path('')), 1, 1, '')");
                query.AppendLine("set @Comando = 'SELECT Safra, Area, Ha, Cultura, R'+@Total+' Fertilizante FROM fGetRelatorioListParcialCorretivos(''" + @IDSafra + "'', ''" + @IDPropriedade + "'', ''" + @IDArea + "'',''" + @numServico + "'', ''" + @CorretivoSelecionado + "'', ''" + @IDCorretivoSelecionado + "'', ''" + @opcaoPerfil + "'', ''" + @opcaoCorretivo + "'' , ''" + @opcaoFertilizante + "'', ''" + @Fertilizante + "'', ''" + @IDFertilizante + "'')");
                query.AppendLine("PIVOT(  MAX(qtde) FOR Corretivo IN('+@teste+'))AS pvt group by Safra,Area,Ha, Cultura, Fertilizante FOR JSON PATH, ROOT(''Orders'')'");
            }
            if (Tipo == "F")
            {
                query.AppendLine("set @teste = stuff((select distinct ',' + quotename(Fertilizante) from fGetRelatorioListParcialCorretivos('" + @IDSafra + "', '" + @IDPropriedade + "', '" + @IDArea + "', '" + @numServico + "', '" + @CorretivoSelecionado + "', '" + @IDCorretivoSelecionado + "', '" + @opcaoPerfil + "', '" + @opcaoCorretivo + "', '" + @opcaoFertilizante + "', '" + @Fertilizante + "', '" + @IDFertilizante + "') for xml path('')), 1, 1, '')");
                query.AppendLine("set @Total = stuff((select distinct 'ROUND(SUM([' + Fertilizante + ']),3) as [' + Fertilizante + '], ' from fGetRelatorioListParcialCorretivos('" + @IDSafra + "', '" + @IDPropriedade + "', '" + @IDArea + "', '" + @numServico + "', '" + @CorretivoSelecionado + "', '" + @IDCorretivoSelecionado + "', '" + @opcaoPerfil + "', '" + @opcaoCorretivo + "', '" + @opcaoFertilizante + "', '" + @Fertilizante + "', '" + @IDFertilizante + "') for xml path('')), 1, 1, '')");
                query.AppendLine("set @Comando = 'SELECT Safra, Area, Cultura, R'+@Total+' Ha FROM fGetRelatorioListParcialCorretivos(''" + @IDSafra + "'', ''" + @IDPropriedade + "'', ''" + @IDArea + "'',''" + @numServico + "'', ''" + @CorretivoSelecionado + "'', ''" + @IDCorretivoSelecionado + "'', ''" + @opcaoPerfil + "'', ''" + @opcaoCorretivo + "'' , ''" + @opcaoFertilizante + "'', ''" + @Fertilizante + "'', ''" + @IDFertilizante + "'')");
                query.AppendLine("PIVOT(MAX(qtde) FOR Fertilizante IN('+@teste+'))AS pvt group by Safra,Area,Ha, Cultura  FOR JSON PATH, ROOT(''Orders'')'");

            }
            query.AppendLine(" print @Comando ");
            query.AppendLine("execute(@Comando) ");
            return Context.Database.SqlQuery<string>(query.ToString()).ToList();
        }
        #region Recomendação alteração de corretivo. 
        public IEnumerable<CorretivoView> GetAllCorretivoSelectAlteracao(Guid? IDArea, Guid IDPropriedade, Guid IDSafra, int Type)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT                            ");
            query.AppendLine("      c.eficiencia               , ");
            query.AppendLine("      c.descricao  AS Corretivo  , ");
            query.AppendLine("      prnt                       , ");
            query.AppendLine("      perCaO                     , ");
            query.AppendLine("      perMgO                     , ");
            query.AppendLine("      perP2O5                    , ");
            query.AppendLine("      perK2O                     , ");
            query.AppendLine("      perCa                      , ");
            query.AppendLine("      perMg                      , ");
            query.AppendLine("      perS                         ");
            query.AppendLine(" FROM Corretivo c                  ");
            query.AppendLine(" INNER JOIN AreaServico asv ON c.IDAreaServico = asv.objID ");
            query.AppendLine(" INNER JOIN Area  a         ON a.objID = asv.IDArea        ");
            query.AppendLine(" INNER JOIN Safra s         ON s.objID = asv.IDSafra       ");
            query.AppendLine(" INNER JOIN Propriedade p   ON p.objID = a.IDPropriedade   ");

            query.AppendLine(" WHERE                                        ");
            query.AppendLine("      (a.objID IS NOT NULL) AND               ");
            query.AppendLine("      (c.opcaoMarcado = 1)  AND               ");
            query.AppendLine("      (c.marcado = 1)       AND               ");
            query.AppendLine("      (s.objID = '" + IDSafra + "') AND       ");
            query.AppendLine("      (p.objID = '" + IDPropriedade + "')     ");

            if (Type == 1)
            {
                query.AppendLine("      AND a.objID = '" + IDArea + "'      ");
                query.AppendLine(" GROUP BY C.descricao, eficiencia, prnt, perCaO, perMgO, perP2O5, perK2O, perCa, perMg, perS ORDER BY c.descricao");
            }
            else
                query.AppendLine(" GROUP BY C.descricao, eficiencia, prnt, perCaO, perMgO, perP2O5, perK2O, perCa, perMg, perS ORDER BY c.descricao");

            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        #endregion
        #region Recomendação tela de correção/ análise/ perfil/ ciclos.
        IEnumerable<Corretivo> ICorretivoRepository.GetListCorretivo(Guid? IDAreaServico, Guid? IDGrid, Guid? IDPropriedade, Guid? IDSafra, int opcao, int perfil, int corretivo)
        {
            StringBuilder query = new StringBuilder();
            if (IDGrid == null)
                query.AppendLine("SELECT * FROM fGetCorretivo('"+IDAreaServico+"', null ,"+opcao+","+perfil+")"); 
            else
                query.AppendLine("SELECT * FROM fGetCorretivo('" + IDAreaServico + "', '" + IDGrid + "'," + opcao + "," + perfil + ")");


            return Context.Database.SqlQuery<Corretivo>(query.ToString()).ToList();
        }
        public int GetCountRegistros(Guid objID, int Type)
        {
            // Area Servico.
            StringBuilder query = new StringBuilder();
            query.AppendLine("  SELECT COUNT(DISTINCT(opcao)) FROM Corretivo    ");
            query.AppendLine("  WHERE                                           ");

            // Essa condição será válida para retornar a quantidade por área serviço. 
            if (Type == 0)
                query.AppendLine("IDAreaServico = '" + objID + "' AND perfil = 0 ");

            // Essa condição será válida para retornar a quantidade por perfil. 
            if (Type == 1)
                query.AppendLine("IDAreaServico = '" + objID + "' AND perfil = 1 ");

            // Essa condição será válida para retornar a quantidade por Grid. 
            if (Type == 2)
                query.AppendLine("IDGrid = '" + objID + "' AND perfil = 0 ");

            return Context.Database.SqlQuery<int>(query.ToString()).SingleOrDefault();
        }
        public ResultadoCorrecao RetornaResultadoCorrecao(Guid IDAreaServico, Guid? IDGrid, String Profundidade, int unid, int tipo, int RetornoP, string Corretivo, int Opcao)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT* FROM fRetornaResultadoCorrecao('" + IDAreaServico + "',"+ (IDGrid == null ? "' '" : "'"+ IDGrid.ToString()+ "'")  +", '" + Profundidade + "' ," + unid + "," + tipo + "," + RetornoP + ","+ Corretivo +"," + Opcao + ")"); 
            return Context.Database.SqlQuery<ResultadoCorrecao>(query.ToString()).SingleOrDefault();
        }


        public IEnumerable<CorretivoView> UpdateListCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, String IDPropriedade, String IDSafra, double S, double Ca)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("UPDATE Corretivo set prnt = pr.prnt, perCaO = pr.cao, perMgO = pr.mgo, perP2O5 = pr.p2o5, perK2O = pr.k2o, perCa = pr.ca, perS = pr.s, descricao = pr.nome");
            query.AppendLine("FROM Corretivo c INNER JOIN AreaServico asv ON c.IDAreaServico = asv.objID INNER JOIN Area  a         ON a.objID = asv.IDArea INNER JOIN Safra s         ON s.objID = '" + IDSafra + "' INNER JOIN Propriedade p   ON p.objID = '" + IDPropriedade + "' ");
            var teste = "INNER JOIN Produto     pr  ON pr.tipo = 'CORRETIVO' AND pr.nome = '" + Corretivoalt + "' AND pr.prnt = " + prnt + " AND pr.cao = " + perCaO + " AND pr.mgo = " + Mgo + " AND pr.p2o5 = " + P2O5 + " AND  pr.k2o = " + K2O5 + " AND pr.ca = " + Ca + " AND pr.s = " + S + "";
            teste = teste.Replace(",", ".");
            query.AppendLine(teste);
            query.AppendLine("WHERE (a.objID IS NOT NULL) AND");
            query.AppendLine("(c.descricao = '" + Corretivoant + "') AND");
            query.AppendLine("(a.objID IS NOT NULL) AND");
            query.AppendLine("(s.objID = 'd2c499cd-e6e7-41d5-a8ea-0c7f254c83e7') AND");
            query.AppendLine("(p.objID = '248ed855-197f-45f3-ad60-8c7d8398ce70')");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        public bool UpdateCorretivoOpcaoMaracada(Guid objID, int opcao, int perfil, bool MarcarOrDesmarcar)
        {
            try
            {
                if (MarcarOrDesmarcar == true)
                {
                    if (perfil == 0)
                    {
                        string removemarcado = "UPDATE Corretivo " +
                                                                " SET opcaoMarcado = 0 " +
                                                                " WHERE IDGrid = '" + objID + "' AND opcaoMarcado = 1";
                        Context.Database.ExecuteSqlCommand(removemarcado);
                    }
                    else
                    {
                        string removemarcado = "UPDATE Corretivo " +
                                                  " SET opcaoMarcado = 0 " +
                                                  " WHERE IDAreaServico = '" + objID + "' AND opcaoMarcado = 1 AND perfil = 1 AND IDGrid IS NULL";
                        Context.Database.ExecuteSqlCommand(removemarcado);
                    }
                }
                if (perfil == 0)
                {
                    string update = "UPDATE Corretivo " +
                    (MarcarOrDesmarcar == true ? " SET opcaoMarcado = 1 " : " SET opcaoMarcado = 0 ") +
                    " WHERE IDGrid = '" + objID + "' AND opcao = " + opcao;
                    Context.Database.ExecuteSqlCommand(update);
                }
                else
                {
                    string update = "UPDATE Corretivo " +
                    (MarcarOrDesmarcar == true ? " SET opcaoMarcado = 1 " : " SET opcaoMarcado = 0 ") +
                    " WHERE IDAreaServico = '" + objID + "' AND opcao = " + opcao + " AND perfil = 1 AND IDGrid IS NULL ";
                    Context.Database.ExecuteSqlCommand(update);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateOpcaoMarcada(Guid objID, bool MarcarOrDesmarcar)
        {
            try
            {
                string update = "UPDATE Corretivo " +
                (MarcarOrDesmarcar == true ? " SET marcado = 1 " : " SET marcado = 0 ") +
                " WHERE objID = '" + objID + "'";
                Context.Database.ExecuteSqlCommand(update);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public CorretivoView GetOpcaoMarcadaByOpcao(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT TOP(1) opcao, opcaoMarcado  FROM Corretivo    ");
            if (retornoGridOrAreaServico == 0)
                query.AppendLine(" WHERE IDGrid = '" + objID + "' AND ");
            else
                query.AppendLine(" WHERE IDAreaServico = '" + objID + "' AND perfil = 1 AND IDGrid IS NULL AND");
            if (opcao != null)
                query.AppendLine(" opcao = " + opcao + " AND opcaoMarcado = 1 ");
            else
                query.AppendLine(" opcaoMarcado  = 1 ");

            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).FirstOrDefault();
        }
        public CorretivoView GetOpcaoValida(Guid objID, int? opcao, int opcaomarcada, int retornoGridOrAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT TOP(1) opcao, opcaoMarcado  FROM Corretivo");
            query.AppendLine("WHERE IDGrid = '"+objID+"' AND opcaoMarcado = 1");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).FirstOrDefault();
        }
        public IEnumerable<CorretivoView> GetListAlteracaoCorretivoNew(string IDAreaServico, string eficiencia, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string s, string perCa, string neweficiencia, string newCorretivo, string newprnt, string newpercao, string newpermgo, string newperp2o5, string newperk2o, string newpers, string newperca, string dosefixa)
        {
            return null;
        }
        public IEnumerable<CorretivoView> GetListCorretivo(Guid IDAreaServico, string corretivo, string prnt, string perCaO, string perMgO, string perP2O5, string perK2O, string S, string perCa)
        {
            prnt = prnt.Replace(',', '.');
            perCaO = perCaO.Replace(',', '.');
            perMgO = perMgO.Replace(',', '.');
            perP2O5 = perP2O5.Replace(',', '.');
            perK2O = perK2O.Replace(',', '.');
            S = S.Replace(',', '.');
            perCa = perCa.Replace(',', '.');


            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT                                                                       ");
            query.AppendLine("       Corretivo.objID        AS objID                                      , ");
            query.AppendLine("       Corretivo.IDGrid       AS IDGrid                                     , ");
            query.AppendLine("       Corretivo.descricao    AS Corretivo                                  , ");
            query.AppendLine("       Corretivo.qtde         AS qtde                                       , ");
            query.AppendLine("       Corretivo.eficiencia   AS eficiencia                                 , ");
            query.AppendLine("       Corretivo.prnt         AS prnt                                       , ");
            query.AppendLine("       Corretivo.perCaO       AS perCaO                                     , ");
            query.AppendLine("       Corretivo.perMgO       AS perMgO                                     , ");
            query.AppendLine("       Corretivo.perP2O5      AS perP2O5                                    , ");
            query.AppendLine("       Corretivo.perK2O       AS perK2O                                     , ");
            query.AppendLine("       Corretivo.perCa        AS perCa                                      , ");
            query.AppendLine("       Corretivo.perMg        AS perMg                                      , ");
            query.AppendLine("       Corretivo.perS         AS perS                                         ");
            query.AppendLine(" FROM Corretivo                                                               ");
            query.AppendLine(" INNER JOIN AreaServico ON AreaServico.objID = Corretivo.IDAreaServico        ");
            query.AppendLine(" INNER JOIN Area ON Area.objID = AreaServico.IDArea                           ");
            query.AppendLine(" INNER JOIN Propriedade ON Propriedade.objID = Area.IDPropriedade             ");
            query.AppendLine(" WHERE                                                                        ");
            query.AppendLine("      (Corretivo.IDAreaServico IS NOT NULL)                               AND ");
            query.AppendLine("      (Corretivo.IDAreaServico = '" + IDAreaServico + "')                 AND ");
            query.AppendLine("      (Corretivo.descricao ='" + corretivo + "')                          AND ");
            query.AppendLine("      (Corretivo.prnt      ='" + prnt + "')                               AND ");
            query.AppendLine("      (Corretivo.perCaO    ='" + perCaO + "')                             AND ");
            query.AppendLine("      (Corretivo.perMgO    ='" + perMgO + "')                             AND ");
            query.AppendLine("      (Corretivo.perP2O5   ='" + perP2O5 + "')                            AND ");
            query.AppendLine("      (Corretivo.perK2O    ='" + perK2O + "')                             AND ");
            query.AppendLine("      (Corretivo.perS      ='" + S + "')                                  AND ");
            query.AppendLine("      (Corretivo.perCa     ='" + perCa + "')                                  ");
            query.AppendLine(" ORDER BY Corretivo.descricao                                                 ");

            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        public CorretivoView MediaGridAlterado(Guid IDAreaServico, Guid IDGrid)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("  SELECT                            ");
            query.AppendLine("       IDGrid                     , ");
            query.AppendLine("       IDAreaServico              , ");
            query.AppendLine("       eficiencia                 , ");
            query.AppendLine("       perCaO                     , ");
            query.AppendLine("       perMgO                     , ");
            query.AppendLine("       perP2O5                    , ");
            query.AppendLine("       perK2O                     , ");
            query.AppendLine("       S                            ");
            query.AppendLine("  FROM fbngGetMediaGridAlterado('" + IDAreaServico + "', '" + IDGrid + "')");

            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).FirstOrDefault();
        }
        public int PostCorretivo(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, double S, double Ca)
        {
            int resultado = CheckProduto(prnt, perCaO, Mgo, P2O5, K2O5, Corretivoalt, Corretivoant, eficiencia, S, Ca);
            if (resultado == 0)
            {
                string row;
                row = "Insert into Produto(IDFornecedor, IDUnidadeMedida, nome, tipo, ativo, eficiencia, densidade, preco, prnt, cao, mgo, p2o5, k2o, s, n, ca, mg, b, zn, cu, mn, mo, co, fe, si, ni " +
                    "values(872C49F5-8A75-4A31-9D32-0312E50AA966; 5B6973A4-5C50-4023-9166-DEAC4F9C7DE8; '" + Corretivoalt + "'; 'CORRETIVO'; ''; '';''; 1; " + eficiencia + "; 100; 0; " + prnt + "; " + perCaO + ";" + Mgo + ";" + P2O5 + ";" + K2O5 + ";" + S + ";0;" + Ca + ";0;0;0;0;0;0;0;0;0;0";
                Context.Database.ExecuteSqlCommand(row);
                return 0;
            }
            else
            {
                return 1;
            }


        }
        public int CheckProduto(double prnt, double perCaO, double Mgo, double P2O5, double K2O5, String Corretivoalt, String Corretivoant, double eficiencia, double S, double Ca)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT COUNT(DISTINCT p.nome) FROM Produto as p where");
            var values = ("p.prnt = " + prnt + " and p.cao = " + perCaO + " and p.mgo = " + Mgo + " and p.p2o5 = " + P2O5 + " and p.k2o = " + K2O5 + " and p.nome = '" + Corretivoalt + "' and p.eficiencia = " + eficiencia + " and p.s = " + S + " and p.Ca = " + Ca + "");
            values = values.Replace(",", ".");
            values = values.Replace(";", ",");
            query.AppendLine(values);

            return Context.Database.SqlQuery<int>(query.ToString()).FirstOrDefault();
        }
        public IEnumerable<CorretivoView> GetListOpcao(Guid IDAreaServico)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" Select* from Corretivo where Corretivo.IDAreaServico = '" + IDAreaServico + "'  order by Corretivo.opcao");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();

        }
        public IEnumerable<CorretivoView> ReplicarCorretivo(IEnumerable<CorretivoView> obj)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" Select  Corretivo.descricao as Corretivo, Corretivo.* from Corretivo where Corretivo.IDAreaServico order by Corretivo.opcao");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        public List<CorretivoView> CorretivoFinal(Guid IDAreaServico, List<CorretivoView> Corretivo)
        {
            String colID = String.Empty;

            int ctList = Corretivo.Count();

            //for (int i = 0; i < ctList; i++)
            //{
            //    if (i < ctList - 1)
            //        colID += "'" + Corretivo[i].objID + "',";
            //    else
            //        colID += "'" + Corretivo[i].objID + "'";
            //}
            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT                                                                           ");
            query.AppendLine("           C.objID                                                              , ");
            query.AppendLine("           C.IDAreaServico                                                      , ");
            query.AppendLine("           C.IDGrid                                                             , ");
            query.AppendLine("           C.IDFornecedor                                                       , ");
            query.AppendLine("           C.descricao                                           AS Corretivo   , ");
            query.AppendLine("           C.qtde                                                               , ");
            query.AppendLine("           C.prnt                                                               , ");
            query.AppendLine("           C.eficiencia                                                         , ");
            query.AppendLine("           C.perCaO                                                             , ");
            query.AppendLine("           C.perMgO                                                             , ");
            query.AppendLine("           C.perP2O5                                                            , ");
            query.AppendLine("           C.perK2O                                                             , ");
            query.AppendLine("           C.perCa                                                              , ");
            query.AppendLine("           C.perMg                                                              , ");
            query.AppendLine("           C.perS                                                               , ");
            query.AppendLine("           C.S                                                                  , ");
            query.AppendLine("           C.Ca                                                                 , ");
            query.AppendLine("           C.Mg                                                                 , ");
            query.AppendLine("           C.K                                                                  , ");
            query.AppendLine("           C.P                                                                  , ");
            query.AppendLine("           C.b                                                                  , ");
            query.AppendLine("           C.zn                                                                 , ");
            query.AppendLine("           C.fe                                                                 , ");
            query.AppendLine("           C.mn                                                                 , ");
            query.AppendLine("           C.cu                                                                 , ");
            query.AppendLine("           C.co                                                                 , ");
            query.AppendLine("           C.momicro                                                              ");
            query.AppendLine(" FROM Corretivo  C                                                                ");
            query.AppendLine(" INNER JOIN AreaServico   ON AreaServico.objID    = C.IDAreaServico               ");
            query.AppendLine(" INNER JOIN Area          ON Area.objID           = AreaServico.IDArea            ");
            query.AppendLine(" INNER JOIN Propriedade   ON Propriedade.objID   = Area.IDPropriedade             ");
            query.AppendLine(" WHERE                                                                            ");
            query.AppendLine("       (C.IDAreaServico = '" + IDAreaServico + "' )                           AND ");
            query.AppendLine("       (C.opcaoMarcado  = 1)                                                  AND ");
            query.AppendLine("       (C.marcado       = 1)                                                  AND ");
            query.AppendLine("       (C.objID   NOT IN (" + colID + "))                                         ");
            query.AppendLine(" ORDER BY C.descricao                                                             ");
            return Context.Database.SqlQuery<CorretivoView>(query.ToString()).ToList();
        }
        public bool UpdateOptionChecked(Guid objID, int Opcao, bool MarcarOrDesmarcar, int type)
        {
            try
            {
                if(MarcarOrDesmarcar == true)
                {
                    // Essa condição vai remover todas as opções válidas. 
                    if (type == 0)
                    {
                        string updateMarcado = "Update Corretivo Set opcaoMarcado = 0 Where IDAreaServico = '" + objID + "'";
                        Context.Database.ExecuteSqlCommand(updateMarcado);
                    }
                    else
                    {
                        string updateMarcado = "Update Corretivo Set opcaoMarcado = 0 Where IDGrid = '" + objID + "'";
                        Context.Database.ExecuteSqlCommand(updateMarcado);
                    }
                  
                }

                string update = "UPDATE Corretivo " +
                (MarcarOrDesmarcar == true ? " SET opcaoMarcado = 1 " : " SET opcaoMarcado = 0 ") + (type == 0 ?
                    " WHERE IDAreaServico = '" + objID + "' and opcao = " + Opcao + " AND IDGrid IS NULL" :
                    " WHERE IDGrid = '" + objID + "' and opcao = " + Opcao + ""
                ); 
                
                Context.Database.ExecuteSqlCommand(update);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteAllCorretivoByOption(Guid objID, int Option, int Type)
        {
            if (Type == 0)
                return Context.Database.SqlQuery<bool>("EXEC delete_all_corretivo_by_areaservico_options '" + objID + "," + Option).FirstOrDefault();
            else
                return Context.Database.SqlQuery<bool>("EXEC delete_all_corretivo_by_grid_options '" + objID + "'," + Option).FirstOrDefault();

        }
        public IEnumerable<Options> GetListOpcaoCorretivo(Guid objID, int Type, int perfil)
        {
            if (Type == 0)
                return Context.Database.SqlQuery<Options>("EXEC get_list_options_by_areaservico '" + objID + "'").ToList();
            else
                return Context.Database.SqlQuery<Options>("EXEC get_list_options_by_grid '" + objID + "', "+ perfil).ToList(); 
        }
        public bool UpdateMarcado(Guid IDAreaServico, Guid IDGrid, int opcao, bool chk)
        {
            if (IDAreaServico != Guid.Empty &&   IDGrid == Guid.Empty)
                return Context.Database.SqlQuery<bool>("EXEC UpdateCorretivoMarcado '" +IDAreaServico+ "', NULL ,"+ opcao + "," + chk).FirstOrDefault();
            else if (IDAreaServico != Guid.Empty && IDGrid != Guid.Empty)
                return Context.Database.SqlQuery<bool>("EXEC UpdateCorretivoMarcado '" + IDAreaServico + "','" +IDGrid+ "'," + opcao + "," + chk).FirstOrDefault();
            else
                return Context.Database.SqlQuery<bool>("EXEC UpdateCorretivoMarcado NULL, NULL," + opcao + "," + chk).FirstOrDefault();
        }
        public CorretivoView GetMediaCorretivo(Guid IDAreaServico, Guid? IDGrid, string profundidade, int tipo, int opcao, int perfil)
        {
            if (IDGrid == null)
                return Context.Database.SqlQuery<CorretivoView>("SELECT * FROM fbngGetMediaCorretivo('" + IDAreaServico + "', null , '" + profundidade + "' , " + tipo + " , " + opcao + " , " + perfil + ")").FirstOrDefault();
            else
                return Context.Database.SqlQuery<CorretivoView>("SELECT * FROM fbngGetMediaCorretivo('" + IDAreaServico + "','" + IDGrid + "','" + profundidade + "' , " + tipo + " , " + opcao + " , " + perfil + ")").FirstOrDefault();
        }
        public bool SetDivideDose(SetDivideDoseCorretivo obj)
        {
            return Context.Database.SqlQuery<bool>("EXEC generatetodividedosecorretivo '" + obj.IDGrid + "','" + obj.IDAreaServico + "','" + obj.IDCorretivo + "','" + obj.NomeCorretivo + "','" + obj.NovoCorretivo + "'," + obj.opcao).FirstOrDefault();
        }

        public bool CheckedAnaliseSolo2040(Guid IDAreaServico)
        {
            return Context.Database.SqlQuery<bool>("EXEC CheckedExistedAnalise2040 '" + IDAreaServico + "'").FirstOrDefault(); 
        }
        #endregion
    }
}

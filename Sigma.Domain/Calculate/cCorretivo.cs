using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.Calculate
{
    public class cCorretivo
    {
        public static CorretivoView ReturnMediaAlteracao(List<CorretivoView> plstCorretivo)
        {
            CorretivoView oCorretivo = new CorretivoView();
            oCorretivo.perCaO = 0;
            oCorretivo.perMgO = 0;
            oCorretivo.perP2O5 = 0;
            oCorretivo.perK2O = 0;
            oCorretivo.perS = 0;
            for (int i = 0; i < plstCorretivo.Count; i++)
            {
                CorretivoView aux = plstCorretivo[i];
                if (aux.eficiencia == null)
                    aux.eficiencia = 100;
                else
                    oCorretivo.eficiencia = aux.eficiencia;
                oCorretivo.perCaO += (((((double.Parse(aux.KgHa.ToString()) / 10000.0) * (double.Parse(aux.prnt.ToString()) * (double.Parse(aux.eficiencia.ToString()) / 100.0)) * (double.Parse(aux.perCaO.ToString()))) / 1.3992) * 0.6833));
                oCorretivo.perMgO += ((((((double.Parse(aux.KgHa.ToString()) / 10000) * double.Parse(aux.prnt.ToString()) * (double.Parse(aux.eficiencia.ToString()) / 100)) * double.Parse(aux.perMgO.ToString())) / 1.6583) * 0.6833));
                oCorretivo.perP2O5 += ((((double.Parse(aux.KgHa.ToString()) / 100) * (double.Parse(aux.perP2O5.ToString()) * (double.Parse(aux.eficiencia.ToString()) / 100))) / 2.2914));
                oCorretivo.perK2O += (((double.Parse(aux.KgHa.ToString()) / 100) * (double.Parse(aux.perK2O.ToString()) * (double.Parse(aux.eficiencia.ToString()) / 100)) / 1.20461));
                oCorretivo.perS += (((double.Parse(aux.KgHa.ToString()) / 100) * (double.Parse(aux.perS.ToString()) * (double.Parse(aux.eficiencia.ToString()) / 100))));
            }
            oCorretivo.perCaO = Math.Round(Double.Parse(oCorretivo.perCaO.ToString()), 2);
            oCorretivo.perMgO = Math.Round(Double.Parse(oCorretivo.perMgO.ToString()), 2);
            oCorretivo.perP2O5 = Math.Round(Double.Parse(oCorretivo.perP2O5.ToString()), 2);
            oCorretivo.perK2O = Math.Round(Double.Parse(oCorretivo.perK2O.ToString()), 2);
            oCorretivo.perS = Math.Round(Double.Parse(oCorretivo.perS.ToString()), 2);
            return oCorretivo;
        }

        public static Double NivelP(MediaAnalise pobjMediaAnalise, CorretivoView pobjMediaCorretivo, bool tipoFosforo)
        {
            Double soma = 0;
            Double analiseP = 0;
            Double corretivoP = 0;
            Double P2O5 = 0;
            if (pobjMediaAnalise != null && pobjMediaCorretivo != null)
            {
                P2O5 = Double.Parse((pobjMediaCorretivo.perP2O5 * 2.2914).ToString());
                Double ef = 100;

                if (pobjMediaCorretivo.eficiencia != null)
                    ef = (int)pobjMediaCorretivo.eficiencia;

                P2O5 = P2O5 * (ef / 100);

                if (pobjMediaAnalise.PMeHl != 0 && !tipoFosforo)
                {
                    analiseP = Double.Parse(pobjMediaAnalise.PMeHl.ToString());
                    corretivoP = P2O5 / ((0.0242 * Math.Pow(Double.Parse(pobjMediaAnalise.Argila.ToString()), 2)) - (0.9298 * Double.Parse(pobjMediaAnalise.Argila.ToString())) + 14);
                }
                else
                {
                    analiseP = Double.Parse(pobjMediaAnalise.PRes.ToString());
                    corretivoP = P2O5 / ((0.0007 * Math.Pow(Double.Parse(pobjMediaAnalise.Argila.ToString()), 2)) + (0.1724 * Double.Parse(pobjMediaAnalise.Argila.ToString())) + 4.2052);
                }
            }
            soma = Math.Round(analiseP + corretivoP, 2);
            return soma;
        }

        public static Double[] ReturnNiveisBasesGrid(MediaAnalise oMediaAnalise, CorretivoView oMediaCorretivo)
        {
            Double[] niveisBase = new Double[5];
            Double mediaCaAnalise = Double.Parse(oMediaAnalise.Ca.ToString());
            Double mediaK2OAnalise = Double.Parse(oMediaAnalise.K.ToString());
            Double mediaMgAnalise = Double.Parse(oMediaAnalise.Mg.ToString());

            Double mediaCaCorretivo = 0;
            mediaCaCorretivo = (Double.Parse(oMediaCorretivo.perCaO.ToString())) / 400.78;

            Double Ca = Math.Round(mediaCaAnalise + mediaCaCorretivo, 2);
            niveisBase[0] = Ca;

            Double mediaMgCorretivo = 0;
            mediaMgCorretivo = (Double.Parse(oMediaCorretivo.perMgO.ToString())) / 243.06;

            Double Mg = Math.Round(mediaMgAnalise + mediaMgCorretivo, 2);
            niveisBase[1] = Mg;

            Double mediaK2OCorretivo = 0;
            mediaK2OCorretivo = (Double.Parse(oMediaCorretivo.perK2O.ToString())) / 2;

            Double K2O = Math.Round(mediaK2OAnalise + mediaK2OCorretivo, 2);
            niveisBase[2] = K2O;

            Double mediaSCorretivo = 0;
            mediaSCorretivo = (Double.Parse(oMediaCorretivo.S.ToString())) / 2 / 8;   //10;

            Double S = Math.Round(Double.Parse(oMediaAnalise.S.ToString()) + mediaSCorretivo, 2);
            niveisBase[4] = S;

            Double K = CalcularK(K2O, mediaK2OAnalise, Ca, Mg, 0, 0, 1);

            Double vFinal = 0;

            if (oMediaAnalise.CTC > 0)
            {
                vFinal = ((Ca + Mg + K) / Double.Parse(oMediaAnalise.CTC.ToString())) * 100;
            }
            niveisBase[3] = niveisBase[3] + Math.Round(vFinal, 2);
            return niveisBase;
        }

        public static Double CalcularK(Double KCorr, Double KAnalise, Double Ca, Double Mg, Double SAT, Double CTC, int tipo)
        {
            //KCorr = K corrigido (análise + correção) em mg.
            //KAnalise = K da análise em mg.
            //Ca e MG da análise em cmol
            //SAT = saturação desejada
            //CTC da análise em cmol.
            //tipo 1 retorna em cmol.

            Double KIdeal;
            if (tipo == 1)
            {
                //Método através do Ca e Mg
                KIdeal = Math.Round((((Ca + Mg) / 0.95) * 0.05) * 390.98, 4);
            }
            else
            {
                //Método através da saturação desejada
                KIdeal = Math.Round(((SAT / 100 * CTC) * 0.05) * 390.98, 4);
            }

            Double Kadic = 0;
            Double KFinal;

            if (KCorr >= KIdeal)
            {
                Kadic = KIdeal - KAnalise;
            }
            if (KCorr == KIdeal)
            {
                Kadic = KIdeal - KAnalise;
            }
            if (KCorr == KAnalise)
            {
                if (KCorr >= KIdeal)
                    Kadic = KIdeal - KAnalise;
                else
                    Kadic = 0;
            }
            if (KCorr < KIdeal)
            {
                Kadic = KCorr - KAnalise;
            }

            if (tipo != 1)
            {
                KFinal = Math.Round(Kadic + KAnalise, 2); // em mg/dm3
            }
            else
            {
                KFinal = Math.Round((Kadic + KAnalise) / 390.98, 2); // em cmol
            }
            return KFinal;
        }
    
        
    }
}

using System;
namespace Sigma.Domain.ViewTables
{
    public class FaixaTeorView
    {
        public FaixaTeorView()
        {
            objID = Guid.NewGuid();
        }

        public Guid objID { get; set; }
        public Guid IDEstagioCultura { get; set; }
        public Guid IDPartePlanta { get; set; }
        public string nutriente { get; set; }
        public Nullable<double> nivel1 { get; set; }
        public Nullable<double> nivel2 { get; set; }
        public Nullable<double> nivel3 { get; set; }
        public Nullable<double> nivel4 { get; set; }
        public string estagioCultura { get; set; }
        public string partePlanta { get; set; }
    }
}

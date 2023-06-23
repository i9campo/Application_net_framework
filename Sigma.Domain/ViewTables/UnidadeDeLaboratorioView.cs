using System;
namespace Sigma.Domain.ViewTables
{
    public class UnidadeDeLaboratorioView
    {
        public Guid objID { get; set; }
        public Guid IDLaboratorio { get; set; }
        public string laboratorio { get; set; }
        public Nullable<float> n { get; set; }
        public Nullable<float> p { get; set; }
        public Nullable<float> k { get; set; }
        public Nullable<float> s { get; set; }
        public Nullable<float> ca { get; set; }
        public Nullable<float> mg { get; set; }
        public Nullable<float> b { get; set; }
        public Nullable<float> zn { get; set; }
        public Nullable<float> cu { get; set; }
        public Nullable<float> co { get; set; }
        public Nullable<float> mo { get; set; }
        public Nullable<float> mn { get; set; }
        public Nullable<float> fe { get; set; }
    }
}

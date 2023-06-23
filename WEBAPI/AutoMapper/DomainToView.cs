using AutoMapper;
using Sigma.Domain.Entities;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.ViewTables;
using static Sigma.Domain.ViewTables.OpenGeo;

namespace WEBAPI.AutoMapper
{
    public class DomainToView : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomain"; }
        }

        public DomainToView()
        {
            CreateMap<UsuarioAtivoView, UsuarioAtivo>();
            CreateMap<CicloProducaoView, CicloProducao>();
            CreateMap<AnaliseSoloView, GeoOBJ>();
            CreateMap<AreaServicoView, GeoOBJ>(); 
        }



    }
}
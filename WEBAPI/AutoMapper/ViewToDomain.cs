using AutoMapper;
using Sigma.Domain.Entities;
using Sigma.Domain.IdentityEntities;
using Sigma.Domain.ViewTables;
using static Sigma.Domain.ViewTables.OpenGeo;

namespace WEBAPI.AutoMapper
{
    public class ViewToDomain : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModel"; }
        }

        public ViewToDomain()
        {
            CreateMap<UsuarioAtivo, UsuarioAtivoView>();
            CreateMap<CicloProducao, CicloProducaoView>();
            CreateMap<GeoOBJ, AnaliseSoloView>();
            CreateMap<GeoOBJ, AreaServicoView>(); 
        }
    }
}
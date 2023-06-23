using AutoMapper;

namespace WEBAPI.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToView>();
                x.AddProfile<ViewToDomain>();
            });
        }
    }
}
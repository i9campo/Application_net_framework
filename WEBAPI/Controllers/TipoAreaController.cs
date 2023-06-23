using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class TipoAreaController : ApiController
    {
    //    private readonly ITipoAreaAppService _tipoAreAppService;
    //    public TipoAreaController(ITipoAreaAppService tipoAreaService)
    //    {
    //        _tipoAreAppService = tipoAreaService;
    //    }


    //    // GET api/tipoarea
    //    public IEnumerable<TipoArea> Get()
    //    {
    //        List<TipoArea> lst = _tipoAreAppService.GetAll().ToList();
    //        return lst;
    //    }

    //    // GET api/tipoarea
    //    public TipoArea Get(string objID)
    //    {
    //        return _tipoAreAppService.Find(Guid.Parse(objID));
    //    }

    //    // POST api/tipoarea
    //    public ValidationResult Post([FromBody] TipoArea obj)
    //    {
    //        return _tipoAreAppService.Add(obj);
    //    }

    //    // PUT api/tipoarea/5
    //    public ValidationResult Put(string objID, [FromBody] TipoArea obj)
    //    {
    //        return _tipoAreAppService.Update(obj);
    //    }

    //    // DELETE api/tipoarea/5
    //    public ValidationResult Delete(string objID)
    //    {
    //        TipoArea obj = _tipoAreAppService.Find(Guid.Parse(objID));
    //        return _tipoAreAppService.Remove(obj);
    //    }
    }
}
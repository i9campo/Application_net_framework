using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.ViewTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI
{
    [AllowedOriginFilter]
    public class ProdutoController : ApiController
    {
        private IProdutoAppService _ProdutoAppService;
        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _ProdutoAppService = produtoAppService;
        }


        // GET api/produto
        public IEnumerable<ProdutoView> Get()
        {
            return _ProdutoAppService.GetAllProduto();
        }

        // GET api/produto
        public ProdutoView Get(Guid objID)
        {
            return _ProdutoAppService.FindProduto(objID);
        }

        // GET api/produto/getprodutobyname]
        [HttpGet]
        [ActionName("getprodutobyname")]
        [Route("api/produto/getprodutobyname")]
        public IEnumerable<Produto> GetProdutoByName(String Name)
        {
            return _ProdutoAppService.GetProdutoByName(Name);
        }

        //GET api/produto/getbytype
        [HttpGet]
        [ActionName("getbytype")]
        [Route("api/produto/getbytype")]
        public IEnumerable<ProdutoView> GetByType(string tipo)
        {
            return _ProdutoAppService.GetByType(tipo);
        }

        // POST api/produto
        public ValidationResult Post([FromBody] Produto obj)
        {
            var produto = _ProdutoAppService.GetProdutoByName(obj.nome).ToArray();

            bool verify = false;
            for (int i = 0; i < produto.Length; i++)
            {

                if (produto[i].nome == obj.nome && produto[i].IDFornecedor == obj.IDFornecedor && produto[i].IDUnidadeMedida == obj.IDUnidadeMedida
                    && produto[i].tipo == obj.tipo && produto[i].tipo == obj.tipo && produto[i].classe == obj.classe && produto[i].principioAtivo == obj.principioAtivo && produto[i].ativo == obj.ativo && produto[i].eficiencia == obj.eficiencia
                    && produto[i].densidade == obj.densidade && produto[i].preco == obj.preco && produto[i].prnt == obj.prnt && produto[i].p2o5 == obj.p2o5 &&
                    produto[i].cao == obj.cao && produto[i].mgo == obj.mgo && produto[i].k2o == obj.k2o && produto[i].s == obj.s &&
                    produto[i].n == obj.n && produto[i].ca == obj.ca && produto[i].mg == obj.mg && produto[i].b == obj.b &&
                    produto[i].zn == obj.zn && produto[i].cu == obj.cu && produto[i].mn == obj.mn && produto[i].mo == obj.mo &&
                    produto[i].mo == obj.mo && produto[i].co == obj.co && produto[i].fe == obj.fe && produto[i].si == obj.si &&
                    produto[i].ni == obj.ni)
                {
                    verify = true;
                }

            }

            if (verify == false)
            {
                if (obj.tipo.Equals("CORRETIVO"))
                {

                    return _ProdutoAppService.CadastroCorretivo(obj);
                }

                if (obj.tipo.Equals("FERTILIZANTE"))
                {

                    return _ProdutoAppService.CadastroFertilizante(obj);
                }

                return _ProdutoAppService.CadastroFoliar(obj);
            }
            return null;
        }

        // PUT api/produto/5
        public ValidationResult Put(string objID, [FromBody] Produto obj)
        {
            return _ProdutoAppService.Update(obj);
        }

        // DELETE api/produto/5
        public ValidationResult Delete(string objID)
        {
            Produto obj = _ProdutoAppService.Find(Guid.Parse(objID));

            return _ProdutoAppService.Remove(obj);
        }
    }
}
using FluentValidation.Results;
using Sigma.App.Interfaces;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WEBAPI.App_Start;

namespace WEBAPI.Controllers
{
    [AllowedOriginFilter]
    public class SequenciaImportacaoController : ApiController
    {
        private readonly ISequenciaImportacaoAppService _sequenciaImportacaoAppService;
        public SequenciaImportacaoController(ISequenciaImportacaoAppService sequenciaAppService)
        {
            _sequenciaImportacaoAppService = sequenciaAppService; 
        }

        // GET api/<controller>
        public IEnumerable<SequenciaImportacao> Get()
        {
            return _sequenciaImportacaoAppService.GetAll();
        }

        [HttpPost]
        [ActionName("OpenFileExcel")]
        [Route("api/sequenciaimportacao/OpenFileExcel")]
        public void OpenFileCSV(FileCSV arquivo)
        {
            //Byte[] csv = Convert.FromBase64String(arquivo.ArquivoB64);

            //string chave = Guid.NewGuid().ToString();
            //string caminhoTemporario = Path.GetTempPath();
            //string file_path = Path.Combine(caminhoTemporario, chave);
            //file_path = Path.ChangeExtension(file_path, arquivo.extension.ToLower());


            //using (var temp_file = File.Create(file_path))
            //{
            //    ExcelPackage.LicenseContext = LicenseContext.Commercial;

            //    using (var package = new ExcelPackage(new FileInfo(file_path)))
            //    {
            //        // Obter a planilha desejada (por índice ou por nome)
            //        var worksheet = package.Workbook.Worksheets["planilha1"]; // ou package.Workbook.Worksheets["Planilha1"]

            //        // Ler os valores da planilha
            //        var cellValue = worksheet.Cells["A1"].Value; // ou worksheet.Cells[1, 1].Value

            //        // Fazer algo com os valores lidos...
            //    }

            //    temp_file.Close();
            //    temp_file.Dispose(); 
            //}
            
   
        }





        [HttpGet]
        [ActionName("FindByLaboratorio")]
        [Route("api/sequenciaimportacao/FindByLaboratorio")]
        public SequenciaImportacao FindByLaboratorio(Guid IDLaboratorio)
        {
            return _sequenciaImportacaoAppService.FindSequenciaByLaboratorio(IDLaboratorio);
        }

        // POST api/<controller>
        public ValidationResult Post(SequenciaImportacao obj)
        {
            obj.objID = Guid.NewGuid(); 
            string stringLimitada = obj.NomeLaboratorio.Substring(0, Math.Min(25, obj.NomeLaboratorio.Length));
            obj.NomeLaboratorio = stringLimitada;
            obj.umpres = " ";
            obj.umphcacl2 = " "; 

            return _sequenciaImportacaoAppService.Add(obj);
        }

        // PUT api/<controller>/5
        public ValidationResult Put(Guid objID, [FromBody] SequenciaImportacao obj)
        {
            return _sequenciaImportacaoAppService.Update(obj); 
        }

        // DELETE api/<controller>/5
        public ValidationResult Delete(Guid objID)
        {
            SequenciaImportacao obj = _sequenciaImportacaoAppService.Find(objID);
            return _sequenciaImportacaoAppService.Remove(obj); 
        }
    }
}
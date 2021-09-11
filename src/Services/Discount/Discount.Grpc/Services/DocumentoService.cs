using Discount.Grpc.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DocumentoService : DocumentoProtoService.DocumentoProtoServiceBase
    {
        public DocumentoService()
        {
        }

        public override Task<DocumentoModel> ObterDocumento(ObterDocumentoRequest request, ServerCallContext context)
        {
            var documentos = new List<DocumentoModel>
            {
                new DocumentoModel
                {
                    Id = 1,
                    Nome = "RG",
                    OrgaoEmissor = "SSP/MS",
                    DataEmissao = "18/09/2013"
                }
            };
            var documento = documentos.FirstOrDefault(x => x.Nome == request.Nome);
            return Task.FromResult(documento ?? new DocumentoModel());
        }
    }
}

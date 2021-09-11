using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DocumentoGrpcService : IDocumentoGrpcService
    {
        private readonly DocumentoProtoService.DocumentoProtoServiceClient _client;

        public DocumentoGrpcService(DocumentoProtoService.DocumentoProtoServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<DocumentoModel> ObterDocumento(string nomeDocumento)
        {
            var obterDocumentoRequest = new ObterDocumentoRequest
            {
                Nome = nomeDocumento
            };
            var documento = await _client.ObterDocumentoAsync(obterDocumentoRequest);
            return documento;
        }
    }
}

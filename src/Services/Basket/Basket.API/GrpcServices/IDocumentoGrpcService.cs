using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public interface IDocumentoGrpcService
    {
        Task<DocumentoModel> ObterDocumento(string nomeDocumento);
    }
}

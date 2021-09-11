using Discount.Grpc.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class PessoaService : PessoaProtoService.PessoaProtoServiceBase
    {
        public override Task<ObterPessoaResponse> ObterPessoa(ObterPessoaRequest request, ServerCallContext context)
        {
            var pessoa = new ObterPessoaResponse
            {
                Nome = "Luzimar",
                Sobrenome = "Oliveira"
            };

            return Task.FromResult(pessoa);
        }
    }
}

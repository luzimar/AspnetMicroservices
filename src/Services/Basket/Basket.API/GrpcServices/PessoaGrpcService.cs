using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class PessoaGrpcService : IPessoaGrpcService
    {
        private readonly PessoaProtoService.PessoaProtoServiceClient _client;

        public PessoaGrpcService(PessoaProtoService.PessoaProtoServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ObterPessoaResponse> ObterPessoa(ObterPessoaRequest pessoa) => await _client.ObterPessoaAsync(pessoa);

    }
}

using Discount.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class CarroGrpcService : ICarroGrpcService
    {
        private readonly CarroProtoService.CarroProtoServiceClient _client;

        public CarroGrpcService(CarroProtoService.CarroProtoServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ObterCarroResponse> ObterCarro(ObterCarroRequest carro) => await _client.ObterCarroAsync(carro);
    }
}

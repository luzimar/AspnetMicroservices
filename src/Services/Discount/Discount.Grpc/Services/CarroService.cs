using Discount.Grpc.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class CarroService : CarroProtoService.CarroProtoServiceBase
    {
        public override Task<ObterCarroResponse> ObterCarro(ObterCarroRequest request, ServerCallContext context)
        {
            var carro = new ObterCarroResponse
            {
                Modelo = "Hatch",
                Marca = "Chevrolet",
                AnoFabricacao = 2021
            };

            return Task.FromResult(carro);
        }
    }
}

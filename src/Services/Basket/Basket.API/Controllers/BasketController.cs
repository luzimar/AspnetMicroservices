using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IDiscountGrpcService _discountGrpcService;
        private readonly IDocumentoGrpcService _documentoGrpcService;
        private readonly IPessoaGrpcService _pessoaGrpcService;
        private readonly ICarroGrpcService _carroGrpcService;

        public BasketController(
            IBasketRepository repository, 
            IDiscountGrpcService discountGrpcService, 
            IDocumentoGrpcService documentoGrpcService, 
            IPessoaGrpcService pessoaGrpcService, 
            ICarroGrpcService carroGrpcService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            _documentoGrpcService = documentoGrpcService ?? throw new ArgumentNullException(nameof(documentoGrpcService));
            _pessoaGrpcService = pessoaGrpcService ?? throw new ArgumentNullException(nameof(pessoaGrpcService));
            _carroGrpcService = carroGrpcService ?? throw new ArgumentNullException(nameof(carroGrpcService));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var documento = await _documentoGrpcService.ObterDocumento("RG");
            var pessoa = await _pessoaGrpcService.ObterPessoa(new ObterPessoaRequest { Nome = "teste" });
            var carro = await _carroGrpcService.ObterCarro(new ObterCarroRequest { Modelo = "teste" });

            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            await CheckAndApplyDiscount(basket);
            return Ok(await _repository.UpdateBasket(basket));
        }

        private async Task CheckAndApplyDiscount(ShoppingCart basket)
        {
            CouponModel coupon = null;
            foreach (var item in basket.Items)
            {
                coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                if (coupon != null)
                    item.Price -= coupon.Amount;
            }
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WoldBetting.Assessment.Models.Interfaces;
using WoldBetting.Assessment.Models;
using System.Xml.Linq;

namespace WorldBetting.Assessment.Web.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class CurrencyConverterController : ControllerBase
    {
        public readonly ICurrencyConverterAsync currencyService;

        public CurrencyConverterController(ICurrencyConverterAsync _currencyService ) {
            currencyService = _currencyService;
        }

        [HttpPost]
        [Route("convert")]
        public async Task<IActionResult> ConvertCurrency([FromBody] ConvertRequest request)
        {
            try
            {

                if (request.Amount <= 0)
                   return BadRequest("Amount must be greater than zero.");

                var exchangeRate = await currencyService.GetExchangeRateAsync(request.FromCurrency, request.ToCurrency);
                var convertedAmount = request.Amount * exchangeRate;

                var responseData = new ConvertResponse
                {
                    FromCurrency = request.FromCurrency,
                    ToCurrency = request.ToCurrency,
                    Amount = request.Amount,
                    ConvertedAmount = convertedAmount,
                    ExchangeRate = exchangeRate
                };

                BaseResponse response = new BaseResponse
                {
                    Data = responseData,
                    Message = "Currency Convertion Successfull.",
                    Success = true
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                BaseResponse error = new BaseResponse()
                {
                    Data = ex.Message ,
                    Success = false,
                    Message = "Currency Convertion Fails."
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);

            }
        }

    }
}

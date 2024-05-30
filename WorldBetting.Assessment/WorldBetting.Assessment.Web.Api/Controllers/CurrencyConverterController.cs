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
        public readonly ILogger<CurrencyConverterController> logger;

        public CurrencyConverterController(ICurrencyConverterAsync _currencyService, ILogger<CurrencyConverterController> _logger) {
            currencyService = _currencyService;
            logger = _logger;
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

                // save data in history table in DB 
                var savedToHistory = currencyService.SaveHistoryToDatabase(request.ToCurrency , exchangeRate);

                return Ok(response);
            }
            catch (Exception ex)
            {
                BaseResponse error = new BaseResponse()
                {
                    Data = ex.Message ,
                    Success = false,
                    Message = "Currency Convertion Call Fails."
                };
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, error);

            }
        }

    }
}

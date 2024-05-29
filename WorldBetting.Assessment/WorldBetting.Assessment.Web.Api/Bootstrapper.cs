using WoldBetting.Assessment.Models.Interfaces;
using WorldBetting.Assessment.Core;

namespace WorldBetting.Assessment.Web.Api
{
    static  class Bootstrapper
    {
        public static IServiceCollection AddCurerencyConverterService(this IServiceCollection services)
        {
            return services.AddTransient<ICurrencyConverterAsync , CurrencyConverterServiceAsync>();

        }
    }
}

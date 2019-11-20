﻿using EncoreTickets.SDK.Api;
using EncoreTickets.SDK.Api.Context;
using EncoreTickets.SDK.Api.Helpers;
using EncoreTickets.SDK.Api.Results.Response;
using EncoreTickets.SDK.Authentication;
using EncoreTickets.SDK.Pricing.Models;
using EncoreTickets.SDK.Pricing.Models.RequestModels;

namespace EncoreTickets.SDK.Pricing
{
    /// <inheritdoc cref="BaseApi" />
    /// <inheritdoc cref="IPricingServiceApi" />
    /// <summary>
    /// The service to provide an interface for calling Pricing API endpoints.
    /// </summary>
    public class PricingServiceApi : BaseApi, IPricingServiceApi
    {
        private const string PricingHost = "pricing-service.{0}tixuk.io/api/";
        private const string DateFormat = "yyyy-MM-ddTHH:mm:sszzz";

        /// <summary>
        /// Gets the authentication service for the current Pricing service./>
        /// </summary>
        public IAuthenticationService AuthenticationService { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="context"></param>
        public PricingServiceApi(ApiContext context) : base(context, PricingHost)
        {
            context.AuthenticationMethod = AuthenticationMethod.JWT;
            AuthenticationService = AuthenticationServiceFactory.Create(context, PricingHost, "login");
        }

        /// <inheritdoc />
        public ResponseForPage<ExchangeRate> GetExchangeRates(ExchangeRatesParameters parameters)
        {
            var result = Executor.ExecuteApiWithWrappedResponse<ResponseForPage<ExchangeRate>>(
                "v2/admin/exchange_rates",
                RequestMethod.Get,
                query: parameters,
                dateFormat: DateFormat);
            return result.DataOrException;
        }
    }
}

﻿namespace EncoreTickets.SDK.Checkout.Models.RequestModels
{
    internal class BookingQueryParameters : BookingCommonParameters
    {
        /// <summary>
        /// Gets or sets delivery method used in the booking.
        /// </summary>
        public DeliveryMethodForQuery? DeliveryMethod { get; set; }

        public BookingQueryParameters(BookingParameters parameters)
        {
            DeliveryMethod = GetDeliveryMethod(parameters.DeliveryMethod);
            Reference = parameters.Reference;
            ChannelId = parameters.ChannelId;
            Shopper = parameters.Shopper;
            BillingAddress = parameters.BillingAddress;
            Origin = parameters.Origin;
            RedirectUrl = parameters.RedirectUrl;
            DeliveryCharge = parameters.DeliveryCharge;
            RecipientName = parameters.RecipientName;
            GiftVoucherMessage = parameters.GiftVoucherMessage;
            DeliveryAddress = parameters.DeliveryAddress;
            HasFlexiTickets = parameters.HasFlexiTickets;
            PaymentType = parameters.PaymentType;
        }

        private DeliveryMethodForQuery? GetDeliveryMethod(DeliveryMethod method)
        {
            switch (method)
            {
                case RequestModels.DeliveryMethod.PrintAtHome:
                    return DeliveryMethodForQuery.E;
                case RequestModels.DeliveryMethod.PostalDelivery:
                    return DeliveryMethodForQuery.M;
                case RequestModels.DeliveryMethod.Collection:
                    return DeliveryMethodForQuery.C;
                default:
                    return null;
            }
        }
    }
}
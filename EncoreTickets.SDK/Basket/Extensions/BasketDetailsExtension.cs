﻿using System;
using System.Linq;
using EncoreTickets.SDK.Basket.Models;
using EncoreTickets.SDK.Inventory.Models;

namespace EncoreTickets.SDK.Basket.Extensions
{
    public static class BasketDetailsExtension
    {
        /// <summary>
        /// Determines whether the basket is expired.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static bool IsExpired(this BasketDetails basketDetails) 
            => basketDetails.expiredAt.UtcDateTime <= DateTime.UtcNow;

        /// <summary>
        /// Calculates the total number of items in all of the reservations in the basket.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static int ItemCount(this BasketDetails basketDetails)
            => basketDetails.reservations?.Sum(r => r.quantity) ?? 0;

        /// <summary>
        /// Determines whether the basket has any automatic promotions.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static bool HasAutomaticPromotion(this BasketDetails basketDetails)
            => basketDetails.coupon == null && basketDetails.HasPromotion();

        /// <summary>
        /// Determines whether the basket has any promotions.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static bool HasPromotion(this BasketDetails basketDetails) 
            => basketDetails.appliedPromotion != null;

        /// <summary>
        /// Gets total adjusted amount in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetBasketTotalInOfficeCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.adjustedSalePriceInOfficeCurrency);

        /// <summary>
        /// Gets total adjusted amount in shopper currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetBasketTotalInShopperCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.adjustedSalePriceInShopperCurrency);

        /// <summary>
        /// Gets total adjustment amount in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalDiscountInOfficeCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.adjustmentAmountInOfficeCurrency);

        /// <summary>
        /// Gets total adjustment amount in shopper currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalDiscountInShopperCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.adjustmentAmountInShopperCurrency);

        /// <summary>
        /// Gets total face value in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalFaceValueInOfficeCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.faceValueInOfficeCurrency);

        /// <summary>
        /// Gets total face value in shopper currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalFaceValueInShopperCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.faceValueInShopperCurrency);

        /// <summary>
        /// Gets total sale price without any adjustments in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalSalePriceInOfficeCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.salePriceInOfficeCurrency);

        /// <summary>
        /// Gets total sale price without any adjustments in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalSalePriceInShopperCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.salePriceInShopperCurrency);

        /// <summary>
        /// Gets basket total without delivery.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetBasketTotalWithoutDelivery(this BasketDetails basketDetails)
        {
            var total = basketDetails.GetBasketTotalInOfficeCurrency();
            return total != null && basketDetails.delivery?.charge != null
                ? new Price
                {
                    value = total.value - basketDetails.delivery.charge.value,
                    currency = total.currency,
                    decimalPlaces = total.decimalPlaces
                }
                : total;
        }

        private static Price GetTotalFromAllReservations(this BasketDetails basketDetails, Func<Reservation, Price> priceFunc)
        {
            return basketDetails.reservations?.Count > 0 ?
                new Price
                {
                    value = basketDetails.reservations.Sum(r => (priceFunc(r).value ?? 0) * r.quantity),
                    currency = priceFunc(basketDetails.reservations[0]).currency,
                    decimalPlaces = priceFunc(basketDetails.reservations[0]).decimalPlaces
                }
                : null;
        }
    }
}

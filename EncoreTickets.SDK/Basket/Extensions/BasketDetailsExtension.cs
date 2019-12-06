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
            => basketDetails.GetTotalFromAllReservations(r => r.GetTotalAdjustedAmountInOfficeCurrency());

        /// <summary>
        /// Gets total adjusted amount in shopper currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetBasketTotalInShopperCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.GetTotalAdjustedAmountInShopperCurrency());

        /// <summary>
        /// Gets total adjustment amount in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalDiscountInOfficeCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.GetTotalAdjustmentAmountInOfficeCurrency());

        /// <summary>
        /// Gets total adjustment amount in shopper currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalDiscountInShopperCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.GetTotalAdjustmentAmountInShopperCurrency());

        /// <summary>
        /// Gets total face value in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalFaceValueInOfficeCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.GetTotalFaceValueInOfficeCurrency());

        /// <summary>
        /// Gets total face value in shopper currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalFaceValueInShopperCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.GetTotalFaceValueInShopperCurrency());

        /// <summary>
        /// Gets total sale price without any adjustments in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalSalePriceInOfficeCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.GetTotalSalePriceInOfficeCurrency());

        /// <summary>
        /// Gets total sale price without any adjustments in office currency.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetTotalSalePriceInShopperCurrency(this BasketDetails basketDetails)
            => basketDetails.GetTotalFromAllReservations(r => r.GetTotalSalePriceInShopperCurrency());

        /// <summary>
        /// Gets basket total without delivery.
        /// </summary>
        /// <param name="basketDetails"></param>
        /// <returns></returns>
        public static Price GetBasketTotalWithoutDelivery(this BasketDetails basketDetails)
        {
            var total = basketDetails.GetBasketTotalInOfficeCurrency();
            return total != null && basketDetails.delivery?.charge != null
                ? total - basketDetails.delivery.charge
                : total;
        }

        private static Price GetTotalFromAllReservations(this BasketDetails basketDetails, Func<Reservation, Price> priceFunc) 
             => basketDetails.reservations?.Count > 0 ? 
                basketDetails.reservations.Select(priceFunc).Aggregate((x, y) => x + y) 
                : null;
    }
}
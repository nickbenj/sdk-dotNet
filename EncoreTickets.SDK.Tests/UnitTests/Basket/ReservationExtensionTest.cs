﻿using System;
using System.Collections.Generic;
using EncoreTickets.SDK.Basket.Extensions;
using EncoreTickets.SDK.Basket.Models;
using EncoreTickets.SDK.Tests.Helpers;
using EncoreTickets.SDK.Utilities.CommonModels.Extensions;
using NUnit.Framework;

namespace EncoreTickets.SDK.Tests.UnitTests.Basket
{
    class ReservationExtensionTest
    {
        private static readonly Price DefaultPrice = new Price
        {
            Currency = "GBP",
            DecimalPlaces = 2,
            Value = 2500
        };

        private static readonly int DefaultQuantity = 2;

        [Test]
        public void Reservation_TotalAdjustedAmountInOfficeCurrency_Correct()
        {
            var reservation = new Reservation { AdjustedSalePriceInOfficeCurrency = DefaultPrice, Quantity = DefaultQuantity };

            var result = reservation.GetTotalAdjustedAmountInOfficeCurrency();

            AssertExtension.AreObjectsValuesEqual(DefaultPrice.MultiplyByNumber(DefaultQuantity), result);
        }

        [Test]
        public void Reservation_TotalAdjustedAmountInShopperCurrency_Correct()
        {
            var reservation = new Reservation { AdjustedSalePriceInShopperCurrency = DefaultPrice, Quantity = DefaultQuantity };

            var result = reservation.GetTotalAdjustedAmountInShopperCurrency();

            AssertExtension.AreObjectsValuesEqual(DefaultPrice.MultiplyByNumber(DefaultQuantity), result);
        }

        [Test]
        public void Reservation_TotalAdjustmentInOfficeCurrency_Correct()
        {
            var reservation = new Reservation { AdjustmentAmountInOfficeCurrency = DefaultPrice, Quantity = DefaultQuantity };

            var result = reservation.GetTotalAdjustmentAmountInOfficeCurrency();

            AssertExtension.AreObjectsValuesEqual(DefaultPrice.MultiplyByNumber(DefaultQuantity), result);
        }

        [Test]
        public void Reservation_TotalAdjustmentInShopperCurrency_Correct()
        {
            var reservation = new Reservation { AdjustmentAmountInShopperCurrency = DefaultPrice, Quantity = DefaultQuantity };

            var result = reservation.GetTotalAdjustmentAmountInShopperCurrency();

            AssertExtension.AreObjectsValuesEqual(DefaultPrice.MultiplyByNumber(DefaultQuantity), result);
        }

        [Test]
        public void Reservation_TotalSalePriceInOfficeCurrency_Correct()
        {
            var reservation = new Reservation { SalePriceInOfficeCurrency = DefaultPrice, Quantity = DefaultQuantity };

            var result = reservation.GetTotalSalePriceInOfficeCurrency();

            AssertExtension.AreObjectsValuesEqual(DefaultPrice.MultiplyByNumber(DefaultQuantity), result);
        }

        [Test]
        public void Reservation_TotalSalePriceInShopperCurrency_Correct()
        {
            var reservation = new Reservation { SalePriceInShopperCurrency = DefaultPrice, Quantity = DefaultQuantity };

            var result = reservation.GetTotalSalePriceInShopperCurrency();

            AssertExtension.AreObjectsValuesEqual(DefaultPrice.MultiplyByNumber(DefaultQuantity), result);
        }

        [Test]
        public void Reservation_TotalFaceInOfficeCurrency_Correct()
        {
            var reservation = new Reservation { FaceValueInOfficeCurrency = DefaultPrice, Quantity = DefaultQuantity };

            var result = reservation.GetTotalFaceValueInOfficeCurrency();

            AssertExtension.AreObjectsValuesEqual(DefaultPrice.MultiplyByNumber(DefaultQuantity), result);
        }

        [Test]
        public void Reservation_TotalFaceInShopperCurrency_Correct()
        {
            var reservation = new Reservation { FaceValueInShopperCurrency = DefaultPrice, Quantity = DefaultQuantity };

            var result = reservation.GetTotalFaceValueInShopperCurrency();

            AssertExtension.AreObjectsValuesEqual(DefaultPrice.MultiplyByNumber(DefaultQuantity), result);
        }

        [Test]
        public void Reservation_ToReservationRequest_Correct()
        {
            var reservation = new Reservation
            {
                Date = DateTimeOffset.Now,
                ProductId = "1234",
                VenueId = "123",
                Items = new List<Seat> { new Seat { AggregateReference = "reference1" }, new Seat { AggregateReference = "reference2" } },
                Quantity = 2
            };

            var result = reservation.ConvertToReservationRequest();

            result.ShouldBeEquivalentToObjectWithMoreProperties(reservation);
        }
    }
}

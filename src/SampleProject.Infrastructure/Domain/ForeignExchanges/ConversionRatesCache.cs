﻿using System.Collections.Generic;
using SampleProject.Domain.ForeignExchange;

namespace SampleProject.Infrastructure.Domain.ForeignExchanges
{
    public class ConversionRatesCache
    {

        public List<ConversionRate> Rates { get; set; }

        public ConversionRatesCache(List<ConversionRate> rates)
        {
            Rates = rates;
        }


        //public List<ConversionRate> Rates { get; }

        //public ConversionRatesCache(List<ConversionRate> rates)
        //{
        //    this.Rates = rates;
        //}
    }
}
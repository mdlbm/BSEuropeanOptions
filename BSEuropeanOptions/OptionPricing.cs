using MathNet.Numerics.Distributions;
using System;

namespace OptionPricingApp
{
    public static class OptionPricing
    {
        public static double CalculateCallOptionPrice(
            double stockPrice, double strikePrice, double interestRate,
            double dividendYield, double volatility, double timeToMaturity)
        {
            double d1 = (Math.Log(stockPrice / strikePrice) + (interestRate - dividendYield + 0.5 * Math.Pow(volatility, 2)) * timeToMaturity)
                       / (volatility * Math.Sqrt(timeToMaturity));

            double d2 = d1 - volatility * Math.Sqrt(timeToMaturity);

            double callOptionPrice = stockPrice * Math.Exp(-dividendYield * timeToMaturity) * Normal.CDF(0, 1, d1)
                                  - strikePrice * Math.Exp(-interestRate * timeToMaturity) * Normal.CDF(0, 1, d2);

            return callOptionPrice;
        }

        public static double CalculatePutOptionPrice(
            double stockPrice, double strikePrice, double interestRate,
            double dividendYield, double volatility, double timeToMaturity)
        {
            double d1 = (Math.Log(stockPrice / strikePrice) + (interestRate - dividendYield + 0.5 * Math.Pow(volatility, 2)) * timeToMaturity)
                       / (volatility * Math.Sqrt(timeToMaturity));

            double d2 = d1 - volatility * Math.Sqrt(timeToMaturity);

            double putOptionPrice = strikePrice * Math.Exp(-interestRate * timeToMaturity) * Normal.CDF(0, 1, -d2)
                                 - stockPrice * Math.Exp(-dividendYield * timeToMaturity) * Normal.CDF(0, 1, -d1);

            return putOptionPrice;
        }
    }
}

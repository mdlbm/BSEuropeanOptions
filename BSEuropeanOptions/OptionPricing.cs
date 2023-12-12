using MathNet.Numerics.Distributions; //Package to get mathematical functions like normal distribution
using System;

namespace OptionPricingApp
{
    public static class OptionPricing //Class for pricing Call and Put options using Black Scholes formula
    {
        public static double CalculateCallOptionPrice( //Call pricing
            double stockPrice, double strikePrice, double interestRate,
            double dividendYield, double volatility, double timeToMaturity)
        {   //Compute the d1, d2
            double d1 = (Math.Log(stockPrice / strikePrice) + (interestRate - dividendYield + 0.5 * Math.Pow(volatility, 2)) * timeToMaturity)
                       / (volatility * Math.Sqrt(timeToMaturity));

            double d2 = d1 - volatility * Math.Sqrt(timeToMaturity);
            // Blacks Scholes formula
            double callOptionPrice = stockPrice * Math.Exp(-dividendYield * timeToMaturity) * Normal.CDF(0, 1, d1)
                                  - strikePrice * Math.Exp(-interestRate * timeToMaturity) * Normal.CDF(0, 1, d2);

            return callOptionPrice;
        }

        public static double CalculatePutOptionPrice(//Put pricing
            double stockPrice, double strikePrice, double interestRate,
            double dividendYield, double volatility, double timeToMaturity)
        {   //Compute the d1, d2
            double d1 = (Math.Log(stockPrice / strikePrice) + (interestRate - dividendYield + 0.5 * Math.Pow(volatility, 2)) * timeToMaturity)
                       / (volatility * Math.Sqrt(timeToMaturity));

            double d2 = d1 - volatility * Math.Sqrt(timeToMaturity);
            // Blacks Scholes formula
            double putOptionPrice = strikePrice * Math.Exp(-interestRate * timeToMaturity) * Normal.CDF(0, 1, -d2)
                                 - stockPrice * Math.Exp(-dividendYield * timeToMaturity) * Normal.CDF(0, 1, -d1);

            return putOptionPrice;
        }
    }
}


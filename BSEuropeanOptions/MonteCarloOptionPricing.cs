using System;

namespace OptionPricingApp
{
    public class MonteCarloOptionPricing
    {
        public static double CalculateOptionPrice(double sigma, double r, double K, double S0, double div, double T, int n, int numSimulations, bool isCall)
        {
            Random rand = new Random();
            double dt = T / n;
            double optionPriceSum = 0.0;
            // We compute the option price for each simulation
            for (int simulation = 0; simulation < numSimulations; simulation++)
            {
                double currentPrice = S0;
                // We simulate a path of the stock price
                for (int step = 0; step < n; step++)
                {
                    double z = rand.NextDouble(); //Generate a uniform variable
                    double randonNum = Math.Sqrt(-2.0 * Math.Log(z)) * Math.Cos(2.0 * Math.PI * rand.NextDouble()); // Box Muller transformation to get a normally distributed variable
                    currentPrice *= Math.Exp((r - div - 0.5 * sigma * sigma) * dt + sigma * Math.Sqrt(dt) * randonNum); //The stock "follows" a Brownian Geometric Motion
                }

                double optionPayoff = isCall ? Math.Max(currentPrice - K, 0) : Math.Max(K - currentPrice, 0);
                optionPriceSum += optionPayoff;
            }

            // Calculate the average option payoff and discount it to present value
            double averageOptionPayoff = optionPriceSum / numSimulations;
            double optionPrice = Math.Exp(-r * T) * averageOptionPayoff;

            return optionPrice;
        }
    }
}


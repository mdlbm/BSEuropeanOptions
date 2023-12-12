namespace OptionPricingApp
{
    public class OptionPricingTree
    {
        public static double CallOrPutEurTree(double sigma, double r, double K, double S0, double div, double T, int n, bool isCall)
        {
            // Parameters for binomial tree
            double dt = T / n;                                      //Time difference between two steps 
            double u = Math.Exp(sigma * Math.Sqrt(dt));             //Coefficient of increase 
            double d = 1 / u;                                       //Coefficient of decrease 
            double p = (Math.Exp((r - div) * dt) - d) / (u - d);    //Compute risk neutral probability
            double q = 1 - p;

            // Create stock tree
            double[,] sMat = new double[n + 1, n + 1];
            sMat[0, 0] = S0;

            for (int j = 1; j <= n; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    sMat[i, j] = sMat[i, j - 1] * u;
                }
                sMat[j, j] = sMat[j - 1, j - 1] * d;
            }

            // Create a matrix of option values; insert terminal values
            //Easy for-loop that values option as call or put depending on the value of isCall parameter
            double[,] opMat = new double[n + 1, n + 1];
            for (int i = 0; i <= n; i++)
            {
                if (isCall)
                {
                    opMat[i, n] = Math.Max(sMat[i, n] - K, 0); // Call option payoff
                }
                else
                {
                    opMat[i, n] = Math.Max(K - sMat[i, n], 0); // Put option payoff
                }
            }

            // Value call by working backward from time n-1 to time 0
            //So goes backwards through the matrix we defined earlier
            for (int j = n - 1; j >= 0; j--)
            {
                for (int i = 0; i <= j; i++)
                {
                    opMat[i, j] = Math.Exp(-r * dt) * (p * opMat[i, j + 1] + q * opMat[i + 1, j + 1]);
                }
            }

            // Return the option value at the first node
            return opMat[0, 0];
        }
    }
}

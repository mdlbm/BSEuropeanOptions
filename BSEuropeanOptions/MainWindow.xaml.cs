using System;
using System.Windows;

namespace OptionPricingApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double stockPrice = ValidatePositiveInput(txtStockPrice.Text, "Stock Price");
                double strikePrice = ValidatePositiveInput(txtStrikePrice.Text, "Strike Price");
                double interestRate = ValidatePositiveInput(txtInterestRate.Text, "Interest Rate") / 100.0;
                double dividendYield = ValidatePositiveInput(txtDividendYield.Text, "Dividend Yield") / 100.0;
                double volatility = ValidatePositiveInput(txtVolatility.Text, "Volatility") / 100.0;
                double timeToMaturity = ValidatePositiveInput(txtTimeToMaturity.Text, "Time to Maturity");
                int nbsteps = (int)ValidatePositiveInput(txtnbsteps.Text, "Number of Steps");
                int nbsim = (int)ValidatePositiveInput(txtnbsim.Text, "Number of Simulations");

                double optionPrice = 0.0;

                if (rbCall.IsChecked == true)
                {
                    if (rbBS.IsChecked == true)
                    {
                        optionPrice = OptionPricing.CalculateCallOptionPrice(
                            stockPrice, strikePrice, interestRate, dividendYield, volatility, timeToMaturity);
                    }
                    else if (rbTree.IsChecked == true)
                    {
                        optionPrice = OptionPricingTree.CallOrPutEurTree(volatility, interestRate, strikePrice, stockPrice, dividendYield, timeToMaturity, nbsteps, true);
                    }
                    else if (rbMonteCarlo.IsChecked == true)
                    {
                        optionPrice = MonteCarloOptionPricing.CalculateOptionPrice(volatility, interestRate, strikePrice, stockPrice, dividendYield, timeToMaturity, nbsteps, nbsim, true);
                    }
                }
                else if (rbPut.IsChecked == true)
                {
                    if (rbBS.IsChecked == true)
                    {
                        optionPrice = OptionPricing.CalculatePutOptionPrice(
                            stockPrice, strikePrice, interestRate, dividendYield, volatility, timeToMaturity);
                    }
                    else if (rbTree.IsChecked == true)
                    {
                        optionPrice = OptionPricingTree.CallOrPutEurTree(volatility, interestRate, strikePrice, stockPrice, dividendYield, timeToMaturity, nbsteps, false);
                    }
                    else if (rbMonteCarlo.IsChecked == true)
                    {
                        optionPrice = MonteCarloOptionPricing.CalculateOptionPrice(volatility, interestRate, strikePrice, stockPrice, dividendYield, timeToMaturity, nbsteps, nbsim, false);
                    }
                }

                txtResult.Text = $"Option Price: {optionPrice:C2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double ValidatePositiveInput(string input, string fieldName)// Function to check if the input is a positive number.
        {
            if (!double.TryParse(input, out double value) || value <= 0)
            {
                throw new ArgumentException($"{fieldName} must be a positive number. Decimal separator is a comma (,). Please check your input.");
            }

            return value;
        }
    }
}

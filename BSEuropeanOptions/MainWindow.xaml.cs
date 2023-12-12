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
            {   //Retrieve input and convert it to numeric values (double and int)
                double stockPrice = ValidatePositiveInput(txtStockPrice.Text, "Stock Price");
                double strikePrice = ValidatePositiveInput(txtStrikePrice.Text, "Strike Price");
                double interestRate = ValidatePositiveInput(txtInterestRate.Text, "Interest Rate") / 100.0;     //Percentage
                double dividendYield = ValidatePositiveInput(txtDividendYield.Text, "Dividend Yield") / 100.0;  //Percentage
                double volatility = ValidatePositiveInput(txtVolatility.Text, "Volatility") / 100.0;            //Percentage
                double timeToMaturity = ValidatePositiveInput(txtTimeToMaturity.Text, "Time to Maturity");
                int nbsteps = (int)ValidatePositiveInput(txtnbsteps.Text, "Number of Steps");
                int nbsim = (int)ValidatePositiveInput(txtnbsim.Text, "Number of Simulations");

                double optionPrice = 0.0;
                // Radio button : Call button selected 
                if (rbCall.IsChecked == true)
                {
                    // Radio button : Black-Scholes button selected
                    if (rbBS.IsChecked == true)
                    {
                        //Call method to OptionPricing class (BS method) called
                        optionPrice = OptionPricing.CalculateCallOptionPrice(
                            stockPrice, strikePrice, interestRate, dividendYield, volatility, timeToMaturity); 
                    }
                    // Radio button : Tree button selected
                    else if (rbTree.IsChecked == true)
                    {
                        //Pricing method to OptionPricingTree class, with True Argument for IsCall
                        optionPrice = OptionPricingTree.CallOrPutEurTree(volatility, interestRate, strikePrice, stockPrice, dividendYield, timeToMaturity, nbsteps, true);
                    }
                    // Radio button : Monte Carlo button selected
                    else if (rbMonteCarlo.IsChecked == true)
                    {
                        //Pricing method to MonteCarloOptionPricing class, with True Argument for IsCall
                        optionPrice = MonteCarloOptionPricing.CalculateOptionPrice(volatility, interestRate, strikePrice, stockPrice, dividendYield, timeToMaturity, nbsteps, nbsim, true);
                    }
                }
                // Radio button : Call button selected 
                else if (rbPut.IsChecked == true)
                {
                    // Radio button : Black-Scholes button selected
                    if (rbBS.IsChecked == true)
                    {
                        //Put method to OptionPricing class (BS method) called
                        optionPrice = OptionPricing.CalculatePutOptionPrice(
                            stockPrice, strikePrice, interestRate, dividendYield, volatility, timeToMaturity);
                    }
                    // Radio button : Tree button selected
                    else if (rbTree.IsChecked == true)
                    {
                        //Pricing method to OptionPricingTree class, with False Argument for IsCall
                        optionPrice = OptionPricingTree.CallOrPutEurTree(volatility, interestRate, strikePrice, stockPrice, dividendYield, timeToMaturity, nbsteps, false);
                    }
                    // Radio button : Monte Carlo button selected
                    else if (rbMonteCarlo.IsChecked == true)
                    {
                        //Pricing method to MonteCarloOptionPricing class, with False Argument for IsCall
                        optionPrice = MonteCarloOptionPricing.CalculateOptionPrice(volatility, interestRate, strikePrice, stockPrice, dividendYield, timeToMaturity, nbsteps, nbsim, false);
                    }
                }
                // Display the 
                txtResult.Text = $"Option Price: {optionPrice:C2}";
            }
            catch (Exception ex) // Error message in case of errorf (might be override by ValidatePositiveInput function for input error)
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

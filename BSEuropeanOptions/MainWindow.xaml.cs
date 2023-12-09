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
                double stockPrice = Convert.ToDouble(txtStockPrice.Text);
                double strikePrice = Convert.ToDouble(txtStrikePrice.Text);
                double interestRate = Convert.ToDouble(txtInterestRate.Text) / 100.0;
                double dividendYield = Convert.ToDouble(txtDividendYield.Text) / 100.0;
                double volatility = Convert.ToDouble(txtVolatility.Text) / 100.0;
                double timeToMaturity = Convert.ToDouble(txtTimeToMaturity.Text);
                int nbsteps = (int)Convert.ToInt64(txtnbsteps.Text);
                int nbsim = (int)Convert.ToInt64(txtnbsim.Text);


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
    }
}

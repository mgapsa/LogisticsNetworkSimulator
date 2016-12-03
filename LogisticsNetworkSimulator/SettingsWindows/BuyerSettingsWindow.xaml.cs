using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogisticsNetworkSimulator.SettingsWindows
{
    /// <summary>
    /// Interaction logic for BuyerSettingsWindow.xaml
    /// </summary>
    public partial class BuyerSettingsWindow : Window
    {
        public BuyerSettingsWindow(Buyer model)
        {
            InitializeComponent();
            this.DataContext = model;
            switch(model.OptionA)
            {
                case EnumTypes.BuyerAOptions.Static:
                    this.Static.IsChecked = true;
                    break;
                case EnumTypes.BuyerAOptions.Random:
                    this.Random.IsChecked = true;
                    break;
                case EnumTypes.BuyerAOptions.Poisson:
                    this.Poisson.IsChecked = true;
                    break;
                case EnumTypes.BuyerAOptions.Gauss:
                    this.Gauss.IsChecked = true;
                    break;
            }
            switch(model.OptionB)
            {
                case EnumTypes.BuyerBOptions.Poisson:
                    break;
                case EnumTypes.BuyerBOptions.Gauss:
                    this.GaussOptionB.IsChecked = true;
                    break;
                case EnumTypes.BuyerBOptions.Static:
                    this.StaticOptionB.IsChecked = true;
                    break;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Buyer model = this.DataContext as Buyer;
            if(this.Static.IsChecked == true)
            {
                model.OptionA = EnumTypes.BuyerAOptions.Static;
            }
            else if (this.Random.IsChecked == true)
            {
                model.OptionA = EnumTypes.BuyerAOptions.Random;
            }
            else if (this.Poisson.IsChecked == true)
            {
                model.OptionA = EnumTypes.BuyerAOptions.Poisson;
            }
            else if (this.Gauss.IsChecked == true)
            {
                model.OptionA = EnumTypes.BuyerAOptions.Gauss;
            }

            if (this.GaussOptionB.IsChecked == true)
            {
                model.OptionB = EnumTypes.BuyerBOptions.Gauss;
            }
            else if (this.StaticOptionB.IsChecked == true)
            {
                model.OptionB = EnumTypes.BuyerBOptions.Static;
            }

            DialogResult = true;
            this.Close();
        }
    }
}

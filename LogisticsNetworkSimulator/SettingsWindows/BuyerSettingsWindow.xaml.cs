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
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}

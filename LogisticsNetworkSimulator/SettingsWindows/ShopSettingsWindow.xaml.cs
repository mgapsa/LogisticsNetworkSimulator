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
    /// Interaction logic for ShopSettingsWindow.xaml
    /// </summary>
    public partial class ShopSettingsWindow : Window
    {
        public ShopSettingsWindow(Shop model)
        {
            InitializeComponent();
            this.DataContext = model;
            switch (model.Option)
            {
                case EnumTypes.ShopOptions.rS:
                    this.rS.IsChecked = true;
                    break;
                case EnumTypes.ShopOptions.sq:
                    this.sq.IsChecked = true;
                    break;
                case EnumTypes.ShopOptions.rsS:
                    this.rsS.IsChecked = true;
                    break;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Shop model = this.DataContext as Shop;
            if(this.rS.IsChecked == true)
            {
                model.Option = EnumTypes.ShopOptions.rS;
            }
            else if (this.sq.IsChecked == true)
            {
                model.Option = EnumTypes.ShopOptions.sq;
            }
            else if (this.rsS.IsChecked == true)
            {
                model.Option = EnumTypes.ShopOptions.rsS;
            }
            DialogResult = true;
            this.Close();
        }
    }
}

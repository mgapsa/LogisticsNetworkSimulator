using Database;
using Database.Brokers;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogisticsNetworkSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Shop shop = new Shop();
            Project project = new Project();
            project.Name = "HURA";
            DapperConfiguration.Map();
            using (IDbConnection connection = new ConnectionProvider().getConnection(false))
            {
                new ProjectBroker().Save(connection, project);
            }
        }
    }
}

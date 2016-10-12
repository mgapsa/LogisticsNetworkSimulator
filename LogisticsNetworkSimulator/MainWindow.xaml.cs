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

            Supplier sup = new Supplier();
            sup.ProjectId = 1;

            SimulationModel mod = new SimulationModel();
            mod.Suppliers.Add(sup);

            mod.Suppliers.Find(x => x.ProjectId == 1).X = 20;

            MessageBox.Show(sup.X.ToString());
        }

        public void TestDB()
        {
            Shop shop = new Shop();
            Project project = new Project();
            project.Name = "HURA";
            //project.Id = 1;
            project.Description = "super project";

            Supplier supplier = new Supplier();
            supplier.X = 22.17;
            supplier.Y = 120.43;
            supplier.ProjectId = 1;

            shop.InitialAmount = 1000;
            shop.MinAmount = 800;
            shop.StorageCost = 319.20;
            shop.X = 11;
            shop.Y = 100;
            shop.ProjectId = 1;
            shop.BasicAmount = 1200;

            Buyer buyer = new Buyer();
            buyer.X = 1;
            buyer.Y = 2;
            buyer.ProjectId = 1;
            buyer.OptionA = 1;
            buyer.OptionB = 2;
            buyer.ParamA = 11;
            buyer.ParamB = 12;
            buyer.ParamC = 13;
            buyer.ParamD = 21;
            buyer.ParamE = 22;
            buyer.ParamF = 23;

            //int x = (int)Connection.ConnectionTypes.ShopToBuyer;
            //MessageBox.Show(x.ToString());

            Connection conn = new Connection();
            //conn.ConnectionType = (Connection.ConnectionTypes) 1;
            //MessageBox.Show(conn.ConnectionType.ToString());
            conn.Id = 1;
            conn.ConnectionType = Connection.ConnectionTypes.ShopToShop;
            conn.ActorAId = 1;
            conn.ActorBId = 1;
            conn.MaxDelay = 23.5;
            conn.MinDelay = 12.25;
            conn.TransportCost = 100;
            conn.ProjectId = 1;
            conn.Usage = 100;



            DapperConfiguration.Map();
            using (IDbConnection connection = new ConnectionProvider().GetConnection(false))
            {
                //new ProjectBroker().Save(connection, project);
                //new SupplierBroker().Save(connection, supplier);
                //new ShopBroker().Save(connection, shop);
                //new BuyerBroker().Save(connection, buyer);
                //new ConnectionBroker().Save(connection, conn);
                conn = new ConnectionBroker().Get(connection, 1);
            }
            MessageBox.Show(conn.ConnectionType.ToString());

        }
    }
}

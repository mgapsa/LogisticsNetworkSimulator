using Database;
using Database.Brokers;
using DataModel;
using Services;
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
            DapperConfiguration.Map();
            AssignInputGestures();

            //Supplier sup = new Supplier();
            //sup.ProjectId = 1;

            //SimulationModel mod = new SimulationModel();
            //mod.Suppliers.Add(sup);

            //mod.Suppliers.Find(x => x.ProjectId == 1).X = 20;

            //MessageBox.Show(sup.X.ToString());

            //TestDB();
            //Project proj;
            //using (IDbConnection connection = new ConnectionProvider().GetConnection(true))
            //{
            //    proj = new ProjectBroker().Get(connection, 8);
            //}
            //SimulationModel model = new SimulationModelService().Get(proj);

            ////proj.Name = "LALALA";
            //model.Connections.Find(x => x.ProjectId == 8).Id = 0;
            //model.Buyers.Find(x => x.ProjectId == 8).Id = 0;
            //model.Shops.Find(x => x.ProjectId == 8).Id = 0;
            //model.Suppliers.Find(x => x.ProjectId == 8).Id = 0;

            //new SimulationModelService().Save(model);

            ////MessageBox.Show(model.Shops.Find( x => x.ProjectId == 8).X.ToString());
            //MessageBox.Show(model.Connections.Find(x => x.ProjectId == 8).ActorB.X.ToString());

        }

        public void TestDB()
        {
            Shop shop = new Shop();
            Project project = new Project();
            project.Name = "HURAaaaa";
            //project.Id = 1;
            project.Description = "super project";

            Supplier supplier = new Supplier();
            supplier.X = 22.17;
            supplier.Y = 120.43;
            supplier.ProjectId = 8;

            shop.InitialAmount = 1000;
            shop.MinAmount = 800;
            shop.StorageCost = 319.20;
            shop.X = 11;
            shop.Y = 100;
            shop.ProjectId = 8;
            shop.BasicAmount = 1200;

            Buyer buyer = new Buyer();
            buyer.X = 1;
            buyer.Y = 2;
            buyer.ProjectId = 8;
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
            //conn.Id = 1;
            conn.ConnectionType = Connection.ConnectionTypes.ShopToShop;
            conn.ActorAId = 1;
            conn.ActorBId = 1;
            conn.MaxDelay = 23.5;
            conn.MinDelay = 12.25;
            conn.TransportCost = 100;
            conn.ProjectId = 8;
            conn.Usage = 100;

            SimulationModel model = new SimulationModel();
            model.Project = project;
            model.Shops.Add(shop);
            model.Suppliers.Add(supplier);
            model.Connections.Add(conn);
            model.Buyers.Add(buyer);

            new SimulationModelService().Save(model);

            //DapperConfiguration.Map();
            //using (IDbConnection connection = new ConnectionProvider().GetConnection(false))
            //{
            //    //new ProjectBroker().Save(connection, project);
            //    //new SupplierBroker().Save(connection, supplier);
            //    //new ShopBroker().Save(connection, shop);
            //    //new BuyerBroker().Save(connection, buyer);
            //    //new ConnectionBroker().Save(connection, conn);

            //    //conn = new ConnectionBroker().Get(connection, 1);

            //    new ProjectBroker().Delete(connection, project.Id);
            //}
            //MessageBox.Show(conn.ConnectionType.ToString());

        }

        public void SetTitle(string name)
        {

            this.Title = Properties.Resources.MainWindowName + " (" + name + ")";
        }

        public void AssignInputGestures()
        {

            RoutedCommand cmdNew = new RoutedCommand();
            cmdNew.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdNew, MnuNew_Click));

            RoutedCommand cmdSave = new RoutedCommand();
            cmdSave.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdSave, MnuSave_Click));

            RoutedCommand cmdOpen = new RoutedCommand();
            cmdOpen.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdOpen, MnuOpen_Click));
        }

        #region Menu items click handlers
        private void MnuNew_Click(object sender, RoutedEventArgs e)
        {
            var w = new NewProjectWindow();
            if(w.ShowDialog() == true)
            {
                Project project = w.Project;
                if(project != null)
                {
                    MessageBox.Show("TODO: odpalic ui z symulacja" + project.Id);
                    SetTitle(project.Name);
                }
            }
        }

        private void MnuOpen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("OPEN");
            SetTitle("nowka");
        }

        private void MnuSave_Click(object sender, RoutedEventArgs e)
        {
            if(MnuSave.IsEnabled)
            {
                new SimulationModelService().Save(null);
            }
        }

        private void MnuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            //TODO dac tutaj model z symulacji ponizej
            var w = new SaveAsWindow(null);
            if (w.ShowDialog() == true)
            {
                SimulationModel model = w.Model;
                if (model != null)
                {
                    SetTitle(model.Project.Name);
                }
            }
        }

        private void MnuExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("EXIT");
        }
        #endregion
    }
}

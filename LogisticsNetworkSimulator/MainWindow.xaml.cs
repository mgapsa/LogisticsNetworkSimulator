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
        public SimulationModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DapperConfiguration.Map();
            AssignInputGestures();
        }

        //public void TestDB()
        //{
        //    Shop shop = new Shop();
        //    Project project = new Project();
        //    project.Name = "HURAaaaa";
        //    //project.Id = 1;
        //    project.Description = "super project";

        //    Supplier supplier = new Supplier();
        //    supplier.X = 22.17;
        //    supplier.Y = 120.43;
        //    supplier.ProjectId = 8;

        //    shop.InitialAmount = 1000;
        //    shop.MinAmount = 800;
        //    shop.StorageCost = 319.20;
        //    shop.X = 11;
        //    shop.Y = 100;
        //    shop.ProjectId = 8;
        //    shop.BasicAmount = 1200;

        //    Buyer buyer = new Buyer();
        //    buyer.X = 1;
        //    buyer.Y = 2;
        //    buyer.ProjectId = 8;
        //    buyer.OptionA = EnumTypes.BuyerAOptions.Static;
        //    buyer.OptionB = 2;
        //    buyer.ParamA = 11;
        //    buyer.ParamB = 12;
        //    buyer.ParamC = 13;
        //    buyer.ParamD = 21;
        //    buyer.ParamE = 22;
        //    buyer.ParamF = 23;

        //    //int x = (int)Connection.ConnectionTypes.ShopToBuyer;
        //    //MessageBox.Show(x.ToString());

        //    Connection conn = new Connection();
        //    //conn.ConnectionType = (Connection.ConnectionTypes) 1;
        //    //MessageBox.Show(conn.ConnectionType.ToString());
        //    //conn.Id = 1;
        //    conn.ConnectionType = DataModel.EnumTypes.ConnectionTypes.ShopToShop;
        //    conn.ActorAId = 1;
        //    conn.ActorBId = 1;
        //    conn.MaxDelay = 23.5;
        //    conn.MinDelay = 12.25;
        //    conn.TransportCost = 100;
        //    conn.ProjectId = 8;
        //    conn.Usage = 100;

        //    SimulationModel model = new SimulationModel();
        //    model.Project = project;
        //    model.Shops.Add(shop);
        //    model.Suppliers.Add(supplier);
        //    model.Connections.Add(conn);
        //    model.Buyers.Add(buyer);

        //    new SimulationModelService().Save(model);

        //    //DapperConfiguration.Map();
        //    //using (IDbConnection connection = new ConnectionProvider().GetConnection(false))
        //    //{
        //    //    //new ProjectBroker().Save(connection, project);
        //    //    //new SupplierBroker().Save(connection, supplier);
        //    //    //new ShopBroker().Save(connection, shop);
        //    //    //new BuyerBroker().Save(connection, buyer);
        //    //    //new ConnectionBroker().Save(connection, conn);

        //    //    //conn = new ConnectionBroker().Get(connection, 1);

        //    //    new ProjectBroker().Delete(connection, project.Id);
        //    //}
        //    //MessageBox.Show(conn.ConnectionType.ToString());

        //}

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

        public MessageBoxResult ShowYesNoDialog(String title, String text)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            return MessageBox.Show(text, title, btnMessageBox, icnMessageBox);
        }

        #region Menu items click handlers
        private void MnuNew_Click(object sender, RoutedEventArgs e)
        {
            if(simulationUI.Content != null)
            {
                if(ShowYesNoDialog("Warning", "Unsaved changes will be lost, do you wish to continue?") == MessageBoxResult.Yes)
                {
                    ShowNewProjectDialog();
                }
            }
            else
            {
                ShowNewProjectDialog();
            }
        }

        private void MnuOpen_Click(object sender, RoutedEventArgs e)
        {
            if (simulationUI.Content != null)
            {
                if(ShowYesNoDialog("Warning", "Unsaved changes will be lost, do you wish to continue?") == MessageBoxResult.Yes)
                {
                    ShowProjectOpenDialog();
                }
            }
            else
            {
                ShowProjectOpenDialog();
            }
        }

        private void MnuSave_Click(object sender, RoutedEventArgs e)
        {
            if(MnuSave.IsEnabled)
            {
                new SimulationModelService().Save(Model);
            }
        }

        private void MnuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var w = new SaveProjectAsWindow(Model);
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

        public void ShowProjectOpenDialog()
        {
            var w = new OpenProjectWindow();
            if (w.ShowDialog() == true)
            {
                Project project = w.Project;
                if (project != null)
                {
                    Model = new SimulationModelService().Get(project);
                    SimulationUI ui = new SimulationUI(Model, false);
                    ui.InitializeComponent();
                    simulationUI.Content = ui;
                    SetTitle(project.Name);
                    MnuSave.IsEnabled = true;
                    MnuSaveAs.IsEnabled = true;
                }
            }
        }

        public void ShowNewProjectDialog()
        {
            var w = new NewProjectWindow();
            if (w.ShowDialog() == true)
            {
                Project project = w.Project;
                if (project != null)
                {
                    Model = new SimulationModel(project);
                    SimulationUI ui = new SimulationUI(Model, true);
                    ui.InitializeComponent();
                    simulationUI.Content = ui;
                    SetTitle(project.Name);
                    MnuSave.IsEnabled = true;
                    MnuSaveAs.IsEnabled = true;
                }
            }
        }
        #endregion
    }
}

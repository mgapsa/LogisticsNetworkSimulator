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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Services;
using LogisticsNetworkSimulator.Actors;
using LogisticsNetworkSimulator.ConnectionCreators;
using LogisticsNetworkSimulator.SimulationEventsHelper;

namespace LogisticsNetworkSimulator
{
    /// <summary>
    /// Interaction logic for SimulationUI.xaml
    /// </summary>
    public partial class SimulationUI : UserControl
    {
        public SimulationModel Model { get; set; }
        public SimulationEventHandler SimulationEventHandler { get; set; }
        public ConnectionCreator ConnectionCreator { get; set; }

        public event Delegates.NewOrderShopToBuyerEventHandler NewOrderShopToBuyer;
        public event Delegates.NewOrderShopToShopEventHandler NewOrderShopToShop;
        public event Delegates.NewOrderSupplierToShopEventHandler NewOrderSupplierToShop;
        public event Delegates.SupplyArrivedToShopEventHandler SupplyArrivedToShop;

        public bool SimulateB { get; set; }

        //Data for simulation - needed for puase/start
        private DateTime startTime;
        private DateTime endTime;
        private DateTime currentTime;
        private int i;
        private int previousI;
        bool wasPaused = false;
        bool wasStopped = false;

        public SimulationUI(SimulationModel model, bool isNewProject)
        {
            InitializeComponent();
            this.Model = model;
            this.SimulationEventHandler = new SimulationEventHandler(this);
            this.ConnectionCreator = new ConnectionCreator(this.Model, this.target);

            this.StartDate.SelectedDate = DateTime.Now;
            this.StartDate.SelectedDate = DateTime.Now.AddMonths(1);

            if(!isNewProject)
            {
                PrintProject();
            }
        }

        //not used
        public SimulationUI()
        {
            InitializeComponent();
        }

        public void PrintProject()
        {
            foreach (Connection connection in Model.Connections)
            {
                //ConnectionCreator.AddActor(conne)
                ConnectionUI conn = new ConnectionUI(this.Model, this.target, connection, this.ConnectionCreator);
                this.ConnectionCreator.ConnectionDictionary.Add(connection, conn);
                conn.PrintOnTarget();
            }
            foreach(Shop shopModel in Model.Shops)
            {
                ShopUserControl shopUserControl= new ShopUserControl(shopModel, Model, ConnectionCreator);
                shopUserControl.printOnTarget(this.target);
            }
            foreach (Buyer buyerModel in Model.Buyers)
            {
                BuyerUserControl buyerUserControl = new BuyerUserControl(buyerModel, Model, ConnectionCreator);
                buyerUserControl.printOnTarget(this.target);
            }
            foreach (Supplier supplierModel in Model.Suppliers)
            {
                SupplierUserControl supplierUserControl = new SupplierUserControl(supplierModel, Model, ConnectionCreator);
                supplierUserControl.printOnTarget(this.target);
            }
        }

        private void panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                // These Effects values are used in the drag source's
                // GiveFeedback event handler to determine which cursor to display.
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.Move;
                }
            }
        }

        private void panel_Drop(object sender, DragEventArgs e)
        {
            // If an element in the panel has already handled the drop,
            // the panel should not also handle it.
            if (e.Handled == false)
            {
                Canvas _canvas = (Canvas)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");

                if (_canvas != null && _element != null)
                {
                    // Get the panel that the element currently belongs to,
                    // then remove it from that panel and add it the Children of
                    // the panel that its been dropped on.
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);
                    if (_parent != null)
                    {
                        //Mouse position
                        System.Windows.Point position = e.GetPosition(_canvas);
                        //Type
                        DataModel.EnumTypes.UserControlTypes _type = (DataModel.EnumTypes.UserControlTypes)e.Data.GetData("UserControlType");

                        if ((e.KeyStates == DragDropKeyStates.ControlKey || _parent.Name == "ToolPanel") &&
                            e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            switch(_type)
                            {
                                case EnumTypes.UserControlTypes.ShopUserControl:
                                    ShopUserControl _shopUserControl = new ShopUserControl((ShopUserControl)_element, Model, ConnectionCreator);
                                    Model.Shops.Add(_shopUserControl.ShopModel);
                                    _shopUserControl.printOnTarget(_canvas, position);
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Copy;
                                    break;
                                case EnumTypes.UserControlTypes.BuyerUserControl:
                                    BuyerUserControl _buyerUserControl = new BuyerUserControl((BuyerUserControl)_element, Model, ConnectionCreator);
                                    Model.Buyers.Add(_buyerUserControl.BuyerModel);
                                    _buyerUserControl.printOnTarget(_canvas, position);
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Copy;
                                    break;
                                case EnumTypes.UserControlTypes.SupplierUserControl:
                                    SupplierUserControl _supplierUserControl = new SupplierUserControl((SupplierUserControl)_element, Model, ConnectionCreator);
                                    Model.Suppliers.Add(_supplierUserControl.SupplierModel);
                                    _supplierUserControl.printOnTarget(_canvas, position);
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Copy;
                                    break;
                                default:
                                    MessageBox.Show("Not habndled yet");
                                    break;
                            }
                        }
                        else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            IActorUserControl _actorUserControl = (IActorUserControl)_element;
                            List<Connection> connections;
                            switch(_actorUserControl.GetUserControlType())
                            {
                                case EnumTypes.UserControlTypes.ShopUserControl:
                                    ShopUserControl _shopUserControl = (ShopUserControl)_element;
                                    _parent.Children.Remove(_element);
                                    _shopUserControl.printOnTarget(_canvas, position);

                                    connections = this.Model.Connections.Where(c => c.ActorA == _shopUserControl.ShopModel || c.ActorB == _shopUserControl.ShopModel).ToList();
                                    foreach (Connection conn in connections)
                                    {
                                        this.ConnectionCreator.ConnectionDictionary[conn].Reprint();
                                    }
                                    //TODO: 
                                    //potem gorzej bo dla kazdej linii musimy nzlaezc aktorow zeby ich przerysowac... nie mam do nich odwolan, po liscie wszystkich narysowanych musze leciec
                                    //odowlania nie moge zrobic bo przy odtwarzaniu z bazki nie mam jeszcze aktorow ui i nie ma ich w connections ui
                                    //zmiana tego bedzie dluzsza niz warto
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;
                                    break;
                                case EnumTypes.UserControlTypes.BuyerUserControl:
                                    BuyerUserControl _buyerUserControl = (BuyerUserControl)_element;
                                    _parent.Children.Remove(_element);
                                    _buyerUserControl.printOnTarget(_canvas, position);

                                    connections = this.Model.Connections.Where(c => c.ActorA == _buyerUserControl.BuyerModel || c.ActorB == _buyerUserControl.BuyerModel).ToList();
                                    foreach (Connection conn in connections)
                                    {
                                        this.ConnectionCreator.ConnectionDictionary[conn].Reprint();
                                    }
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;
                                    break;
                                case EnumTypes.UserControlTypes.SupplierUserControl:
                                    SupplierUserControl _supplierUserControl = (SupplierUserControl)_element;
                                    _parent.Children.Remove(_element);
                                    _supplierUserControl.printOnTarget(_canvas, position);

                                    connections = this.Model.Connections.Where(c => c.ActorA == _supplierUserControl.SupplierModel || c.ActorB == _supplierUserControl.SupplierModel).ToList();
                                    foreach (Connection conn in connections)
                                    {
                                        this.ConnectionCreator.ConnectionDictionary[conn].Reprint();
                                        
                                    }
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;
                                    break;
                            }

                            this.ReprintActors();
                        }
                    }
                }
            }
        }

        public void ReprintActors()
        {
            List<IActorUserControl> actorsList = new List<IActorUserControl>();
            foreach (UIElement element in target.Children)
            {
                IActorUserControl actor = element as IActorUserControl;
                if(actor!=null)
                {
                    actorsList.Add(actor);                    
                }
            }

            foreach (IActorUserControl actor in actorsList)
            {
                actor.Reprint(this.target);
            }
        }

        private void target_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ConnectionCreator.CancelConnection();
        }

        private void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            SimulateB = true;
            this.PauseSimulation.IsEnabled = true;
            this.StopSimulation.IsEnabled = true;
            this.StartSimulation.IsEnabled = false;
            if(!wasPaused)
            {
                startTime = this.StartDate.SelectedDate.Value.AddHours(8);
                endTime = this.EndDate.SelectedDate.Value.AddHours(22);
            }
            Task task = Task.Run((Action)Simulate);
        }

        private void PauseSimulation_Click(object sender, RoutedEventArgs e)
        {
            SimulateB = false;
            wasPaused = true;
            this.StartSimulation.IsEnabled = true;
            this.PauseSimulation.IsEnabled = false;
            this.StopSimulation.IsEnabled = true;
        }

        private void StopSimulation_Click(object sender, RoutedEventArgs e)
        {
            SimulateB = false;
            wasStopped = true;
            Cleanup();
        }

        private void Simulate()
        {
            if(!wasPaused)
            {
                currentTime = startTime;


                int size = Convert.ToInt32((endTime - startTime).TotalMinutes) - (endTime - startTime).Days * 10 * 60 + (endTime - startTime).Days + 2; 
                //MessageBox.Show(size.ToString());

                PreSet(size, startTime);
                //do PRESET - go through everything ans set size of arrays for graphdata  - set starting DATE! (or global and pass it? as it can be specified by user?)
                //and set when buyers will buy for the first time->next time it will be set when they will be buy -> unless it will be some special option

                //varialbe i which will be used to determine where to add (each minute/each hour?)
                i = 0;
                //determine whether to copy last values or not
                previousI = 0;
            }

            wasStopped = false;
            wasPaused = false;

            while (currentTime <= endTime && SimulateB)
            {
                i++;//now every minute so like this
                if (i != previousI)
                {
                    foreach (Shop shop in Model.Shops)
                    {
                        shop.GraphData[i] = shop.GraphData[i - 1];
                    }
                }
                previousI = i;

                foreach (Shop shop in Model.Shops)
                {
                    if (shop.OrderArrived(currentTime))
                    {
                        SupplyArrivedToShopEventArgs args = new SupplyArrivedToShopEventArgs(shop, i, currentTime);
                        if (SupplyArrivedToShop != null)
                        {
                            //delete orders there so they are not in memory anmore
                            this.SupplyArrivedToShop(this, args);
                        }
                    }
                }

                //    //teraz wysyłam z shopów do buyerów TODO for each such connection check if buyer want to buy sth -> if yes -> event (gets shop, buyer)
                foreach (Connection connection in Model.Connections)
                {
                    if (connection.ConnectionType == EnumTypes.ConnectionTypes.ShopToBuyer)
                    {
                        Shop shop = connection.ActorA as Shop;
                        Buyer buyer = connection.ActorB as Buyer;

                        if (buyer.MakeOrder(currentTime))
                        {
                            if (this.NewOrderShopToBuyer != null)
                            {
                                NewOrderShopToBuyerEventArgs args = new NewOrderShopToBuyerEventArgs(shop, buyer, i, currentTime);
                                try
                                {
                                    this.NewOrderShopToBuyer(this, args);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(shop.Id.ToString() + "   " + ex.Message);
                                    //Cleanup();
                                    //return;
                                }
                            }
                        }
                    }
                }

                //Take care of percentage
                foreach (Connection connection in Model.Connections)
                {
                    if (connection.ConnectionType == EnumTypes.ConnectionTypes.ShopToShop)
                    {
                        Shop shopA = connection.ActorA as Shop;
                        Shop shopB = connection.ActorB as Shop;

                        if (shopB.MakeOrder(currentTime, i))
                        {
                            if (this.NewOrderShopToShop != null)
                            {
                                NewOrderShopToShopEventArgs args = new NewOrderShopToShopEventArgs(shopA, shopB, connection, i, currentTime);
                                try
                                {
                                    this.NewOrderShopToShop(this, args);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(shopA.Id.ToString() + "   " + ex.Message);
                                    Cleanup();
                                    return;
                                }
                            }
                        }
                    }
                }

                //Take care of percentage
                foreach (Connection connection in Model.Connections)
                {
                    if (connection.ConnectionType == EnumTypes.ConnectionTypes.SupplierToShop)
                    {
                        Supplier supplier = connection.ActorA as Supplier;
                        Shop shop = connection.ActorB as Shop;

                        if (shop.MakeOrder(currentTime, i))
                        {
                            if (this.NewOrderSupplierToShop != null)
                            {
                                NewOrderSupplierToShopEventArgs args = new NewOrderSupplierToShopEventArgs(supplier, shop, connection, i, currentTime);
                                try
                                {
                                    this.NewOrderSupplierToShop(this, args);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    Cleanup();
                                    return;
                                }
                            }
                        }
                    }
                }
                if (currentTime.Hour >= 22 && currentTime < endTime)
                {
                    currentTime = currentTime.AddHours(10);
                    //to equalize further +1
                    //currentTime = currentTime.AddMinutes(-1);
                    //Its new day so lets start with setting stuff
                    i++;
                    foreach (Shop shop in Model.Shops)
                    {
                        shop.GraphData[i] = shop.GraphData[i - 1];
                    }
                }

                foreach (Buyer buyer in Model.Buyers)
                {
                    buyer.SetNextOrderIfNeeded(currentTime);
                }

                currentTime = currentTime.AddMinutes(1);

                UpdateProgressBar(i);
            }

            Cleanup();
        }

        private void UpdateProgressBar(int i)
        {
            Dispatcher.Invoke(() =>
            {
                this.ProgressBar.Value = i;
            });
        }

        private void PreSet(int size, DateTime startTime)
        {
            Dispatcher.Invoke(() =>
            {
                this.ProgressBar.Minimum = 0;
                this.ProgressBar.Maximum = size;
                this.ProgressBar.Value = 0;
            });
            foreach (Shop shop in Model.Shops)
            {
                shop.SetDataSize(size);
            }
            foreach (Buyer buyer in Model.Buyers)
            {
                buyer.SetDataSize(size);
                buyer.SetNextOrderIfNeeded(startTime);
            }
            foreach (Supplier supplier in Model.Suppliers)
            {
                supplier.SetDataSize(size);
            }
        }

        void Cleanup()
        {
            foreach(Shop shop in Model.Shops)
            {
                //buyerow jeszcze przeczyscic
                shop.LastOrderTime = new DateTime();
                shop.LastOrderAmount = -1;
                shop.OrdersList = new List<Order>();
            }
            foreach(Buyer buyer in Model.Buyers)
            {
                buyer.NextOrderTime = new DateTime();
                buyer.NextOrderAmount = 0;
            }
            Dispatcher.Invoke(() =>
            {
                this.ProgressBar.Value = 0;
                this.StartSimulation.IsEnabled = true;
                this.PauseSimulation.IsEnabled = false;
                this.StopSimulation.IsEnabled = false;
            });
        }

    }
}

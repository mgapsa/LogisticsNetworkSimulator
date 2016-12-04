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

        public SimulationUI(SimulationModel model, bool isNewProject)
        {
            InitializeComponent();
            this.Model = model;
            this.SimulationEventHandler = new SimulationEventHandler(this);
            this.ConnectionCreator = new ConnectionCreator(this.Model, this.target);
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

        //jak wywoluje to eventy to tworze argysy i jak moje eventy sie sa snullam ito lece
        private void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            DateTime startTime = new DateTime(2016, 12, 1, 12, 0, 0);
            DateTime endTime = new DateTime(2016, 12, 12, 12, 0, 0);
            DateTime currentTime = startTime;

            int size = (endTime - startTime).Minutes + (endTime - startTime).Hours * 60 + (endTime - startTime).Days * 24 * 60;
            //MessageBox.Show(size.ToString());

            PreSet(size, startTime);
            //do PRESET - go through everything ans set size of arrays for graphdata  - set starting DATE! (or global and pass it? as it can be specified by user?)
            //and set when buyers will buy for the first time->next time it will be set when they will be buy -> unless it will be some special option

            //varialbe i which will be used to determine where to add (each minute/each hour?)
            int i = 0;
            //determine whether to copy last values or not
            int previousI = 0;
            while(currentTime <= endTime)
            {
                i++;//now every minute so like this
                if(i != previousI)
                {
                    foreach(Shop shop in Model.Shops)
                    {
                        shop.GraphData[i] = shop.GraphData[i - 1];
                    }
                }
                previousI = i;

                foreach(Shop shop in Model.Shops)
                {
                    if(shop.OrderArrived(currentTime))
                    {
                        SupplyArrivedToShopEventArgs args = new SupplyArrivedToShopEventArgs();
                        if(SupplyArrivedToShop != null)
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
                                NewOrderShopToBuyerEventArgs args = new NewOrderShopToBuyerEventArgs();
                                try
                                {
                                    this.NewOrderShopToBuyer(this, args);
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                }


                currentTime.AddMinutes(1);

                foreach(Buyer buyer in Model.Buyers)
                {
                    buyer.SetNextOrderIfNeeded(currentTime);
                }
            }

            //??    //generwoanie needa u buyerow
            //    foreach (BuyerLink shop in BuyerList)
            //    {
            //        generateNeed(shop, i);
            //        shop.results[i] = shop.need;
            //        if (i == 1)
            //        {
            //            shop.results[0] = shop.results[1];
            //        }
            //    }
            //??

            
            //    foreach (LineC line in LineC.LineList) //TODOcheck if shop want to make an order 0> if yes event -> gets shop and shop
            //    {
            //        if (line.link1 as ShopLink != null && line.link2 as ShopLink != null)
            //        {
            //            ShopLink slink1 = line.link1 as ShopLink;
            //            ShopLink slink2 = line.link2 as ShopLink;
            //            double amount = 0;
            //            foreach (Order order in slink2.OrderList)
            //            {
            //                amount += order.amount;
            //            }
            //            amount = slink2.need - amount - slink2.results[i];
            //            amount = (double)((double)(line.prc * amount) / 100);
            //            if (amount > 0 && slink1.send(amount, i))
            //            {
            //                Order order = new Order(amount, line.delay);
            //                slink2.OrderList.Add(order);
            //                slink2.S += slink2.orderCost;
            //            }
            //            else if (amount > 0)
            //            {
            //                MessageBox.Show("ShopLink " + slink1._label.Content.ToString() + "doesnt have enough resources at " + i.ToString() + " day");
            //                this.delete_orders(ShopList);
            //                return false;
            //            }
            //            //porownuje stan obecny sklepu z jego needem i tworze zamownie dla niego korzystając tez z procentów na linii, od drugiego sklepu natychmiast odejmuje te wartosc
            //        }
            //    }

            //    foreach (LineC line in LineC.LineList) TODO jak wyzej
            //    {
            //        if (line.link1 as SourceLink != null && line.link2 as ShopLink != null)
            //        {
            //            SourceLink slink1 = line.link1 as SourceLink;
            //            ShopLink slink2 = line.link2 as ShopLink;
            //            double amount = 0;
            //            foreach (Order order in slink2.OrderList)
            //            {
            //                amount += order.amount;
            //            }
            //            amount = slink2.need - amount - slink2.results[i];
            //            amount = (double)((double)(line.prc * amount) / 100);
            //            if (amount > 0)
            //            {
            //                //MessageBox.Show(line.delay.ToString());
            //                Order order = new Order(amount, line.delay);
            //                slink1.results[i] += amount;
            //                if (i == 1)
            //                {
            //                    slink1.results[0] += amount;
            //                }
            //                slink2.OrderList.Add(order);
            //                slink2.S += slink2.orderCost;
            //                //MessageBox.Show(slink2.S.ToString());
            //            }
            //            else if (amount > 0)
            //            {
            //                MessageBox.Show("SourceLink " + slink1._label.Content.ToString() + " doesnt have enough resources at " + i.ToString() + " day");
            //                this.delete_orders(ShopList);
            //                return false;
            //            }
            //            //to samo co wyzej
            //        }
            //    }
            //}
            //this.delete_orders(ShopList);
            //return true;
        }

        private void PreSet(int size, DateTime startTime)
        {
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
    }
}

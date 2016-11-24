﻿using DataModel;
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
                            switch(_actorUserControl.GetUserControlType())
                            {
                                case EnumTypes.UserControlTypes.ShopUserControl:
                                    ShopUserControl _shopUserControl = (ShopUserControl)_element;
                                    _parent.Children.Remove(_element);
                                    _shopUserControl.printOnTarget(_canvas, position);
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;
                                    break;
                                case EnumTypes.UserControlTypes.BuyerUserControl:
                                    BuyerUserControl _buyerUserControl = (BuyerUserControl)_element;
                                    _parent.Children.Remove(_element);
                                    _buyerUserControl.printOnTarget(_canvas, position);
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;
                                    break;
                                case EnumTypes.UserControlTypes.SupplierUserControl:
                                    SupplierUserControl _supplierUserControl = (SupplierUserControl)_element;
                                    _parent.Children.Remove(_element);
                                    _supplierUserControl.printOnTarget(_canvas, position);
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;
                                    break;
                            }
                            
                        }
                    }
                }
            }
        }

        private void target_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ConnectionCreator.CancelConnection();
        }
    }
}

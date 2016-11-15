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

namespace LogisticsNetworkSimulator.Actors
{
    /// <summary>
    /// Interaction logic for BuyerUserControl.xaml
    /// </summary>
    public partial class BuyerUserControl : UserControl, IActorUserControl
    {
        public Buyer BuyerModel { get; set; }
        public BuyerUserControl()
        {
            InitializeComponent();
            BuyerModel = new Buyer();
        }

        //copy constructor
        public BuyerUserControl(BuyerUserControl buyer)
        {
            InitializeComponent();
            this.buyerUI.Height = buyer.buyerUI.Height;
            this.buyerUI.Width = buyer.buyerUI.Height;
            this.BuyerModel = new Buyer(buyer.BuyerModel);
        }

        //create constructor
        public BuyerUserControl(Buyer buyer)
        {
            InitializeComponent();
            this.BuyerModel = buyer;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Double", buyerUI.Height);
                data.SetData("UserControlType", DataModel.EnumTypes.UserControlTypes.BuyerUserControl);
                data.SetData("Object", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.
            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(Cursors.Pen);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }

        public void printOnTarget(Canvas target)
        {
            Canvas.SetTop(this, this.BuyerModel.Y);
            Canvas.SetLeft(this, this.BuyerModel.X);
            this.Width = 75;
            ContextMenu pMenu = new ContextMenu();
            MenuItem delete = new MenuItem();
            MenuItem item2 = new MenuItem();
            MenuItem graph = new MenuItem();
            this.ContextMenu = pMenu;

            target.Children.Add(this);
        }

        public void printOnTarget(Canvas target, Point position)
        {
            Canvas.SetTop(this, position.Y);
            Canvas.SetLeft(this, position.X);
            this.BuyerModel.X = position.X;
            this.BuyerModel.Y = position.Y;
            this.Width = 75;
            ContextMenu pMenu = new ContextMenu();
            MenuItem delete = new MenuItem();
            MenuItem item2 = new MenuItem();
            MenuItem graph = new MenuItem();
            this.ContextMenu = pMenu;

            target.Children.Add(this);
            //delete.Header = "Delete";
            //delete.Click += new RoutedEventHandler(delete_Click);
            //pMenu.Items.Add(delete);

            //item2.Header = "Change values";
            //item2.Click += new RoutedEventHandler(change_values);
            //pMenu.Items.Add(item2);

            //graph.Header = "Show graph";
            //graph.Click += new RoutedEventHandler(print_graph);
            //pMenu.Items.Add(graph);

            //_link.ContextMenu = pMenu;
            //target.Children.Add(_link);
            //if (_label == null)
            //    printlabel(target);
        }

        public EnumTypes.UserControlTypes GetUserControlType()
        {
            return EnumTypes.UserControlTypes.BuyerUserControl;
        }
    }
}

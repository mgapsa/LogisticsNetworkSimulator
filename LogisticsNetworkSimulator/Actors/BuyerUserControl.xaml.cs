﻿using DataModel;
using LogisticsNetworkSimulator.ConnectionCreators;
using LogisticsNetworkSimulator.Graphs;
using LogisticsNetworkSimulator.SettingsWindows;
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

        private DateTime clickTime;
        private object clickSender;
        public SimulationModel SimulationModel { get; set; }
        public ConnectionCreator ConnectionCreator { get; set; }
        private Label Label = new Label();

        public BuyerUserControl()
        {
            InitializeComponent();
            BuyerModel = new Buyer();
        }

        //copy constructor
        public BuyerUserControl(BuyerUserControl buyer, SimulationModel model, ConnectionCreator creator)
        {
            InitializeComponent();
            this.buyerUI.Height = buyer.buyerUI.Height;
            this.buyerUI.Width = buyer.buyerUI.Height;
            this.SimulationModel = model;
            this.BuyerModel = new Buyer(buyer.BuyerModel);
            this.ConnectionCreator = creator;
        }

        //create constructor
        public BuyerUserControl(Buyer buyer, SimulationModel model, ConnectionCreator creator)
        {
            InitializeComponent();
            this.BuyerModel = buyer;
            this.SimulationModel = model;
            this.ConnectionCreator = creator;
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

            this.CreateMenu();
            
            target.Children.Add(this);
            printLabel(target);
        }

        public void printOnTarget(Canvas target, Point position)
        {
            this.Width = 75;
            Canvas.SetTop(this, position.Y - this.Width / 2);
            Canvas.SetLeft(this, position.X - this.Width / 2);
            this.BuyerModel.X = position.X - this.Width / 2;
            this.BuyerModel.Y = position.Y - this.Width / 2;

            this.CreateMenu();

            target.Children.Add(this);
            printLabel(target);
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

        private void printLabel(Canvas target)
        {
            Canvas.SetTop(Label, this.BuyerModel.Y + 60);
            Canvas.SetLeft(Label, this.BuyerModel.X + 25);
            Label.Content = this.BuyerModel.Id.ToString();
            //_label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            if(!target.Children.Contains(Label))
            {
                target.Children.Add(Label);
            }
        }


        public void Reprint(Canvas target)
        {
            Image img = this.buyerUI;
            target.Children.Remove(this);

            Canvas.SetTop(this, this.BuyerModel.Y);
            Canvas.SetLeft(this, this.BuyerModel.X);
            this.Width = 75;

            this.CreateMenu();

            target.Children.Add(this);
            printLabel(target);
        }

        public void Reprint(Object targetPanel)
        {
            Image img = this.buyerUI;
            Canvas target = targetPanel as Canvas;
            target.Children.Remove(this);

            Canvas.SetTop(this, this.BuyerModel.Y);
            Canvas.SetLeft(this, this.BuyerModel.X);
            this.Width = 75;

            this.CreateMenu();

            target.Children.Add(this);
            printLabel(target);
        }

        #region Menu
        public void CreateMenu()
        {
            ContextMenu pMenu = new ContextMenu();
            MenuItem delete = new MenuItem();
            MenuItem settings = new MenuItem();
            MenuItem graph = new MenuItem();

            delete.Header = "Delete";
            delete.Click += new RoutedEventHandler(delete_Click);
            pMenu.Items.Add(delete);

            settings.Header = "Settings";
            settings.Click += new RoutedEventHandler(settings_Click);
            pMenu.Items.Add(settings);

            graph.Header = "Show Graph";
            graph.Click += new RoutedEventHandler(showGraph_Click);
            pMenu.Items.Add(graph);

            this.ContextMenu = pMenu;
        }

        public void delete_Click(object sender, RoutedEventArgs e)
        {
            this.SimulationModel.Buyers.Remove(this.BuyerModel);
            Image img = this.buyerUI;
            Panel _parent = (Panel)VisualTreeHelper.GetParent(img);
            _parent.Children.Remove(img);
            Panel _parent2 = (Panel)VisualTreeHelper.GetParent(Label);
            _parent2.Children.Remove(Label);

            List<Connection> connections = this.SimulationModel.Connections.Where(c => c.ActorA == this.BuyerModel || c.ActorB == this.BuyerModel).ToList();
            foreach (Connection conn in connections)
            {
                this.SimulationModel.Connections.Remove(conn);
                this.ConnectionCreator.ConnectionDictionary[conn].RemoveLineUI();
                this.ConnectionCreator.ConnectionDictionary.Remove(conn);
            }
        }

        public void settings_Click(object sender, RoutedEventArgs e)
        {
            EnumTypes.BuyerAOptions optiona = this.BuyerModel.OptionA;
            EnumTypes.BuyerBOptions optionb = this.BuyerModel.OptionB;
            double amount = this.BuyerModel.Amount;
            double min = this.BuyerModel.MinAmount;
            double max = this.BuyerModel.MaxAmount;
            double lambda = this.BuyerModel.Lambda;
            double meana = this.BuyerModel.MeanOptionA;
            double deva = this.BuyerModel.DeviationOptionA;
            double minutes = this.BuyerModel.Minutes;
            double meanb = this.BuyerModel.MeanOptionB;
            double devb = this.BuyerModel.DeviationOptionB;


            var w = new BuyerSettingsWindow(this.BuyerModel);
            if (w.ShowDialog() != true)
            {
                this.BuyerModel.Amount = amount;
                this.BuyerModel.MinAmount = min;
                this.BuyerModel.MaxAmount = max;
                this.BuyerModel.Lambda = lambda;
                this.BuyerModel.MeanOptionA = meana;
                this.BuyerModel.DeviationOptionA = deva;
                this.BuyerModel.Minutes = minutes;
                this.BuyerModel.MeanOptionB = meanb;
                this.BuyerModel.DeviationOptionB = devb;
            }
        }

        public void showGraph_Click(object sender, RoutedEventArgs e)
        {
            GraphWindow graph = new GraphWindow(this.BuyerModel);
            graph.Show();
        }
        #endregion

        public EnumTypes.UserControlTypes GetUserControlType()
        {
            return EnumTypes.UserControlTypes.BuyerUserControl;
        }

        public Actor GetActor()
        {
            return this.BuyerModel;
        }

        private void buyerUI_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.clickSender = sender;
                this.clickTime = DateTime.Now;
            }
        }

        private void buyerUI_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && sender == this.clickSender)
            {
                TimeSpan timeSinceDown = DateTime.Now - this.clickTime;
                if (timeSinceDown.TotalMilliseconds < 500)
                {
                    try
                    {
                        this.ConnectionCreator.AddActor(this);
                        this.ConnectionCreator.CreateConnectionIfPossible();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }
    }
}

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
using DataModel;
using LogisticsNetworkSimulator.SettingsWindows;

namespace LogisticsNetworkSimulator.Actors
{
    /// <summary>
    /// Interaction logic for ShopUserControl.xaml
    /// </summary>
    public partial class ShopUserControl : UserControl, IActorUserControl
    {
        public Shop ShopModel { get; set; }

        private DateTime clickTime;
        private object clickSender;

        public ShopUserControl()
        {
            InitializeComponent();
            ShopModel = new Shop();
        }

        //copy constructor
        public ShopUserControl(ShopUserControl shop, SimulationModel model)
        {
            InitializeComponent();
            this.shopUI.Height = shop.shopUI.Height;
            this.shopUI.Width = shop.shopUI.Height;
            this.ShopModel = new Shop(shop.ShopModel, model);
        }

        //create constructor
        public ShopUserControl(Shop shop)
        {
            InitializeComponent();
            this.ShopModel = shop;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Double", shopUI.Height);
                data.SetData("UserControlType", DataModel.EnumTypes.UserControlTypes.ShopUserControl);
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
            Canvas.SetTop(this, this.ShopModel.Y);
            Canvas.SetLeft(this, this.ShopModel.X);
            this.Width = 75;

            this.CreateMenu();

            target.Children.Add(this);
        }

        public void printOnTarget(Canvas target, Point position)
        {
            this.Width = 75;
            Canvas.SetTop(this, position.Y - this.Width/2);
            Canvas.SetLeft(this, position.X - this.Width / 2);
            this.ShopModel.X = position.X - this.Width / 2;
            this.ShopModel.Y = position.Y - this.Width / 2;

            this.CreateMenu();

            target.Children.Add(this);

            //graph.Header = "Show graph";
            //graph.Click += new RoutedEventHandler(print_graph);
            //pMenu.Items.Add(graph);

            //_link.ContextMenu = pMenu;
            //target.Children.Add(_link);
            //if (_label == null)
            //    printlabel(target);
        }

        #region Menu
        public void CreateMenu()
        {
            ContextMenu pMenu = new ContextMenu();
            MenuItem delete = new MenuItem();
            MenuItem settings = new MenuItem();

            delete.Header = "Delete";
            delete.Click += new RoutedEventHandler(delete_Click);
            pMenu.Items.Add(delete);

            settings.Header = "Settings";
            settings.Click += new RoutedEventHandler(settings_Click);
            pMenu.Items.Add(settings);

            this.ContextMenu = pMenu;
        }

        public void delete_Click(object sender, RoutedEventArgs e)
        {
            this.ShopModel.SimulationModel.Shops.Remove(this.ShopModel);
            Image img = this.shopUI;
            Panel _parent = (Panel)VisualTreeHelper.GetParent(img);
            _parent.Children.Remove(img);
        }

        public void settings_Click(object sender, RoutedEventArgs e)
        {
            //backup
            double min = this.ShopModel.MinAmount;
            double basic = this.ShopModel.BasicAmount;
            double cost = this.ShopModel.StorageCost;
            double initial = this.ShopModel.InitialAmount;
            var w = new ShopSettingsWindow(this.ShopModel);
            if (w.ShowDialog() != true)
            {
                this.ShopModel.MinAmount = min;
                this.ShopModel.BasicAmount = basic;
                this.ShopModel.StorageCost = cost;
                this.ShopModel.InitialAmount = initial;
            }
        }
        #endregion

        public EnumTypes.UserControlTypes GetUserControlType()
        {
            return EnumTypes.UserControlTypes.ShopUserControl;
        }

        #region Mouse click
        private void shopUI_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && sender == this.clickSender)
            {
                TimeSpan timeSinceDown = DateTime.Now - this.clickTime;
                if (timeSinceDown.TotalMilliseconds < 500)
                {
                    this.ShopModel.SimulationModel.ConnectionCreator.AddActor(this);
                    this.ShopModel.SimulationModel.ConnectionCreator.CreateConnectionIfPossible();
                }
            }
        }

        private void shopUI_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.clickSender = sender;
                this.clickTime = DateTime.Now;
            }
        }
        #endregion
    }
}

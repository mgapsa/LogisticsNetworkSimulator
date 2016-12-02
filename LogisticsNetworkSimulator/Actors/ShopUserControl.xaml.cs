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
using LogisticsNetworkSimulator.ConnectionCreators;

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
        public SimulationModel SimulationModel { get; set; }
        public ConnectionCreator ConnectionCreator { get; set; }

        public ShopUserControl()
        {
            InitializeComponent();
            ShopModel = new Shop();
        }

        //copy constructor
        public ShopUserControl(ShopUserControl shop, SimulationModel model, ConnectionCreator creator)
        {
            InitializeComponent();
            this.shopUI.Height = shop.shopUI.Height;
            this.shopUI.Width = shop.shopUI.Height;
            this.ShopModel = new Shop(shop.ShopModel);
            this.SimulationModel = model;
            this.ConnectionCreator = creator;
        }

        //create constructor
        public ShopUserControl(Shop shop, SimulationModel model, ConnectionCreator creator)
        {
            InitializeComponent();
            this.SimulationModel = model;
            this.ShopModel = shop;
            this.ConnectionCreator = creator;
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

        public void Reprint(Canvas target)
        {
            Image img = this.shopUI;
            target.Children.Remove(this);

            Canvas.SetTop(this, this.ShopModel.Y);
            Canvas.SetLeft(this, this.ShopModel.X);
            this.Width = 75;

            this.CreateMenu();

            target.Children.Add(this);
        }
        public void Reprint(Object targetPanel)
        {
            Image img = this.shopUI;
            Canvas target = targetPanel as Canvas;
            target.Children.Remove(this);

            Canvas.SetTop(this, this.ShopModel.Y);
            Canvas.SetLeft(this, this.ShopModel.X);
            this.Width = 75;

            this.CreateMenu();

            target.Children.Add(this);
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
            this.SimulationModel.Shops.Remove(this.ShopModel);
            Image img = this.shopUI;
            Panel _parent = (Panel)VisualTreeHelper.GetParent(img);
            _parent.Children.Remove(img);

            //remove connections
            List<Connection> connections = this.SimulationModel.Connections.Where(c => c.ActorA == this.ShopModel || c.ActorB == this.ShopModel).ToList();
            foreach(Connection conn in connections)
            {
                this.SimulationModel.Connections.Remove(conn);
                this.ConnectionCreator.ConnectionDictionary[conn].RemoveLineUI();
                this.ConnectionCreator.ConnectionDictionary.Remove(conn);
            }
        }

        public void settings_Click(object sender, RoutedEventArgs e)
        {
            //backup
            EnumTypes.ShopOptions option = this.ShopModel.Option;

            double init = this.ShopModel.InitialAmount;

            double sqq = this.ShopModel.Policy_sq_q;
            double sqs = this.ShopModel.Policy_sq_s;

            double rsr = this.ShopModel.Policy_rS_r;
            double rss = this.ShopModel.Policy_rS_S;

            double rssr = this.ShopModel.Policy_rsS_r;
            double rsss = this.ShopModel.Policy_rsS_s;
            double rssS = this.ShopModel.Policy_rsS_Sbig;

            var w = new ShopSettingsWindow(this.ShopModel);
            if (w.ShowDialog() != true)
            {
                this.ShopModel.Option = option;

                this.ShopModel.InitialAmount = init;

                this.ShopModel.Policy_sq_q = sqq;
                this.ShopModel.Policy_sq_s = sqs;

                this.ShopModel.Policy_rS_r = rsr;
                this.ShopModel.Policy_rS_S = rss;

                this.ShopModel.Policy_rsS_r = rssr;
                this.ShopModel.Policy_rsS_s = rsss;
                this.ShopModel.Policy_rsS_Sbig = rssS; 	
            }
        }
        #endregion

        public EnumTypes.UserControlTypes GetUserControlType()
        {
            return EnumTypes.UserControlTypes.ShopUserControl;
        }

        public Actor GetActor()
        {
            return this.ShopModel;
        }

        #region Mouse click
        private void shopUI_MouseUp(object sender, MouseButtonEventArgs e)
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

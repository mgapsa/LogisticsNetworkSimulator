using DataModel;
using LogisticsNetworkSimulator.ConnectionCreators;
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
    /// Interaction logic for SupplierUserControl.xaml
    /// </summary>
    public partial class SupplierUserControl : UserControl, IActorUserControl
    {
        public Supplier SupplierModel { get; set; }
        public SimulationModel SimulationModel { get; set; }
        public ConnectionCreator ConnectionCreator { get; set; }

        private DateTime clickTime;
        private object clickSender;

        public SupplierUserControl()
        {
            InitializeComponent();
            SupplierModel = new Supplier();
        }

        //copy constructor
        public SupplierUserControl(SupplierUserControl supplier, SimulationModel model, ConnectionCreator creator)
        {
            InitializeComponent();
            this.supplierUI.Height = supplier.supplierUI.Height;
            this.supplierUI.Width = supplier.supplierUI.Height;
            this.SupplierModel = new Supplier(supplier.SupplierModel);
            this.SimulationModel = model;
            this.ConnectionCreator = creator;
        }

        //create constructor
        public SupplierUserControl(Supplier supplier, SimulationModel model, ConnectionCreator creator)
        {
            InitializeComponent();
            this.SupplierModel = supplier;
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
                data.SetData("Double", supplierUI.Height);
                data.SetData("UserControlType", DataModel.EnumTypes.UserControlTypes.SupplierUserControl);
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
            Canvas.SetTop(this, this.SupplierModel.Y);
            Canvas.SetLeft(this, this.SupplierModel.X);
            this.Width = 75;

            this.CreateMenu();

            target.Children.Add(this);
        }

        public void printOnTarget(Canvas target, Point position)
        {
            this.Width = 75;
            Canvas.SetTop(this, position.Y - this.Width / 2);
            Canvas.SetLeft(this, position.X - this.Width / 2);
            this.SupplierModel.X = position.X - this.Width / 2;
            this.SupplierModel.Y = position.Y - this.Width / 2;

            this.CreateMenu();

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

        public void Reprint(Canvas target)
        {
            Image img = this.supplierUI;
            target.Children.Remove(this);

            Canvas.SetTop(this, this.SupplierModel.Y);
            Canvas.SetLeft(this, this.SupplierModel.X);
            this.Width = 75;

            this.CreateMenu();

            target.Children.Add(this);
        }

        public void Reprint(Object targetPanel)
        {
            Image img = this.supplierUI;
            Canvas target = targetPanel as Canvas;
            target.Children.Remove(this);

            Canvas.SetTop(this, this.SupplierModel.Y);
            Canvas.SetLeft(this, this.SupplierModel.X);
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

            this.ContextMenu = pMenu;
        }

        public void delete_Click(object sender, RoutedEventArgs e)
        {
            this.SimulationModel.Suppliers.Remove(this.SupplierModel);
            Image img = this.supplierUI;
            Panel _parent = (Panel)VisualTreeHelper.GetParent(img);
            _parent.Children.Remove(img);

            List<Connection> connections = this.SimulationModel.Connections.Where(c => c.ActorA == this.SupplierModel || c.ActorB == this.SupplierModel).ToList();
            foreach (Connection conn in connections)
            {
                this.SimulationModel.Connections.Remove(conn);
                this.ConnectionCreator.ConnectionDictionary[conn].RemoveLineUI();
                this.ConnectionCreator.ConnectionDictionary.Remove(conn);
            }
        }
        #endregion

        public EnumTypes.UserControlTypes GetUserControlType()
        {
            return EnumTypes.UserControlTypes.SupplierUserControl;
        }

        public Actor GetActor()
        {
            return this.SupplierModel;
        }

        private void supplierUI_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.clickSender = sender;
                this.clickTime = DateTime.Now;
            }
        }

        private void supplierUI_MouseUp(object sender, MouseButtonEventArgs e)
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

using DataModel;
using LogisticsNetworkSimulator.ConnectionCreators;
using LogisticsNetworkSimulator.SettingsWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace LogisticsNetworkSimulator.Actors
{
    public class ConnectionUI
    {
        //tutaj wszystko ziwazane z rysowanie
        //Linia
        public Line Line { get; set; }
        public Canvas Target { get; set; }
        public Connection Connection { get; set; }
        public SimulationModel SimulationModel { get; set; }
        public ConnectionCreator ConnectionCreator { get; set; }

        public ConnectionUI(SimulationModel model, Canvas target, Connection connection, ConnectionCreator connCreator)
        {
            Line = new Line();
            this.SimulationModel = model;
            this.Target = target;
            this.Connection = connection;
            this.ConnectionCreator = connCreator;
        }

        public void PrintOnTarget()
        {
            Line.Stroke = System.Windows.Media.Brushes.Black;
            Line.X1 = Connection.ActorA.X + 45;
            Line.Y1 = Connection.ActorA.Y + 45;

            Line.X2 = Connection.ActorB.X + 45;
            Line.Y2 = Connection.ActorB.Y + 45;
            Line.HorizontalAlignment = HorizontalAlignment.Left;
            Line.VerticalAlignment = VerticalAlignment.Center;
            Line.StrokeThickness = 4;

            CreateMenu();

            Target.Children.Add(Line);
        }

        #region Menu
        public void CreateMenu()
        {
            ContextMenu pMenu = new ContextMenu();

            MenuItem deleteItem = new MenuItem();
            deleteItem.Header = "Delete";
            deleteItem.Click += new RoutedEventHandler(Delete_Click);
            pMenu.Items.Add(deleteItem);

            if(this.Connection.ConnectionType != EnumTypes.ConnectionTypes.ShopToBuyer)
            {
                MenuItem settingsItem = new MenuItem();
                settingsItem.Header = "Change properties";
                settingsItem.Click += new RoutedEventHandler(Settings_Click);
                pMenu.Items.Add(settingsItem);
            }

            Line.ContextMenu = pMenu;
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            this.SimulationModel.Connections.Remove(this.Connection);
            this.RemoveLineUI();
            this.ConnectionCreator.ConnectionDictionary.Remove(this.Connection);
        }

        public void Settings_Click(object sender, RoutedEventArgs e)
        {
            double min = this.Connection.MinDelay;
            double max = this.Connection.MaxDelay;
            double usage = this.Connection.Usage;
            var w = new ConnectionSettingsWindow(this.Connection);
            if (w.ShowDialog() != true)
            {
                this.Connection.MinDelay = min;
                this.Connection.MaxDelay = max;
                this.Connection.Usage = usage;
            }
        }
        #endregion

        public void RemoveLineUI()
        {
            Target.Children.Remove(this.Line);
        }

        public void Reprint()
        {
            this.RemoveLineUI();
            this.PrintOnTarget();
        }
    }
}

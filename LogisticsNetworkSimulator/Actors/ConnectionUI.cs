using DataModel;
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
        public SimulationModel SimmulationModel { get; set; }

        public ConnectionUI(SimulationModel model, Canvas target, Connection connection)
        {
            Line = new Line();
            this.SimmulationModel = model;
            this.Target = target;
            this.Connection = connection;
        }

        public void PrintOnTarget()
        {
            Line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            Line.X1 = Connection.ActorA.X + 30;
            Line.Y1 = Connection.ActorA.Y + 30;

            Line.X2 = Connection.ActorB.X + 30;
            Line.Y2 = Connection.ActorB.Y + 30;
            Line.HorizontalAlignment = HorizontalAlignment.Left;
            Line.VerticalAlignment = VerticalAlignment.Center;
            Line.StrokeThickness = 4;

            Target.Children.Add(Line);
        }
    }
}

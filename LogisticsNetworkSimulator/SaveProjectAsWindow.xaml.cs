using DataModel;
using Services;
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
using System.Windows.Shapes;

namespace LogisticsNetworkSimulator
{
    /// <summary>
    /// Interaction logic for SaveAsWindow.xaml
    /// </summary>
    public partial class SaveAsWindow : Window
    {
        public SaveAsWindow(SimulationModel model)
        {
            InitializeComponent();
            this.Model = model;
            DataContext = model.Project;
        }

        public SimulationModel Model { get; set; }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Project.Name.Trim() != "")
            {
                try
                {
                    new SimulationModelService().Save(Model);
                    MessageBox.Show("Project saved!");
                    DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Project with given name already exists!");
                }
            }
            else
            {
                MessageBox.Show("Name cannot be empty");
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

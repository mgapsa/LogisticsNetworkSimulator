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
using System.Windows.Shapes;

namespace LogisticsNetworkSimulator
{
    /// <summary>
    /// Interaction logic for OpenProjectWindow.xaml
    /// </summary>
    public partial class OpenProjectWindow : Window
    {
        public Project Project { get; set; }

        public OpenProjectWindow()
        {
            InitializeComponent();
            ProjectsListUI ui = new ProjectsListUI();
            ui.InitializeComponent();
            ProjectsList.Content = ui;
            //<app:ProjectsListUI Grid.Column="1" Grid.ColumnSpan="3" Grid.Row="1" Name="ProjectsList"></app:ProjectsListUI>
        }

        public void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectsListUI ui = ProjectsList.Content as ProjectsListUI;
            Project = ui.projectsList.SelectedItem as Project;
            if(Project == null)
            {
                MessageBox.Show("Please choose project");
            }
            else
            {
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

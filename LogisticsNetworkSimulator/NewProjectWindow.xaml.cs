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
using Services;

namespace LogisticsNetworkSimulator
{
    /// <summary>
    /// Interaction logic for NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : Window
    {
        public NewProjectWindow()
        {
            InitializeComponent();
            Project = new Project();
            DataContext = Project;
        }

        public Project Project { get; set; }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Project.Name != null && Project.Name.Trim() != "")
            {
                Project.Name = NameTextBox.Text.Trim();
                Project.Description = DescriptionTextBox.Text.Trim();
                try
                {
                    new ProjectService().AddProject(Project);
                    MessageBox.Show("New Project created!");
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

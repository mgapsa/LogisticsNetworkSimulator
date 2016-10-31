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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogisticsNetworkSimulator
{
    /// <summary>
    /// Interaction logic for ProjectsListUI.xaml
    /// </summary>
    public partial class ProjectsListUI : UserControl
    {
        private List<Project> baseProjectList;
        private List<Project> projectList;
        private OpenProjectWindow parentWindow;

        public ProjectsListUI()
        {
            InitializeComponent();
            ProjectSearchModel model = new ProjectSearchModel();
            SearchModel = model;
            Task task = Task.Run((Action)getProjectsInBackground);
        }

        public ProjectSearchModel SearchModel
        {
            //if I would add CompleteProduct class(whicg will contain cat string) setting category would not be necessary
            //I have chosen to base only on this siple objects so here I have to get category 
            //of product from database.
            get { return DataContext as ProjectSearchModel; }
            set
            {
                DataContext = value;
            }
        }

        private void getProjectsInBackground()
        {
            try
            {
                baseProjectList = new ProjectService().GetAllProjects().ToList();
                projectList = baseProjectList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
            Dispatcher.Invoke(() =>
            {
                projectsList.ItemsSource = projectList;
            });
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (baseProjectList != null)
            {
                projectList = new ProjectSearchController().Filter(baseProjectList, SearchModel.Name).ToList();
                projectsList.ItemsSource = projectList;
            }
        }

        private void projectsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if(parentWindow != null)
            {
                OpenProjectWindow parentOpenWindow = parentWindow as OpenProjectWindow;
                parentOpenWindow.OkButton_Click(this,null);
            }
        }

    }
}

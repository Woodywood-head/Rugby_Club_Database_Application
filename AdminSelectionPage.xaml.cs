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

namespace StevenWoodheadGradedUnit
{
    /// <summary>
    /// Interaction logic for AdminSelectionPage.xaml
    /// </summary>
    /// Steven Woodhead
    /// HND Sotware Development
    /// Graded Unit
    /// 20160893
    public partial class AdminSelectionPage : Window                        // A Class to set up the Admins options of where they can navigate to
    {                                                                       
        public AdminSelectionPage()                                         // A function to set up the current page
        {                                                                   
            InitializeComponent();                                          // build the page
        }                                          
        private void btnAddMember_Click(object sender, RoutedEventArgs e)   //  Button click method to take Admin to the AddEditMember page
        {                                                                   
            AddEditMember addEditMember = new AddEditMember();              //  Set up a new instance of AddEditMember
            addEditMember.Show();                                           //  Show new page
            this.Close();                                                   //  Close current page
        }                                                                    
        private void btnAddUser_Click(object sender, RoutedEventArgs e)     //  Button click method to take Admin to the AddNewUser page
        {                                                                   
            //Solution -- the pasge is called AdminOptions.                 
            AddNewUser options1 = new AddNewUser();                         //  Set up a new instance of AddNewUser
            options1.Show();                                                //  Show new page
            this.Close();                                                   //  Close current page
        }                                                                   
        private void btnBack_Click(object sender, RoutedEventArgs e)        //  Button click method to take Admin to the MainWindow page
        {                                                                    
            MainWindow mainWindow = new MainWindow();                       //  Set up a new instance of MainWindow
            mainWindow.Show();                                              //  Show new page
            this.Close();                                                   //  Close current page
        }
    }
}

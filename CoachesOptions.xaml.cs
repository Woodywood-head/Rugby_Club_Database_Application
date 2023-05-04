using System;
using System.Collections;
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
using StevenWoodheadGradedUnit._classes;

namespace StevenWoodheadGradedUnit
{
    /// <summary>
    /// Interaction logic for CoachesOptions.xaml
    /// </summary>
    /// Steven Woodhead
    /// HND Sotware Development
    /// Graded Unit
    /// 20160893
    public partial class CoachesOptions : Window                                        // A class to set up the coaches option for where they can navigate too
    {                                                                                   
        public string CurrentMember { get; set; }                                       // A new instance of string current member using the program
        public CoachesOptions(string currentMember )                                    // method to set up the page with the current member being passed into this page
        {                                                                               
            InitializeComponent();                                                      // build the page
            CurrentMember = currentMember;                                              //  this CurrentMember is the currentmember being passed into this page
        }                                                               
        private void btnMembers_Click(object sender, RoutedEventArgs e)                 //  Button click method to take coach to the search page
        {                                                                               
            CoachSearchPage coachSearchPage = new CoachSearchPage(CurrentMember);       //  set up a new instance of CoachSearchPage and pass in the CurrentMember string
            coachSearchPage.Show();                                                     //  Show new page
            this.Close();                                                               //  Close current page
        }                                                                                
        private void btnSkills_Click(object sender, RoutedEventArgs e)                  //  Button click method to take coach to the Skills Page
        {                                                                               
            PlayerSkills playerSkills = new PlayerSkills(CurrentMember);                //  set up a new instance of PlayerSkills page and pass in the CurrentMember string
            playerSkills.Show();                                                        //  Show new page
            this.Close();                                                               //  Close current page
        }                                                                                
        private void btnLogOut_Click(object sender, RoutedEventArgs e)                  //
        {                                                                               
            MainWindow mainWindow = new MainWindow();                                   //  Set up a new instance of MainWindow
            mainWindow.Show();                                                          //  Show new page
            this.Close();                                                               //  Close current page
        }
    }
}

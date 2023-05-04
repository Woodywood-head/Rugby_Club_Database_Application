using System;
using System.Data.SqlClient;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// Steven Woodhead 
    /// HND Software Development
    /// 20160893
    public partial class MainWindow : Window                                        // Main menu class where users can sign into the program
    {                                                                               
                                                                                    
        public string UserNameInput                                                 // string set up for user name
        {                                                                           
            get { return txtUserName.Text; }                                        //  get text box input
            set { txtUserName.Text = value; }                                       //  set textbox input as value
        }                                                                           
                                                                                    
        public string PasswordInput2                                                //  string set up for password
        {                                                                           
            get { return txtPasswordBox.Password; }                                 //  get password box input
            set { txtPasswordBox.Password = value; }                                //  set password box input as value
        }                                                                           
                                                                                    
        public MainWindow()                                                         //  set up of the main page
        {                                                                           
            InitializeComponent();                                                  //  build the page
            txtUserName.Clear();                                                    //  clears user name
            txtPasswordBox.Clear();                                                 //  clears password box
            txtUserName.Focus();                                                    //  focuses on username
        }                                                                           
                                                                                    
        public bool IsValidUser()                                                   //  boolean checker to check if user is valid
        {                                                                           
            string usernameChecker = txtUserName.Text;                              //  srting set up of username input
            string passwordChecker = txtPasswordBox.Password;                       //  string set up of password input
            return LoginValidation.isValidLogin(usernameChecker, passwordChecker);  //  pass both string to LoginValidation class to validate if log in is valid
        }                                                                           
                                                                                    
        private void SignIn()                                                       //  method to check whos signing in
        {                                                                           
            if (IsValidUser())                                                      //  if the user is a valid user continue else skip
            {                                                                       
                if (txtUserName.Text == "Admin")                                    //  if the user name is Admin then continue esle go to the next condition
                {                                                                   
                                                                                    
                    AdminSelectionPage options = new AdminSelectionPage();          //  if correct then open new instance of AdminSelectionPage
                    options.Show();                                                 //  show new page
                    this.Close();                                                   //  close this page
                }                                                                   
                else                                                                //  if (txtUserName.Text != "Admin") //  code removed to test.
                {         
                    CoachesOptions options2 = new CoachesOptions(UserNameInput);    //  if correct then open new instance of CoachesOptions page passing in the username
                    options2.Show();                                                //  show new page
                    this.Close();                                                   //  close this page                                           
                }                                                                   
            }                                                                       
            else                                                                    // If users login details are not correct then do this
            {                                                                       
                                                                                    
                MessageBox.Show("Password and Username do not match");              //  error message to show password and username do not match
                txtUserName.Clear();                                                //  clear username
                txtPasswordBox.Clear();                                             //  clear password
                txtUserName.Focus();                                                //  focus on username box
            }                                                                       
        }                                                                                         
        private void btnSignIn_Click(object sender, RoutedEventArgs e)              //  button click event to sign in
        {                                                                               
            SignIn();                                                               //  go to SignIn method
        }                                                                           
    }                                                                               
}

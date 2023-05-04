using System;
using System.Data.SqlClient;
using System.Data;

using System.IO;
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
using System.Windows.Markup;
using StevenWoodheadGradedUnit._classes;

namespace StevenWoodheadGradedUnit
{
    /// <summary>
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    /// Steven Woodhead
    /// HND Sotware Development
    /// Graded Unit
    /// 20160893
    public partial class AddNewUser : Window
    {
        public AddNewUser()
        {

            InitializeComponent();                                                  //  Initialize the form its XAML
            printList();                                                            //  Print a list of the stored Users from tbl_Users Table
            txtUserName.Focus();                                                    //  Focuses on the Username text box txtUserName

        }
        private void printList()                                                    //  Method called printList that will print all stored Usernames form the datatbase in table tbl_Users
        {
            string queryLoadDB = "SELECT UserName FROM tbl_Users";                  //  String queryLoadDB writen in SQL to retrive User Names from table tbl_Users
            DataTable table = _classes.classDatabase.Fill_Data_Table(queryLoadDB);  //  Fill the data table with all the usernamse using the query query_LoadDB 
            foreach (DataRow row in table.Rows)                                     //  Loop through each row in the datatable returned
            {
                lstCoachesAdded.Items.Add(row["UserName"].ToString());              // Add User Name from current row in table to list box lstCoachesAdded, repeat till loop ends
            }
        }
        private int checkButtons()                                                  // method to retunr and intiger value depending on what Checkbox is checked
        {                                                                                                                                                     
            if (chkUnder16.IsChecked == true) { return 1; }                         //  If Checkbox Under 16 is checked return value 1
            else if (chkUnder18.IsChecked == true) { return 2; }                    //  If Checkbox Under 18 is checked return value 1
            else if (chkUnder21.IsChecked == true) { return 3; }                    //  If Checkbox Under 21 is checked return value 1
            else if (chkSeniors.IsChecked == true) { return 4; }                    //  If Checkbox Seniros is checked return value 1
            else                                                                                                                                                              
            {                                                                                                                                                                 
                MessageBox.Show("Please Select A Squad");                           // Else return Error message to say Choose a Squade
                return 5;                                                           //  Return 5
            }                                                                                                                                                 
        }

        public string UserNameInput                                                                                     // Propetry to get and set the text in User Name text bpx
        {
            get { return txtUserName.Text; }                                                                            //  get the text from txtUserName text box
            set { txtUserName.Text = value; }                                                                           //  set the text from the txtUserName text box
        }
        public string PasswordInput                                                                                     //  Property to get and set the text in the Password password box
        {
            get { return txtPasswordBox.Password; }                                                                     //  get the password from the txtPasswordBox password box
            set { txtPasswordBox.Password = value; }                                                                    //  set the password from the txtPasswordBox password box
        }
        private bool isValidLogin()                                                                                     // boolean Mathod to check if the login details are valid
        {
            string UserNameEntry = UserNameInput;                                                                       //  Sets up a new string entered by user from user name text box UseNameInput
            return LoginValidation.isNewUser(UserNameEntry);                                                            //  passed the Username to Class LoginValidation to perform the validation check, returning true or false if User is in database or new.
        }
        private void updateDatabase()                                                                                   // Method to update  the database with new user log in details
        {
            string queeryLoadDB = "SELECT * FROM tbl_Users";                                                            //  SQL query Selecting all Records from tbl_Users
            _classes.classDatabase.Fill_Data_Table(queeryLoadDB);                                                       //  Filles the data table with the records returned from the table tbl_Users
        }
        private void add_User_Database()                                                                                // A method to add User to Database, return conditions for if name or password is blank or if name is in database already
        {
            int checkbuttonUpdate = checkButtons();                                                                     // stores the value returned from method checkButtons in the integer checkButtonUpdate
            if (checkbuttonUpdate == 5) { return; }                                                                     // if returned value from method checkButtons is 5 then exit this method.
            _classes.classDatabase.Get_Sql_Connection();                                                                //  Open the SQL Connection by using the method in the ClassDatabase Class
            int CheckButton = checkbuttonUpdate;                                                                        //  Assign a value to CheckButton
            string UserNameEntry = UserNameInput;                                                                       //  Assign a value to UserNameEntry
            string PasswordEntry = PasswordInput;                                                                       //  Assign a value to PasswordEntry
            if (UserNameEntry.Length <= 0)                                                                              //  Check is Username is empty
            {
                MessageBox.Show("Please Enter User Name");                                                              //  If Empty return MessageBox asking user to input username
                txtUserName.Focus();                                                                                    //  focus on username text box
            }
            else if (PasswordEntry.Length <= 0)                                                                         //  Check is password box is empty
            {
                MessageBox.Show("Please Enter Password");                                                               //  If Empty return MessageBox asking user to input password
                txtPasswordBox.Focus();                                                                                 //  Focus on password text box
            }
            else if (isValidLogin())                                                                                    //  if isValidLogin method returns true then do the next function
            {
                string query_UsersADD = "INSERT INTO tbl_Users ([UserName], [Password], [Squad]) VALUES('"              //  Set up a string SQL query called query_UserAdd to insert data in to the table tbl_Users
                    + UserNameEntry + "','" + PasswordEntry + "','" + CheckButton + "')";                               //  this is still the sql query called query_UserADD
                SqlCommand sqlCommand = new SqlCommand(query_UsersADD, _classes.classDatabase.Get_Sql_Connection());    //  Set up the query usinf SQL Command and open Server connection
                sqlCommand.ExecuteNonQuery();                                                                           //  https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?view=dotnet-plat-ext-8.0
                updateDatabase();                                                                                       //  Update the sdatabase with this method
            }
            else                                                                                                        // iff all other dirrections fail to perform There is already a user by that name in the database
            {
                MessageBox.Show("User Already Exists In Database, Please Choose a Different One");                      // Error Message showing that User Already exists
                txtUserName.Clear();                                                                                    //  Clears Username text box
                txtPasswordBox.Clear();                                                                                 //  Clears Password box
                txtUserName.Focus();                                                                                    //  Focuses on username box
            }
            chkSeniors.IsChecked = false;                                                                               //  unchecks Seniors Checkbox
            chkUnder16.IsChecked = false;                                                                               //  unchecks Under 16's Checkbox
            chkUnder18.IsChecked = false;                                                                               //  unchecks Under 18's Checkbox
            chkUnder21.IsChecked = false;                                                                               //  unchecks Under 21's Checkbox
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)                                        // This Method is called when "Create New User" button (btnAddUser) is clicked 
        {
            add_User_Database();                                                                               // Call the method add_User_Database to add user to database table tbl_Users
            lstCoachesAdded.Items.Clear();                                                                     // Clears the list box lstCoachesAdded
            txtUserName.Clear();                                                                               // Clears the User Name box txtUserName
            txtUserName.Focus();                                                                               // Focuses on the User Name box txtUserName
            txtPasswordBox.Clear();                                                                            // Clears the Password Box txtPasswordBox
            printList();                                                                                       // Calls the Method printList to print and updated list of all the users in the database table tbl_Users
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)                                            // This Method is called when the Back Button is Clicked
        {
            AdminSelectionPage AdminHome = new AdminSelectionPage();                                            // Create a new instance of AdminSelectionPage
            AdminHome.Show();                                                                                   // Open AdminSelectionPage
            this.Close();                                                                                       // Close current window
        }

        private void chkUnder16_Checked(object sender, RoutedEventArgs e)                                       // This method is called when the Under 16's Checkbox is Checked
        {
            if (chkUnder16.IsChecked == true)                                                                   // Check If Under 16's Checkbox is checked then uncheck the following boxes
            {
                chkUnder18.IsChecked = false; chkUnder21.IsChecked = false; chkSeniors.IsChecked = false;       // If Under 16's Checkbox is checked, uncheck Under 18, Under 21, and Seniors Checkboxes
            }
        }

        private void chkUnder18_Checked(object sender, RoutedEventArgs e)                                       // This method is called when the Under 18's Checkbox is Checked
        {
            if (chkUnder18.IsChecked == true)                                                                   // Check If Under 18's Checkbox is checked then uncheck the following boxes
            {
                chkUnder16.IsChecked = false; chkUnder21.IsChecked = false; chkSeniors.IsChecked = false;       // If Under 18's Checkbox is checked, uncheck Under 16, Under 21, and Seniors Checkboxes
            }
        }

        private void chkUnder21_Checked(object sender, RoutedEventArgs e)                                       // This method is called when the Under 21's Checkbox is Checked
        {
            if (chkUnder21.IsChecked == true)                                                                   // Check If Under 21's Checkbox is checked then uncheck the following boxes
            {
                chkUnder16.IsChecked = false; chkUnder18.IsChecked = false; chkSeniors.IsChecked = false;       // If Under 21's Checkbox is checked, uncheck Under 16, Under 18, and Seniors Checkboxes
            }
        }

        private void chkSeniors_Checked(object sender, RoutedEventArgs e)                                       // This method is called when the Seniors Checkbox is Checked
        {
            if (chkSeniors.IsChecked == true)                                                                   // Check If Seniors Checkbox is checked then uncheck the following boxes
            {
                chkUnder16.IsChecked = false; chkUnder18.IsChecked = false; chkUnder21.IsChecked = false;       // If Seniors Checkbox is checked, uncheck Under 16, Under 18, and Under 21 Checkboxes
            }
        }
    }
}

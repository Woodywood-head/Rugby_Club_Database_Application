
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddEditMember.xaml
    /// </summary>
    /// Steven Woodhead
    /// HND Sotware Development
    /// Graded Unit
    /// 20160893
    public partial class AddEditMember : Window                                                          // Class set up to add new members to the database
    {                                                                                                    
        public AddEditMember()                                                                           // page set up commands
        {                                                                                                
            InitializeComponent();                                                                       // build the page
            txtConsent.Visibility = Visibility.Hidden;                                                   // hide the consent text box
            lableParental.Visibility = Visibility.Hidden;                                                // hide the labe for consent
        }                                                                                                
        private string DOB { get; set; }                                                                 // string get and set for date of birth
        private string FullName { get; set; }                                                            // string get and set for full name
        private string Email { get; set; }                                                               // string get and set for email
        private string SRUNumb { get; set; }                                                             // string get and set for sru number
        private string ContactNumb { get; set; }                                                         // string get and set for contact number
        private string ICENumb { get; set; }                                                             // string get and set for emergency number
        private string Parental { get; set; }                                                            // string get and set for consent
                                                                                                         
                                                                                                         
        private bool IsNumber(string input)                                                              // boolean regex for matching numbers
        {                                                                                                
            return Regex.IsMatch(input, @"^\d+$");                                                       // regex to check if entry is only numbers
        }                                                                                                
        private bool IsEmail(string email)                                                               // boolean regex for matching emails
        {                                                                                                
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");            // regex to check if entry is in email standard form
        }                                                                                                
        private bool IsDate(string date)                                                                 // boolean checker to date format
        {                                                                                                
            return Regex.IsMatch(date, @"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$");           // regex to check if date is in correct format dd/mm/yyyy
        }                                                                                                
        private bool IsConsent()                                                                         // boolean checker to see if the consent text box contails a y or Y
        {                                                                                                
            if (txtConsent.Text == "Y" || txtConsent.Text == "y") { return true; }                       // if text box contains a Y or y return true
            else { return false; }                                                                       // if not return false
        }                                                                                                
        public string LoadTable()                                                                        // method to return the correct query string with condition of what check box is ticked
        {                                                                                                
            if (chkUn16.IsChecked == true)                                                               // if under 16 is selected return this string query
            { return "SELECT * FROM tbl_Members16"; }                                                    
            else if (chkUn18.IsChecked == true)                                                          // if under 18 is selected return this string query
            { return "SELECT * FROM tbl_Members18"; }                                                    
            else if (chkUn21.IsChecked == true)                                                          // if under 21 is selected return this string query
            { return "SELECT * FROM tbl_Members21"; }                                                       
            else if (chkSenior.IsChecked == true) { return "SELECT * FROM tbl_Members"; }                // if seniors is selected return this string query
            else { MessageBox.Show("Load Table Error"); return null; }                                   // if non is selected return this message box and return null
        }
        public string QueryAddMember()                                                                  // add member query method depending on what check box is selected
        {
            if (chkUn16.IsChecked == true)                                                                       // if under 16 is selected return this string query
            {
                return "INSERT INTO tbl_Members16 ([IdMember],[FullName],[d_o_b],[Email],[Contact],[Ice],[Parental])VALUES" +
                                             "('" + SRUNumb + "','" + FullName + "','" + DOB + "','" + Email + "','" + ContactNumb + "','" + ICENumb + "','" + Parental + "')";
            }   
            else if (chkUn18.IsChecked == true)                                                                  // if under 18 is selected return this string query
            {
                return "INSERT INTO tbl_Members18 ([IdMember],[FullName],[d_o_b],[Email],[Contact],[Ice])VALUES" +
                                           "('" + SRUNumb + "','" + FullName + "','" + DOB + "','" + Email + "','" + ContactNumb + "','" + ICENumb + "')";                              // **** I thing i could shorten this by using || comparisons
            }
            else if (chkUn21.IsChecked == true)                                                                 // if under 21 is selected return this string query
            {
                return "INSERT INTO tbl_Members21 ([IdMember],[FullName],[d_o_b],[Email],[Contact],[Ice])VALUES" +
                                           "('" + SRUNumb + "','" + FullName + "','" + DOB + "','" + Email + "','" + ContactNumb + "','" + ICENumb + "')";
            }
            else if (chkSenior.IsChecked == true)                                                                // if seniors is selected return this string query
            {
                return "INSERT INTO tbl_Members ([IdMember],[FullName],[d_o_b],[Email],[Contact],[Ice])VALUES" +
                                           "('" + SRUNumb + "','" + FullName + "','" + DOB + "','" + Email + "','" + ContactNumb + "','" + ICENumb + "')";
            }
            else { MessageBox.Show("Query add Member Error"); return null; }                                // if non is selected return this message box and return null
        }
        public string QuerySearchMember()                                                                                                  //   Method to set up the search button depending on what check box is selected
        {                                                                                                                                  
            if (chkUn16.IsChecked == true) { return "SELECT * FROM tbl_Members16"; }                                                       //   if under 16 is selected return this string query
            else if (chkUn18.IsChecked == true) { return "SELECT * FROM tbl_Members18"; }                                                  //   if under 18 is selected return this string query
            else if (chkUn21.IsChecked == true) { return "SELECT * FROM tbl_Members21"; }                                                  //   if under 21 is selected return this string query
            else if (chkSenior.IsChecked == true) { return "SELECT * FROM tbl_Members"; }                                                  //   if seniors is selected return this string query
            else { return null; }                                                                                                          //   if non is selected return this message box and return null
        }                                                                                                                                  
                                                                                                                                              
        private string printSearchMember()                                                                                                 //  A method to print the searched member(s) to the list box  
        {                                                                                                                                  
            string SearchBoxChecker = txtSearchMember.Text;                                                                                //   set up string from what user enters into search box
                                                                                                                                           
            string query = "";                                                                                                             // set up string query to be empty so it can be updated next
            if(QuerySearchMember() != null)                                                                                                //   if the query search member return a string then continue else skipp
            {                                                                                                                              
               string queryLoadDB = QuerySearchMember() ;                                                                                  //   save new query as new string
                DataTable table = classDatabase.Fill_Data_Table(queryLoadDB);                                                              //   pass new string query to the fill Data Table method in classDatabase class
                foreach(DataRow row in table.Rows)                                                                                         //   loop through each row in new DataTable
                {                                                                                                                          
                    lstMemberPersonal.Items.Add(row["FullName"].ToString());                                                               //   print fullname in listbox
                    lstMemberPersonal.Items.Add(row["Email"].ToString());                                                                  //   print email in listbox
                    lstMemberPersonal.Items.Add(row["Ice"].ToString());                                                                    //   print emergency contact number in list box
                }                                                                                                                          
            }                                                                                                                              
            else if (QuerySearchMember() == null)                                                                                          // if the method QuerySearchMember returns null value then do these steps
            {                                                                                                                              
                string[] tables = { "tbl_Members16", "tbl_Members18", "tbl_Members21", "tbl_Members" };                                    //   set up an array of string that match the different members tables in database
                query = "SELECT * FROM " + tables[0] + " WHERE FullName = @SearchBoxChecker OR IdMember =@SearchBoxChecker";               //   set up new query with the array added into the SQL query   *** This could be deleted?? will need to test
                                                                                                                                           
                for (int i = 0; i < tables.Length; i++)                                                                                    //   use for loop to iterate through the array with query
                {                                                                                                                          
                    query = "SELECT * FROM " + tables[i] + " WHERE FullName = @SearchBoxChecker OR IdMember =@SearchBoxChecker";           //   use query with for loop to loop through array returning new query to be checked for each table 
                    DataTable table = classDatabase.Get_MemberSearch_Table(query, SearchBoxChecker);                                       //   set up a new datatable and pass query and string from searchbox to get_memberSearch method in class classDatabase
                    foreach (DataRow row in table.Rows)                                                                                    //   foreach loop to loop through each row in new datatable
                    {                                                                                                                      
                        lstMemberPersonal.Items.Add(row["FullName"].ToString());                                                           //   print returned value from FullName Row to listbox
                        lstMemberPersonal.Items.Add(row["Email"].ToString());                                                              //   print returned value from Email Row to listbox
                        lstMemberPersonal.Items.Add(row["Ice"].ToString());                                                                //   print returned value from Ice Row to listbox
                    }                                                                                                                      
                }                                                                                                                          
            }                                                                                                                              
            if (lstMemberPersonal.Items.Count == 0)                                                                                        //   use count of number of items returned from datatale, if it is zero then do this
            {                                                                                                                              
                MessageBox.Show("There Are No Members By That Name", "Error Encountered");                                                 //   error message to tell user that there are no members by that searched value (name or SRU number)
            }                                                                                                                              
                return query;                                                                                                              // return the query                                                                                              
        }                                                                                                                                  

        public bool isMember()                                                                             // Boolean checker to check if the sru number matches that that is in the database
        {                                                                                                  
            string IdMemberChecker = txtSRU.Text;                                                          //   set up a new string with the value in the sru textbox
            return LoginValidation.isValidMember(IdMemberChecker);                                         //   pass this string into the loginvalidation class using the isValidMember method
        }                                                                                                  
        public bool memberChecker()                                                                        // boolean checker to check if the searched member is a member
        {                                                                                                  
            string SearchBoxChecker = txtSearchMember.Text;                                                //   set up a new string with the value in the search member textbox
            return LoginValidation.isMemberSearch(SearchBoxChecker);                                       //   pass this string into the loginvalidation class using the isMemberSearch method
        }                                                                                                  
        public void clearForm()                                                                            //   Method to clear all the page
        {                                                                                                  
            txtConsent.Clear();                                                                            //   clear consent
            txtContactNumb.Clear();                                                                        //   clear contact num
            txtDOB.Clear();                                                                                //   clear dob
            txtEmail.Clear();                                                                              //   clear email
            txtICENumb.Clear();                                                                            //   clear emegency
            txtMemberName.Clear();                                                                         //   clear name
            txtSRU.Clear();                                                                                //   clear sru number
            txtSearchMember.Clear();                                                                       //   clear search box
        }  
        private void btnSaveMember_Click(object sender, RoutedEventArgs e)                                 //   click event for when the save button is clicked
        {                                                                                                  
            this.FullName = txtMemberName.Text;                                                            //   set full name to text box value
            this.DOB = txtDOB.Text;                                                                        //   set dob to text box value
            this.Email = txtEmail.Text;                                                                    //   set email to text box value
            this.SRUNumb = txtSRU.Text;                                                                    //   set sru numb to text box value
            this.ContactNumb = txtContactNumb.Text;                                                        //   set contact numb to text box value
            this.ICENumb = txtICENumb.Text;                                                                //   set ice numb to text box value
            this.Parental = txtConsent.Text;                                                               //   set consent to text box value

            if (FullName.Length > 0)                                                                       //   if text box is not empty continue 
            {  
                if (IsDate(DOB))                                                                           //   if method is date returns true continue
                {                                                                                          
                    if (IsNumber(SRUNumb) && SRUNumb.Length == 7)                                          //   if method isNumebr returns true for sru number and is 7 chars long continue
                    {                                                                                      
                        if (IsNumber(ContactNumb) && ContactNumb.Length == 11)                             //   if method isNumebr returns true for contact number and is 11 chars long continue
                        {                                                                                  
                            if (IsNumber(ICENumb) && ICENumb.Length == 11)                                 //   if method isNumebr returns true for emergency number and is 11 chars long continue
                            {                                                                              
                                if (IsEmail(Email))                                                        //   if method IsEmail returns true for email continue
                                {                                                                          
                                    if (isMember())                                                        //   if method isMember returns true continue
                                    {                                                                      
                                        if (chkUn16.IsChecked == true)                                     //   if checkbox is ticked is true then continue
                                        {                                                                  
                                            if (IsConsent())                                               //   if method IsConsent is true continue
                                            {
                                                string query_AddMember = QueryAddMember();                                                                                                              // new string with query from this method
                                                SqlCommand sqlCommand = new SqlCommand(query_AddMember, _classes.classDatabase.Get_Sql_Connection());                                                   //  set up command passing in query and open connection
                                                sqlCommand.ExecuteNonQuery();                                                                                                                           // execute command
                                                string queryLoadDB = LoadTable();                                                                                                                       //  set uop new string with new query from method
                                                _classes.classDatabase.Fill_Data_Table(queryLoadDB);                                                                                                    //  pass query to classDatabase class fill_Data_Table method
                                                lstMemberPersonal.Items.Clear();                                                                                                                        //  clear list box
                                                lstMemberPersonal.Items.Add(SRUNumb + ",\n" + FullName + ",\n" + DOB + ",\n" + Email + ",\n" + ContactNumb + ",\n" + ICENumb + ",\n" + Parental);       //  add these values from the DataTable to listbox
                                                clearForm();                                                                                                                                            //  clear the form uising this method
                                            }                                                                                                                                                           
                                            else if (txtConsent.Text == "N" || txtConsent.Text == "n")                                                                                                  //  if consent box is n or N show error message box
                                            {                                                                                                                                                           
                                                MessageBox.Show("Before continuing with this members form please get concent");                                                                         //  error message box
                                            }                                                                                                                                                           
                                            else                                                                                                                                                        //  if consent box is empty or other values do next
                                            {                                                                                                                                                           
                                                MessageBox.Show("Please only type Y or N if there has been concent on the form");                                                                       //  error message box
                                                txtConsent.Clear();                                                                                                                                     // clear consent box
                                                txtConsent.Focus();                                                                                                                                     //  focus on consent box
                                            }                                                                                                                                                           
                                        }                                                                                                                                                               
                                        else if (chkUn18.IsChecked == true)                                                                                                                             //  if checkbox is ticked is true then continue
                                        {                                                                                                                                                               
                                            string query_AddMember = QueryAddMember();                                                                                                                  //  new string with query from this method
                                            SqlCommand sqlCommand = new SqlCommand(query_AddMember, _classes.classDatabase.Get_Sql_Connection());                                                       //   set up command passing in query and open connection
                                            sqlCommand.ExecuteNonQuery();                                                                                                                               //  execute command
                                            string queryLoadDB = LoadTable();                                                                                                                           //   set uop new string with new query from method
                                            _classes.classDatabase.Fill_Data_Table(queryLoadDB);                                                                                                        //   pass query to classDatabase class fill_Data_Table method
                                            lstMemberPersonal.Items.Clear();                                                                                                                            //   clear list box
                                            lstMemberPersonal.Items.Add(SRUNumb + ",\n" + FullName + ",\n" + DOB + ",\n" + Email + ",\n" + ContactNumb + ",\n" + ICENumb);                              //   add these values from the DataTable to listbox
                                            clearForm();                                                                                                                                                //   clear the form uising this method
                                                                                                                                                                                                        
                                        }                                                                                                                                                               
                                        else if (chkUn21.IsChecked == true)
                                        {                                                                                                                                                               //  if checkbox is ticked is true then continue
                                            string query_AddMember = QueryAddMember();                                                                                                                  //  new string with query from this method
                                            SqlCommand sqlCommand = new SqlCommand(query_AddMember, _classes.classDatabase.Get_Sql_Connection());                                                       //   set up command passing in query and open connection
                                            sqlCommand.ExecuteNonQuery();                                                                                                                               //  execute command
                                            string queryLoadDB = LoadTable();                                                                                                                           //   set uop new string with new query from method
                                            _classes.classDatabase.Fill_Data_Table(queryLoadDB);                                                                                                        //   pass query to classDatabase class fill_Data_Table method
                                            lstMemberPersonal.Items.Clear();                                                                                                                            //   clear list box
                                            lstMemberPersonal.Items.Add(SRUNumb + ",\n" + FullName + ",\n" + DOB + ",\n" + Email + ",\n" + ContactNumb + ",\n" + ICENumb);                              //   add these values from the DataTable to listbox
                                            clearForm();                                                                                                                                                //   clear the form uising this method
                                                                                                                                                                                                        
                                        }                                                                                                                                                               
                                        else if (chkSenior.IsChecked == true)                                                                                                                           //  if checkbox is ticked is true then continue
                                        {                                                                                                                                                               
                                            string query_AddMember = QueryAddMember();                                                                                                                  //  new string with query from this method
                                            SqlCommand sqlCommand = new SqlCommand(query_AddMember, _classes.classDatabase.Get_Sql_Connection());                                                       //   set up command passing in query and open connection
                                            sqlCommand.ExecuteNonQuery();                                                                                                                               //  execute command
                                            string queryLoadDB = LoadTable();                                                                                                                           //   set uop new string with new query from method
                                            _classes.classDatabase.Fill_Data_Table(queryLoadDB);                                                                                                        //   pass query to classDatabase class fill_Data_Table method
                                            lstMemberPersonal.Items.Clear();                                                                                                                            //   clear list box
                                            lstMemberPersonal.Items.Add(SRUNumb + ",\n" + FullName + ",\n" + DOB + ",\n" + Email + ",\n" + ContactNumb + ",\n" + ICENumb);                              //   add these values from the DataTable to listbox
                                            clearForm();                                                                                                                                                //   clear the form uising this method
                                        }                                                                                                                                                               
                                        else                                                                                                                                                            // if no box is ticked then show error box
                                        {
                                            MessageBox.Show("Please select a age catagory for the member you are adding");                                                                              // error box message
                                        }
                                    }
                                    else                                                                  // if member already exist then show error
                                    {
                                        MessageBox.Show("Memeber Already Exists in Database");          // error message for double member entry
                                        txtSRU.Clear();                                                 // clear sru feild
                                        txtSRU.Focus();                                                 // focus on sru feild
                                        return;                                                         //return to program 
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter A Email In Correct Format");              // error message to show email is wron format
                                    txtEmail.Clear();                                                       //  clear email
                                    txtEmail.Focus();                                                       //  focus on email
                                }                                                                           
                            }                                                                               
                            else                                                                            
                            {                                                                               
                                MessageBox.Show("Phone Number Must Be Numeric and 11 Numbers Long");        //  error message to show number is not long enough
                                txtICENumb.Clear();                                                         //  clear text box
                                txtICENumb.Focus();                                                         //  focus on box
                            }                                                                               
                        }                                                                                   
                        else                                                                                
                        {                                                                                   
                            MessageBox.Show("Phone Number Must Be Numeric and 11 Numbers Long");            // error message to show number is not long enough
                            txtContactNumb.Clear();                                                         //  clear box
                            txtContactNumb.Focus();                                                         // focus on box
                        }                                                                                   
                    }                                                                                       
                    else                                                                                    
                    {                                                                                       
                        MessageBox.Show("SRU Number Must Be Numeric And 7 Numbers Long");                   //  error message to show number is not long enough
                        txtSRU.Clear();                                                                     //  clear box
                        txtSRU.Focus();                                                                     //  focus on box
                    }                                                                                       
                }                                                                                           
                else                                                                                        
                {                                                                                           
                    MessageBox.Show("Please Enter the date in this format dd/mm/yyyy");                     // error message to show date is wrong format
                    txtDOB.Clear();                                                                         // clear date
                    txtDOB.Focus();                                                                         // focus on box
                }                                                                                           
            }                                                                                               
            else                                                                                            
            {                                                                                               
                MessageBox.Show("Please Make Sure There is a Full Name in the Text Box");                   //  error message to show name box is empty
                txtMemberName.Focus();                                                                      //  focus on box
            }                                                                                               
            chkUn16.IsChecked = false;                                                                      // unselect check box
            chkUn18.IsChecked = false;                                                                      //unselect check box
            chkUn21.IsChecked = false;                                                                      //unselect check box
            chkSenior.IsChecked = false;                                                                    //unselect check box
        }

        private void chkUn16_Checked(object sender, RoutedEventArgs e)              // event handler to controle the check boxes
        {
            if (chkUn16.IsChecked == true)                                          // if under16 is check then uncheck the rest and show the consent lable and text bo
            {
                txtConsent.Visibility = Visibility.Visible;
                lableParental.Visibility = Visibility.Visible;
                chkUn18.IsChecked = false; 
                chkUn21.IsChecked = false; 
                chkSenior.IsChecked = false;
            }
            else                                                                 // if it is unselected then hid the consent lable and text box
            {
                txtConsent.Visibility = Visibility.Hidden;
                lableParental.Visibility = Visibility.Hidden;
            }
        }

        private void chkUn18_Checked(object sender, RoutedEventArgs e)  // event handler to controle the check boxes
        {
            if (chkUn18.IsChecked == true)                               // if under18 is check then uncheck the other check boxes
            {
                txtConsent.Visibility = Visibility.Hidden;
                lableParental.Visibility = Visibility.Hidden;
                chkUn16.IsChecked = false;
                chkUn21.IsChecked = false;
                chkSenior.IsChecked = false;
            }
        }

        private void chkUn21_Checked(object sender, RoutedEventArgs e)      // event handler to controle the check boxes
        {
            if (chkUn21.IsChecked == true)                                  // if under18 is check then uncheck the other check boxes
            {
                txtConsent.Visibility = Visibility.Hidden;
                lableParental.Visibility = Visibility.Hidden;
                chkUn16.IsChecked = false;
                chkUn18.IsChecked = false;
                chkSenior.IsChecked = false;
            }
        }

        private void chkSenior_Checked(object sender, RoutedEventArgs e)    // event handler to controle the check boxes
        {
            if (chkSenior.IsChecked == true)                                // if under18 is check then uncheck the other check boxes
            {
                txtConsent.Visibility = Visibility.Hidden;
                lableParental.Visibility = Visibility.Hidden;
                chkUn16.IsChecked = false;
                chkUn18.IsChecked = false;
                chkUn21.IsChecked = false;
            }

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)                // button click event handler to open home page
        {   
            AdminSelectionPage adminSelectionPage = new AdminSelectionPage();       // New instance of AdminSelectionPage
            adminSelectionPage.Show();                                              // open page
            this.Close();                                                           // close this page
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)              // button click event handler to log out
        {
            MainWindow mainWindow = new MainWindow();                               // New instance of mainWindow
            mainWindow.Show();                                                      // open page
            this.Close();                                                           // close this page
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)              // button click event handler to control the search button
        {
                lstMemberPersonal.Items.Clear();                                        // clear list box
                memberChecker();                                                        //  run this method
                printSearchMember();                                                    //  run this method
                txtSearchMember.Clear();                                                //  clear text box
                txtSearchMember.Focus();                                                //  clear text box
            chkUn16.IsChecked = false;                                                  //  uncheck check box
            chkUn18.IsChecked = false;                                                  //  uncheck check box
            chkUn21.IsChecked = false;                                                  //  uncheck check box
            chkSenior.IsChecked = false;                                                //  uncheck check box
        }
        private void txtDOB_TextChanged_1(object sender, TextChangedEventArgs e)        // text change event handler to auto input / in the date box
        {
            if (txtDOB.Text.Length == 2 || txtDOB.Text.Length == 5)                     // if statment to read the length of the text being input to dob text box
            {
                txtDOB.Text += "/";                                                     // if length is 2 add / or if length is 5 enter /
                txtDOB.SelectionStart = txtDOB.Text.Length;                              // gets the curent length of the text being entered
            }
        }
    }
}
/*
 * https://stackoverflow.com/questions/14337057/how-to-get-the-new-text-in-textchanged
 */


// line 124 could be deleted *** test later***
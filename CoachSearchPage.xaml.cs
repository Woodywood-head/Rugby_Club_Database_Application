using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
    /// Interaction logic for CoachSearchPage.xaml
    /// </summary>
    /// Steven Woodhead
    /// HND Sotware Development
    /// Graded Unit
    /// 20160893
    public partial class CoachSearchPage : Window
    {
        public string CurrentMember { get; set; }                             // Geter and setter for Current member
        public int CurrentSquad { get; set; }                                 //Getter and setter for Current squad
        public string FullName { get; set; }                                  //Getter and setter for full name
        public string SearchBoxCheker { get; set; }                           //Getter and setter for serch box

        public CoachSearchPage(string currentMember)                                             // set up for coach serch page with current member beign passed in
        {                                                                                        
            InitializeComponent();                                                               // build page
            CurrentMember = currentMember;                                                       // set CurrentMember to value being passed in
            CurrentSquad = CurrentMemberLoggedin(CurrentMember);                                 // set current squad to value from this method
            clearPage();                                                                         // method to clear page
            getSquadNames();                                                                     // method to get squad value
        }                                                                                        //
        public void clearPage()                                                                  // method to clear page
        {                                                                                        
            lstTeam.Items.Clear();                                                               // clear lsit box
            combSearch.Items.Clear();                                                            // clear combo box
            combSearch.SelectedItem = null;                                                      // set combo box item to empty
        }                                                                                        
        public int CurrentMemberLoggedin(string currentMember)                                   // method to get what user is logged in
        {                                                                                        
            _classes.classDatabase.Get_Sql_Connection();                                         // open sql server connection
            string usernameCheck = currentMember;                                                // set string to string being passed into class
            string Login_query = "SELECT * FROM tbl_Users WHERE UserName = @usernameCheck";      // set up new query to search user table
            DataTable table = classDatabase.Get_Data_Table(Login_query, usernameCheck);          // set up new datatable by passing in query and string
            foreach (DataRow row in table.Rows)                                                  // loop through each row in DataTable
            {                                                                                    
                CurrentSquad = Convert.ToInt32(row["Squad"]);                                    // return squad value to string CurrentSquad
            }                                                                                    
            return Convert.ToInt32(CurrentSquad);                                                // return current squad as and integer
        }                                                                                        
        public void getSquadNames()                                                              // method to use currentsquad value to pick the correct query 
        {                                                                                        
            int cs = Convert.ToInt32(CurrentSquad);                                              // new integer value set to currentSquad int
            string query = "";                                                                   // new query as empty string to be updated through switch case
            switch (cs)                                                                          // start switch case iteration
            {                                                                                    
                case 1:                                                                          // if value is 1 do create this query as this string
                    query = "SELECT * FROM tbl_Members16";                                       // new string query
                    break;                                                                       // end case
                case 2:                                                                          // if value is 2 do create this query as this string
                    query = "SELECT * FROM tbl_Members18";                                       // new string query
                    break;                                                                       // end case
                case 3:                                                                          //if value is 3 do create this query as this string
                    query = "SELECT * FROM tbl_Members21";                                       //new string query
                    break;                                                                       //end case
                case 4:                                                                          // if value is 4 do create this query as this string
                    query = "SELECT * FROM tbl_Members";                                         // new string query
                    break;                                                                       // end case
                default:                                                                         // if no value is hit run this
                    break;                                                                       // end case
            }                                                                                    
            DataTable table = classDatabase.Fill_Data_Table(query);                              // build new datatable and pass new query to Fill_Data_Table method in class classDatabase
            combSearch.ItemsSource = table.DefaultView;                                          // set combobox source items to table defaultview assembly
            combSearch.DisplayMemberPath = "FullName";                                           // print only the full name from table in combobox
        }
        public void playerDetails()                                                                 // Method to print players details based on what name is selected in the combo box
        {                                                                                           
            SearchBoxCheker = combSearch.Text.ToString();                                           // get the name from the combo box that has been selected and set SearchBoxChearch string as this value
            string query = "";                                                                      // new query as empty string to be updated through switch case
            int cs = CurrentSquad;                                                                  //  new integer set to value from CurrentSquad
            switch (cs)                                                                             //  start switch case iteration
            {                                                                                       //
                case 1:                                                                             //  if value is 1 do create this query as this string
                    query = "SELECT * FROM tbl_Members16 WHERE FullName = @SearchBoxChecker";       //  new string query
                    break;                                                                          //  end case
                case 2:                                                                             //  if value is 2 do create this query as this string
                    query = "SELECT * FROM tbl_Members18 WHERE FullName = @SearchBoxChecker";       //  new string query
                    break;                                                                          //  end case
                case 3:                                                                             //  if value is 3 do create this query as this string
                    query = "SELECT * FROM tbl_Members21 WHERE FullName = @SearchBoxChecker";       //  ew string query
                    break;                                                                          //  nd case
                case 4:                                                                             //  if value is 4 do create this query as this string
                    query = "SELECT * FROM tbl_Members WHERE FullName = @SearchBoxChecker";         //  new string query
                    break;                                                                          //  end case
                default:                                                                            //  if no value is hit run this
                    break;                                                                          //  end case
            }                                                                                       
            DataTable table = classDatabase.Get_MemberSearch_Table(query, SearchBoxCheker );        // New DataTable set up by passing query and combo box string into method Get_MemberSearch_Table in the classDatabase class
            foreach (DataRow row in table.Rows)                                                     //  foreach loop to iterate through earch row returned from datatable
            {                                                                                       
                lstTeam.Items.Add("Full Name:");                                                    //  print string to list box  
                lstTeam.Items.Add(row["FullName"].ToString());                                      //  print row to lsit box with the value from FullName
                lstTeam.Items.Add("SRU Number:");                                                   //  print string to list box
                lstTeam.Items.Add(row["IdMember"].ToString());                                      //  print row to lsit box with the value from IdMember
                lstTeam.Items.Add("Emergency Contact:");                                            //  print string to list box
                lstTeam.Items.Add(row["Ice"].ToString());                                           //  print row to lsit box with the value from Ice
            }                                                                                       
        }                                                                                           
        public void allPlayers()                                                                    //  method to print all player in the squad when button is clicked
        {                                                                                           
            string query = "";                                                                      //  new query as empty string to be updated through switch case
            int cs = CurrentSquad;                                                                  //  new integer set to value from CurrentSquad
            switch (cs)                                                                             //  start switch case iteration
            {                                                                                       
                case 1:                                                                             //  if value is 1 do create this query as this string
                    query = "SELECT * FROM tbl_Members16";                                          //  new string query
                    break;                                                                          //  end case
                case 2:                                                                             //  if value is 2 do create this query as this string
                    query = "SELECT * FROM tbl_Members18";                                          //  new string query
                    break;                                                                          //  end case
                case 3:                                                                             //  if value is 3 do create this query as this string
                    query = "SELECT * FROM tbl_Members21";                                          //  ew string query
                    break;                                                                          //  nd case
                case 4:                                                                             //  if value is 4 do create this query as this string
                    query = "SELECT * FROM tbl_Members";                                            //  new string query
                    break;                                                                          //  end case
                default:                                                                            //  if no value is hit run this
                    break;                                                                          //  end case
            }                                                                                       
            DataTable table = classDatabase.Fill_Data_Table(query);                                 //  set up new DataTable by passing query string into Fill_Data_Table method in the classDatase class
            foreach (DataRow row in table.Rows)                                                     //   foreach loop to iterate through earch row returned from datatable 
            {                                                                                       //
                lstTeam.Items.Add("Full Name:");                                                    //   print string to list box  
                lstTeam.Items.Add(row["FullName"].ToString());                                      //   print row to lsit box with the value from FullName
                lstTeam.Items.Add("SRU Number:");                                                   //   print string to list box
                lstTeam.Items.Add(row["IdMember"].ToString());                                      //   print row to lsit box with the value from IdMember
                lstTeam.Items.Add("Emergency Contact:");                                            //   print string to list box
                lstTeam.Items.Add(row["Ice"].ToString());                                           //   print row to lsit box with the value from Ice
            }                                                                                         
        }                                                  
        private void btnHome_Click(object sender, RoutedEventArgs e)                                //  Event handler for when button is clicked
        {                                                                                           
            CoachesOptions coachesOptions = new CoachesOptions(CurrentMember);                      // set up new isntance of page CoachesOptions and pass in currentMember value
            coachesOptions.Show();                                                                  // Open new page 
            this.Close();                                                                           // Close this page
        }                                                                                           
                                                                                                    
        private void btnSearch_Click(object sender, RoutedEventArgs e)                              //  Event handler for when button is clicked
        {                                                                                           
            lstTeam.Items.Clear();                                                                  //  Clear list box
            playerDetails();                                                                        //  run playerDetails Method
        }                                                                                           
                                                                                                    
        private void btnViewAll_Click(object sender, RoutedEventArgs e)                             //  Event handler for when button is clicked
        {                                                                                           
            lstTeam.Items.Clear();                                                                  //  Clear list box
            allPlayers();                                                                           //  run allPlayers method
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
    /// Interaction logic for PlayerSkills.xaml
    /// </summary>
    /// Steven Woodhead
    /// HND Sotware Development
    /// Graded Unit
    /// 20160893
    public partial class PlayerSkills : Window
    {
        public PlayerSkills(string currentMember)                                       // Set up PlayerSkills page with string currentMember being passed into it
        {                                                                               
            InitializeComponent();                                                      //  build the page
            CurrentMember = currentMember;                                              //  new string CurrentMember is equal to the string passed into this class
            CurrentSquad = CurrentMemberLoggedin(CurrentMember);                        //  new string CurrentSquad = the value taken from the method CurrentMemberLoggedin passing in the member to that method
        }
        //Strings for Passing comboBoxes Values
        private string Standard { get; set; }
        private string Spin { get; set; }
        private string Pop { get; set; }
        //Strings for Tackling ComboBoxes Values
        private string Front { get; set; }
        private string Rear { get; set; }
        private string Side { get; set; }
        private string Scrabble { get; set; }
        //strings Kicking Combo Boxes Values
        private string Dropp { get; set; }
        private string Punt { get; set; }
        private string Grubber { get; set; }
        private string Goal { get; set; }
        //Comments text boxes for Passing, Tackling and kicking
        private string Pass_Comments { get; set; }
        private string Tack_Comments { get; set; }
        private string Kick_Comments { get; set; }
        //Variable to store the searched player name
        public string SearchPlayer { get; set; }
        //Variable to store the current memeber using the system
        public string CurrentMember { get; set; }
        //Variable squad value assinged to the current member using the system
        public int CurrentSquad { get; set; }
        //Id of the member beig added to the database
        public string IdMember { get; set; }
        //Name of the member being added to the database
        public string FullName { get; set; }
        // Variable to store counts through table loops
        public int tableCount { get; set; }

        public string updateSkillsTable()                                                              //   Method to perform the preperation to update the skills table
        {                                                                                              
            string SearchBoxChecker = txtAddPlayerName.Text;                                           //   new string to pass to the classDatabase class where sql is performed
            string query = "DELETE FROM Skills WHERE FullName = @SearchBoxChecker";                    //   New query to pass to sql class, Delete all records from table where name matches string
            classDatabase.Get_MemberSearch_Table(query, SearchBoxChecker);                             //   instantuate the DataTable 
            return null;                                                                               //   return null
        }                                                                                              
        public string getMemberId()                                                                    //   method to get the members sru number
        {                                                                                              
            string SearchBoxChecker = txtAddPlayerName.Text;                                           //   new string by the value in the full Name box
            int cs = Convert.ToInt32(CurrentSquad);                                                    //   new integer value set to currentSquad int
            string query = "";                                                                         //   new query as empty string to be updated through switch case
            switch (cs)                                                                                //   start switch case iteration
            {                                                                                          
                case 1:                                                                                //   if value is 1 do create this query as this string
                    query = "SELECT * FROM tbl_Members16 WHERE FullName = @SearchBoxChecker";          //   new string query
                    break;                                                                              //    end case
                case 2:                                                                                //   if value is 2 do create this query as this string
                    query = "SELECT * FROM tbl_Members18 WHERE FullName = @SearchBoxChecker";          //   new string query
                    break;                                                                             //     end case
                case 3:                                                                                //   f value is 3 do create this query as this string
                    query = "SELECT * FROM tbl_Members21 WHERE FullName = @SearchBoxChecker";          //   ew string query
                    break;                                                                             //     nd case
                case 4:                                                                                //   if value is 4 do create this query as this string
                    query = "SELECT * FROM tbl_Members WHERE FullName = @SearchBoxChecker";            //   new string query
                    break;                                                                             //     end case
                default:                                                                               //   if no value is hit run this
                    break;                                                                             //     end case
            }                                                                                          
            DataTable table = classDatabase.Get_MemberSearch_Table(query, SearchBoxChecker);                                                            // New DataTable set up by passing query and searchboxChecker values into method Get_MemberSearch_Table in classDatabase class
            foreach (DataRow row in table.Rows)                                                                                                         //  foreach loop to loop through each row in datatable
            {                                                                                                                                           
                if (row["FullName"].ToString() == SearchBoxChecker)                                                                                     //  if statment to compare fullName row with string SearchBoxChecker
                {                                                                                                                                       
                   IdMember = row["IdMember"].ToString();                                                                                               //  if comparison is true string IdMember is IdMember row from table
                                                                                                                                                        
                }                                                                                                                                       
                else                                                                                                                                    // if not true
                {                                                                                                                                       
                    MessageBox.Show("Please Make Sure Name Is Correct, If You Head To The Search Page You Can Find All Members There", "Pro Tip");      //  return this error message
                                                                                                                                                        
                }                                                                                                                                       
            }                                                                                                                                           
            return IdMember;                                                                                                                            // return the string IdMember to get set
        }

        public bool SkillsTableChecker()                                                                    // Boolean checker to check if player is in the skills table in database
        {                                                                                                   
            this.IdMember = IdMember;                                                                       //  set up current string for IdMember
            this.FullName = FullName;                                                                       //  set up current string for FullName
            this.CurrentSquad = CurrentSquad;                                                               //  set up current string for CurrentSquad
            return SkillsPageDB.playerIsInSkills(FullName, IdMember, CurrentSquad);                         //  pass the string to playerIsInSkills method in SkillsPageDB class to return bool
        }                                                                                                   
        public int CurrentMemberLoggedin(string currentMember)                                              // Method to check what squad the current user is in.
        {                                                                                                   
            _classes.classDatabase.Get_Sql_Connection();                                                    //  open sql connection
            string usernameCheck = currentMember;                                                           //  new string set by currentMember string
            string Login_query = "SELECT * FROM tbl_Users WHERE UserName = @usernameCheck";                 //  new query so search user table
            DataTable table = classDatabase.Get_Data_Table(Login_query, usernameCheck);                     //  new DataTable passing the query and string to Get_Data_table method in the classDatabase class
            foreach (DataRow row in table.Rows)                                                             //  foreach loop to iterate through each row in the datatable
            {                                                                                               
                CurrentSquad = Convert.ToInt32(row["Squad"]);                                               //  set CurrentSquad intiger to value from Squad in table row
            }                                                                                               
            return Convert.ToInt32(CurrentSquad);                                                           //  return the value CurrentSquad
        }

        public string addSkillToTable()                                                                 // Method to insert the values from all the skills page into the Skills tabel in database using the query below
        {
            return "INSERT INTO Skills([IdMemberCopy],[FullName],[Squad],[Pass_Standard],[Pass_Spin],[Pass_Pop],[Tack_Front],[Tack_Rear],[Tack_Side],[Tack_Scrabble],[Kick_Drop],[Kick_Punt],[Kick_Grubber],[Kick_Goal],[Pass_Comments],[Tack_Comments],[Kick_Comments])VALUES" +
                "('" + IdMember + "','" + FullName + "','" + CurrentSquad + "','" + Standard + "','" + Spin + "','" + Pop + "','" + Front + "','" + Rear + "','" + Side + "','" + Scrabble + "','" + Dropp + "','" + Punt + "','" + Grubber + "','" + Goal + "','" + Pass_Comments + "','" + Tack_Comments + "','" + Kick_Comments + "')";
        }
        private void printSkill()                                                                                     // Method to print the skills from player being searched to the skills page 
        {                                                                                                             
            if (txtSearchPlayer.Text.Length > 0)                                                                      // if the search box is not empty then keep going
            {                                                                                                         
                string SearchBoxChecker = txtSearchPlayer.Text;                                                       //    new string with value from search box text box
                string query = "SELECT * FROM Skills WHERE FullName = @SearchBoxChecker";                             //    new string query to be used for datatable
                                                                                                                      
                DataTable table = _classes.classDatabase.Get_MemberSearch_Table(query, SearchBoxChecker);             //    new datatable with the query and palyer name being passed to Get_memberSearch_Table methof in the classDatabase class
                foreach (DataRow row in table.Rows)                                                                   //    foreach loop to loop round each row in datatable 
                {                                                                                                     //
                    if (row["FullName"].ToString() == SearchBoxChecker)                                               //    if the row with FullName is the same as the searched name then fill in the combo boxes and text boxes wthis the below values
                    {   
                        //Player Name
                        txtAddPlayerName.Text = row["FullName"].ToString();
                        //Passing ComboBoxes values
                        combStandard.Text = row["Pass_Standard"].ToString();
                        combSpin.Text = row["Pass_Spin"].ToString();
                        combPop.Text = row["Pass_Pop"].ToString();
                        //Tackling ComboBoxes Values
                        combFront.Text = row["Tack_Front"].ToString();
                        combRear.Text = row["Tack_Rear"].ToString();
                        combSide.Text = row["Tack_Side"].ToString();
                        combScrabble.Text = row["Tack_Scrabble"].ToString();
                        //Kicking ComboBoxes Values
                        combDrop.Text = row["Kick_Drop"].ToString();
                        combPunt.Text = row["Kick_Punt"].ToString();
                        combGrub.Text = row["Kick_Grubber"].ToString();
                        combGoal.Text = row["Kick_Goal"].ToString();
                        //Comments tables text boxes
                        txtPassing.Text = row["Pass_Comments"].ToString();
                        txtTackling.Text = row["Tack_Comments"].ToString();
                        txtKicking.Text = row["Kick_Comments"].ToString();
                    }
                    else                                                                                                                                    // if the datatable doesnt contain an exact match the show this error message
                    {
                        MessageBox.Show("Please Make Sure Name Is Correct, If You Head To The Search Page You Can Find All Members There", "Pro Tip");      // the error message
                        txtSearchPlayer.Clear();                                                                                                            // Clear the search box
                        txtSearchPlayer.Focus();                                                                                                            // focus on the search box
                    }
                }
                tableCount = table.Rows.Count;                                          // count used for error message condition buy search button                                        
            }
            else                                                                        // if search box is empty then show this message
            {
                MessageBox.Show("Search box is empty, Please Search Full Name");        // error message for empty search box
                txtSearchPlayer.Focus();                                                // focus on search box
            }
        }
        public string LoadTable()                                               //   Method for loading the skills page with information from database
        {                                                                       
            return "SELECT * FROM Skills";                                      //  returning this query 
        }                                                                       
                                                                                
        public void clearPage()                                                 //  meathod to clear the skills page
        {                                                                       
            txtAddPlayerName.Clear();                                           // Clear text box
            txtKicking.Clear();                                                 //  Clear text box
            txtPassing.Clear();                                                 //  Clear text box
            txtSearchPlayer.Clear();                                            //  Clear text box
            txtTackling.Clear();                                                //  Clear text box
            combStandard.SelectedIndex = 0;                                     //  set combo box to 0
            combSpin.SelectedIndex = 0;                                         //  set combo box to 0
            combPop.SelectedIndex = 0;                                          //  set combo box to 0
            combFront.SelectedIndex = 0;                                        //  set combo box to 0
            combRear.SelectedIndex = 0;                                         //  set combo box to 0
            combSide.SelectedIndex = 0;                                         //  set combo box to 0
            combScrabble.SelectedIndex = 0;                                     //  set combo box to 0
            combDrop.SelectedIndex = 0;                                         //  set combo box to 0
            combPunt.SelectedIndex = 0;                                         //  set combo box to 0
            combGrub.SelectedIndex = 0;                                         //  set combo box to 0
            combGoal.SelectedIndex = 0;                                         //  set combo box to 0
            IdMember = null;                                                    //  set IdMember to null
        }                                                                       
                                                                                
        //button was not named before this object sender was created
        private void Button_Click(object sender, RoutedEventArgs e)                                         // Event handler to handle button click for HOME, Button named after even created to scared to change it
        {                                                                                                   
            CoachesOptions coachesOptions = new CoachesOptions(CurrentMember);                              //  create new instance of CoachesOptions page passiung in CurrentMember string
            coachesOptions.Show();                                                                          //  open new page
            this.Close();                                                                                   //  close this page
        }                                                                                                   
                                                                                                            
        private void btnSearchPlayer_Click(object sender, RoutedEventArgs e)                                //  Event handler to handle button click for search player button
        {                                                                                                   
            printSkill();                                                                                   //  PrintSkills methos this will run when count doesnt return 0
            this.tableCount = tableCount;                                                                   //  new table count set
            if (tableCount == 0)                                                                            //  if count = 0 then show this error message
            {                                                                                               
                MessageBox.Show("Member is not in database");                                               //  error message for member not being in database
            }                                                                                               
        }                                                                                                   
        public void addToSkills()                                                                           //  method to add skills to skills table in database
        {                                                                                                   
            string query = addSkillToTable();                                                               //  set query to the query on this method
            SqlCommand sqlCommand = new SqlCommand(query, _classes.classDatabase.Get_Sql_Connection());     //  pass query to sql connection
            sqlCommand.ExecuteNonQuery();                                                                   //  execute command
            string queryLoadDB = LoadTable();                                                               //  creayt new query with this method
            _classes.classDatabase.Fill_Data_Table(queryLoadDB);                                            //  pass qury to Fill_Data_Table method in classDatabase class
        }
            
        private void btnSaveDetails_Click(object sender, RoutedEventArgs e)                 // event handler to handle button click for save / update button
        {
            //return the number from Passing comboboxes
            this.Standard = combStandard.Text;
            this.Spin = combSpin.Text;
            this.Pop = combPop.Text;
            //return the number from Tackling comboboxes
            this.Front = combFront.Text;
            this.Rear = combRear.Text;
            this.Side = combSide.Text;
            this.Scrabble = combScrabble.Text;
            //return the number from Kicking comboboxes
            this.Dropp = combDrop.Text;
            this.Punt = combPunt.Text;
            this.Grubber = combGrub.Text;
            this.Goal = combGoal.Text;
            this.IdMember = IdMember; //can remove
            //return comment boxes
            this.Pass_Comments = txtPassing.Text;
            this.Kick_Comments = txtKicking.Text;
            this.Tack_Comments = txtTackling.Text;
            //Return Full Name
            this.FullName = txtAddPlayerName.Text;
            //Return Searched Player
            this.SearchPlayer = txtSearchPlayer.Text;
            //Return Id For add or reaching player
            this.IdMember = getMemberId();

            if (FullName != "")                                                                                                     // if text box is not empty then continue
            {                                                                                                                       
                if (IdMember != null)                                                                                               //  if string IdMember is not null then continue
                {                                                                                                                   
                    if (SkillsTableChecker() == true)                                                                               //  if method SkillsTableChecker returns a true value then continue
                    {                                                                                                               
                        updateSkillsTable();                                                                                        //  run Mehtod updateSkillsTabel - this deletes the record
                        addToSkills();                                                                                              //  run method addToSkills  -   this re-adds a record to replace deleted one
                        MessageBox.Show("Skills have been updated", "SUCCESS");                                                     //  Show that skills have been updated
                        clearPage();                                                                                                //  run method clearPage - clears all the boxes
                    }
                    else                                                                                                            //  if SkillsTableChecker is false the do the next steps
                    {                                                                                                               
                        addToSkills();                                                                                              //  run method addToSkills - becuase the members skills havent been added yet
                        MessageBox.Show(FullName + "'s Skills have been added to Database", "Player skills added");                 //  show message to say name of player's skill have been added
                        clearPage();                                                                                                //  run method clearPage - clears all the boxes
                    }                                                                                                               
                }                                                                                                                   
                else                                                                                                                // If the value from IdMember is null then that member doesnt exist in the coaches squad or database
                {                                                                                                                   
                    MessageBox.Show(FullName + " has not been added to Squad Database, Please contact your Admin", "ERROR");        //  error message to say that player name isnt in database 
                    clearPage();                                                                                                    //  run method clearPage - clears all the boxes
                }                                                                                                                   
            }                                                                                                                       
            else                                                                                                                    // if name text box is empty then show this message
            {                                                                                                                       
                MessageBox.Show("Please Make Sure Players Full Name Box Has Been Filled In");                                       //  message to tell user to fill in full name box
                txtAddPlayerName.Focus();                                                                                           //  focus on full name box
            }
        }
    }
}

/*https://stackoverflow.com/questions/4351603/get-selected-value-from-combo-box-in-c-sharp-wpf */ // origionally this was used but i found that i was able to use COMBOBOXNAME.TEXT once i put the private strings get sets

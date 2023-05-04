using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;                                                                        // Link to this at bottom of Class
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
/// Steven Woodhead
/// HND Sotware Development
/// Graded Unit
/// 20160893
namespace StevenWoodheadGradedUnit._classes
{ 
    
    public static class classDatabase                                                              //Class created specifically to run database querys and manage tables
    {
        public static SqlConnection Get_Sql_Connection()                                            //public static method that returns an sql connection object for the database connection
        {
            string connectionString = Properties.Settings.Default.connectionString;                 // Get the connection string from the settings - this is the location of the database
            SqlConnection sqlConnection = new SqlConnection(connectionString);                      // Create a new SqlConnection object using the connection string
            if (sqlConnection.State != System.Data.ConnectionState.Open) sqlConnection.Open();      // If the connection is not open, open it
            return sqlConnection;                                                                   // Return the SqlConnection object
        }
        public static DataTable Fill_Data_Table(string queryLoadDB)                                 //A definition for fillin a table that accepts a query string input and returns a DataTable
        {
            SqlConnection sqlConnection = Get_Sql_Connection();                                     //returns the sqlConnection and makes sure its open
            //get table
            DataTable table = new DataTable();                                                      //Creates a new instance of DataTable
            SqlDataAdapter adapter = new SqlDataAdapter(queryLoadDB, sqlConnection);                //Creates a new SqlDataAdapter with provided query and connection to SQL
            adapter.Fill(table);                                                                    //Fills the DataTable with data from adapter
            //output
            return table;                                                                           //Returns the populated DataTable
        }
        public static DataTable Get_Data_Table(string Login_query, string usernameCheck)             //A definition for Get_Data_Table method that accepts two string input a query to check if a user is valid and the username input
        {
            SqlConnection sqlConnection = Get_Sql_Connection();                                     //returns the sqlConnection and makes sure its open
            //get table
            DataTable table = new DataTable();                                                      //Creates a new instance of DataTable
            SqlCommand command = new SqlCommand(Login_query, sqlConnection);                        //Creates a new SqlDataAdapter with provided query and connection to SQL
            command.Parameters.AddWithValue("@usernameCheck", usernameCheck);                       //Use command with value passed into Method, Add as a SqlParameter
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);                                   //Create sqlDataAdapter with previous command
            adapter.Fill(table);                                                                    //Fill DataTable with data from adapter
            return table;                                                                           //Return the populated DataTabel.    
        }
        public static DataTable Get_MemberSearch_Table(string query,string SearchBoxChecker)        //Dafinition for Get_MemberSearch_Table method that accepts two strings input. a query, Different querys from different classes and the string
                                                                                                    //SearchBoxChecker wich has different values from different classes
        {
            DataTable table = new DataTable();                                                      //Creates a new instance of DataTable
            SqlCommand command = new SqlCommand(query, Get_Sql_Connection());                       //Creates a new SqlDataAdapter with provided query and opens a connection to SQL
            command.Parameters.AddWithValue("@SearchBoxChecker", SearchBoxChecker);                 //Use command with value passed into Method, Add as a SqlParameter

            SqlDataAdapter adapter = new SqlDataAdapter(command);                                   //Create sqlDataAdapter with previous command
            adapter.Fill(table);                                                                    //Fill DataTable with data from adapte
            return table;                                                                           //Return the populated DataTabel.  
        }
        public static DataTable Get_Skills_Table(string query,  string FullName, string IdMember, int CurrentSquad)   // a definition for Get_Skills_Table Method that takes 3 strings and an integer passed into it, Query to check is playe is in skills table, their name and SRU number and Squad
        {
            SqlConnection sqlConnection = Get_Sql_Connection();                                     //returns the sqlConnection and makes sure its open
            DataTable table = new DataTable();                                                      //Creates a new instance of DataTable
            SqlCommand command  =new SqlCommand(query, sqlConnection);                              //Creates a new SqlDataAdapter with provided query and connection to SQL
            command.Parameters.AddWithValue("@FullName", FullName);                                 //Use command with value FullName passed into Method, Add as a SqlParameter
            command.Parameters.AddWithValue("@IdMember", IdMember);                                 //Use command with value IdMember passed into Method, Add as a SqlParameter
            command.Parameters.AddWithValue("Squad", CurrentSquad);                                 //Use command with value Squad passed into Method, Add as a SqlParameter
            SqlDataAdapter adapter = new SqlDataAdapter(command);                                   //Create sqlDataAdapter with previous command
            adapter.Fill(table);                                                                    //Fill DataTable with data from adapte
            return table;                                                                           //Return the populated DataTabel.    
        }
    }
}

/*https://www.youtube.com/watch?v=_1Hdk5pi8C4                                                               // This YouTube Video helped me connect visual studio to an SQL sever database
 * https://stackoverflow.com/questions/49035178/unable-to-locate-system-data-sqlclient-reference            // this link helped me figure out why my sqlConnection wasnt working properly 
                                                                                                         // "Unable to locate System.Data.SqlClient" - this is the error i was recieving 
https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand?view=dotnet-plat-ext-7.0      // Helped me undertand the use of FullName = @StringName

*/
// on line i passed in the method Get_Sql_Connection in to the new instance of the SqlCommand rather than creating a new instance of SqlConnection
// i feel i can do this on all the methods but due to time constrainst i will need to test that they all work so will leave that for now.










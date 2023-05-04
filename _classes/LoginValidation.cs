using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Windows.Documents;
using System.Windows;
using System.Printing;

/// Steven Woodhead
/// HND Sotware Development
/// Graded Unit
/// 20160893
namespace StevenWoodheadGradedUnit._classes
{

    public static class LoginValidation
    {
        public static bool isValidLogin(string usernameCheck, string passwordCheck)                                                     // A boolean Checker to see if log in details are valid, passed in user name and passeword
        {                                                                                                                               
            string Login_query = "SELECT * FROM tbl_Users WHERE UserName=@usernameCheck";                                               //  A query to retrieve everything from the user table where user name matches what has been entered
            DataTable table = classDatabase.Get_Data_Table(Login_query, usernameCheck);                                                 //  execute the query and store it the results in a datatable
            foreach (DataRow row in table.Rows)                                                                                         //  A loop to iterare through each row in the datatable
            {  
                if (passwordCheck == row["Password"].ToString() && usernameCheck == row["UserName"].ToString())                         //  If the Password passed in Matches that stored in database and the Usenamae passed in matches that stored in the database return true
                {                                                                                                                       
                    return true;                                                                                                        //  Return True if if statment is true
                }                                                                                                                       
            }                                                                                                                           
            return false;                                                                                                               // if if statment is false return false
        }                                                                                                                               
        public static bool isNewUser(string usernameCheck)                                                                              // A boolean checker to check if user exist in database
        {                                                                                                                               
            string Login_query = "SELECT * FROM tbl_Users WHERE UserName=@usernameCheck";                                               // An SQL query checking the Users table
            DataTable table = classDatabase.Get_Data_Table(Login_query, usernameCheck);                                                 //  Execute the query and store it the results in a datatable 
                                                                                                                                        
            foreach (DataRow row in table.Rows)                                                                                         // A loop to iterate through each row in datatable
            {
                if (usernameCheck == row["UserName"].ToString())                                                                        //  and if statment to check if usename matches that of the database
                {                                                                                                                       
                    return false;                                                                                                       // if there is a match return fasle so the username cant be added into the database - becasue there is already that username
                }                                                                                                                       
            }                                                                                                                           
            return true;                                                                                                                // If there is no match return true so that username can be used by new user
        }                                                                                                                               
        public static bool isValidMember(string SearchBoxChecker)                                                                       //  Boolean checker to check if the string passed in is the same as IdMembers in any of the tables
        {                                                                                                                               
            string[] tables = { "tbl_Members16", "tbl_Members18", "tbl_Members21", "tbl_Members" };                                     // An array set up for interchanging through the different database tables
            string query = "SELECT * FROM " + tables[0] + " WHERE IdMember =@SearchBoxChecker";                                         // A query to select all the data from each table in the array where IdMember is the same as the value passed into this method
            for (int i = 0; i < tables.Length; i++)                                                                                     //  a for loop to iterat through the query joined with the array
            {                                                                                                                           
                query += " OR IdMember = @SearchBoxChecker";                                                                            // returning thr  query with new array string each loop and adding on the rest of the query
            }                                                                                                                           
            DataTable table = classDatabase.Get_MemberSearch_Table(query, SearchBoxChecker);                                            // Stor the results in the datatable
            foreach (DataRow row in table.Rows)                                                                                         // interate throu each row
            {                                                                                                                           
                if (SearchBoxChecker == row["IdMember"].ToString())                                                                     // if the string passed in is the same as IdMember row return false so it can be added
                { 
                    return false;                                                                                                       // return fasle so it can be added to database
                }                                                                                                                       
            }                                                                                                                           
                                                                                                                                        
            return true;                                                                                                                //  else return true so it can not be added to the database
        }                                                                                                                               
        public static bool isMemberSearch( string SearchBoxChecker)                                                                     // This is similar to the code above but with an extra check of the Full Name for search function so user can search by id or name
        {                                                                                                                               
            string[] tables = { "tbl_Members16", "tbl_Members18", "tbl_Members21", "tbl_Members" };                                     //  An array set up for interchanging through the different database tables
            for (int i = 0; i < tables.Length; i++)                                                                                     //  a for loop to iterat through the query joined with the array
            {                                                                                                                           
                string query = "SELECT * FROM " + tables[i] + " WHERE FullName = @SearchBoxChecker OR IdMember =@SearchBoxChecker";     //  query with array string each loop and adding on the rest of the query
                DataTable table = classDatabase.Get_MemberSearch_Table(query, SearchBoxChecker);                                        //  Stor the results in the datatable
                foreach (DataRow row in table.Rows)                                                                                     //  interate throu each row
                {                                                                                                                       
                    if (SearchBoxChecker == row["IdMember"].ToString() || SearchBoxChecker == row["FullName"].ToString())               // if the string passed in is the same as IdMember or Full Name row the return true
                    {                                                                                                                   
                        return true;                                                                                                    //  return true is either fields match what has been passed in to search box
                    }                                                                                                                   
                }                                                                                                                       
            }                                                                                                                           
           return false;                                                                                                                // if no match return false
        }
    }
}
/*https://stackoverflow.com/questions/1774498/how-to-iterate-through-a-datatable                                                        // how to iterate through a database
 * https://www.w3schools.com/cs/cs_for_loop.php                                                                                         // this helped me shorten my code by using a loop along with an array
 */

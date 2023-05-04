using System;
using System.Collections.Generic;
using System.Data;
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
    public static class SkillsPageDB                                                                        //static class to perform a members check for the PlayerSkills page
    {                                                                                                       
        public static bool playerIsInSkills(  string FullName, string IdMember, int CurrentSquad)                             // Class that is dedicated to checking if a player with the name FullName and SRU Number IdMember exist in the database table Skills
        {                                                                                                   
            string query = "SELECT * FROM Skills WHERE FullName = @FullName";                               //  a string for and SQL query to Sellect all Records where FullName matched the skills tables FullName column
            DataTable table = classDatabase.Get_Skills_Table(query, FullName, IdMember, CurrentSquad);                    //  Fill the data table with all tyhe records from the Skills table 
            foreach (DataRow row in table.Rows)                                                             //  Loop through each row in the data table created
            {                                                                                               
                if ((row["FullName"].ToString() == FullName && row["IdMemberCopy"].ToString() == IdMember) && row["Squad"].ToString() == CurrentSquad.ToString())   // if the strings that whewre passed in FullName and IdMember match on a row in the skills table with the stored values in columns FullName and IdMember then rewturn true
                {                                                                                           
                    return true;                                                                            // Return a true value to PlayerSkills Class
                }                                                                                           
            }                                                                                               
            return false;                                                                                   // If no match then return a false value to PlayerSkills Class
        }
       
    }
}
/*
 * https://stackoverflow.com/questions/1774498/how-to-iterate-through-a-datatable
 * */
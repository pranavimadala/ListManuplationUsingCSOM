using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateLists
{
    class Strings
    {
        public string url = "SharePoint Online site URL:";
        public string username = "User Name:";
        public string password = "Enter your password.";
        public string mainMenu = "\n\n\nENter chooose \n 1.Create List \n 2.Add Fields \n 3.Break Permissions at list level \n 4.Break Permissions at item level \n 5.Add permissions at list level \n 6.Add permissions at item level \n  7.Exit \n";
        public string wrongChoice = "\n\nEntered choice is wrong Re-enter";
        public string listTitle = "Enter the List Title";
        public string listDescription = "Enter the Description of the list";
        public string success = "List is created successfully ";
        public string fieldsMenu = "Select the type of field you wanted to add \n 1.Text \n 2.Number \n 3.Dropdown \n 4.Radio buttons \n 5.Checkboxes \n 6.User \n 7.Date and time \n 8.Meta Data \n 9.exit";
        public string fieldName = "Enter the Name of the field";
        public string displayName = "Enter the Display name of the field";
        public string addListPermissions = "select the choice \n 1.Add the user\n 2.Add the group \n 3.Create group \n 4.Add user to the group\n5.exit";
        public string perimissions = "Enter the permission level that are to be added ";
        public string groupTitle = "Enter the Group Title";
        public string groupDescription = "Enter the Group Description of the list";
        public string group = "The below groups are present ";
    }
}

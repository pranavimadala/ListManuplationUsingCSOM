using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
namespace CreateLists
{
    class Details
    {
        public  string SPurl { get;  set; }
        public  string UserName { get;  set; }
        public  ClientContext ClientContext { get; set; }
        public  Web web { get; set; }
        public  List newList { get; set; }
        public  string title { get; set; }
        public  int choice { get; set; }
        public int choicee { get; set; }
        public SecureString password { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
    }
}

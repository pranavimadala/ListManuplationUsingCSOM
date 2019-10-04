using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

namespace CreateLists
{
    class Program
    {
       
        static void Main(string[] args)
        {
            ListManuplation listmanuplation = new ListManuplation();
            listmanuplation.Init();
        }
    
    }



}
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CreateLists
{
    class ListManuplation
    {
        public  string SPurl { get; private set; }
        public  string UserName { get; private set; }
        public  ClientContext ClientContext { get; set; }
        public  Web web { get; set; }
        public  List newList { get; set; }
        public  string title { get; set; }
        public  int choice { get; set; }
        public  SecureString password { get; set; }
        public string description { get; set; }
        public int choicee { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }

        public void Init()
        {
            

            Service service = new Service();
            UI ui = new UI();
            Strings strings = new Strings();
            
            ui.ShowMessage(strings.url);
            SPurl = ui.ReadMessage();
            ui.ShowMessage(strings.username);
           UserName = ui.ReadMessage();
            ui.ShowMessage(strings.password);
            password = service.GetPassword();

            service.Login(ClientContext, UserName, password,web,SPurl);
            do
            {
                ui.ShowMessage(strings.mainMenu);
                choice = ui.GetChoice();
                ui.ShowMessage(strings.listTitle);
                title = ui.ReadMessage();
                switch (choice)
                {
                    case 1:
                        
                        ui.ShowMessage(strings.listDescription);
                        description = ui.ReadMessage();
                        title=service.CreateList(ClientContext,newList,title,description);
                        ui.ShowMessage(strings.success + title);
                        break;
                    case 2:
                        ui.ShowMessage(strings.fieldsMenu);
                        choicee = ui.GetChoice();
                        ui.ShowMessage(strings.fieldName);
                        name = ui.ReadMessage();
                        ui.ShowMessage(strings.displayName);
                        displayName = ui.ReadMessage();
                        service.AddFields(title,newList, ClientContext,choicee,name,displayName);
                        break;

                    case 3:
                        service.BreakListPermissions(ClientContext,newList,title);
                        break;
                    case 4:
                        service.BreakItemPermissions(ClientContext, newList, title);
                        break;
                    case 5:
                        ui.ShowMessage(strings.addListPermissions);
                        choicee = ui.GetChoice();
                        ui.ShowMessage(strings.listTitle);
                        title = ui.ReadMessage();
                       if(choicee==1)
                            ui.ShowMessage(strings.username);
                       else
                            ui.ShowMessage(strings.groupTitle);
                        name = ui.ReadMessage();
                        if (choicee == 1 || choicee == 2 || choicee == 3)
                        {
                            if (choicee == 1 || choicee == 2)
                                ui.ShowMessage(strings.perimissions);
                            else
                                ui.ShowMessage(strings.groupDescription);
                                description = ui.ReadMessage();
                        }
                        if (choicee == 2)
                            ui.ShowMessage(strings.group);
                        service.AddListPermissions(title, newList, ClientContext, choicee,web,name,description,ui);
                        break;
                    case 6:
                        ui.ShowMessage(strings.mainMenu);
                        choice = ui.GetChoice();
                        ui.ShowMessage(strings.listTitle);
                        title = ui.ReadMessage();
                        ui.ShowMessage(strings.username);
                        name = ui.ReadMessage();
                        ui.ShowMessage(strings.perimissions);
                        description = ui.ReadMessage();
                        if (choicee == 2)
                            ui.ShowMessage(strings.group);
                        service.AddItemPermissions(title, newList, ClientContext, choicee, web, name, description, ui);
                        break;
                    case 7:
                        break;
                    default:
                        ui.ShowMessage(strings.wrongChoice);
                        break;
                }
            } while (choice != 7);
        }
    }
    
}

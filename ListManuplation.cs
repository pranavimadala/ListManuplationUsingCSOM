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
        public void Init()
        {
            Details details = new Details();
            Service service = new Service();
            UI ui = new UI();
            Strings strings = new Strings();
            
            ui.ShowMessage(strings.url);
            details.SPurl = ui.ReadMessage();
            ui.ShowMessage(strings.username);
            details.UserName = ui.ReadMessage();
            ui.ShowMessage(strings.password);
            details.password = service.GetPassword();

            service.Login(details.ClientContext, details.UserName, details.password,details.web,details.SPurl);
            do
            {
                ui.ShowMessage(strings.mainMenu);
                details.choice = ui.GetChoice();
                ui.ShowMessage(strings.listTitle);
                details.title = ui.ReadMessage();
                switch (details.choice)
                {
                    case 1:
                        
                        ui.ShowMessage(strings.listDescription);
                        details.description = ui.ReadMessage();
                        details.title=service.CreateList(details.ClientContext,details.newList,details.title,details.description);
                        ui.ShowMessage(strings.success + details.title);
                        break;
                    case 2:
                        ui.ShowMessage(strings.fieldsMenu);
                        details.choicee = ui.GetChoice();
                        ui.ShowMessage(strings.fieldName);
                        details.name = ui.ReadMessage();
                        ui.ShowMessage(strings.displayName);
                        details.displayName = ui.ReadMessage();
                        service.AddFields(details.title,details.newList, details.ClientContext,details.choicee,details.name,details.displayName);
                        break;

                    case 3:
                        service.BreakListPermissions(details.ClientContext,details.newList,details.title);
                        break;
                    case 4:
                        service.BreakItemPermissions(details.ClientContext, details.newList, details.title);
                        break;
                    case 5:
                        ui.ShowMessage(strings.addListPermissions);
                        details.choicee = ui.GetChoice();
                        ui.ShowMessage(strings.listTitle);
                        details.title = ui.ReadMessage();
                       if(details.choicee==1)
                            ui.ShowMessage(strings.username);
                       else
                            ui.ShowMessage(strings.groupTitle);
                        details.name = ui.ReadMessage();
                        if (details.choicee == 1 || details.choicee == 2 || details.choicee == 3)
                        {
                            if (details.choicee == 1 || details.choicee == 2)
                                ui.ShowMessage(strings.perimissions);
                            else
                                ui.ShowMessage(strings.groupDescription);
                                details.description = ui.ReadMessage();
                        }
                        if (details.choicee == 2)
                            ui.ShowMessage(strings.group);
                        service.AddListPermissions(details.title, details.newList, details.ClientContext, details.choicee,details.web,details.name,details.description,ui);
                        break;
                    case 6:
                        ui.ShowMessage(strings.mainMenu);
                        details.choice = ui.GetChoice();
                        ui.ShowMessage(strings.listTitle);
                        details.title = ui.ReadMessage();
                        ui.ShowMessage(strings.username);
                        details.name = ui.ReadMessage();
                        ui.ShowMessage(strings.perimissions);
                        details.description = ui.ReadMessage();
                        if (details.choicee == 2)
                            ui.ShowMessage(strings.group);
                        service.AddItemPermissions(details.title, details.newList, details.ClientContext, details.choicee, details.web, details.name, details.description, ui);
                        break;
                    case 7:
                        break;
                    default:
                        ui.ShowMessage(strings.wrongChoice);
                        break;
                }
            } while (details.choice != 7);
        }
    }
    
}

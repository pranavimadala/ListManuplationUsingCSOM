using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

namespace CreateLists
{
    class Service
    {
        public void Login(ClientContext ClientContext, string UserName, SecureString password, Web web, string SPurl)
        {
            using (ClientContext = new ClientContext(SPurl))
            {

                ClientContext.Credentials = new SharePointOnlineCredentials(UserName, password);
                web = ClientContext.Web;
                ClientContext.Load(web, w => w.Lists);
                ClientContext.ExecuteQuery();
                foreach (List list in web.Lists)
                {
                    Console.WriteLine("List title is: " + list.Title);
                }
            }
        }
        public SecureString GetPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            var securePassword = new SecureString();
            //Convert string to secure string  
            foreach (char c in password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            return securePassword;
        }
        public string CreateList(ClientContext ClientContext, List newList, string title, string description)
        {



            ListCreationInformation creationInfo = new ListCreationInformation();
            creationInfo.Title = title;
            creationInfo.Description = description;
            creationInfo.TemplateType = (int)ListTemplateType.GenericList;
            newList = ClientContext.Web.Lists.Add(creationInfo);
            newList.OnQuickLaunch = true;
            newList.Update();
            ClientContext.Load(newList);
            ClientContext.ExecuteQuery();
            return newList.Title;

        }
        public void AddFields(string title, List newList, ClientContext ClientContext, int choice, string name, string Displayname)
        {
            newList = ClientContext.Web.Lists.GetByTitle(title);
            switch (choice)
            {
                case 1:
                    string schemaTextField = "<Field Type='Text' Name='" + name + "'  DisplayName='" + Displayname + "' />";
                    newList.Fields.AddFieldAsXml(schemaTextField, true, AddFieldOptions.AddFieldInternalNameHint);
                    newList.Update();
                    ClientContext.Load(newList);
                    ClientContext.ExecuteQuery();
                    break;
                case 2:
                    string schemaNumberField = "<Field  Type='Number' Name='" + name + "'  DisplayName = '" + Displayname + "'  /> ";
                    newList.Fields.AddFieldAsXml(schemaNumberField, true, AddFieldOptions.AddFieldInternalNameHint);
                    newList.Update();
                    ClientContext.Load(newList);
                    ClientContext.ExecuteQuery();
                    break;
                case 3:
                    string schemaChoiceField = "<Field  Type='Choice' DisplayName='" + Displayname + "' Name='" + name + "'  Format = 'Dropdown' > "
+ "<Default>SELECT</Default>"
+ "<CHOICES>"
+ "    <CHOICE>YES</CHOICE>"
+ "    <CHOICE>NO</CHOICE>"
+ "</CHOICES>"
+ "</Field>";
                    newList.Fields.AddFieldAsXml(schemaChoiceField, true, AddFieldOptions.AddFieldInternalNameHint);
                    newList.Update();
                    ClientContext.Load(newList);
                    ClientContext.ExecuteQuery();
                    break;
                case 4:
                    schemaChoiceField = "<Field  Type='Choice' Name='" + name + "'  DisplayName = '" + Displayname + "' Format = 'RadioButtons' > "
+ "<Default>Select</Default>"
+ "<CHOICES>"
+ "    <CHOICE>YES</CHOICE>"
+ "    <CHOICE>NO</CHOICE>"
+ "</CHOICES>"
+ "</Field>";
                    newList.Fields.AddFieldAsXml(schemaChoiceField, true, AddFieldOptions.AddFieldInternalNameHint);
                    newList.Update();
                    ClientContext.Load(newList);
                    ClientContext.ExecuteQuery();
                    break;
                case 5:
                    schemaChoiceField = "<Field  Type='MultiChoice' Name='" + name + "'   DisplayName = '" + Displayname + "' > "
+ "<Default>Select</Default>"
+ "<CHOICES>"
+ "    <CHOICE>Yes</CHOICE>"
+ "    <CHOICE>No</CHOICE>"
+ "    <CHOICE>Both</CHOICE>"
+ "</CHOICES>"
+ "</Field>";
                    newList.Fields.AddFieldAsXml(schemaChoiceField, true, AddFieldOptions.AddFieldInternalNameHint);
                    newList.Update();
                    ClientContext.Load(newList);
                    ClientContext.ExecuteQuery();
                    break;
                case 6:
                    string schemaUserField = "<Field  Type='User' Name='" + name + "'  DisplayName='" + Displayname + "' />";
                    newList.Fields.AddFieldAsXml(schemaUserField, true, AddFieldOptions.AddFieldInternalNameHint);
                    newList.Update();
                    ClientContext.Load(newList);
                    ClientContext.ExecuteQuery();
                    break;
                case 7:
                    string schemaArrivalField = "<Field  Type='DateTime' Name='" + name + "'  DisplayName = '" + Displayname + "' Format = 'DateTime' > "
 + "<Default>[Today]</Default></Field>";
                    newList.Fields.AddFieldAsXml(schemaArrivalField, true, AddFieldOptions.AddFieldInternalNameHint);
                    newList.Update();
                    ClientContext.Load(newList);
                    ClientContext.ExecuteQuery();
                    break;
                case 8:
                    string schemaTaxonomyField = "<Field  Type='TaxonomyFieldTypeMulti' Name='" + name + "' DisplayName = '" + Displayname + "' /> ";
                    Field field = newList.Fields.AddFieldAsXml(schemaTaxonomyField, true, AddFieldOptions.AddFieldInternalNameHint);
                    newList.Update();
                    ClientContext.Load(newList);
                    ClientContext.ExecuteQuery();
                    Guid termStoreId = Guid.Empty;
                    Guid termSetId = Guid.Empty;
                    GetTaxonomyFieldInfo(ClientContext, out termStoreId, out termSetId);

                    // Retrieve as Taxonomy Field
                    TaxonomyField taxonomyField = ClientContext.CastTo<TaxonomyField>(field);
                    taxonomyField.SspId = termStoreId;
                    taxonomyField.TermSetId = termSetId;
                    taxonomyField.TargetTemplate = String.Empty;
                    taxonomyField.AnchorId = Guid.Empty;
                    taxonomyField.Update();

                    ClientContext.ExecuteQuery();
                    break;
                default:
                    break;

            }
        }
        static private void GetTaxonomyFieldInfo(ClientContext clientContext, out Guid termStoreId, out Guid termSetId)
        {
            termStoreId = Guid.Empty;
            termSetId = Guid.Empty;

            TaxonomySession session = TaxonomySession.GetTaxonomySession(clientContext);
            TermStore termStore = session.GetDefaultSiteCollectionTermStore();
            TermSetCollection termSets = termStore.GetTermSetsByName("Department", 1033);

            clientContext.Load(termSets, tsc => tsc.Include(ts => ts.Id));
            clientContext.Load(termStore, ts => ts.Id);
            clientContext.ExecuteQuery();

            termStoreId = termStore.Id;
            termSetId = termSets.FirstOrDefault().Id;
        }
        public void BreakListPermissions(ClientContext ClientContext, List newList, string title)
        {
            newList = ClientContext.Web.Lists.GetByTitle(title);
            newList.BreakRoleInheritance(false, true);
            newList.Update();
            ClientContext.ExecuteQuery();
        }
        public void BreakItemPermissions(ClientContext ClientContext, List newList, string title)
        {

            newList = ClientContext.Web.Lists.GetByTitle(title);
            ListItemCollection items = newList.GetItems(CamlQuery.CreateAllItemsQuery());
            ClientContext.Load(items);

            ClientContext.ExecuteQuery();

            foreach (ListItem item in items)
            {
                Console.WriteLine(item["Title"]);
                item.BreakRoleInheritance(false, false);
            }
            newList.Update();
            ClientContext.ExecuteQuery();


        }
        public void AddListPermissions(string title, List newList, ClientContext ClientContext, int choice, Web web, string username, string permission, UI ui)
        {
            newList = ClientContext.Web.Lists.GetByTitle(title);
            switch (choice)
            {
                case 1:

                    User user = ClientContext.Web.EnsureUser(username);
                    RoleDefinition roleDefinition = ClientContext.Web.RoleDefinitions.GetByName(permission);
                    RoleDefinitionBindingCollection collRoleDefinitionBinding = new RoleDefinitionBindingCollection(ClientContext);
                    collRoleDefinitionBinding.Add(roleDefinition);
                    newList.RoleAssignments.Add(user, collRoleDefinitionBinding);
                    ClientContext.ExecuteQuery();
                    break;
                case 2:
                    ClientContext.Load(web, w => w.SiteGroups);
                    ClientContext.ExecuteQuery();
                    GroupCollection groups = web.SiteGroups;
                    foreach (Group grp in groups)
                    {
                        ui.ShowMessage(grp.Title);
                    }
                    Group newgroup = ClientContext.Web.SiteGroups.GetByName(username);
                    RoleDefinitionBindingCollection roleDefCollection = new RoleDefinitionBindingCollection(ClientContext);

                    // Set the permission level of the group for this particular list
                    RoleDefinition readDef = ClientContext.Web.RoleDefinitions.GetByName(permission);
                    roleDefCollection.Add(readDef);

                    Principal userGroup = newgroup;
                    RoleAssignment roleAssign = newList.RoleAssignments.Add(userGroup, roleDefCollection);

                    ClientContext.Load(roleAssign);
                    roleAssign.Update();
                    ClientContext.ExecuteQuery();
                    break;
                case 3:
                    GroupCreationInformation groupCreationInfo = new GroupCreationInformation();
                    groupCreationInfo.Title = username;
                    groupCreationInfo.Description = permission;
                    ClientContext.Load(web.CurrentUser);
                    ClientContext.ExecuteQuery();
                    var currentUser = ClientContext.Web.CurrentUser.LoginName;
                    User owner = web.EnsureUser(currentUser);
                    Group group = web.SiteGroups.Add(groupCreationInfo);
                    group.Owner = owner;
                    group.Update();
                    ClientContext.ExecuteQuery();
                    break;
                case 4:

                    newgroup = ClientContext.Web.SiteGroups.GetByName(username);
                    User member = web.EnsureUser(@"qwert");
                    newgroup.Users.AddUser(member);
                    newgroup.Update();
                    ClientContext.ExecuteQuery();
                    break;
            }
        }
        public void AddItemPermissions(string title, List newList, ClientContext ClientContext, int choice, Web web, string username, string permission, UI ui)
        {
           
            newList = ClientContext.Web.Lists.GetByTitle(title);
            switch (choice)
            {
                case 1:
                    User oUser = ClientContext.Web.SiteUsers.GetByLoginName("i:0#.f|membership|pranavi.a_technovert.net#ext#@mapsr.onmicrosoft.com");
                    RoleDefinition roleDefinition = ClientContext.Web.RoleDefinitions.GetByName(permission);
                    ListItemCollection items = newList.GetItems(CamlQuery.CreateAllItemsQuery());

                    ClientContext.Load(items);

                    ClientContext.ExecuteQuery();

                    foreach (ListItem item in items)
                    {
                        item.RoleAssignments.Add(oUser, new RoleDefinitionBindingCollection(ClientContext) { roleDefinition });
                    }
                    ClientContext.Load(items);

                    ClientContext.ExecuteQuery();

                    break;
                case 2:
                    ClientContext.Load(web, w => w.SiteGroups);
                    ClientContext.ExecuteQuery();
                    GroupCollection groups = web.SiteGroups;
                    foreach (Group grp in groups)
                    {
                        ui.ShowMessage(grp.Title);
                    }
                    Group newgroup = ClientContext.Web.SiteGroups.GetByName(username);
                    roleDefinition = ClientContext.Web.RoleDefinitions.GetByName(permission);
                    items = newList.GetItems(CamlQuery.CreateAllItemsQuery());

                    ClientContext.Load(items);

                    ClientContext.ExecuteQuery();

                    foreach (ListItem item in items)
                    {
                        item.RoleAssignments.Add(newgroup, new RoleDefinitionBindingCollection(ClientContext) { roleDefinition });
                    }
                    ClientContext.Load(items);

                    ClientContext.ExecuteQuery();
                    break;

            }
        }
    }
}

using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        List<Client> AllClients = Client.GetAll();
        return View["index.cshtml", AllClients];
      };

      Get["/stylists"] = _ =>
      {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };
      Post["/stylists"] = _ =>
      {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Get["/stylists/new"] = _ =>
      {
        return View["stylists-form.cshtml"];
      };

      Get["/stylist/{id}"] = param =>
      {
        Stylist Found = Stylist.Find(param.id);
        List<Client> FoundClients = Found.GetClients();
        return View["stylist.cshtml", FoundClients];
      };



      // Get["/stylist/{id}"] = param =>
      // {
      //   Dictionary<string, object> model = new Dictionary<string, object>(); //Used to build the model, hence the name model
      //   Stylist FoundStylist = Stylist.Find(param.id);
      //   List<Client> ListOfClients = FoundStylist.GetClients();
      //   model.Add("Stylist: ", FoundStylist);
      //   model.Add("Clients: ", ListOfClients);
      //   return View["stylist.cshtml", model];
      // };

      Get["/stylists/{id}"] = param =>
      {
        Stylist FoundStylist = Stylist.Find(param.id);
        List<Client> ListOfClients = FoundStylist.GetClients();
        return View["stylist.cshtml", ListOfClients];
      };

      Post["/stylists/delete"] = _ =>
      {
        Stylist.DeleteAll();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      // Get["/clients/{id}"] = param =>
      // {
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   Client selectedClients = Client.Find(param.id);
      //   List<Stylist> StylistsClients = selectedClients.GetAll();
      //   model.Add("client", selectedClients);
      //   model.Add("stylists", StylistsClients);
      //   return View["clients.cshtml", model];
      // };

      Get["/clients"] = _ =>
      {
        List<Client> AllClients = Client.GetAll();
        return View["clients.cshtml", AllClients];
      };

      Post["/clients"] = _ =>
      {
        Client newClient = new Client(Request.Form["client-name"], Request.Form["client-stylist"]);
        newClient.Save();
        List<Client> AllClients = Client.GetAll();
        return View["clients.cshtml", AllClients];
      };

      Get["/clients/new"] = _ =>
      {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["client-form.cshtml", AllStylists];
      };

      Post["/clients/new"] = _ =>
      {
        Client newClient = new Client(Request.Form["client-name"], Request.Form["stylist-name"]);
        newClient.Save();
        return View["success.cshtml"];
      };

      Post["/clients/delete"] = _ =>
      {
        Client.DeleteAll();
        List<Client> AllClients = Client.GetAll();
        return View["clients.cshtml", AllClients];
      };

      Get["client/edit/{id}"] = param =>
      {
        Client SelectedClient = Client.Find(param.id);
        return View["client_edit.cshtml", SelectedClient];
      };

      Patch["client/edit/{id}"] = param =>
      {
        Client SelectedClient = Client.Find(param.id);
        SelectedClient.Update(Request.Form["client-name"]);
        return View["success.cshtml"];
      };

      Get["clients/delete/{id}"] = param =>
      {
        Client ThisClient = Client.Find(param.id);
        return View["client-delete.cshtml", ThisClient];
      };

      Delete["clients/delete/{id}"] = param =>
      {
        Client SelectedClient = Client.Find(param.id);
        SelectedClient.Delete();
        return View["success.cshtml", SelectedClient];
      };

      // Post["clients/delete/{id}"] = param =>
      // {
      //   Client SelectedClient = Client.Find(param.id);
      //   SelectedClient.Delete();
      //   return View["success.cshtml"];
      // };
    }
  }
}

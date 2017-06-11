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
        List<Client> allClients = Client.GetAll();
        return View["index.cshtml", allClients];
      };

      Get["/stylists"] = _ =>
      {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Post["/stylists"] = _ =>
      {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylists/new"] = _ =>
      {
        return View["stylists-form.cshtml"];
      };

      Get["/stylist/{id}"] = param =>
      {
        Stylist found = Stylist.Find(param.id);
        List<Client> foundClients = found.GetClients();
        return View["stylist.cshtml", foundClients];
      };

      Post["/stylists/delete"] = _ =>
      {
        Stylist.DeleteAll();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/clients"] = _ =>
      {
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };

      Post["/clients"] = _ =>
      {
        Client newClient = new Client(Request.Form["client-name"], Request.Form["client-stylist"]);
        newClient.Save();
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };

      Get["/clients/new"] = _ =>
      {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["client-form.cshtml", allStylists];
      };

      Post["/clients/delete"] = _ =>
      {
        Client.DeleteAll();
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };

      Get["clients/delete/{id}"] = param =>
      {
        Client thisClient = Client.Find(param.id);
        return View["delete-client.cshtml", thisClient];
      };




    }
  }
}

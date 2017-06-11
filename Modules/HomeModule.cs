using Nancy;
using System;
using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Client> allClients = Client.GetAll();
        return View["index.cshtml", allClients];
      };

      Get["/stylists"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Post["/stylists"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylists/new"] = _ => {
        return View["stylists-form.cshtml"];
      };

      // Get["/stylists/{id}"] = param => {
      //   Stylist thisStylist = Stylist.Find(param.id);
      //   List<Client> foundStylists = thisStylist.GetClients();
      //   return View["stylist.cshtml", foundStylists];
      // };

      Get["/clients"] = _ => {
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };

      Post["/clients"] = _ => {
        Client newClient = new Client(Request.Form["client-name"], Request.Form["client-stylist"]);
        newClient.Save();
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };

      Get["/clients/new"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["clients.cshtml", allStylists];
      };

    }
  }
}

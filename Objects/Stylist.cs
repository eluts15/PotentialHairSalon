using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace HairSalon
{
  public class Stylist
  {
    private string _name;
    private int _id;

    public Stylist(string Name, int StylistId=0)
    {
      _name = Name;
      _id = StylistId;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());

        return (idEquality && nameEquality);
      }
    }

    //Tbh, i don't quite understand what this does (yet!), but it removes warnings.
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }


    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn); //Allow ability to access the database, select fields from stylist and act on them.
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allStylists; //Return the list that was created.
    }

    //Search by a particular stylist's name
    public static Stylist Find(int searchById)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @Id;", conn); //Search the stylist table, and group by a specific id.
      SqlParameter stylistIdParam = new SqlParameter("@Id", searchById.ToString());
      cmd.Parameters.Add(stylistIdParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      int stylistId = 0;
      string stylistName = null;

      while(rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }

      Stylist newStylist = new Stylist(stylistName, stylistId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return newStylist;
    }

    public List<Client> GetClients()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM  clients WHERE stylist_id = @StylistId", conn); //Grep all the clients that belong to a specific stylist!
      SqlParameter stylistParam = new SqlParameter("@StylistId", this.GetId()); //Instantiate a new object based off the information gathered from the table in the database.
      cmd.Parameters.Add(stylistParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Client> allClients = new List<Client>{};
      while(rdr.Read()) //Read it, until you cant read anymore.nomnomnomnom!
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId); //Instantiate a clientObject that has these properties.
        allClients.Add(newClient); //Add any new clients to our list of current clients.
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return allClients; //Lastly, return a list of all of our clients.
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@Name);", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@Name";
      nameParam.Value = this.GetName();

      cmd.Parameters.Add(nameParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}

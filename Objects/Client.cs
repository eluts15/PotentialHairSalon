using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Client
  {
    private string _name;
    private int _stylistId; //A client also needs reference to a particular stylist.

    private int _id;

    public Client(string clientName, int stylistId, int clientId=0)
    {
      _name = clientName;
      _stylistId = stylistId;

      _id = clientId;
    }

    //Getters
    public string GetName()
    {
      return _name;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public int GetId()
    {
      return _id;
    }

    //Setters
    public void SetName(string newName)
    {
      _name = newName;
    }
    public void SetStylistId(int newStylist)
    {
      _stylistId = newStylist;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }

    //Overrides, What if a Stylist has multiple clients who share the same name?
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        bool idEquality = (this.GetId() == newClient.GetId());

        return (nameEquality && stylistIdEquality && idEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0); //int
        string clientName = rdr.GetString(1); //string
        int clientStylistId = rdr.GetInt32(2); //int

        Client newClient = new Client(clientName, clientStylistId, clientId); //string, int, int
        allClients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allClients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (client_name, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @Id);", conn); //client_name and stylist_id correspond to columns in DB.
      SqlParameter nameParam = new SqlParameter("@ClientName", this.GetName());
      SqlParameter stylistIdParam = new SqlParameter("@Id", this.GetStylistId());

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(stylistIdParam);

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

    public static Client Find(int searchById)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @SearchById;", conn);
      SqlParameter findClientById = new SqlParameter("@SearchById", searchById.ToString());
      cmd.Parameters.Add(findClientById);
      SqlDataReader rdr = cmd.ExecuteReader();

      int clientFoundById = 0;
      string clientFoundByName = null;
      int foundClientForStylist = 0;

      while(rdr.Read())
      {
        clientFoundById = rdr.GetInt32(0);
        clientFoundByName = rdr.GetString(1);
        foundClientForStylist = rdr.GetInt32(2);
      }
      Client found = new Client(clientFoundByName, foundClientForStylist, clientFoundById);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return found;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET client_name = @NewName OUTPUT INSERTED.client_name WHERE id = @ClientId;", conn);

      SqlParameter newNameParam = new SqlParameter();
      newNameParam.ParameterName = "@NewName";
      newNameParam.Value = newName;
      cmd.Parameters.Add(newNameParam);

      SqlParameter clientIdParam = new SqlParameter();
      clientIdParam.ParameterName = "@ClientId";
      clientIdParam.Value = this.GetId();
      cmd.Parameters.Add(clientIdParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId; DELETE FROM stylists WHERE client_id = @ClientId;", conn);

      SqlParameter clientIdParam = new SqlParameter();
      clientIdParam.ParameterName = "@ClientId";
      clientIdParam.Value = this.GetId();

      cmd.Parameters.Add(clientIdParam);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}

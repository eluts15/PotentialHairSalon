using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Client
  {
    private int _stylistId;
    private string _name;
    private int _id;

    public Client(int stylistId, string clientName, int clientId=0)
    {
      _stylistId = stylistId;
      _name = clientName;
      _id = clientId;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());

        return (idEquality && nameEquality && stylistIdEquality); //int, string, int
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

        Client newClient = new Client(clientId, clientName, clientStylistId); //int, string, int
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
      // nameParam.ParameterName = "@ClientName";
      //nameParam.Value =this.GetName();

      SqlParameter stylistIdParam = new SqlParameter("@Id", this.GetStylistId());
      // stylistIdParam.ParameterName = "@ClientId";
      // stylistIdParam.Value = this.GetStylistId();

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
      Client found = new Client(clientFoundById, clientFoundByName, foundClientForStylist);

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

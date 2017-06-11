using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  [Collection("HairSalon")]
  public class StylistTests: IDisposable
  {
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    //Tests will go below.

    [Fact] //Verify an empty database as we don't want to mess anything up if it is already populated.
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange
      int stylistResult = Stylist.GetAll().Count;
      //Assert
      Assert.Equal(0, stylistResult);
    }

    [Fact] //Verify we are saving stylist to our Database.
    public void Test_Stylist_Save_SaveToDB()
    {
      //Arrange
      Stylist bob = new Stylist("Bob the SuperStylist");
      //Act
      bob.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{bob};
      //Assert
      Assert.Equal(result, testList);
    }

    [Fact]
    public void Test_FindStylist()
    {
      //Arrange
      Stylist search = new Stylist("Ricky Bobby the Great Stylist");
      //Act
      search.Save();
      Stylist found = Stylist.Find(search.GetId());
      //Assert
      Assert.Equal(search, found);
    }

    [Fact]
    public void Test_GetAllClients_GetClientsForThisStylist()
    {
      //Arrange, Act
      Stylist testStylist = new Stylist("Dave the Stylish Stylist");
      testStylist.Save();

      Client client = new Client("Ricky Bobby is a client", testStylist.GetId());
      client.Save();

      List<Client> testClientList = new List<Client> {client};
      List<Client> resultClientList = testStylist.GetClients();

      //Assert
      Assert.Equal(testClientList, resultClientList);  
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
  }
}

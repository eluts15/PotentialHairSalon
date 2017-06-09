using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  [Collection("HairSalon")]
  public class StylistClientTests: IDisposable
  {
    public StylistClientTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    //Tests will go below.

    [Fact] //Verify an empty database as we don't want to mess anything up if it is already populated.
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange
      int stylistResult = Stylist.GetAll().Count;
      int clientResult = Client.GetAll().Count;
      //Assert
      Assert.Equal(0, stylistResult + clientResult);
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

    [Fact] //Verify we are saving clients to our Database.
    public void Test_Client_Save_SaveToDB()
    {
      //Arrange
      Client testClient = new Client(1,"Jimmy the Client", 1); //Verify datatypes are equal as expected.
      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};
      //Assert
      Assert.Equal(result, testList);
    }

    [Fact]
    public void Test_FindClient()
    {
      //Arrange
      Client search = new Client(1, "Jimmy the Client", 1);
      //Act
      search.Save();
      Client found = Client.Find(search.GetId());
      //Assert
      Assert.Equal(search, found);
    }

    [Fact]
    public void Test_FindStylist()
    {
      //Arrange
      Stylist search = new Stylist("Ricky Bobby the Great");
      //Act
      search.Save();
      Stylist found = Stylist.Find(search.GetId());
      //Assert
      Assert.Equal(search, found);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
  }
}

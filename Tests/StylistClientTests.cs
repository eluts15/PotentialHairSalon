using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistClientTests: IDisposable
  {
    public StylistClientTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    //Tests will go below.

    [Fact]
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
      Stylist bob = new Stylist("Bob");
      //Act
      bob.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{bob};
      //Assert
      Assert.Equal(result, testList);
    }

    // [Fact] //Verify we are saving clients to our Database.
    // public void Test_Client_Save_SaveToDB()
    // {
    //   //Arrange
    //   Client testClient = new Client("Jimmy the client");
    //   //Act
    //   testClient.Save();
    //   List<Client> result = Client.GetAll();
    //   List<Client> testList = new List<Client>{testClient};
    //   //Assert
    //   Assert.Equal(result, testList);
    // }
    // 
    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
  }
}

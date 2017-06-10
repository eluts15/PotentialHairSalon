using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  [Collection("HairSalon")]
  public class ClientTests: IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    //Tests will go below.

    [Fact] //Verify an empty database as we don't want to mess anything up if it is already populated.
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange
      int clientResult = Client.GetAll().Count;
      //Assert
      Assert.Equal(0, clientResult);
    }

    [Fact] //Verify we are saving clients to our Database.
    public void Test_Client_Save_SaveToDB()
    {
      //Arrange
      Client testClient = new Client(1, "Jimmy the Client"); //Verify datatypes are equal as expected.
      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_FindClient()
    {
      //Arrange
      Client search = new Client(1, "Jimmy the Client");
      //Act
      search.Save();
      Client found = Client.Find(search.GetId());
      //Assert
      Assert.Equal(found, search);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
  }
}

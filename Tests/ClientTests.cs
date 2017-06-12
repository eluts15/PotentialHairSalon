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
      Client testClient = new Client("Jimmy the Client", 1); //Verify datatypes are equal as expected.
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
      Client search = new Client("Jimmy the Client", 1);
      //Act
      search.Save();
      Client found = Client.Find(search.GetId());
      //Assert
      Assert.Equal(search, found);
    }

    [Fact]
    public void Test_Update_UpdateClient()
    {
      //Arrange
      string name = "Billy the Goat";
      Client testClient = new Client(name, 1);
      testClient.Save();
      string updateName = "Billy the Giraffe";
      //Act
      testClient.Update(updateName);
      string result = testClient.GetName();
      //Assert
      Assert.Equal(updateName, result);
    }

    // [Fact]
    // public void Test_Delete_DeleteClientFromDB()
    // {
    //   //Arrange, Act
    //   string name = "Fox McCloud";
    //   Client testClient = new Client(name, 1);
    //   testClient.Save();
    //   testClient.Delete();
    //   List<Client> resultClientList = Client.GetAll();
    //   List<Client> testClientList = new List<Client> {testClient};
    //   //Assert
    //   Assert.Equal(resultClientList, testClientList);
    // }

    [Fact]
    public void Update_UpdatesClientInDB()
    {
      //Arrange
      string name = "John Mayer";
      Client testClient = new Client(name, 2);
      testClient.Save();
      string newName = "Hayley Williams";
      //Act
      testClient.Update(newName);
      string result = testClient.GetName();
      //Assert
      Assert.Equal(newName, result);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
  }
}

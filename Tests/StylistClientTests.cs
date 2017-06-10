using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistClientTests
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

    // public void Dispose()
    // {
    //
    // }
  }
}

using Xunit;
using Moq;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

public class DatabasePeleMeleTests
{
    [Fact]
    public void UpdateFilm_UpdatesAFilm()
    {
        // Arrange
        Mock<DatabasePeleMele> dbMock = new Mock<DatabasePeleMele>();
        var film = new Film
        {
            Id = ObjectId.GenerateNewId(),
            Titre = "Le Matrix",
        };

        
        
    }
}
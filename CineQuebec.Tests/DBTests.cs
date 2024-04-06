using Xunit;
using Moq;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

public class DatabaseFilmsTests
{
    [Fact]
    public void UpdateFilm_UpdatesAFilm()
    {
        // Arrange
        Mock<DatabaseFilms> dbMock = new Mock<DatabaseFilms>();
        var film = new Film
        {
            Id = ObjectId.GenerateNewId(),
            Titre = "Le Matrix",
        };

        
        
    }
}
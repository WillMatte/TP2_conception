﻿using Xunit;
using Moq;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;

public class DatabaseAbonnesTests
{
    [Fact]
    public void ReadAbonnes_RetourneUneListeAbonnes()
    {
        // Arrange
        List<Abonne> abonnes = new List<Abonne>() {new Abonne(), new Abonne()};
        Mock<IDatabaseAbonnes> dbMock = new Mock<IDatabaseAbonnes>();
        dbMock.Setup(x => x.ReadAbonnes()).Returns(abonnes);
        
        // Act
        var result = dbMock.Object.ReadAbonnes();
        
        // Assert
        Assert.True(abonnes.SequenceEqual(result));

    }
}
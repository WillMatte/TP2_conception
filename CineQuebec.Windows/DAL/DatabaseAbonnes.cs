﻿using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;


namespace CineQuebec.Windows.DAL;

public class DatabaseAbonnes : IDatabaseAbonnes
{
    private readonly IMongoClient _mongoDBClient;
    private readonly IMongoDatabase _database;

    public DatabaseAbonnes()
    {
        _mongoDBClient = GetClient();
        _database = GetDatabase(_mongoDBClient);
    }
    public IMongoDatabase GetDatabase(IMongoClient client)
    {
        return client.GetDatabase("TP2DB");
    }
    
    public IMongoClient GetClient()
    {
        return new MongoClient("mongodb://localhost:27017/");
    }
    public List<Abonne> ReadAbonnes()
    {
        var abonnes = new List<Abonne>();
        try
        {
            var collection = _database.GetCollection<Abonne>("Abonnes");
            abonnes = collection.Aggregate().ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
        }
        return abonnes;
    }
}
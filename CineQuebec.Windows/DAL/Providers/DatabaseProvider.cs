using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Providers;

public class DatabaseProvider : IDatabaseProvider
{
    public IMongoDatabase GetDatabase(IMongoClient client)
    {
        return client.GetDatabase("TP2DB");
    }
    public IMongoClient GetClient()
    {
        return new MongoClient("mongodb://localhost:27017/");
    }
    
    public DatabasePeleMele GetDatabasePeleMele()
    {
        return new DatabasePeleMele(this);
    }
}
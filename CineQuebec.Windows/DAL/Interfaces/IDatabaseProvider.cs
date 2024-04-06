using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Interfaces;

public interface IDatabaseProvider
{
    IMongoDatabase GetDatabase(IMongoClient client);
    IMongoClient GetClient();
    DatabaseFilms GetDatabasePeleMele();
}
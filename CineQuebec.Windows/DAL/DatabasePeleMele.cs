using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Providers;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL
{
    public class DatabasePeleMele
    {
        private readonly DatabaseProvider _databaseProvider;
        private readonly IMongoClient _mongoDBClient;
        private readonly IMongoDatabase _database;

        public DatabasePeleMele(DatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
            _mongoDBClient = _databaseProvider.GetClient();
            _database = _databaseProvider.GetDatabase(_mongoDBClient);
        }
        
        virtual public List<Abonne> ReadAbonnes()
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

        virtual public List<Film> ReadFilms()
        {
            var films = new List<Film>();
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                films = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return films;
        }   

        virtual public void CreateFilm(Film film)
        {
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                collection.InsertOne(film);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'insérer le film " + ex.Message, "Erreur");
            }
        }

        virtual public void UpdateFilm(Film film)
        {
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                var filter = Builders<Film>.Filter.Eq("Id", film.Id);
                var update = Builders<Film>.Update.Set("Projections", film.Projections);
                collection.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de mettre à jour le film " + ex.Message, "Erreur");
            }
        }

        virtual public void DeleteFilmById(ObjectId id)
        {
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                var filter = Builders<Film>.Filter.Eq("Id", id);
                collection.FindOneAndDelete(filter);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de mettre à jour le film " + ex.Message, "Erreur");
                throw;
            }
        }
        virtual public List<List<string>> GetAllProjections()
        {
            var projections = new List<List<string>>();
            try
            {
                var collection = _database.GetCollection<Film>("Films");
                var films = collection.Aggregate().ToList();
                foreach (var film in films)
                {
                    projections.AddRange(film.Projections);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir les projections " + ex.Message, "Erreur");
            }
            return projections;
        }  
    }
}

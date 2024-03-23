using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL
{
    public class DatabasePeleMele
    {
        private IMongoClient mongoDBClient;
        private IMongoDatabase database;

        public DatabasePeleMele(IMongoClient client = null)
        {
            mongoDBClient = client ?? OuvrirConnexion();
            database = ConnectDatabase();
        }
        private IMongoClient OuvrirConnexion()
        {
            MongoClient dbClient = null;
            try
            {
                dbClient = new MongoClient("mongodb://localhost:27017/");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de se connecter à la base de données " + ex.Message, "Erreur");
            }
            return dbClient;
        }

        private IMongoDatabase ConnectDatabase()
        {
            IMongoDatabase db = null;
            try
            {
                db = mongoDBClient.GetDatabase("TP2DB");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de se connecter à la base de données " + ex.Message, "Erreur");
            }
            return db;
        }
        public List<Abonne> ReadAbonnes()
        {
            var abonnes = new List<Abonne>();
            try
            {
                var collection = database.GetCollection<Abonne>("Abonnes");
                abonnes = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return abonnes;
        }

        public List<Film> ReadFilms()
        {
            var films = new List<Film>();
            try
            {
                var collection = database.GetCollection<Film>("Films");
                films = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return films;
        }   

        public void CreateFilm(Film film)
        {
            try
            {
                var collection = database.GetCollection<Film>("Films");
                collection.InsertOne(film);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'insérer le film " + ex.Message, "Erreur");
            }
        }

        public void UpdateFilm(Film film)
        {
            try
            {
                var collection = database.GetCollection<Film>("Films");
                var filter = Builders<Film>.Filter.Eq("Id", film.Id);
                var update = Builders<Film>.Update.Set("Projections", film.Projections);
                collection.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de mettre à jour le film " + ex.Message, "Erreur");
            }
        }
    }
}

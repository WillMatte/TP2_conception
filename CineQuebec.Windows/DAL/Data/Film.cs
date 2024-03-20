using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film
    {
        public ObjectId Id { get; set; }
        public string Titre { get; set; }

        public override string ToString()
        {
            return $"{Titre}";
        }
    }
}

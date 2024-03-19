using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Abonne
    {
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public DateTime DateJoined { get; set; }

        public override string ToString()
        {
            return $"{Username} - Membre depuis {DateJoined.Year}/{DateJoined.Month}/{DateJoined.Day}";
        }
    }
}

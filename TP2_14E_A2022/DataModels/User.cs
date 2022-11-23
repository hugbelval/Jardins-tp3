using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TP2_14E_A2022.DataModels
{
    public class User
    {
        [BsonId]
        public ObjectId id;

        [BsonElement("email")]
        public string email;

        [BsonElement("hashPwd")]
        public string hashPwd;

        [BsonElement("firstName")]
        public string firstName;

        [BsonElement("lastName")]
        public string lastName;

        [BsonElement("address")]
        public string address;

        [BsonElement("telephone")]
        public string telephone;

        [BsonElement("subscriptionEnd")]
        public DateTime subscriptionEnd;

        [BsonElement("isAdmin")]
        public bool isAdmin;

        [BsonConstructor]
        public User(string email, string hashPwd, string firstName, string lastName, string address, string telephone, DateTime subscriptionEnd, bool isAdmin)
        {
            this.email = email;
            this.hashPwd = hashPwd;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.telephone = telephone;
            this.subscriptionEnd = subscriptionEnd;
            this.isAdmin = isAdmin;
        }

        public override string ToString()
        {
            string abonnement = subscriptionEnd > DateTime.Now ? "Abonné" : "Non abonné";
            return firstName + " " + lastName + ", " + abonnement;
        }
    }
}

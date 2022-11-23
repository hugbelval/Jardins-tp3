using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TP2_14E_A2022.DataModels
{
    public class Material
    {
        [BsonId]
        public ObjectId id;

        [BsonElement("name")]
        public string name;

        [BsonElement("description")]
        public string description;

        [BsonConstructor]
        public Material(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public override string ToString()
        {
            return name;
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TP2_14E_A2022.Lots;

namespace TP2_14E_A2022.DataModels
{
    public class Lot
    {
        [BsonId]
        public ObjectId id;

        [BsonElement("lotNumber")]
        public int lotNumber;

        [BsonElement("ownerId")]
        public ObjectId? ownerId;

        [BsonElement("state")]
        public LotState state;

        [BsonElement("areaInMeterSquared")]
        public float areaInMeterSquared;

        [BsonElement("depthInMeters")]
        public float depthInMeters;
    }
}

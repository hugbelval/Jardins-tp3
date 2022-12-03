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

        [BsonConstructor]
        public Lot(int lotNumber, ObjectId? ownerId, float areaInMeterSquared, float depthInMeters)
        {
            this.lotNumber = lotNumber;
            this.ownerId = ownerId;
            this.state = LotState.Inactive;
            this.areaInMeterSquared = areaInMeterSquared;
            this.depthInMeters = depthInMeters;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is Lot other)
            {
                return lotNumber == other.lotNumber &&
                    ownerId == other.ownerId &&
                    state == other.state &&
                    areaInMeterSquared == other.areaInMeterSquared &&
                    depthInMeters == other.depthInMeters;
            }
            return false;

        }
    }
}

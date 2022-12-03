using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TP2_14E_A2022.Lots;

namespace TP2_14E_A2022.DataModels
{
    public class Lot
    {
        private const float cubicMeterToLiterConversionRate = 1000f; 

        [BsonId]
        public ObjectId id;

        [BsonElement("lotNumber")]
        public int lotNumber;

        [BsonElement("ownerId")]
        public ObjectId? ownerId;

        [BsonElement("state")]
        public LotState state;

        [BsonElement("areaInSquareMeters")]
        public float areaInSquareMeters;

        [BsonElement("depthInMeters")]
        public float depthInMeters;

        [BsonConstructor]
        public Lot(int lotNumber, ObjectId? ownerId, float areaInSquareMeters, float depthInMeters)
        {
            this.lotNumber = lotNumber;
            this.ownerId = ownerId;
            this.state = LotState.Inactive;
            this.areaInSquareMeters = areaInSquareMeters;
            this.depthInMeters = depthInMeters;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is Lot other)
            {
                return lotNumber == other.lotNumber &&
                    ownerId == other.ownerId &&
                    state == other.state &&
                    areaInSquareMeters == other.areaInSquareMeters &&
                    depthInMeters == other.depthInMeters;
            }
            return false;
        }

        public float GetVolumeInLiters()
        {
            return areaInSquareMeters * depthInMeters * cubicMeterToLiterConversionRate; 
        }
    }
}

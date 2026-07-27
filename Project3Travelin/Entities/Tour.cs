using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities
{
    public class Tour
    {
        [BsonId] //mssql de primary key olarak düşünülebilir
        [BsonRepresentation(BsonType.ObjectId)] 
        public string TourId { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public DateTime TourDate { get; set; }
        public string DayNight { get; set; }  // 3 gece 5 gün gibi ifadeler için
    }
}

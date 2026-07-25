using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tour> _tourCollection;

        public TourService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client=new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tourCollection = database.GetCollection<Tour>(databaseSettings.TourCollectionName);
            _mapper = mapper;
        }

        public async Task CreateTourAsync(CreateTourDto createTourDto)
        {
            var values= _mapper.Map<Tour>(createTourDto);
            await _tourCollection.InsertOneAsync(values);
        }

        public async Task DeleteTourAsync(string id)
        {
            await _tourCollection.DeleteOneAsync(x => x.TourId == id);

        }

        public async Task<List<ResultTourDto>> GetAllToursAsync()
        {
            var values=await _tourCollection.Find(x=>true).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public Task<GetTourByIdDto> GetTourByIdAsync(string id)
        {
            var value= _tourCollection.Find(x=>x.TourId==id).FirstOrDefaultAsync();
            return _mapper.Map<Task<GetTourByIdDto>>(value);
        }

        public async Task UpdateTourAsync(UpdateTourDto updateTourDto)
        {
           var values=_mapper.Map<Tour>(updateTourDto);
           await _tourCollection.FindOneAndReplaceAsync(x=>x.TourId==updateTourDto.TourId,values);
        }
    }
}

using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.CategoryDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.CategoryServices
{
    public class CategoryService : ICategoryService //interface deki metodları çağırmış olduk.
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection; //dışarıdan _categoryCollection isimli field örnekledik.Dışarıda örnekledik çünkü Dependency injection kullanmak için.

        public CategoryService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); //veritabanı adresine ulaşmak için MongoClient sınıfından client nesnesi oluşturduk.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); //veritabanına bağlanmak için GetDatabase metodunu kullandık ve database nesnesi oluşturduk.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); //veritabanındaki Category koleksiyonuna ulaşmak için GetCollection metodunu kullandık ve _categoryCollection field'ını örnekledik.
            _mapper = mapper;
        }
        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var values = _mapper.Map<Category>(createCategoryDto); //CreateCategoryDto nesnesini Category nesnesine dönüştürdük.
            await _categoryCollection.InsertOneAsync(values); //Category nesnesini veritabanına ekledik.
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id); //veritabanındaki Category koleksiyonundan id'si verilen Category nesnesini sildik.
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values=await _categoryCollection.Find(x=>true).ToListAsync(); //veritabanındaki Category koleksiyonundaki tüm Category nesnelerini listeledik.values verileri MongoDb den çekicek.
            return _mapper.Map<List<ResultCategoryDto>>(values); //Category nesnelerini ResultCategoryDto nesnelerine dönüştürdük ve listeledik.
        }

        public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id)
        {
            var value=await _categoryCollection.Find(x=>x.CategoryId==id).FirstOrDefaultAsync(); //veritabanındaki Category koleksiyonundan id'si verilen Category nesnesini bulduk.
            return _mapper.Map<GetCategoryByIdDto>(value); //Category nesnesini GetCategoryByIdDto nesnesine dönüştürdük.
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var values=_mapper.Map<Category>(updateCategoryDto); //UpdateCategoryDto nesnesini Category nesnesine dönüştürdük.
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategoryDto.CategoryId,values); //veritabanındaki Category koleksiyonunda id'si verilen Category nesnesini güncelledik.
        }
    }
}

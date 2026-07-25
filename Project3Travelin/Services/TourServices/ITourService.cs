using Project3Travelin.Dtos.TourDtos;

namespace Project3Travelin.Services.TourServices
{
    public interface ITourService
    {
        Task<List<ResultTourDto>> GetAllToursAsync();
        Task CreateTourAsync(CreateTourDto createTourDto);
        Task UpdateTourAsync(UpdateTourDto updateTourDto);
        Task DeleteTourAsync(string id);
        Task<GetTourByIdDto> GetTourByIdAsync(string id);
    }
}

using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.ViewComponents.TourViewComponents
{
    public class _TourListComponentPartial:ViewComponent
    {
        private readonly ITourService _tourService;
        public _TourListComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _tourService.GetAllToursAsync();
            return View(values);
        }
        
    }
}

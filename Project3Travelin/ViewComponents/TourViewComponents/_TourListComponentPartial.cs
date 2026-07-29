using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.ViewComponents.TourViewComponents
{
    public class _TourListComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;
        private const int PageSize = 3;

        public _TourListComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Sayfa numarasını URL'den oku (?page=2 gibi). Yoksa 1. sayfa.
            int page = 1;
            if (int.TryParse(HttpContext.Request.Query["page"], out var parsedPage) && parsedPage > 0)
            {
                page = parsedPage;
            }

            var allTours = await _tourService.GetAllToursAsync();

            var totalCount = allTours.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            var pagedTours = allTours
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = PageSize;

            return View(pagedTours);
        }
    }
}

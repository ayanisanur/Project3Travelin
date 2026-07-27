using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CategoryDtos;
using Project3Travelin.Services.CategoryServices;

namespace Project3Travelin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult CreateCategory() //sayfa ilk açıldığında boş bi şekilde sadece tasrımı getiricek. post yaptığımızda async metodlar çlaışıcak.bunun amacı boş sayfayı getirmek.
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            createCategoryDto.IsSatus = true;
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return RedirectToAction("CategoryList");
        }
    }
}

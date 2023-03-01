using BookStore.API.Dtos.Category;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using BookStore.API.Utils;
using BookStore.Domain.Entities;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService; 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAll();

            // Map Entity to Dto..
            var listCategoriesDto = categories.Adapt<IEnumerable<CategoryResultDto>>();

            return Ok(listCategoriesDto);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(long id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null) 
                return base.NotFound(Enums.StatusCode.CategoryNotFound.GetDescription());

            // Map Entity to Dto..
            var categoryDto = category.Adapt<CategoryResultDto>();

            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory(CategoryAddDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = categoryDto.Adapt<Category>();

            await _categoryService.Add(category);
            return Ok(category);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory(long id, CategoryEditDto categoryDto)
        {
            if (id != categoryDto.Id) 
                return base.BadRequest(Enums.StatusCode.CategoryNotMatch.GetDescription());

            if (!ModelState.IsValid) return BadRequest();

            var category = categoryDto.Adapt<Category>();
            await _categoryService.Update(category);
            
            return Ok(categoryDto);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null) 
                return base.NotFound(Enums.StatusCode.CategoryNotFound.GetDescription());

           var isDeleted = await _categoryService.Remove(category);

           if (!isDeleted) 
                return base.BadRequest(Enums.StatusCode.CategoryDeletedKO.GetDescription());

            return base.BadRequest(Enums.StatusCode.CategorySuccessDeleted.GetDescription());

           //var CategoryDto = category.Adapt<CategoryResultDto>();
           //return Ok(CategoryDto);
        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryResultDto>>> SearchCategory(string searchCategory)
        {
            var listCategories = await _categoryService.Search(searchCategory);

            if (listCategories != null && listCategories.Any())
            {
                var categories = listCategories.Adapt<IEnumerable<CategoryResultDto>>();
                
                return Ok(categories);
            }

            return base.NotFound(Enums.StatusCode.CategoryNotFound.GetDescription());
        }
    }
}
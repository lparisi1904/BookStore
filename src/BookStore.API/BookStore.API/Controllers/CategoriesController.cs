using BookStore.API.Dtos.Category;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster;


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
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();

            // Map Entity to Dto..
            var listCategoriesDto = categories.Adapt<IEnumerable<Dtos.CategoryResultDto>>();

            return Ok(listCategoriesDto);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null) return NotFound("Categoria non presente.");

            // Map Entity to Dto..
            var categoryDto = category.Adapt<Dtos.CategoryResultDto>();

            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CategoryAddDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = categoryDto.Adapt<Category>();

            await _categoryService.Add(category);
            return Ok(category);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(long id, CategoryEditDto categoryDto)
        {
            if (id != categoryDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var category = categoryDto.Adapt<Category>();
            await _categoryService.Update(category);
            
            return Ok(categoryDto);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null) return NotFound();

           var isDeleted = await _categoryService.Remove(category);

           if (!isDeleted) return BadRequest();

           var CategoryDto = category.Adapt<Dtos.CategoryResultDto>();

           return Ok(CategoryDto);
        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Dtos.CategoryResultDto>>> Search(string searchCategory)
        {
            var listCategories = await _categoryService.Search(searchCategory);

            if (listCategories != null && listCategories.Count() != 0)
            {
                var categories = listCategories.Adapt<IEnumerable<Dtos.CategoryResultDto>>();

                
                return Ok(categories);
            }

            return NotFound("Nessuna categoria trovata.");
        }
    }
}

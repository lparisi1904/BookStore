using BookStore.API.Dtos;
using BookStore.API.Dtos.Category;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Domain.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

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
            //var categories = await _categoryService.GetAll();

            //return Ok(_mapper.Map<IEnumerable<CategoryResultDto>>(categories));

            var categories = await _categoryService.GetAll();
            var listCategories = categories.Adapt<IEnumerable<Dtos.CategoryResultDto>>();

            return Ok(listCategories);
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
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = categoryAddDto.Adapt(categoryAddDto.Adapt<Category>());

            await _categoryService.Add(category);
            return Ok(category);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(long id, CategoryEditDto editDto)
        {
            if (id != editDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var category = editDto.Adapt<Category>();
            await _categoryService.Update(category);
            
            return Ok(editDto);
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

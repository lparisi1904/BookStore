using BookStore.API.Dtos;
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
            var listCategories = categories.Adapt<IEnumerable<CategoryResultDto>>();

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
            var categoryDto = category.Adapt<CategoryResultDto>();

            return Ok(categoryDto);
        }


    }
}

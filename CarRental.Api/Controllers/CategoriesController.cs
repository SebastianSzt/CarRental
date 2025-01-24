using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRental.Dto.Categories;
using CarRental.Model.Entities;
using CarRental.Repository.Categories;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            if (categories == null)
                return NotFound();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryInputDto category)
        {
            if (category == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCategory = new Category()
            {
                Name = category.Name
            };

            var result = await _categoryRepository.SaveCategoryAsync(newCategory);
            if (!result)
                throw new Exception("Error saving category");

            var categoryDto = _mapper.Map<CategoryDto>(newCategory);

            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryInputDto category)
        {
            if (category == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                return NotFound();

            existingCategory.Name = category.Name;

            var result = await _categoryRepository.SaveCategoryAsync(existingCategory);
            if (!result)
                throw new Exception("Error updating category");

            var categoryDto = _mapper.Map<CategoryDto>(existingCategory);

            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                return NotFound();

            var result = await _categoryRepository.DeleteCategoryAsync(id);
            if (!result)
                throw new Exception("Error deleting category");

            return Ok();
        }
    }
}

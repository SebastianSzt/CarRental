using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRental.Dto.Products;
using CarRental.Model.Entities;
using CarRental.Repository.Products;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllProductsAsync();
            if (products == null)
                return NotFound();

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return Ok(productsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductInputDto product)
        {
            if (product == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            var result = await _productRepository.SaveProductAsync(newProduct);
            if (!result)
                throw new Exception("Error saving product");

            // Ponownie pobieram produkt, aby załadować powiązaną kategorię (bez tego nie wyświetla się w odpowiedzi nazwa kategorii). Nie wiem jak to zrobić lepiej.
            newProduct = await _productRepository.GetProductByIdAsync(newProduct.Id);

            var productDto = _mapper.Map<ProductDto>(newProduct);

            return Ok(productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductInputDto product)
        {
            if (product == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;

            var result = await _productRepository.SaveProductAsync(existingProduct);
            if (!result)
                throw new Exception("Error updating product");

            var productDto = _mapper.Map<ProductDto>(existingProduct);

            return Ok(productDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
                return NotFound();

            var result = await _productRepository.DeleteProductAsync(id);
            if (!result)
                throw new Exception("Error deleting product");

            return Ok();
        }
    }
}

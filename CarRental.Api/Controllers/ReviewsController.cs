using AutoMapper;
using CarRental.Dto.Reviews;
using CarRental.Model.Entities;
using CarRental.Repository.Reviews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();

            var reviewDto = _mapper.Map<ReviewDto>(review);

            return Ok(reviewDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            if (reviews == null)
                return NotFound();

            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);

            return Ok(reviewsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReviewInputDto review)
        {
            if (review == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newReview = new Review()
            {
                Comment = review.Comment,
                Rating = review.Rating,
                Date = review.Date,
                CarId = review.CarId,
                UserId = review.UserId
            };

            var result = await _reviewRepository.SaveReviewAsync(newReview);
            if (!result)
                throw new Exception("Error saving review");

            newReview = await _reviewRepository.GetReviewByIdAsync(newReview.Id);

            var reviewDto = _mapper.Map<ReviewDto>(newReview);

            return Ok(reviewDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReviewInputDto review)
        {
            if (review == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingReview = await _reviewRepository.GetReviewByIdAsync(id);
            if (existingReview == null)
                return NotFound();

            existingReview.Comment = review.Comment;
            existingReview.Rating = review.Rating;
            existingReview.Date = review.Date;
            existingReview.CarId = review.CarId;
            existingReview.UserId = review.UserId;

            var result = await _reviewRepository.SaveReviewAsync(existingReview);
            if (!result)
                throw new Exception("Error updating review");

            var reviewDto = _mapper.Map<ReviewDto>(existingReview);

            return Ok(reviewDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingReview = await _reviewRepository.GetReviewByIdAsync(id);
            if (existingReview == null)
                return NotFound();

            var result = await _reviewRepository.DeleteReviewAsync(id);
            if (!result)
                throw new Exception("Error deleting review");

            return Ok();
        }
    }
}

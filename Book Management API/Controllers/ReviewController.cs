using Book_Management_API.Data;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;
using Book_Management_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult GetReview(int id)
        {

            
            try
            {
                var review = _reviewService.GetReviewById(id);
                return Ok(review);
            }
            catch (NotFoundErrorException ex)
            {

                return BadRequest(ex.Message);
            }
        
            
        }

        [HttpGet("GetBookReviews")]
        [Authorize]
        public IActionResult GetAllReviews(int bookId)
        {
            var reviews = _reviewService.GetAllReviews(bookId);
            return Ok(reviews);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddReview(ReviewDto review)
        {
            var userClaims = User.Claims;
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (userIdClaim != null)
            {
               
                var user = userIdClaim.Value;
                try
                {
                    _reviewService.CreateBookReviews(review, user);
                    return Ok();
                }
                catch (ConflictErrorException Ex)
                {
                    return BadRequest(Ex.Message);
                }
                catch (Exception Ex)
                {
                    return BadRequest(Ex.Message);
                }

               

            }
            else
            {
                return NotFound("User claim not found in the token.");
            }

            
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateReview(int id, ReviewDto review)
        {
            var userClaims = User.Claims;
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (userIdClaim != null)
            {

                var user = userIdClaim.Value;
                var roleClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                if (roleClaim == null) {
                    return NotFound("Role claim not found in the token.");
                }
                 var role = roleClaim.Value;

                try
                {
                    _reviewService.UpdateBookReviews(review, id, user, role);
                }                
                catch (ConflictErrorException Ex)
                {
                  return BadRequest(Ex.Message);
                }
                catch (Exception Ex)
                {
                  return BadRequest(Ex.Message);
                }


            return Ok();

            }
            else
            {
                return NotFound("User not found in the token.");
            }
                       
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteReview(int id)
        {
            _reviewService.DeleteBookReviews(id);
            return Ok();
        }
    }
}

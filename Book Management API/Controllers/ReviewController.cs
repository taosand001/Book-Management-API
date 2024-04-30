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
        public ActionResult GetReview(int id)
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
        public ActionResult GetAllReviews(int bookId)
        {
            var reviews = _reviewService.GetAllReviews(bookId);
            if (reviews == null)
                return NotFound();

            double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            return Ok(new { AverageRating = averageRating, Reviews = reviews });
            
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddReview(ReviewDto review)
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
        public ActionResult UpdateReview(int id, ReviewDto review)
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
        public ActionResult DeleteReview(int id)
        {
            _reviewService.DeleteBookReviews(id);
            return Ok();
        }
    }
}

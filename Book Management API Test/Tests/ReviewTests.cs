using AutoFixture;
using AutoFixture.Xunit2;
using Book_Management_API.Controllers;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;
using Book_Management_API.Service;
using Book_Management_API_Test.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book_Management_API_Test.Tests
{
    
    public class ReviewTests
    {
       

        [Theory ,ReviewData]
        public void GetReview_Returns_OkResult(int id ,Review review)
        {
            //Arrange
            var mockUserService = new Mock<IReviewService>();
            var sut = new ReviewController(mockUserService.Object);
            // mockUserService.Setup(x=>x.
            mockUserService.Setup(x => x.GetReviewById(It.IsAny<int>())).Returns(review);

            //Act
            var result = sut.GetReview(id);
                
            //Assert    


           var okresult = Assert.IsType<OkObjectResult>(result);

            //Assert.IsType<OkObjectResult>(okresult.);
            Assert.Equal(review, okresult.Value);

        }

        [Theory, ReviewData]
        public void AddReview_Returns_OkResult(ReviewDto review, string userId,
            [Frozen] Mock<ClaimsPrincipal> mockClaimsPrincipal)
        {
            //Arrange
            var mockUserService = new Mock<IReviewService>();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userId) };
            mockClaimsPrincipal.Setup(cp => cp.Claims).Returns(claims);
            
            var sut = new ReviewController(mockUserService.Object);

            sut.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = mockClaimsPrincipal.Object } };
            
            
            // mockUserService.Setup(x=>x.
            mockUserService.Setup(x => x.CreateBookReviews(It.IsAny<ReviewDto>(), It.IsAny<string>()));

            //Act
            var result = sut.AddReview(review);

            //Assert    


            var okResult = Assert.IsType<OkResult>(result);
            Assert.NotNull(okResult);

        }

        [Theory, ReviewData]
        public void DelteReview_Returns_OkResult(ReviewDto review, string userId,int reviewId,
    [Frozen] Mock<ClaimsPrincipal> mockClaimsPrincipal)
        {
            //Arrange
            var mockUserService = new Mock<IReviewService>();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userId) };
            mockClaimsPrincipal.Setup(cp => cp.Claims).Returns(claims);

            var sut = new ReviewController(mockUserService.Object);

            sut.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = mockClaimsPrincipal.Object } };


            // mockUserService.Setup(x=>x.
            mockUserService.Setup(x => x.DeleteBookReviews(It.IsAny<int>()));

            //Act
            var result = sut.DeleteReview(reviewId);

            //Assert    


            var okResult = Assert.IsType<OkResult>(result);
            Assert.NotNull(okResult);

        }

        [Theory, ReviewData]
        public void UpdateReview_Returns_OkResult(ReviewDto review, string userId, int reviewId,
[Frozen] Mock<ClaimsPrincipal> mockClaimsPrincipal)
        {
            //Arrange
            var mockUserService = new Mock<IReviewService>();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userId) };
            mockClaimsPrincipal.Setup(cp => cp.Claims).Returns(claims);

            var sut = new ReviewController(mockUserService.Object);

            sut.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = mockClaimsPrincipal.Object } };


            // mockUserService.Setup(x=>x.
            mockUserService.Setup(x => x.UpdateBookReviews(It.IsAny<ReviewDto>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

            //Act
            var result = sut.DeleteReview(reviewId);

            //Assert    


            var okResult = Assert.IsType<OkResult>(result);
            Assert.NotNull(okResult);

        }

        [Theory, ReviewData]
        public void GetAllReviews_Returns_OkResult(int bookId)
        {
            //Arrange
            var mockUserService = new Mock<IReviewService>();
            var sut = new ReviewController(mockUserService.Object);            


            // mockUserService.Setup(x=>x.
            mockUserService.Setup(x => x.GetAllReviews(It.IsAny<int>())).Returns(new List<Review>());

            //Act
            var result = sut.GetAllReviews(bookId);

            //Assert    


            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);

        }
    }
}

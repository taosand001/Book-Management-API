using AutoFixture;
using Book_Management_API.Controllers;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;
using Book_Management_API.Service;
using Book_Management_API_Test.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Management_API_Test.Tests
{
    
    public class ReviewTests
    {
        //private Fixture _fixture = new Fixture();        
        //public ReviewTests(Fixture fixture)
        //{
        //    _fixture.Customizations.Add(new ReviewSpecimenBuilder());
        //}

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


            // var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.IsType<Review>(result);
            Assert.Equal(review, result.Value);
        }
    }
}

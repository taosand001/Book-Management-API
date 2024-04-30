using Book_Management_API.Controllers;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Services;
using Book_Management_API_Test.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Book_Management_API_Test.Tests
{
    public class UserTests
    {
        //[Theory, UserData]
        //public void Service_SignUp_SignUps_Succesful(UserDto user)
        //{
        //    Arrange
        //    var serviceMock = new Mock<IUserService>();
        //    var jwtMock = new Mock<IJwtService>();
        //    var sut = new UserController(serviceMock.Object, jwtMock.Object);
        //    serviceMock.Setup(s => s.Register(It.IsAny<CreateUserDto>())).Returns(user);

        //    Act
        //    var testResponse = sut.SingUp(new CreateUserDto(Username: "Username", Password: "Password"));
        //    var okResult = Assert.IsType<OkObjectResult>(testResponse.Result);
        //    var returnedUser = Assert.IsType<UserDto>(okResult.Value);

        //    Assert
        //    Assert.Equal(returnedUser, user);
        //}

        [Theory, UserData]
        public void Service_LogIn_LogsIn_Succesful(string token)
        {
            //Arrange
            var serviceMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var sut = new UserController(serviceMock.Object, jwtMock.Object);
            serviceMock.Setup(s => s.Login(It.IsAny<LoginDto>())).Returns(token);

            //Act
            var testResponse = sut.Login(new LoginDto("Username", "Password"));
            var okResult = Assert.IsType<OkObjectResult>(testResponse.Result);
            var returnedToken = Assert.IsType<string>(okResult.Value);

            //Assert
            Assert.Equal(returnedToken, token);
        }
    }
}
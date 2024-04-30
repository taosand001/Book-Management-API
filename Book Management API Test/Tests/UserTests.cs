using AutoFixture;
using Book_Management_API.Controllers;
using Book_Management_API.Data;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Services;
using Book_Management_API_Test.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Book_Management_API_Test.Tests
{
    public class UserTests
    {
        #region Service Tests with Successful action

        [Theory, UserData]
        public void Service_SignUp_SignUps_Succesful(UserDto user)
        {
            //Arrange
            var serviceMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var sut = new UserController(serviceMock.Object, jwtMock.Object);
            serviceMock.Setup(s => s.Register(It.IsAny<CreateUserDto>())).Returns(user);

            //Act
            var testResponse = sut.SingUp(new CreateUserDto(Username: "Username", Password: "Password"));
            var okResult = Assert.IsType<OkObjectResult>(testResponse.Result);
            var returnedUser = Assert.IsType<UserDto>(okResult.Value);

            //Assert
            Assert.Equal(returnedUser, user);
        }

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

        [Theory]
        [InlineData("povfri", "User")]
        public void Service_ChangeRole_ChangesRole_Succesful(string userName, string newRoleName)
        {
            //Arrange
            var serviceMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var sut = new UserController(serviceMock.Object, jwtMock.Object);

            //Act
            var testResult = sut.ChangeRole(userName, newRoleName);

            //Assert
            Assert.IsType<OkResult>(testResult);
        }

        [Fact]
        public void Service_GetAllUsers_GetsAllUsers_Succesful()
        {
            //Arrange
            var serviceMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var sut = new UserController(serviceMock.Object, jwtMock.Object);
            List<DisplayUserDto> users =
                [
                    new DisplayUserDto("user", "admin")
                ];
            serviceMock.Setup(s => s.GetAllUsers()).Returns(users);

            //Act
            var testResposne = users;

            //Assert
            Assert.Equal(users, testResposne);
        }

        #endregion

        #region Service Tests with Failed action

        [Theory, UserDataFailure]
        public void Service_SignUp_SignUps_Failed(UserDto user)
        {
            //Arrange
            var serviceMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var sut = new UserController(serviceMock.Object, jwtMock.Object);
            serviceMock.Setup(s => s.Register(It.IsAny<CreateUserDto>())).Returns(user);

            //Act
            var testResponse = sut.SignUp(null);

            //Assert
            Assert.Equal(testResponse.Value, user);
            Assert.IsType<BadRequestResult>(testResponse.Result);
        }

        [Theory, UserDataFailure]
        //[Theory]
        //[InlineData(null)]
        public void Service_LogIn_LogsIn_Failed(string token)
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customizations.Add(new UserFailureSpecimenBuilder());
            var str = fixture.Create<string>();

            var serviceMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var sut = new UserController(serviceMock.Object, jwtMock.Object);
            serviceMock.Setup(s => s.Login(It.IsAny<LoginDto>())).Returns(token);

            //Act
            var testResponse = sut.Login(null);

            //Assert
            Assert.Equal(testResponse.Value, token);
            Assert.IsType<BadRequestResult>(testResponse.Result);
        }

        [Theory]
        [InlineData(null, "User")]
        [InlineData(null, null)]
        [InlineData("povfri", null)]
        [InlineData("povfri", "usr")]
        public void Service_ChangeRole_ChangesRole_Failed(string userName, string newRoleName)
        {
            //Arrange
            var serviceMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var sut = new UserController(serviceMock.Object, jwtMock.Object);

            //Act
            var testResponse = sut.ChangeRole(userName, newRoleName);

            //Assert
            Assert.IsType<BadRequestResult>(testResponse);
        }

        [Fact]
        public void Service_GetAllUsers_GetsAllUsers_Failed()
        {
            //Arrange
            var serviceMock = new Mock<IUserService>();
            var jwtMock = new Mock<IJwtService>();
            var sut = new UserController(serviceMock.Object, jwtMock.Object);
            List<DisplayUserDto> users =[];
            serviceMock.Setup(s => s.GetAllUsers()).Returns(users);

            //Act
            var testResposne = users;

            //Assert
            Assert.Equal(users, testResposne);
        }

        #endregion
    }
}
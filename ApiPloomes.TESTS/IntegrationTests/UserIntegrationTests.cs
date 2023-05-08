using ApiPloomes.DOMAIN.DTOs.Request;
using ApiPloomes.DOMAIN.DTOs.Response;
using ApiPloomes.TESTS.Configurations;
using ApiPloomes.TESTS.Configurations.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiPloomes.TESTS.IntegrationTests
{
    public class UserIntegrationTest
    {

        [Theory]
        [InlineData("user1@mail.com", "Username1")]
        [InlineData("user2@mail.com", "Username2")]
        public async Task GET_Return_User_By_Email_Success(string email, string username)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);
            var expectedUser = new UserResponse(username, email);
            var expectedJson = JsonConvert.SerializeObject(expectedUser);

            var url = $"/api/User/{email}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<UserResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedJson, actualResponse);
        }

        [Fact]
        public async Task GET_Return_User_By_Email_DoesntExist()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var fakeUserMail = "user3@mail.com";

            var url = $"/api/User/{fakeUserMail}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GET_Return_User_By_Id_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var usersId = await UserMockData.GetUsersId(application);

            var expectedEmail = "user1@mail.com";
            var expectedUsername = "Username1";
            var expectedUser = new UserResponse(expectedUsername, expectedEmail);
            var expectedJson = JsonConvert.SerializeObject(expectedUser);

            var url = $"/api/User/{usersId[0]}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<UserResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedJson, actualResponse);
        }

        [Fact]
        public async Task GET_Return_User_By_Id_BadRequest()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var userId = new Guid("11111111-1111-1111-1111-111111111111");

            var url = $"/api/User/{userId}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<UserResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task POST_Register_New_User_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var registerUser = new UserCreationRequest("Username", "ValidPassword3", "user3@mail.com");
            var url = $"/api/User/Create";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = JsonContent.Create(new
            {
                username = registerUser.Username,
                email = registerUser.Email,
                password = registerUser.Password
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PostAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Theory]
        [InlineData("Username", "email.com", "ValidPassword1")]
        [InlineData("Username", "@email.com", "ValidPassword1")]
        [InlineData("Username", "email@", "ValidPassword1")]
        [InlineData("Username", "email@mail.com", "short")]
        [InlineData("Username", "email@mail.com", "nouppercasepassword1")]
        [InlineData("Username", "email@mail.com", "PaswordNoNumber")]
        [InlineData("EmailExists", "user1@mail.com", "ValidPassword1")]
        public async Task POST_Register_New_User_BadRequest(string username, string email, string password)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var registerUser = new UserCreationRequest(username, password, email);
            var url = $"/api/User/Create";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = JsonContent.Create(new
            {
                username = registerUser.Username,
                email = registerUser.Email,
                password = registerUser.Password
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PostAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Theory]
        [InlineData("Valid Username", "@mail.com")]
        [InlineData("Valid Username", "invalidemail")]
        [InlineData("Valid Username", "user1@@mail.com")]
        public async Task PUT_Update_User_BadRequest(string username, string email)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var usersId = await UserMockData.GetUsersId(application);

            var registerUser = new UserUpdateRequest(username, email);
            var url = $"/api/User/Update/{usersId[0]}";
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            request.Content = JsonContent.Create(new
            {
                username = registerUser.Username,
                email = registerUser.Email,
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PutAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task PUT_Update_User_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var usersId = await UserMockData.GetUsersId(application);

            var expectedUser = new UserResponse("changedUsername", "changed@mail.com");
            var expectedJson = JsonConvert.SerializeObject(expectedUser);

            var registerUser = new UserUpdateRequest("changedUsername", "changed@mail.com");

            var url = $"/api/User/Update/{usersId[0]}";
            var confirmUrl = $"/api/User/{usersId[0]}";

            var request = new HttpRequestMessage(HttpMethod.Put, url);


            request.Content = JsonContent.Create(new
            {
                username = registerUser.Username,
                email = registerUser.Email,
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PutAsync(request.RequestUri, request.Content);

            var confirmResult = await client.GetAsync(confirmUrl);
            var resultContent = confirmResult.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<UserResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(HttpStatusCode.OK, confirmResult.StatusCode);
            Assert.Equal(expectedJson, actualResponse);


        }

        [Fact]
        public async Task PUT_Change_Password_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var usersId = await UserMockData.GetUsersId(application);

            var validPassword = "ValidPassword1";

            var url = $"/api/User/Update/ChangePassword/{usersId[0]}";

            var request = new HttpRequestMessage(HttpMethod.Put, url);



            request.Content = JsonContent.Create(validPassword);

            var client = application.CreateClient();


            //Act
            var result = await client.PutAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("short")]
        [InlineData("nouppercase1")]
        [InlineData("NOLOWERCASE1")]
        [InlineData("NoNumberPassword")]
        public async Task PUT_Change_Password_Fail(string password)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var usersId = await UserMockData.GetUsersId(application);

            var url = $"/api/User/Update/ChangePassword/{usersId[0]}";

            var request = new HttpRequestMessage(HttpMethod.Put, url);



            request.Content = JsonContent.Create(password);

            var client = application.CreateClient();


            //Act
            var result = await client.PutAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task DELETE_Delete_User_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var usersId = await UserMockData.GetUsersId(application);

            var url = $"/api/User/Delete/{usersId[0]}";
            var confirmUrl = $"/api/User/{usersId[0]}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            var client = application.CreateClient();


            //Act
            var result = await client.DeleteAsync(request.RequestUri);

            var confirmResult = await client.GetAsync(confirmUrl);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, confirmResult.StatusCode);
        }

        [Fact]
        public async Task DELETE_Delete_User_BadRequest()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await UserMockData.CreateUsers(application, true);

            var userId = new Guid("11111111-1111-1111-1111-111111111111");

            var url = $"/api/User/Delete/{userId}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            var client = application.CreateClient();


            //Act
            var result = await client.DeleteAsync(request.RequestUri);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}

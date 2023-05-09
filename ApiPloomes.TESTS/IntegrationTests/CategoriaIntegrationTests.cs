using ApiPloomes.DOMAIN.DTOs.Request;
using ApiPloomes.DOMAIN.DTOs.Response;
using ApiPloomes.TESTS.Configurations;
using ApiPloomes.TESTS.Configurations.Mocks;
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
    public class CategoriaIntegrationTests
    {
        [Theory]
        [InlineData("Eletrônicos")]
        [InlineData("Móveis")]
        public async Task GET_Return_Categoria_By_Name_Success(string nome)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);
            var expectedCategoria = new CategoriaResponse(nome);
            var expectedJson = JsonConvert.SerializeObject(expectedCategoria);

            var url = $"/api/Categoria/{nome}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<CategoriaResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedJson, actualResponse);
        }

        [Fact]
        public async Task GET_Return_Categoria_By_Nome_DoesntExist()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var fakeNome = "Veículos";

            var url = $"/api/Categoria/{fakeNome}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GET_Return_Categoria_By_Id_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var categoriasId = await CategoriaMockData.GetCategoriasId(application);

            var expectedNome = "Eletrônicos";
            var expectedCategoria = new CategoriaResponse(expectedNome);
            var expectedJson = JsonConvert.SerializeObject(expectedCategoria);

            var url = $"/api/Categoria/{categoriasId[0]}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<CategoriaResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedJson, actualResponse);
        }

        [Fact]
        public async Task GET_Return_Categoria_By_Id_BadRequest()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var categoriaId = new Guid("11111111-1111-1111-1111-111111111111");

            var url = $"/api/Categoria/{categoriaId}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<CategoriaResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task POST_Register_New_Categoria_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var registerCategoria = new CategoriaRequest("Roupas");
            var url = $"/api/Categoria/Create";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = JsonContent.Create(new
            {
                nome = registerCategoria.Nome
            });

            var client = application.CreateClient();

            //Act
            var result = await client.PostAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task POST_Register_New_Categoria_BadRequest(string nome)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var registerCategoria = new CategoriaRequest(nome);
            var url = $"/api/Categoria/Create";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = JsonContent.Create(new
            {
                nome = nome
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PostAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task PUT_Update_Categoria_BadRequest(string nome)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var categoriasId = await CategoriaMockData.GetCategoriasId(application);

            var registerCategoria = new CategoriaRequest(nome);
            var url = $"/api/Categoria/Update/{categoriasId[0]}";
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            request.Content = JsonContent.Create(new
            {
                nome = registerCategoria.Nome
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PutAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task PUT_Update_Categoria_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var categoriasId = await CategoriaMockData.GetCategoriasId(application);

            var expectedCategoria = new CategoriaResponse("CategoriaModificada");
            var expectedJson = JsonConvert.SerializeObject(expectedCategoria);

            var registerCategoria = new CategoriaRequest("CategoriaModificada");

            var url = $"/api/Categoria/Update/{categoriasId[0]}";
            var confirmUrl = $"/api/Categoria/{categoriasId[0]}";

            var request = new HttpRequestMessage(HttpMethod.Put, url);


            request.Content = JsonContent.Create(new
            {
                nome = registerCategoria.Nome
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PutAsync(request.RequestUri, request.Content);

            var confirmResult = await client.GetAsync(confirmUrl);
            var resultContent = confirmResult.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<CategoriaResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(HttpStatusCode.OK, confirmResult.StatusCode);
            Assert.Equal(expectedJson, actualResponse);
        }

        [Fact]
        public async Task DELETE_Delete_Categoria_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var categoriasId = await CategoriaMockData.GetCategoriasId(application);

            var url = $"/api/Categoria/Delete/{categoriasId[0]}";
            var confirmUrl = $"/api/Categoria/{categoriasId[0]}";

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
        public async Task DELETE_Delete_Categoria_BadRequest()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await CategoriaMockData.CreateCategorias(application, true);

            var categoriaId = new Guid("11111111-1111-1111-1111-111111111111");

            var url = $"/api/Categoria/Delete/{categoriaId}";

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

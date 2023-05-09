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
    public class ProdutoIntegrationTests
    {
        [Fact]
        public async Task GET_Return_Produto_By_Id_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await ProdutoMockData.CreateProdutos(application, true);

            var produtosId = await ProdutoMockData.GetProdutosId(application);

            var expectedNome = "Celular";
            var expectedDescricao = "Aparelho Android com 64gb";
            var expectedPreco = 1200;

            var expectedUser = new UserResponse("Username1", "user1@mail.com");
            var expectedCategoria = new CategoriaResponse("Eletrônicos");

            var expectedProduto = new ProdutoResponse(expectedNome, expectedDescricao, expectedPreco, expectedCategoria, expectedUser);

            var expectedJson = JsonConvert.SerializeObject(expectedProduto);

            var url = $"/api/Produtos/{produtosId[0]}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<ProdutoResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedJson, actualResponse);
        }

        [Fact]
        public async Task GET_Return_Produto_By_Id_BadRequest()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await ProdutoMockData.CreateProdutos(application, true);

            var produtoId = new Guid("11111111-1111-1111-1111-111111111111");

            var url = $"/api/Produtos/{produtoId}";
            var client = application.CreateClient();

            //Act
            var result = await client.GetAsync(url);
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<ProdutoResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task POST_Register_New_Produto_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await ProdutoMockData.CreateProdutos(application, true);

            var usersId = await ProdutoMockData.GetUsersId(application);
            var categoriasId = await ProdutoMockData.GetCategoriasId(application);

            var registerProduto = new ProdutoRequest("Monitor", "Monitor 20 polegadas", 600, categoriasId[0], usersId[0]);
            var url = $"/api/Produtos/Create";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = JsonContent.Create(new
            {
                nome = registerProduto.Nome,
                descricao = registerProduto.Descricao,
                preco = registerProduto.Preco,
                CategoriaId = registerProduto.CategoriaId,
                UsuarioId = registerProduto.UsuarioId
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PostAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Theory]
        [InlineData("", "Celular Android", 1000)]
        [InlineData("Smartphone", "Celular Android", 0)]
        [InlineData("Smartphone", "", 1000)]
        [InlineData("Smartphone", "Celular Android", -1)]
        public async Task POST_Register_New_User_BadRequest(string nome, string descricao, double preco)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await ProdutoMockData.CreateProdutos(application, true);

            var usersId = await ProdutoMockData.GetUsersId(application);
            var categoriasId = await ProdutoMockData.GetCategoriasId(application);

            var registerProduto = new ProdutoRequest(nome, descricao, preco, categoriasId[0], usersId[0]);
            var url = $"/api/Produtos/Create";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = JsonContent.Create(new
            {
                nome = registerProduto.Nome,
                descricao = registerProduto.Descricao,
                preco = registerProduto.Preco,
                CategoriaId = registerProduto.CategoriaId,
                UsuarioId = registerProduto.UsuarioId
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PostAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Theory]
        [InlineData("", "Celular Android", 1000)]
        [InlineData("Smartphone", "Celular Android", 0)]
        [InlineData("Smartphone", "", 1000)]
        [InlineData("Smartphone", "Celular Android", -1)]
        public async Task PUT_Update_Produto_BadRequest(string nome, string descricao, double preco)
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await ProdutoMockData.CreateProdutos(application, true);

            var produtosId = await ProdutoMockData.GetProdutosId(application);
            var usersId = await ProdutoMockData.GetUsersId(application);
            var categoriasId = await ProdutoMockData.GetCategoriasId(application);

            var registerProduto = new ProdutoRequest(nome, descricao, preco, categoriasId[0], usersId[0]);
            var url = $"/api/Produtos/Update/{produtosId[0]}";
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            request.Content = JsonContent.Create(new
            {
                nome = registerProduto.Nome,
                descricao = registerProduto.Descricao,
                preco = registerProduto.Preco,
                CategoriaId = registerProduto.CategoriaId,
                UsuarioId = registerProduto.UsuarioId
            });

            var client = application.CreateClient();


            //Act
            var result = await client.PutAsync(request.RequestUri, request.Content);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task PUT_Update_Produto_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await ProdutoMockData.CreateProdutos(application, true);

            var produtosId = await ProdutoMockData.GetProdutosId(application);
            var usersId = await ProdutoMockData.GetUsersId(application);
            var categoriasId = await ProdutoMockData.GetCategoriasId(application);

            var registerProduto = new ProdutoRequest("Cama", "Cama solteiro", 800, categoriasId[1], usersId[0]);

            var expectedUser = new UserResponse("Username1", "user1@mail.com");
            var expectedCategoria = new CategoriaResponse("Móveis");
            var expectedProduto = new ProdutoResponse("Cama", "Cama solteiro", 800, expectedCategoria, expectedUser);
            var expectedJson = JsonConvert.SerializeObject(expectedProduto);

            var registerUser = new UserUpdateRequest("changedUsername", "changed@mail.com");

            var url = $"/api/Produtos/Update/{produtosId[0]}";
            var confirmUrl = $"/api/Produtos/{produtosId[0]}";

            var request = new HttpRequestMessage(HttpMethod.Put, url);


            request.Content = JsonContent.Create(new
            {
                nome = registerProduto.Nome,
                descricao = registerProduto.Descricao,
                preco = registerProduto.Preco,
                CategoriaId = registerProduto.CategoriaId,
                UsuarioId = registerProduto.UsuarioId
            });

            var client = application.CreateClient();

            //Act
            var result = await client.PutAsync(request.RequestUri, request.Content);

            var confirmResult = await client.GetAsync(confirmUrl);
            var resultContent = confirmResult.Content.ReadAsStringAsync().Result;
            var serializedResult = JsonConvert.DeserializeObject<ProdutoResponse>(resultContent);
            var actualResponse = JsonConvert.SerializeObject(serializedResult);

            await application.DisposeAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(HttpStatusCode.OK, confirmResult.StatusCode);
            Assert.Equal(expectedJson, actualResponse);
        }

        [Fact]
        public async Task DELETE_Delete_Produto_Success()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await ProdutoMockData.CreateProdutos(application, true);

            var produtosId = await ProdutoMockData.GetProdutosId(application);

            var url = $"/api/Produtos/Delete/{produtosId[0]}";
            var confirmUrl = $"/api/Produtos/{produtosId[0]}";

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
        public async Task DELETE_Delete_Produto_BadRequest()
        {
            //Arrange
            await using var application = new DatabaseApiApplication();

            await ProdutoMockData.CreateProdutos(application, true);

            var produtoId = new Guid("11111111-1111-1111-1111-111111111111");

            var url = $"/api/Produtos/Delete/{produtoId}";

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

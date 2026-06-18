using System.Net;
using System.Text;

namespace WebServiceFiap.Tests;

public class StatusCodeTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public StatusCodeTests(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Auth_Login_ReturnsHttpStatusCode200()
    {
        var json = """
        {
            "email": "teste@fiap.com.br",
            "senha": "123456"
        }
        """;
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/Auth/login", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Usuarios_Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/Usuario?page=1&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Itens_Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/Itens?page=1&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CentrosColeta_Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/CentrosColeta?page=1&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Catadores_Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/Catadores?page=1&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Descartadores_Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/Descartadores?page=1&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Coletas_Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/Coletas?page=1&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Itens_Post_WithoutToken_ReturnsHttpStatusCode401()
    {
        var json = """
        {
            "nome": "Papel",
            "volume": 1
        }
        """;
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/Itens", content);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}

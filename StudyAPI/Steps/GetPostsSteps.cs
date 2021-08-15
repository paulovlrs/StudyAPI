using NUnit.Framework;
using RestSharp;
using StudyAPI.Base;
using StudyAPI.Models;
using StudyAPI.Utils;
using TechTalk.SpecFlow;

namespace StudyAPI.Steps
{
  [Binding]
  public class GetPostsSteps
  {
    private readonly Settings _settings;
    public GetPostsSteps(Settings settings) => _settings = settings;

    public RestClient client = new RestClient("http://localhost:3000/");
    public RestRequest request = new RestRequest();
    public IRestResponse<Posts> response = new RestResponse<Posts>();

    [Given(@"que eu realizo a operação GET para o (.*)")]
    public void QuandoQueEuRealizoAOperacaoGETParaO(string url)
    {
      _settings.Request = new RestRequest(url, Method.GET);
    }

    [Given(@"que eu realizo a operação post para o (.*)")]
    [System.Obsolete("teste")]
    public void QuandoQueEuRealizoAOperacaoPostParaO(string id)
    {
      _settings.Request.AddUrlSegment("postid", id);
      _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
    }

    [When(@"solicito a resposta para o (.*)")]
    public void WhenSolicitoARespostaParaO(string id)
    {
      _settings.Request.AddUrlSegment("postid", id);
      _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
    }

    [Then(@"devo visualizar na (.*) o valor (.*)")]
    public void ThenDevoVisualizarNaOValor(string chave, string valor)
    {
      Assert.That(_settings.Response.GetResponseObject(chave), Is.EqualTo(valor), $"The {chave} is not matching");
    }

  }
}
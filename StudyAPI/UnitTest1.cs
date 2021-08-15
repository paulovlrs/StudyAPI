using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using StudyAPI.Models;
using StudyAPI.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StudyAPI
{
  [TestFixture]
  public class UnitTest1
  {
    [Test]
    public void TestMethod1()
    {
      var cliente = new RestClient("http://localhost:3000/");

      var request = new RestRequest("posts/{postid}", Method.GET);
      request.AddUrlSegment("postid", 1);

      var response = cliente.Execute(request);

      JObject obs = JObject.Parse(response.Content);

      Assert.That(obs["author"].ToString(), Is.EqualTo("Karthik KK"), "Author is not correct");
    }

    [Test]
    public void PostWithAnonymousBody()
    {
      var cliente = new RestClient("http://localhost:3000/");

      var request = new RestRequest("posts/", Method.POST);

      request.RequestFormat = DataFormat.Json;
      // Adiciono a informação no json
      request.AddJsonBody(new { name = "Paulo", lastname = "Silva" });

      var response = cliente.Execute(request);

      // pego o nome do campo como resultado
      var result = response.DeserializeResponse()["name"];

      Assert.That(result, Is.EqualTo("Paulo"), "Author is not correct");
    }

    [Test]
    public void PostWithTypesClassBody()
    {
      var cliente = new RestClient("http://localhost:3000/");

      var request = new RestRequest("posts", Method.POST);

      request.RequestFormat = DataFormat.Json;
      // Adiciono a informação no json
      request.AddJsonBody(new Posts() { Author = "Paulo", Title = "Aprendendo a usar RestSharp" });

      // Desserialização generica
      var response = cliente.Execute<Posts>(request);

      Assert.That(response.Data.Title, Is.EqualTo("Aprendendo a usar RestSharp"), "Author is not correct");
    }

    [Test]
    [Obsolete("Aplicando estudos")]
    public void PostWithAsync()
    {
      var cliente = new RestClient("http://localhost:3000/");

      var request = new RestRequest("posts", Method.POST);

      request.RequestFormat = DataFormat.Json;
      // Adiciono a informação no json
      request.AddJsonBody(new Posts() { Author = "Paulo", Title = "Aprendendo a usar RestSharp" });

      // Desserialização generica
      var resposta = cliente.ExecutePostAsync<Posts>(request).GetAwaiter().GetResult();

      Assert.That(resposta.Data.Title, Is.EqualTo("Aprendendo a usar RestSharp"), "O título está incorreto!!!");
    }

    [Test]
    public void AuthenticationMechanism()
    {
      var client = new RestClient("http://localhost:3000/");

      var request = new RestRequest("auth/login", Method.POST);

      request.RequestFormat = DataFormat.Json;
      request.AddBody(new { email = "paulo@email.com", password = "123456" });

      var response = client.ExecutePostTaskAsync(request).GetAwaiter().GetResult();
      var access_token = response.DeserializeResponse()["access_token"];

      var jwtAuth = new JwtAuthenticator(access_token);
      client.Authenticator = jwtAuth;

      var getRequest = new RestRequest("products/{postid}", Method.GET);
      getRequest.AddUrlSegment("postid", 1);

      // Desserialização generica
      var result = client.ExecuteAsyncRequest<Products>(getRequest).GetAwaiter().GetResult();
      Assert.That(result.Data.Name, Is.EqualTo("Product001"), "O produto está incorreto!!!");
    }

    [Test]
    public void AuthenticationMechanismWithJSONFile()
    {
      var client = new RestClient("http://localhost:3000/");

      var request = new RestRequest("auth/login", Method.POST);

      var file = @"TestData\Data.json";

      request.RequestFormat = DataFormat.Json;
      var jsonData = JsonConvert.DeserializeObject<User>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)).ToString());
      request.AddJsonBody(jsonData);

      var response = client.ExecutePostTaskAsync(request).GetAwaiter().GetResult();
      var access_token = response.DeserializeResponse()["access_token"];

      var jwtAuth = new JwtAuthenticator(access_token);
      client.Authenticator = jwtAuth;

      var getRequest = new RestRequest("products/{postid}", Method.GET);
      getRequest.AddUrlSegment("postid", 1);

      // Desserialização generica
      var result = client.ExecuteAsyncRequest<Products>(getRequest).GetAwaiter().GetResult();
      Assert.That(result.Data.Name, Is.EqualTo("Product001"), "O produto está incorreto!!!");
    }
  }
}

using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using StudyAPI.Models;
using StudyAPI.Utils;
using System;
using System.Collections.Generic;
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
  }
}

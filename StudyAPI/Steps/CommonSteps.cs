using RestSharp;
using RestSharp.Authenticators;
using StudyAPI.Base;
using StudyAPI.Utils;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace StudyAPI.Steps
{
  [Binding]
  public class CommonSteps
  {
    private Settings _settings;

    public CommonSteps (Settings settings)
    {
      _settings = settings;
    }

    [Given(@"I get JWT authentication of user with following details")]
    public void GivenIGetJWTAuthenticationOfUserWithFollowingDetails(Table table)
    {
      dynamic data = table.CreateDynamicInstance();

      _settings.Request = new RestRequest("auth/login", Method.POST);

      _settings.Request.RequestFormat = DataFormat.Json;
      _settings.Request.AddBody(new { email = (string)data.Email, password = Convert.ToString(data.Password) });

      _settings.Response = _settings.RestClient.ExecutePostTaskAsync(_settings.Request).GetAwaiter().GetResult();
      var access_token = _settings.Response.GetResponseObject("access_token");

      var jwtAuth = new JwtAuthenticator(access_token);
      _settings.RestClient.Authenticator = jwtAuth;
    }
  }
}

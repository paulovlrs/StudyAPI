﻿using RestSharp;
using StudyAPI.Base;
using StudyAPI.Models;
using StudyAPI.Utils;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace StudyAPI.Steps
{
  [Binding]
  public class PostProfileSteps
  {
    private readonly Settings _settings;
    public PostProfileSteps(Settings settings) => _settings = settings;

    [Given(@"I Perform POST operation for ""(.*)"" with body")]
    [System.Obsolete("estudo")]
    public void GivenIPerformPOSTOperationForWithBody(string url, Table table)
    {
      dynamic data = table.CreateDynamicInstance();

      _settings.Request = new RestRequest(url, Method.POST);

      _settings.Request.RequestFormat = DataFormat.Json;
      _settings.Request.AddJsonBody(new { name = data.name.ToString() });
      _settings.Request.AddUrlSegment("profile", ((int)data.profile).ToString());

      _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
    }
  }
}

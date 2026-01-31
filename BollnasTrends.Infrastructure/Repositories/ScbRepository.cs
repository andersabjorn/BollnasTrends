using System.Text;
using System.Text.Json;
using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Models;
using BollnasTrends.Infrastructure.Dtos;

namespace BollnasTrends.Infrastructure.Repositories;

public class ScbRepository : IPopulationRepository
{
  private readonly HttpClient _httpClient;

  public ScbRepository(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }


  public async Task<List<PopulationPoint>> GetPopulationDataAsync(string regionCode)
  {
    try
    {
      var url = "https://api.scb.se/OV0104/v1/doris/sv/ssd/BE/BE0101/BE0101A/BefolkningNy";

      var query = @"{
          ""query"": [
            {
              ""code"": ""Region"",
              ""selection"": {
                ""filter"": ""vs:RegionKommun07"",
                ""values"": [ ""2183"" ] 
              }
            },
            {
              ""code"": ""ContentsCode"",
              ""selection"": {
                ""filter"": ""item"",
                ""values"": [ ""BE0101N1"" ]
              }
            },
            {
              ""code"": ""Tid"",
              ""selection"": {
                ""filter"": ""item"",
                ""values"": [ ""2015"", ""2016"", ""2017"", ""2018"", ""2019"", ""2020"", ""2021"", ""2022"", ""2023"" ]
              }
            }
          ],
          ""response"": { ""format"": ""json"" }
        }";

      var content = new StringContent(query, Encoding.UTF8, "application/json");

      var response = await _httpClient.PostAsync(url, content);

      response.EnsureSuccessStatusCode();

      var jsonString = await response.Content.ReadAsStringAsync();

      var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
      var scbResult = JsonSerializer.Deserialize<ScbResponse>(jsonString, options);

      var result = new List<PopulationPoint>();

      if (scbResult?.Data != null)
      {
        foreach (var row in scbResult.Data)
        {
          if (int.TryParse(row.Key[1], out int year) &&
              int.TryParse(row.Values[0], out int population))
          {
            result.Add(new PopulationPoint
            {
              Year = year,
              Population = population
            });
          }
        }
      }

      return result;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Något gick fel {ex.Message}");
      throw;
    }

  }
}



       

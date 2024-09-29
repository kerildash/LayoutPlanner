using Domain.Info;
using Newtonsoft.Json.Linq;
namespace Services;

public class MissionService(HttpService httpService)
{
    public async Task<Mission> GetMissionAsync(string uri = "http://promark94.marking.by/client/api/get/task/")
    {

        Mission mission = null;

        string jsonResponse = await httpService.GetAsync(uri);
        JObject response = JObject.Parse(jsonResponse);

        JToken result = response["mission"];

        mission = result.ToObject<Mission>();
        mission.Package = result["lot"]["package"].ToObject<Package>();
        mission.Product = result["lot"]["product"].ToObject<Product>();

        return mission;
    }
}

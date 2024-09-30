using Domain.Info;
using Domain;
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

    public async Task LoadCodesAsync(string path, Mission mission)
    {
        string? line = null;
        using Stream stream = File.OpenRead(path);
        using StreamReader reader = new(stream);

        Layout layout = new(mission.Product.Gtin, mission.Package.BoxFormat, mission.Package.PalletFormat);

        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (line.Contains(mission.Product.Gtin))
            {
                Item item = new Item { Code = line, Id = Guid.NewGuid() };
                layout.AddItem(item);
            }
        }
    }
}

using Domain.Info;
using Domain;
using Newtonsoft.Json.Linq;
using Database.Repositories;
namespace Services;

public class MissionService(
    HttpService httpService,
    IRepository<Pallet> palletRepository,
    IRepository<Box> boxRepository,
    IItemRepository itemRepository)
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

    public async Task<Layout> LoadCodesAsync(string path, Mission mission)
    {
        string? line = null;
        using Stream stream = File.OpenRead(path);
        using StreamReader reader = new(stream);

        Layout layout = new(
                mission.Product.Name,
                mission.Product.Gtin,
                mission.Package.BoxFormat,
                mission.Package.PalletFormat);

        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (line.Contains(mission.Product.Gtin) && !itemRepository.Exists(line))
            {
                Item item = new Item { Code = line };
                layout.AddItem(item);
            }
        }
        InsertLayoutToDatabase(layout);
        return layout;
    }

    public void InsertLayoutToDatabase(Layout layout)
    {
        foreach (Pallet pallet in layout.GetPallets())
        {
            palletRepository.AddAsync(pallet);

        }
        foreach (Box box in layout.GetBoxes())
        {
            boxRepository.AddAsync(box);
        }
        foreach (Item item in layout.GetItems())
        {
            itemRepository.AddAsync(item);
        }
        itemRepository.SaveChanges();

    }
}

namespace Domain;

public class Layout
{
    public string ProductName { get; set; }
    public string Gtin { get; }
    public int BoxFormat { get; }

    public int PalletFormat { get; }

    public List<Pallet> Pallets { get; set; }
    public Layout()
    {
        Pallets = [];
    }
    public Layout(string productName, string gtin, int boxFormat, int palletFormat)
    {
        ProductName = productName;
        Pallets = [];
        Gtin = gtin;
        BoxFormat = boxFormat;
        PalletFormat = palletFormat;
    }

    #region iterators

    public IEnumerable<Pallet> GetPallets()
    {
        //if (Pallets.Any()) yield return new Pallet { };
        foreach (var pallet in Pallets)
        {
            yield return pallet;
        }
    }

    public IEnumerable<Box> GetBoxes()
    {
        foreach (var pallet in Pallets)
        {
            foreach (var box in pallet.Boxes)
            {
                yield return box;
            }
        }
    }

    public IEnumerable<Item> GetItems()
    {
        foreach(var box in GetBoxes())
        {
            foreach(var item in box.Items)
            {
                yield return item;
            }
        }
    }
    #endregion


    public void AddItem(Item item)
    {
        if (Pallets.Any() && !Pallets.Last().IsFull())
        {
            Pallets.Last().AddItemToPallet(item, Gtin, BoxFormat);

        }
        else
        {
            Pallet newPallet = new Pallet(Gtin, PalletFormat);
            newPallet.AddItemToPallet(item, Gtin, BoxFormat);
            Pallets.Add(newPallet);
        }

    }
}

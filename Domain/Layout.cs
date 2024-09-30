namespace Domain;

public class Layout
{
    public string Gtin { get; }
    public int BoxFormat { get; }

    public int PalletFormat { get; }

    public List<Pallet> Pallets { get; private set; }
    public Layout(string gtin, int boxFormat, int palletFormat)
    {
        Gtin = gtin;
        BoxFormat = boxFormat;
        PalletFormat = palletFormat;
        Pallets = [];
    }

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

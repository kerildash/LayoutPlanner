using AsyncAwaitBestPractices.MVVM;
using Domain;
using Domain.Info;
using Newtonsoft.Json;
using Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Gui;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly DialogService _dialog;
    private readonly MissionService _missionService;

    private Mission _mission;

    public Mission Mission
    {
        get
        {
            return _mission;
        }
        set
        {
            _mission = value;
            OnPropertyChanged();
        }
    }

    private Layout _layout;

    public Layout Layout
    {
        get
        {
            return _layout;
        }
        set
        {
            _layout = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Pallets));
            OnPropertyChanged(nameof(Boxes));
            OnPropertyChanged(nameof(Items));
            SaveLayoutAsJsonAsync.RaiseCanExecuteChanged();
        }
    }

    private List<Pallet> _test;
    public List<Pallet> Pallets
    {
        get => Layout.GetPallets().ToList();
        set { }
    }
    public IEnumerable<Box> Boxes
    {
        get => Layout.GetBoxes().ToList();
        set { }
    }
    public IEnumerable<Item> Items
    {
        get => Layout.GetItems().ToList();
        set { }
    }

    public MainWindowViewModel(MissionService service, DialogService dialog)
    {


        LoadCodesAsync = new AsyncCommand(OnLoadCodesAsync, CanLoadCodesAsyncExecuted);
        SaveLayoutAsJsonAsync = new AsyncCommand(OnSaveLayoutAsJsonAsync, CanSaveLayoutAsJsonAsyncExecuted);

        _missionService = service;
        _dialog = dialog;

        Layout = new Layout();
        Mission = GetMission();

    }

    public Mission GetMission()
    {
        try
        {
            return Task.Run(() => _missionService.GetMissionAsync()).Result;
        }
        catch (Exception ex)
        {
            string message =
                "Ошибка при попытке получить данные о миссии от сервера. Повторить попытку?\n" +
                ex.Message;

            bool result = _dialog.ShowYesNoMessage(message, image: MessageBoxImage.Exclamation);
            if (result) 
                {
                    return GetMission();
                }
            Application.Current.Shutdown();
            return null;
        }
    }
    #region commands
    public IAsyncCommand LoadCodesAsync { get; }
    public async Task OnLoadCodesAsync()
    {
        string? path;
        try
        {
            int itemsAdded = 0;
            bool result = _dialog.OpenFile(out path);
            if (result)
            {
                Layout = await _missionService.LoadCodesAsync(path, Mission);
                _dialog.ShowMessage("Коды импортированы. Перейдите на другие вкладки для просмотра информации.");
            }

        }
        catch (Exception e)
        {
            _dialog.ShowMessage(e.Message);
        }
    }

    public bool CanLoadCodesAsyncExecuted(object parameter) => true;


    public IAsyncCommand SaveLayoutAsJsonAsync { get; }
    public async Task OnSaveLayoutAsJsonAsync()
    {
        string? path;
        try
        {
            string json = JsonConvert.SerializeObject(Layout, Formatting.Indented);
            if (_dialog.SaveFileAs(out path))
            {
                System.IO.File.WriteAllText(path, json);
                _dialog.ShowMessage($"JSON-файл с раскладкой продукции сохранён:\n {path}");
            }
        }
        catch (Exception e)
        {
            _dialog.ShowMessage(e.Message);
        }
    }

    public bool CanSaveLayoutAsJsonAsyncExecuted(object parameter) => Layout.Pallets.Count > 0;
    #endregion


    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

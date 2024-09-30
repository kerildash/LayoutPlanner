using AsyncAwaitBestPractices.MVVM;
using Domain;
using Domain.Info;
using Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gui;

internal class MainWindowViewModel : INotifyPropertyChanged
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
        }
    }
    #region commands
    public IAsyncCommand LoadCodesAsync { get; }
    public async Task OnLoadCodesAsync()
    {
        string? path;
        try
        {
            bool result = _dialog.OpenFile(out path);
            if (result)
            {
                Layout = await _missionService.LoadCodesAsync(path, Mission);
            }
        }
        catch (Exception e)
        {
            _dialog.ShowMessage(e.Message);
        }
    }

    public bool CanLoadCodesAsyncExecuted(object parameter) => true;
    #endregion


    public MainWindowViewModel(MissionService service, DialogService dialog)
    {
        _missionService = service;
        _dialog = dialog;

        Mission mission = Task.Run(() => service.GetMissionAsync()).Result;
        Mission = mission;

        LoadCodesAsync = new AsyncCommand(OnLoadCodesAsync, CanLoadCodesAsyncExecuted);

    }
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

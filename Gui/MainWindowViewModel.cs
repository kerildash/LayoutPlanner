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

    public MainWindowViewModel(MissionService service, DialogService dialog)
    {
        _missionService = service;
        _dialog = dialog;

        Mission mission = Task.Run(() => service.GetMissionAsync()).Result;
        
        Mission = mission;

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

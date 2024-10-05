using Microsoft.Win32;
using System.Windows;

namespace Gui;

public class DialogService
{
    public bool OpenFile(out string? path)
    {
        try
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Text files(*.txt)|*.txt| All files(*.*) |*.*";
            if (openDialog.ShowDialog() == true)
            {
                path = openDialog.FileName;
                return true;
            }
            path = null;
            return false;
        }
        catch
        {
            throw;
        }
    }
    public bool SaveFileAs(out string? path)
    {
        try
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Json files(*.json)|*.json|Text files(*.txt)|*.txt| All files(*.*) |*.*";
            if (saveDialog.ShowDialog() == true)
            {
                path = saveDialog.FileName;
                return true;
            }
            path = null;
            return false;
        }
        catch
        {
            throw;
        }
    }
    public void ShowMessage(string message)
    {
        {
            MessageBox.Show(message);
        }
    }
    public bool ShowYesNoMessage(
        string message,
        string caption = "Внимание",
        MessageBoxImage image = MessageBoxImage.Question)
    {
        MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, image);
        if (result == MessageBoxResult.Yes)
        {
            return true;
        }        
        return false;
    }
}

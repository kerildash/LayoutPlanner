﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gui;

public class DialogService
{
    public string FilePath { get; set; }

    public bool OpenFile()
    {
        try
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Text files(*.txt)|*.txt| All files(*.*) |*.*";
            if (openDialog.ShowDialog() == true)
            {
                FilePath = openDialog.FileName;
                return true;
            }
            return false;
        }
        catch
        {
            throw;
        }
    }
    public bool SaveFile()
    {
        try
        {
            if (FilePath == null)
            {
                return SaveFileAs();
            }
            return true;
        }
        catch
        {
            throw;
        }
    }
    public bool SaveFileAs()
    {
        try
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text files(*.txt)|*.txt| All files(*.*) |*.*";
            if (saveDialog.ShowDialog() == true)
            {
                FilePath = saveDialog.FileName;
                return true;
            }
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
}

﻿using GTA5Core;
using GTA5Core.Native;
using GTA5Menu;
using GTA5MenuExtra;
using GTA5Heists;
using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// GTA5View.xaml 的交互逻辑
/// </summary>
public partial class GTA5View : UserControl
{
    private GTA5MenuWindow GTA5MenuWindow = null;
    private GTA5HeistsWindow GTA5HeistsWindow = null;

    private StatScriptsWindow StatScriptsWindow = null;
    private OutfitsEditWindow OutfitsEditWindow = null;

    /// <summary>
    /// 关闭全部GTA5窗口委托
    /// </summary>
    public static Action ActionCloseAllGTA5Window;

    public GTA5View()
    {
        InitializeComponent();
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        ActionCloseAllGTA5Window = CloseAllGTA5Window;
    }

    private void MainWindow_WindowClosingEvent()
    {

    }

    [RelayCommand]
    private void GTA5ViewClick(string modelName)
    {
        AudioHelper.PlayClickSound();

        if (ProcessHelper.IsGTA5Run())
        {
            // GTA5内存模块初始化窗口
            if (!Memory.IsInitialized)
            {
                var gta5InitWindow = new GTA5InitWindow
                {
                    Owner = MainWindow.MainWindowInstance
                };
                if (gta5InitWindow.ShowDialog() == false)
                    return;
            }

            switch (modelName)
            {
                case "ExternalMenu":
                    ExternalMenuClick();
                    break;
                case "HeistsEditor":
                    HeistsEditorClick();
                    break;
                case "StatScripts":
                    StatScriptsClick();
                    break;
                case "OutfitsEdit":
                    OutfitsEditClick();
                    break;
            }
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现《GTA5》进程，请先运行《GTA5》游戏");
        }
    }

    ///////////////////////////////////////////////////////////////////

    #region 第三方模块按钮点击事件
    private void ExternalMenuClick()
    {
        if (GTA5MenuWindow == null)
        {
            GTA5MenuWindow = new GTA5MenuWindow();
            GTA5MenuWindow.Show();
        }
        else
        {
            if (GTA5MenuWindow.IsVisible)
            {
                if (!GTA5MenuWindow.Topmost)
                {
                    GTA5MenuWindow.Topmost = true;
                    GTA5MenuWindow.Topmost = false;
                }

                GTA5MenuWindow.WindowState = WindowState.Normal;
            }
            else
            {
                GTA5MenuWindow = null;
                GTA5MenuWindow = new GTA5MenuWindow();
                GTA5MenuWindow.Show();
            }
        }
    }

    private void HeistsEditorClick()
    {
        if (GTA5HeistsWindow == null)
        {
            GTA5HeistsWindow = new GTA5HeistsWindow();
            GTA5HeistsWindow.Show();
        }
        else
        {
            if (GTA5HeistsWindow.IsVisible)
            {
                GTA5HeistsWindow.Topmost = true;
                GTA5HeistsWindow.Topmost = false;
                GTA5HeistsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                GTA5HeistsWindow = null;
                GTA5HeistsWindow = new GTA5HeistsWindow();
                GTA5HeistsWindow.Show();
            }
        }
    }

    private void StatScriptsClick()
    {
        if (StatScriptsWindow == null)
        {
            StatScriptsWindow = new StatScriptsWindow();
            StatScriptsWindow.Show();
        }
        else
        {
            if (StatScriptsWindow.IsVisible)
            {
                StatScriptsWindow.Topmost = true;
                StatScriptsWindow.Topmost = false;
                StatScriptsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                StatScriptsWindow = null;
                StatScriptsWindow = new StatScriptsWindow();
                StatScriptsWindow.Show();
            }
        }
    }

    private void OutfitsEditClick()
    {
        if (OutfitsEditWindow == null)
        {
            OutfitsEditWindow = new OutfitsEditWindow();
            OutfitsEditWindow.Show();
        }
        else
        {
            if (OutfitsEditWindow.IsVisible)
            {
                OutfitsEditWindow.Topmost = true;
                OutfitsEditWindow.Topmost = false;
                OutfitsEditWindow.WindowState = WindowState.Normal;
            }
            else
            {
                OutfitsEditWindow = null;
                OutfitsEditWindow = new OutfitsEditWindow();
                OutfitsEditWindow.Show();
            }
        }
    }
    #endregion

    ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// 关闭全部GTA5窗口
    /// </summary>
    private void CloseAllGTA5Window()
    {
        Memory.CloseHandle();

        this.Dispatcher.Invoke(() =>
        {
            if (GTA5MenuWindow != null)
            {
                GTA5MenuWindow.Close();
                GTA5MenuWindow = null;
            }

            if (GTA5HeistsWindow != null)
            {
                GTA5HeistsWindow.Close();
                GTA5HeistsWindow = null;
            }

            if (StatScriptsWindow != null)
            {
                StatScriptsWindow.Close();
                StatScriptsWindow = null;
            }

            if (OutfitsEditWindow != null)
            {
                OutfitsEditWindow.Close();
                OutfitsEditWindow = null;
            }
        });
    }
}

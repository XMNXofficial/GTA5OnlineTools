﻿using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Windows;
using GTA5OnlineTools.Views.ReadMe;

using GTA5Inject;
using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;
using GTA5Core.Native;

namespace GTA5OnlineTools.Views;

/// <summary>
/// HacksView.xaml 的交互逻辑
/// </summary>
public partial class HacksView : UserControl
{
    /// <summary>
    /// 数据模型绑定
    /// </summary>
    public HacksModel HacksModel { get; set; } = new();

    private readonly Kiddion Kiddion = new();
    private readonly GTAHax GTAHax = new();
    private readonly BincoHax BincoHax = new();
    private readonly LSCHax LSCHax = new();
    private readonly YimMenu YimMenu = new();

    private Kiddion2Window Kiddion2Window = null;
    private GTAHaxWindow GTAHaxWindow = null;

    public HacksView()
    {
        InitializeComponent();
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        new Thread(CheckCheatsIsRun)
        {
            Name = "CheckCheatsIsRun",
            IsBackground = true
        }.Start();
    }

    private void MainWindow_WindowClosingEvent()
    {

    }

    /// <summary>
    /// 检查第三方辅助是否正在运行线程
    /// </summary>
    private void CheckCheatsIsRun()
    {
        while (MainWindow.IsAppRunning)
        {
            // 判断 Kiddion 是否运行
            HacksModel.KiddionIsRun = ProcessHelper.IsAppRun("Kiddion");
            // 判断 GTAHax 是否运行
            HacksModel.GTAHaxIsRun = ProcessHelper.IsAppRun("GTAHax");
            // 判断 BincoHax 是否运行
            HacksModel.BincoHaxIsRun = ProcessHelper.IsAppRun("BincoHax");
            // 判断 LSCHax 是否运行
            HacksModel.LSCHaxIsRun = ProcessHelper.IsAppRun("LSCHax");

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 点击第三方辅助开关按钮
    /// </summary>
    /// <param name="hackName"></param>
    [RelayCommand]
    private void CheatsClick(string hackName)
    {
        AudioHelper.PlayClickSound();

        if (ProcessHelper.IsGTA5Run())
        {
            switch (hackName)
            {
                case "Kiddion":
                    KiddionClick();
                    break;
                case "GTAHax":
                    GTAHaxClick();
                    break;
                case "BincoHax":
                    BincoHaxClick();
                    break;
                case "LSCHax":
                    LSCHaxClick();
                    break;
                case "YimMenu":
                    YimMenuClick();
                    break;
            }
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现《GTA5》进程，请先运行《GTA5》游戏");
        }
    }

    /// <summary>
    /// 点击第三方辅助使用说明
    /// </summary>
    /// <param name="Name"></param>
    [RelayCommand]
    private void ReadMeClick(string Name)
    {
        AudioHelper.PlayClickSound();

        switch (Name)
        {
            case "Kiddion":
                ShowReadMe(Kiddion);
                break;
            case "GTAHax":
                ShowReadMe(GTAHax);
                break;
            case "BincoHax":
                ShowReadMe(BincoHax);
                break;
            case "LSCHax":
                ShowReadMe(LSCHax);
                break;
            case "YimMenu":
                ShowReadMe(YimMenu);
                break;
        }
    }

    /// <summary>
    /// 点击第三方辅助配置文件路径
    /// </summary>
    /// <param name="funcName"></param>
    [RelayCommand]
    private void ExtraClick(string funcName)
    {
        AudioHelper.PlayClickSound();

        switch (funcName)
        {
            #region Kiddion额外功能
            case "KiddionKey104":
                KiddionKey104Click();
                break;
            case "KiddionKey87":
                KiddionKey87Click();
                break;
            case "KiddionConfigDirectory":
                KiddionConfigDirectoryClick();
                break;
            case "KiddionScriptsDirectory":
                KiddionScriptsDirectoryClick();
                break;
            case "KiddionScriptsManage":
                KiddionScriptsManageClick();
                break;
            case "EditKiddionConfig":
                EditKiddionConfigClick();
                break;
            case "EditKiddionTheme":
                EditKiddionThemeClick();
                break;
            case "EditKiddionTP":
                EditKiddionTPClick();
                break;
            case "EditKiddionVC":
                EditKiddionVCClick();
                break;
            case "ResetKiddionConfig":
                ResetKiddionConfigClick();
                break;
            case "KiddionChsPolish":
                KiddionChsPolishClick();
                break;
            #endregion
            ////////////////////////////////////
            #region 其他额外功能
            case "EditGTAHaxStat":
                EditGTAHaxStatClick();
                break;
            case "RunGTAHaxStat":
                RunGTAHaxStatClick();
                break;
            case "DefaultGTAHaxStat":
                DefaultGTAHaxStatClick();
                break;
            //////////////
            case "YimMenuDirectory":
                YimMenuDirectoryClick();
                break;
            case "YimMenuScriptsDirectory":
                YimMenuScriptsDirectoryClick();
                break;
            case "EditYimMenuConfig":
                EditYimMenuConfigClick();
                break;
            case "Xenos64Injector":
                Xenos64InjectorClick();
                break;
            case "ResetYimMenuConfig":
                ResetYimMenuConfigClick();
                break;
                #endregion
        }
    }

    /// <summary>
    /// 显示使用说明窗口
    /// </summary>
    /// <param name="userControl"></param>
    private void ShowReadMe(UserControl userControl)
    {
        var readMeWindow = new ReadMeWindow(userControl)
        {
            Owner = MainWindow.MainWindowInstance
        };
        readMeWindow.ShowDialog();
    }

    #region 第三方辅助功能开关事件
    /// <summary>
    /// Kiddion点击事件
    /// </summary>
    private async void KiddionClick()
    {
        if (!HacksModel.KiddionIsRun)
        {
            ProcessHelper.CloseProcess("Kiddion");
            return;
        }

        ProcessHelper.OpenProcessWithWorkDir(FileHelper.File_Kiddion_Kiddion);

        Process pKiddion = null;

        for (int i = 0; i < 8; i++)
        {
            // 尝试获取Kiddion进程
            var pArray = Process.GetProcessesByName("Kiddion");
            if (pArray.Length > 0)
                pKiddion = pArray.First();

            if (pKiddion != null)
                break;

            await Task.Delay(250);
        }

        if (pKiddion is null)
        {
            NotifierHelper.Show(NotifierType.Error, "发生错误，无法获取Kiddion进程信息");
            return;
        }

        var result = Injector.DLLInjector(pKiddion.Id, FileHelper.File_Kiddion_KiddionChs, false);
        if (result.IsSuccess)
            NotifierHelper.Show(NotifierType.Success, "Kiddion汉化加载成功");
        else
            NotifierHelper.Show(NotifierType.Error, $"Kiddion汉化加载失败\n错误信息：{result.Content}");
    }

    /// <summary>
    /// GTAHax点击事件
    /// </summary>
    private void GTAHaxClick()
    {
        if (HacksModel.GTAHaxIsRun)
            ProcessHelper.OpenProcessWithWorkDir(FileHelper.File_Cache_GTAHax);
        else
            ProcessHelper.CloseProcess("GTAHax");
    }

    /// <summary>
    /// BincoHax点击事件
    /// </summary>
    private void BincoHaxClick()
    {
        if (HacksModel.BincoHaxIsRun)
            ProcessHelper.OpenProcessWithWorkDir(FileHelper.File_Cache_BincoHax);
        else
            ProcessHelper.CloseProcess("BincoHax");
    }

    /// <summary>
    /// LSCHax点击事件
    /// </summary>
    private void LSCHaxClick()
    {
        if (HacksModel.LSCHaxIsRun)
            ProcessHelper.OpenProcessWithWorkDir(FileHelper.File_Cache_LSCHax);
        else
            ProcessHelper.CloseProcess("LSCHax");
    }

    /// <summary>
    /// YimMenu点击事件
    /// </summary>
    private void YimMenuClick()
    {
        try
        {
            // 释放Yimmenu官中语言文件
            FileHelper.CreateDirectory(FileHelper.Dir_AppData_YimMenu_Translations);
            FileHelper.ExtractResFile(FileHelper.Res_YimMenu_Index, FileHelper.File_YimMenu_Index);
            FileHelper.ExtractResFile(FileHelper.Res_YimMenu_ZHCN, FileHelper.File_YimMenu_ZHCN);
        }
        catch (Exception ex)
        {
            LoggerHelper.Error($"释放Yimmenu官中语言文件失败，异常信息：{ex.Message}");
        }

        // 由于玩家可能只使用YimMenu，GTA5Core模块不会初始化，这里要单独处理
        Process gta5Process;
        if (Memory.GTA5ProId == 0)
        {
            var pArray = Process.GetProcessesByName("GTA5");
            gta5Process = pArray.First();
        }
        else
        {
            gta5Process = Memory.GTA5Process;
        }

        var result = Injector.DLLInjector(gta5Process.Id, FileHelper.File_YimMenu_DLL, true);
        if (result.IsSuccess)
            NotifierHelper.Show(NotifierType.Success, "YimMenu菜单注入成功");
        else
            NotifierHelper.Show(NotifierType.Error, $"YimMenu菜单注入失败\n错误信息：{result.Content}");
    }
    #endregion

    #region Kiddion额外功能
    /// <summary>
    /// 启用Kiddion[104键]
    /// </summary>
    private void KiddionKey104Click()
    {
        ProcessHelper.CloseProcess("Kiddion");
        FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Config, FileHelper.File_Kiddion_Config);
        NotifierHelper.Show(NotifierType.Success, "切换到《Kiddion [104键]》成功");
    }

    /// <summary>
    /// 启用Kiddion[87键]
    /// </summary>
    private void KiddionKey87Click()
    {
        ProcessHelper.CloseProcess("Kiddion");
        FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Config87, FileHelper.File_Kiddion_Config);
        NotifierHelper.Show(NotifierType.Success, "切换到《Kiddion [87键]》成功");
    }

    /// <summary>
    /// Kiddion配置目录
    /// </summary>
    private void KiddionConfigDirectoryClick()
    {
        ProcessHelper.OpenDir(FileHelper.Dir_Kiddion);
    }

    /// <summary>
    /// Kiddion脚本目录
    /// </summary>
    private void KiddionScriptsDirectoryClick()
    {
        ProcessHelper.OpenDir(FileHelper.Dir_Kiddion_Scripts);
    }

    /// <summary>
    /// Kiddion LUA脚本管理
    /// </summary>
    private void KiddionScriptsManageClick()
    {
        var kiddionWindow = new KiddionWindow
        {
            Owner = MainWindow.MainWindowInstance
        };
        kiddionWindow.ShowDialog();
    }

    /// <summary>
    /// 编辑Kiddion配置文件
    /// </summary>
    private void EditKiddionConfigClick()
    {
        ProcessHelper.Notepad2EditTextFile(FileHelper.File_Kiddion_Config);
    }

    /// <summary>
    /// 编辑Kiddion主题文件
    /// </summary>
    private void EditKiddionThemeClick()
    {
        ProcessHelper.Notepad2EditTextFile(FileHelper.File_Kiddion_Themes);
    }

    /// <summary>
    /// 编辑Kiddion自定义传送
    /// </summary>
    private void EditKiddionTPClick()
    {
        ProcessHelper.Notepad2EditTextFile(FileHelper.File_Kiddion_Teleports);
    }

    /// <summary>
    /// 编辑Kiddion自定义载具
    /// </summary>
    private void EditKiddionVCClick()
    {
        ProcessHelper.Notepad2EditTextFile(FileHelper.File_Kiddion_Vehicles);
    }

    /// <summary>
    /// 重置Kiddion配置文件
    /// </summary>
    private async void ResetKiddionConfigClick()
    {
        try
        {
            if (MessageBox.Show("你确定要重置Kiddion配置文件吗？如有重要文件请提前备份\n\n" +
                $"程序会自动重置此文件夹：\n{FileHelper.Dir_Kiddion}",
                "重置Kiddion配置文件", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ProcessHelper.CloseProcess("Kiddion");
                ProcessHelper.CloseProcess("Notepad2");
                await Task.Delay(100);

                FileHelper.ClearDirectory(FileHelper.Dir_Kiddion);
                Directory.CreateDirectory(FileHelper.Dir_Kiddion_Scripts);
                await Task.Delay(100);

                FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Kiddion, FileHelper.File_Kiddion_Kiddion);
                FileHelper.ExtractResFile(FileHelper.Res_Kiddion_KiddionChs, FileHelper.File_Kiddion_KiddionChs);

                FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Config, FileHelper.File_Kiddion_Config);
                FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Themes, FileHelper.File_Kiddion_Themes);
                FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Teleports, FileHelper.File_Kiddion_Teleports);
                FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Vehicles, FileHelper.File_Kiddion_Vehicles);

                FileHelper.ExtractResFile(FileHelper.Res_Kiddion_Scripts_Readme, FileHelper.File_Kiddion_Scripts_Readme);

                NotifierHelper.Show(NotifierType.Success, "重置Kiddion配置文件成功");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// Kiddion汉化修正
    /// </summary>
    private void KiddionChsPolishClick()
    {
        if (Kiddion2Window == null)
        {
            Kiddion2Window = new Kiddion2Window();
            Kiddion2Window.Show();
        }
        else
        {
            if (Kiddion2Window.IsVisible)
            {
                if (!Kiddion2Window.Topmost)
                {
                    Kiddion2Window.Topmost = true;
                    Kiddion2Window.Topmost = false;
                }

                Kiddion2Window.WindowState = WindowState.Normal;
            }
            else
            {
                Kiddion2Window = null;
                Kiddion2Window = new Kiddion2Window();
                Kiddion2Window.Show();
            }
        }
    }
    #endregion

    #region 其他额外功能
    /// <summary>
    /// 编辑GTAHax导入文件
    /// </summary>
    private void EditGTAHaxStatClick()
    {
        ProcessHelper.Notepad2EditTextFile(FileHelper.File_Cache_Stat);
    }

    /// <summary>
    /// 运行GTAHax导入文件
    /// </summary>
    private void RunGTAHaxStatClick()
    {
        HackUtil.ImportGTAHax();
    }

    /// <summary>
    /// GTAHax预设代码
    /// </summary>
    private void DefaultGTAHaxStatClick()
    {
        if (GTAHaxWindow == null)
        {
            GTAHaxWindow = new GTAHaxWindow();
            GTAHaxWindow.Show();
        }
        else
        {
            if (GTAHaxWindow.IsVisible)
            {
                if (!GTAHaxWindow.Topmost)
                {
                    GTAHaxWindow.Topmost = true;
                    GTAHaxWindow.Topmost = false;
                }

                GTAHaxWindow.WindowState = WindowState.Normal;
            }
            else
            {
                GTAHaxWindow = null;
                GTAHaxWindow = new GTAHaxWindow();
                GTAHaxWindow.Show();
            }
        }
    }

    /// <summary>
    /// YimMenu配置目录
    /// </summary>
    private void YimMenuDirectoryClick()
    {
        ProcessHelper.OpenDir(FileHelper.Dir_AppData_YimMenu);
    }

    /// <summary>
    /// YimMenu脚本目录
    /// </summary>
    private void YimMenuScriptsDirectoryClick()
    {
        ProcessHelper.OpenDir(FileHelper.Dir_AppData_YimMenu_Scripts);
    }

    /// <summary>
    /// YimMenu配置文件
    /// </summary>
    private void EditYimMenuConfigClick()
    {
        ProcessHelper.Notepad2EditTextFile(FileHelper.File_AppData_YimMenu_Settings);
    }

    /// <summary>
    /// Xenos64注入器
    /// </summary>
    private void Xenos64InjectorClick()
    {
        ProcessHelper.OpenProcess(FileHelper.File_Cache_Xenos64);
    }

    /// <summary>
    /// 重置YimMenu配置文件
    /// </summary>
    private void ResetYimMenuConfigClick()
    {
        try
        {
            if (FileHelper.IsOccupied(FileHelper.File_YimMenu_DLL))
            {
                NotifierHelper.Show(NotifierType.Warning, "YimMenu被占用，请先卸载YimMenu菜单后再执行操作");
                return;
            }

            if (MessageBox.Show($"你确定要重置YimMenu配置文件吗？\n\n将清空「{FileHelper.Dir_AppData_YimMenu}」文件夹，如有重要文件请提前备份",
                "重置YimMenu配置文件", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileHelper.ClearDirectory(FileHelper.Dir_AppData_YimMenu);

                NotifierHelper.Show(NotifierType.Success, "重置YimMenu配置文件成功");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion
}

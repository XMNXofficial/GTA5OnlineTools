﻿using GTA5Core.Features;
using GTA5Shared.Helper;

namespace GTA5MenuExtra.Views.HeistsEditor.Doomsday;

/// <summary>
/// MissionView.xaml 的交互逻辑
/// </summary>
public partial class MissionView : UserControl
{
    public MissionView()
    {
        InitializeComponent();
    }

    private async void STAT_SET_INT(string hash, int value)
    {
        await STATS.STAT_SET_INT(hash, value);
    }

    ////////////////////////////////////////////////////

    private void Button_GANGOPS_FLOW_MISSION_PROG_1_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        STAT_SET_INT("MPx_GANGOPS_FLOW_MISSION_PROG", -1);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_503_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        STAT_SET_INT("MPx_GANGOPS_FLOW_MISSION_PROG", 503);
        STAT_SET_INT("MPx_GANGOPS_HEIST_STATUS", 819193);
        STAT_SET_INT("MPx_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_240_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        STAT_SET_INT("MPx_GANGOPS_FLOW_MISSION_PROG", 240);
        STAT_SET_INT("MPx_GANGOPS_HEIST_STATUS", 819198);
        STAT_SET_INT("MPx_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_16368_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        STAT_SET_INT("MPx_GANGOPS_FLOW_MISSION_PROG", 16368);
        STAT_SET_INT("MPx_GANGOPS_HEIST_STATUS", 819190);
        STAT_SET_INT("MPx_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }
}

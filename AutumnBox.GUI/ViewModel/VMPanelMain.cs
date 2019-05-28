﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 19:08:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.OS;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.View.LeafContent;
using AutumnBox.GUI.View.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMPanelMain : ViewModelBase
    {
        public int TabSelectedIndex
        {
            get => tabSelectedIndex;
            set
            {
                tabSelectedIndex = value;
                RaisePropertyChanged();
            }
        }
        private int tabSelectedIndex;

        public VMPanelMain()
        {
            Util.Bus.DeviceSelectionObserver.Instance.SelectedNoDevice += NoDevice;
            Util.Bus.DeviceSelectionObserver.Instance.SelectedDevice += Instance_SelectedDevice;
        }

        private void Instance_SelectedDevice(object sender, EventArgs e)
        {
            TabSelectedIndex = 1;
        }

        private void NoDevice(object sender, EventArgs e)
        {
            TabSelectedIndex = 0;
        }
    }
}

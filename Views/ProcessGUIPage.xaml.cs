using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Maui.Controls;

namespace LTXTools.Views
{
    public partial class ProcessGUIPage : ContentView
    {
        public ProcessGUIPage() { InitializeComponent(); RefreshProcesses(); }
        
        private async void btnRefresh_Clicked(object sender, EventArgs e) => await RefreshProcesses();
        
        private async System.Threading.Tasks.Task RefreshProcesses()
        {
            btnRefresh.IsEnabled = false; txtStatus.Text = "⏳ 正在获取进程列表...";
            var processes = await System.Threading.Tasks.Task.Run(() => 
                Process.GetProcesses().Select(p => new { p.ProcessName, p.Id, Memory = $"{p.WorkingSet64 / 1024 / 1024} MB" }).ToList());
            gridProcesses.ItemsSource = processes; 
            txtStatus.Text = $"✅ 已加载 {processes.Count} 个进程"; 
            btnRefresh.IsEnabled = true;
        }
    }
}

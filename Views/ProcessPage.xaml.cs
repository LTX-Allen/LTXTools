using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Maui.Controls;

namespace LTXTools.Views
{
    public partial class ProcessPage : ContentView
    {
        public ProcessPage() { InitializeComponent(); RefreshProcesses(); }
        
        private async void btnRefresh_Clicked(object sender, EventArgs e) => await RefreshProcesses();
        
        private async System.Threading.Tasks.Task RefreshProcesses()
        {
            btnRefresh.IsEnabled = false; txtStatus.Text = "⏳ 正在获取进程列表...";
            var processes = await System.Threading.Tasks.Task.Run(() => 
                Process.GetProcesses().Select(p => new { p.ProcessName, p.Id }).ToList());
            lstProcesses.ItemsSource = processes; 
            txtStatus.Text = $"✅ 已加载 {processes.Count} 个进程"; 
            btnRefresh.IsEnabled = true;
        }
    }
}

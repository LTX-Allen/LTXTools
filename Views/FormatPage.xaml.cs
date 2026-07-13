using System;
using System.IO;
using Microsoft.Maui.Controls;

namespace LTXTools.Views
{
    public partial class FormatPage : ContentView
    {
        public FormatPage() { InitializeComponent(); LoadDrives(); txtStatus.Text = "✅ 就绪"; }
        
        private void LoadDrives()
        {
            try 
            { 
                var drives = DriveInfo.GetDrives(); 
                cmbDrives.ItemsSource = System.Linq.Enumerable.Select(drives, d => d.Name).ToList(); 
            }
            catch (Exception ex) { txtStatus.Text = $"❌ 加载磁盘失败：{ex.Message}"; }
        }
        
        private async void btnFormat_Clicked(object sender, EventArgs e)
        {
            var page = Application.Current?.MainPage;
            if (cmbDrives.SelectedItem == null) 
            { await page.DisplayAlert("提示", "请选择要格式化的磁盘！", "确定"); return; }
            string drive = cmbDrives.SelectedItem.ToString();
            bool confirm = await page.DisplayAlert("确认格式化", $"确定要格式化 {drive} 吗？\n此操作将删除所有数据！", "是", "否");
            if (!confirm) return;
            await page.DisplayAlert("提示", $"格式化 {drive} 需要使用 diskutil 命令", "确定");
            txtStatus.Text = $"✅ 请手动执行：diskutil eraseDisk JHFS+ 名称 {drive}";
        }
    }
}

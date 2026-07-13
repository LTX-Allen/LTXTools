using System;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace LTXTools.Views
{
    public partial class FileCopyPage : ContentView
    {
        public FileCopyPage() { InitializeComponent(); txtStatus.Text = "✅ 就绪"; }

        private async void btnBrowseSource_Clicked(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions { PickerTitle = "请选择源文件" });
            if (result != null) txtSource.Text = result.FullPath;
        }

        private async void btnBrowseDest_Clicked(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions { PickerTitle = "请选择目标文件" });
            if (result != null) txtDest.Text = result.FullPath;
        }

        private async void btnCopy_Clicked(object sender, EventArgs e)
        {
            var page = Application.Current?.MainPage;
            if (string.IsNullOrEmpty(txtSource.Text) || string.IsNullOrEmpty(txtDest.Text))
            { await page.DisplayAlert("提示", "请选择源文件和目标文件！", "确定"); return; }
            btnCopy.IsEnabled = false; btnCopy.Text = "⏳ 复制中..."; txtStatus.Text = "⏳ 正在复制...";
            await Task.Run(() => File.Copy(txtSource.Text, txtDest.Text, true));
            txtStatus.Text = "✅ 复制成功！"; btnCopy.IsEnabled = true; btnCopy.Text = "🚀 开始复制";
            await page.DisplayAlert("成功", "文件复制完成！", "确定");
        }
    }
}

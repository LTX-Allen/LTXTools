using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace LTXTools.Views
{
    public partial class ActivatePage : ContentView
    {
        private const string ActivationKey = "TMW23-23RED-CD57G-RACAD-MD456";
        public ActivatePage() { InitializeComponent(); UpdateStatus(); }
        
        private async void btnActivate_Clicked(object sender, EventArgs e)
        {
            var page = Application.Current?.MainPage;
            if (string.IsNullOrEmpty(txtActivationCode.Text))
            { await page.DisplayAlert("提示", "请输入激活码！", "确定"); return; }
            if (txtActivationCode.Text.Trim() == ActivationKey)
            { Preferences.Set("IsActivated", true); await page.DisplayAlert("成功", "✅ 激活成功！", "确定"); UpdateStatus(); }
            else await page.DisplayAlert("错误", "❌ 激活码错误！", "确定");
        }
        
        private void UpdateStatus() => txtStatus.Text = Preferences.Get("IsActivated", false) ? "✅ 已激活" : "⭕ 未激活";
    }
}

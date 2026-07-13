using System;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace LTXTools.Views
{
    public partial class ShutdownPage : ContentView
    {
        private bool _isLoaded = false;

        public ShutdownPage()
        {
            InitializeComponent();
            _isLoaded = true;
            txtStatus.Text = "✅ 就绪";
        }

        private void rbMinutes_Checked(object sender, CheckedChangedEventArgs e)
        {
            if (!_isLoaded) return;
            panelMinutes.IsVisible = true;
            panelTime.IsVisible = false;
        }

        private void rbTime_Checked(object sender, CheckedChangedEventArgs e)
        {
            if (!_isLoaded) return;
            panelMinutes.IsVisible = false;
            panelTime.IsVisible = true;
        }

        private async void btnStart_Clicked(object sender, EventArgs e)
        {
            var page = Application.Current?.MainPage;
            try
            {
                btnStart.IsEnabled = false;
                btnStart.Text = "⏳ 执行中...";
                txtStatus.Text = "⏳ 正在执行关机命令...";

                if (rbMinutes.IsChecked)
                {
                    if (!int.TryParse(txtMinutes.Text, out int minutes) || minutes <= 0)
                    {
                        await page.DisplayAlert("提示", "请输入有效的分钟数！", "确定");
                        btnStart.IsEnabled = true;
                        btnStart.Text = "🛑 开始关机";
                        return;
                    }
                    await RunShutdownAsync($"-s -t {minutes * 60}");
                    txtStatus.Text = $"✅ 已设置 {minutes} 分钟后关机";
                }
                else
                {
                    if (!int.TryParse(txtHour.Text, out int hour) || hour < 0 || hour > 23)
                    {
                        await page.DisplayAlert("提示", "请输入有效的小时（0-23）！", "确定");
                        btnStart.IsEnabled = true;
                        btnStart.Text = "🛑 开始关机";
                        return;
                    }
                    if (!int.TryParse(txtMinute.Text, out int minute) || minute < 0 || minute > 59)
                    {
                        await page.DisplayAlert("提示", "请输入有效的分钟（0-59）！", "确定");
                        btnStart.IsEnabled = true;
                        btnStart.Text = "🛑 开始关机";
                        return;
                    }
                    DateTime now = DateTime.Now;
                    DateTime target = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
                    if (target <= now) target = target.AddDays(1);
                    await RunShutdownAsync($"-s -t {(int)(target - now).TotalSeconds}");
                    txtStatus.Text = $"✅ 已设置 {target:HH:mm} 关机";
                }
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("错误", $"错误：{ex.Message}", "确定");
                txtStatus.Text = $"❌ 失败：{ex.Message}";
            }
            finally
            {
                btnStart.IsEnabled = true;
                btnStart.Text = "🛑 开始关机";
            }
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            var page = Application.Current?.MainPage;
            try
            {
                btnCancel.IsEnabled = false;
                btnCancel.Text = "⏳ 执行中...";
                await RunShutdownAsync("-a");
                txtStatus.Text = "✅ 已取消关机";
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("错误", $"取消失败：{ex.Message}", "确定");
                txtStatus.Text = $"❌ 取消失败：{ex.Message}";
            }
            finally
            {
                btnCancel.IsEnabled = true;
                btnCancel.Text = "❌ 取消";
            }
        }

        private void btnQuery_Clicked(object sender, EventArgs e)
        {
            txtStatus.Text = "ℹ️ 请打开终端输入 'shutdown /a' 查看状态";
        }

        private async Task RunShutdownAsync(string args)
        {
            await Task.Run(() =>
            {
                var info = new ProcessStartInfo
                {
                    FileName = "shutdown.exe",
                    Arguments = args,
                    UseShellExecute = true,
                    Verb = "runas",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                using (var p = new Process { StartInfo = info })
                {
                    p.Start();
                    p.WaitForExit(3000);
                }
            });
        }
    }
}

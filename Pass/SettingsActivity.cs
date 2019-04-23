using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Widget;
using System.Collections.Generic;
using System.Threading;
using Android.Support.V14;
using Xamarin.Essentials;

namespace Pass
{
    [Activity(Label = "SettingsActivity", Theme = "@style/MyTheme", MainLauncher = true)]
    public class SettingsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_settings);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Settings";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            LoadPreferences();
        }

        private void LoadPreferences()
        {
            EditText ftpServer = FindViewById<EditText>(Resource.Id.settingsFtpServer);
            EditText ftpUser = FindViewById<EditText>(Resource.Id.settingsFtpUser);
            EditText ftpPass = FindViewById<EditText>(Resource.Id.settingsFtpPass);
            EditText filePass = FindViewById<EditText>(Resource.Id.settingsFilePass);

            ftpServer.Text = Preferences.Get("ftp_server", "");
            ftpUser.Text = Preferences.Get("ftp_user", "");
            ftpPass.Text = Preferences.Get("ftp_pass", "");
            filePass.Text = Preferences.Get("file_pass", "");
        }

        private void SavePreferences()
        {
            EditText ftpServer = FindViewById<EditText>(Resource.Id.settingsFtpServer);
            EditText ftpUser = FindViewById<EditText>(Resource.Id.settingsFtpUser);
            EditText ftpPass = FindViewById<EditText>(Resource.Id.settingsFtpPass);
            EditText filePass = FindViewById<EditText>(Resource.Id.settingsFilePass);

            Preferences.Set("ftp_server", ftpServer.Text);
            Preferences.Set("ftp_user", ftpUser.Text);
            Preferences.Set("ftp_pass", ftpPass.Text);
            Preferences.Set("file_pass", filePass.Text);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_settings, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted == null)
            {
                OnBackPressed();
            }

            else if (item != null && item.TitleFormatted.ToString() == "Save")
            {
                SavePreferences();
                Toast.MakeText(this, "Settings saved", ToastLength.Short).Show();
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
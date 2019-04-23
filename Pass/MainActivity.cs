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
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;
        private RecyclerView.LayoutManager layoutManager;

        private PasswordList passwordList;
        private PasswordListAdapter adapter;

        private ProgressBar spinner;

        private string cryptPass;
        private string ftpAddress;
        private string ftpUser;
        private string ftpPass;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Pass";

            spinner = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            spinner.Visibility = ViewStates.Gone;

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewPasswords);
            layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            passwordList = new PasswordList();
            adapter = new PasswordListAdapter(passwordList);
            adapter.ItemClick += OnItemClick;
            recyclerView.SetAdapter(adapter);

            UpdatePasswordList();
        }

        private void UpdatePasswordList()
        {
            ftpAddress = Preferences.Get("ftp_server", "");
            ftpUser = Preferences.Get("ftp_user", "");
            ftpPass = Preferences.Get("ftp_pass", "");
            cryptPass = Preferences.Get("file_pass", "");

            spinner.Visibility = ViewStates.Visible;
            ThreadPool.QueueUserWorkItem(o => UpdatePasswordListAsync());
        }

        private void UpdatePasswordListAsync()
        {
            string encryptedList = FTP.GetFileContents(ftpAddress, ftpUser, ftpPass);
            string decryptedList = StringCipher.Decrypt(encryptedList, cryptPass);
            List<Dictionary<string, string>> list = ProcessFile.GetProcessed(decryptedList);

            passwordList.Clear();
            passwordList.AddRange(list);
            RunOnUiThread(() => UpdatePasswordListDone());
        }

        private void UpdatePasswordListDone()
        {
            adapter.NotifyDataSetChanged();
            spinner.Visibility = ViewStates.Gone;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item != null && item.TitleFormatted.ToString() == "Refresh") UpdatePasswordList();

            if (item != null && item.TitleFormatted.ToString() == "Settings")
            {
                var intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }

        void OnItemClick(object sender, int position)
        {
            var intent = new Intent(this, typeof(ViewActivity));
            intent.PutExtra("name", passwordList[position].Name);
            intent.PutExtra("address", passwordList[position].Address);
            intent.PutExtra("user", passwordList[position].User);
            intent.PutExtra("pass", passwordList[position].Pass);
            intent.PutExtra("comments", passwordList[position].Comments);
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class PasswordListViewHolder : RecyclerView.ViewHolder
    {
        public TextView PasswordName { get; private set; }
        public TextView PasswordAddress { get; private set; }

        public PasswordListViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            PasswordName = itemView.FindViewById<TextView>(Resource.Id.textViewPasswordName);
            PasswordAddress = itemView.FindViewById<TextView>(Resource.Id.textViewPasswordAddress);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

    public class PasswordListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public PasswordList passwordList;

        public PasswordListAdapter(PasswordList passList)
        {
            passwordList = passList;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.recycler_item, parent, false);
            PasswordListViewHolder ph = new PasswordListViewHolder(itemView, OnClick);
            return ph;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PasswordListViewHolder ph = holder as PasswordListViewHolder;
            ph.PasswordName.Text = passwordList[position].Name;
            ph.PasswordAddress.Text = passwordList[position].Address;
        }

        public override int ItemCount
        {
            get { return passwordList.NumPasswords; }
        }

        void OnClick(int position)
        {
            if (ItemClick != null) ItemClick(this, position);
        }

    }
}
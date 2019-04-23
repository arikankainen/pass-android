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

namespace Pass
{
    [Activity(Label = "ViewActivity", Theme = "@style/MyTheme")]
    public class ViewActivity : AppCompatActivity
    {
        private bool copied = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_view);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "View credentials";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            TextView nameTopic = FindViewById<TextView>(Resource.Id.textViewSpecsNameTopic);
            TextView addressTopic = FindViewById<TextView>(Resource.Id.textViewSpecsAddressTopic);
            TextView userTopic = FindViewById<TextView>(Resource.Id.textViewSpecsUserTopic);
            TextView passTopic = FindViewById<TextView>(Resource.Id.textViewSpecsPassTopic);
            TextView commentsTopic = FindViewById<TextView>(Resource.Id.textViewSpecsCommentsTopic);

            TextView name = FindViewById<TextView>(Resource.Id.textViewSpecsName);
            TextView address = FindViewById<TextView>(Resource.Id.textViewSpecsAddress);
            TextView user = FindViewById<TextView>(Resource.Id.textViewSpecsUser);
            TextView pass = FindViewById<TextView>(Resource.Id.textViewSpecsPass);
            TextView comments = FindViewById<TextView>(Resource.Id.textViewSpecsComments);

            name.Text = Intent.Extras.GetString("name");
            address.Text = Intent.Extras.GetString("address");
            user.Text = Intent.Extras.GetString("user");
            pass.Text = Intent.Extras.GetString("pass");
            comments.Text = Intent.Extras.GetString("comments");

            if (name.Text == "")
            {
                nameTopic.Visibility = ViewStates.Gone;
                name.Visibility = ViewStates.Gone;
            }

            if (address.Text == "")
            {
                addressTopic.Visibility = ViewStates.Gone;
                address.Visibility = ViewStates.Gone;
            }

            if (user.Text == "")
            {
                userTopic.Visibility = ViewStates.Gone;
                user.Visibility = ViewStates.Gone;
            }

            if (pass.Text == "")
            {
                passTopic.Visibility = ViewStates.Gone;
                pass.Visibility = ViewStates.Gone;
            }

            if (comments.Text == "")
            {
                commentsTopic.Visibility = ViewStates.Gone;
                comments.Visibility = ViewStates.Gone;
            }

            nameTopic.Click += Name_Click;
            name.Click += Name_Click;

            addressTopic.Click += Address_Click;
            address.Click += Address_Click;

            userTopic.Click += User_Click;
            user.Click += User_Click;

            passTopic.Click += Pass_Click;
            pass.Click += Pass_Click;
        }

        private void Name_Click(object sender, EventArgs e)
        {
            SetClipboard(FindViewById<TextView>(Resource.Id.textViewSpecsName).Text);
            Toast.MakeText(this, "Name copied to clipboard", ToastLength.Short).Show();
            copied = true;
        }

        private void Address_Click(object sender, EventArgs e)
        {
            SetClipboard(FindViewById<TextView>(Resource.Id.textViewSpecsAddress).Text);
            Toast.MakeText(this, "Address copied to clipboard", ToastLength.Short).Show();
            copied = true;
        }

        private void User_Click(object sender, EventArgs e)
        {
            SetClipboard(FindViewById<TextView>(Resource.Id.textViewSpecsUser).Text);
            Toast.MakeText(this, "Username copied to clipboard", ToastLength.Short).Show();
            copied = true;
        }

        private void Pass_Click(object sender, EventArgs e)
        {
            SetClipboard(FindViewById<TextView>(Resource.Id.textViewSpecsPass).Text);
            Toast.MakeText(this, "Password copied to clipboard", ToastLength.Short).Show();
            copied = true;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_view, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted == null)
            {
                OnBackPressed();
            }

            else if (item.TitleFormatted.ToString() == "Edit") Toast.MakeText(this, "Option not available yet.", ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (copied)
            {
                SetClipboard("");
                Toast.MakeText(this, "Clipboard cleared", ToastLength.Short).Show();
            }

            base.OnBackPressed();
        }

        private void SetClipboard(string text = "")
        {
            ClipboardManager clipboard = (ClipboardManager)GetSystemService(Context.ClipboardService);
            ClipData clip = ClipData.NewPlainText(text, text);
            clipboard.PrimaryClip = clip;
        }
    }
}
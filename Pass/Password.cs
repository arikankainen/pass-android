using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Pass
{
    public class Password
    {
        public int id;
        public int Id
        {
            get { return id; }
        }

        public string name;
        public string Name
        {
            get { return name; }
        }

        public string address;
        public string Address
        {
            get { return address; }
        }

        public string user;
        public string User
        {
            get { return user; }
        }

        public string pass;
        public string Pass
        {
            get { return pass; }
        }

        public string comments;
        public string Comments
        {
            get { return comments; }
        }
    }

    public class PasswordList
    {
        private List<Password> passwords;

        public PasswordList()
        {
            passwords = new List<Password>();
        }

        public void AddRange(List<Dictionary<string, string>> list)
        {
            foreach (Dictionary<string, string> dict in list)
            {
                passwords.Add(new Password { id = 0, name = dict["Name"], address = dict["Address"], user = dict["User"], pass = dict["Pass"], comments = dict["Comments"] });
            }
        }

        public void Add(Dictionary<string, string> dict)
        {
            passwords.Add(new Password { id = 0, name = dict["Name"], address = dict["Address"], user = dict["User"], pass = dict["Pass"], comments = dict["Comments"] });
        }

        public void Clear()
        {
            passwords.Clear();
        }

        public int NumPasswords
        {
            get { return passwords.Count; }
        }

        public Password this[int i]
        {
            get { return passwords[i]; }
        }

    }
}
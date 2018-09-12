using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Register
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://admin:a123456@ds141902.mlab.com:41902/ox");
                MongoServer server = client.GetServer();
                MongoDatabase database = server.GetDatabase("ox");
                MongoCollection mongoCollection = database.GetCollection<User>("User");
                User user = new User();
                BindingList<User> doclist = new BindingList<User>();
                var getCollection = database.GetCollection<User>("User");
                var insertUser = getCollection.AsQueryable().Where(pd => pd.Username == Username.Text);

                foreach (var p in insertUser)
                {
                    doclist.Add(p);
                    Application.DoEvents();
                }
                dataGridView1.DataSource = doclist;
                if (dataGridView1.Rows.Count == 0)
                {
                    if (!string.IsNullOrEmpty(Username.Text.Trim()) && !Username.Text.Equals("Username"))
                    {
                        user.Username = Username.Text;
                    }
                    else
                    {
                        MessageBox.Show("กรุณากรอก Username");
                    }
                    if (!string.IsNullOrEmpty(Password.Text.Trim()) && !Password.Text.Equals("Password"))
                    {
                        user.Password = Password.Text;
                    }
                    else
                    {
                        MessageBox.Show("กรุณากรอก Password");
                    }
                    if (!string.IsNullOrEmpty(ConfirmPassword.Text.Trim()) && !ConfirmPassword.Text.Equals("Confirm Password"))
                    {
                        user.Password = ConfirmPassword.Text;
                    }
                    else
                    {
                        MessageBox.Show("กรุณากรอก Comfirm Password");
                    }

                    user.Name = Username.Text;
                    user.Avatar = null;
                    user.Win = 0;
                    user.Draw = 0;
                    user.Lose = 0;

                    if (!string.IsNullOrEmpty(Username.Text.Trim()) &&
                        !string.IsNullOrEmpty(Password.Text.Trim()) &&
                        !string.IsNullOrEmpty(ConfirmPassword.Text.Trim()) &&
                        (!Username.Text.Equals("Username") &&
                         !Password.Text.Equals("Password") &&
                         !ConfirmPassword.Text.Equals("Confirm Password")))
                    {
                        if (Password.Text.Equals(ConfirmPassword.Text))
                        {
                            mongoCollection.Insert(user);
                            MessageBox.Show("สมัครเสร็จสิ้น");
                        }
                        else
                        {
                            MessageBox.Show("กรุณาใส่ Password ให้ตรงกัน");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Username.Text = "";
            Password.Text = "";
            ConfirmPassword.Text = "";
        }

        private void Username_Click(object sender, EventArgs e)
        {
            Username.Text = "";
        }

        private void Password_Click(object sender, EventArgs e)
        {
            Password.Text = "";
            Password.UseSystemPasswordChar = true;
        }

        private void ConfirmPassword_Click(object sender, EventArgs e)
        {
            ConfirmPassword.Text = "";
            ConfirmPassword.UseSystemPasswordChar = true;
        }
    }
    class User
    {
        public ObjectId _id { get; set; }

        public string Username
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Avatar
        {
            get; set;
        }
        public int Win
        {
            get; set;
        }
        public int Draw
        {
            get; set;
        }
        public int Lose
        {
            get; set;
        }
    }
}

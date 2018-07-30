﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Net;
using System.IO;
using System.Net;

namespace Ex01.FacebookApp
{
    public partial class Form1 : Form
    {
        private readonly string k_EnterTitleMsg = "Enter Title";
        LoginResult m_loginResult;
        string m_photoPath = string.Empty;

        FacebookWrapper.ObjectModel.User m_currentUser;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            m_loginResult = FacebookService.Login("273882356720887", "email", "user_hometown", "user_birthday", "user_friends", "user_events", "groups_access_member_info", "publish_video");
            m_currentUser = m_loginResult.LoggedInUser;
            pictureBox1.Image = m_currentUser.ImageNormal;
        }

        private void postStatus(string post)
        {
            m_currentUser.PostStatus(post);
        }

        //somethings wrong here
        public void on_logOut()
        {
            FacebookService.Logout(ShowByeMsg);
        }

        private void ShowByeMsg()
        {
            pictureBox1.Image = null;
            MessageBox.Show("Logged Out!");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox l = sender as ListBox;
            Group g = l.SelectedItem as Group;

            textBoxDescriptionOfGroup.Text = g.Description;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            on_logOut();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            postStatus(textBox2.Text);
            textBox2.Text = "enter post here";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (m_currentUser != null)
            {

                m_currentUser.ReFetch(DynamicWrapper.eLoadOptions.Full);


                FacebookObjectCollection<Group> groupCollection = m_currentUser.Groups;

                foreach (Group g in groupCollection)
                {
                    listBoxGroups.Items.Add(g);
                }

                if (listBoxGroups.Items.Count == 0)
                {
                    listBoxGroups.Items.Add("no items to show");
                }
            }
            else
            {
                MessageBox.Show("not logged in");
            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            string title = string.Empty;
            if (textBoxTitle.Text != k_EnterTitleMsg)
            {
                title = textBoxTitle.Text;
            }
            try
            {
                m_currentUser.PostPhoto(m_photoPath, title);
                MessageBox.Show("Success uploadin photo!" + Environment.NewLine + Path.GetExtension(m_photoPath));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Must Choose A legal File" + Environment.NewLine + ex.Message);

            }

            resetPictureButtons();

        }

        private void resetPictureButtons()
        {
            pictureBoxPostPhotoPreviewImage.Image = null;
            textBoxTitle.Text = k_EnterTitleMsg;
            buttonPostPhoto.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_photoPath = openFileDialog1.FileName;
            }
            if (!string.IsNullOrEmpty(m_photoPath))
            {
                pictureBoxPostPhotoPreviewImage.LoadAsync(m_photoPath);
                buttonPostPhoto.Enabled = true;
            }
        }

        private void textBoxTitle_Click(object sender, EventArgs e)
        {
            textBoxTitle.Text = string.Empty;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            m_currentUser.PostLink(webBrowser.Url.ToString());

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxWebBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            string urlToShow = comboBox.SelectedItem as string;

            if(!string.IsNullOrEmpty(urlToShow))
            {
                webBrowser.Url = new System.Uri(urlToShow);
            }
        }



        private void buttonSubmitUrl_onClick(object sender, EventArgs e)
        {
            string urlToShow = comboBoxWebBrowser.Text;

            Uri uriResult;
            bool result = Uri.TryCreate(urlToShow, UriKind.Absolute, out uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttp;

            if (result)
            {
                 webBrowser.Url = uriResult;
            }
            else
            {
                MessageBox.Show(
@"insert a valid http format url.
example:http://www.google.com");
            }
        }
    }
}

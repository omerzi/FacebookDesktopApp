using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using Singleton;
using FacebookLogic;
using FacebookUI;

namespace FacebookUI
{
    public partial class FormMain : Form
    {
        private readonly AppManagement r_AppManager = Singleton<AppManagement>.Instance;
        private readonly DesigningPanel r_DesignPanel = new DesigningPanel();
        private Random r_RandomIndex = new Random();

        public enum eConnectOrLogin
        {
            Login = 0,
            Connect = 1
        }

        public FormMain()
        {
            InitializeComponent();
            initDesignPanel();
            presentPanel(m_WelcomePanelUI);
        }

        private void initDesignPanel()
        {
            r_DesignPanel.Location = new Point(275, 6);
            r_DesignPanel.Size = new Size(187, 85);
            r_DesignPanel.Add(new ButtonItem() { CommandDelegate = changeColor, Text = "Random Back Color" });
            r_DesignPanel.Add(new ButtonItem() { CommandDelegate = changeFontStyle, Text = "Random Font Style" });
            m_PanelHeader.Controls.Add(r_DesignPanel);
        }

        private void changeColor()
        {
            Color randomColor = Color.FromArgb(r_RandomIndex.Next(256), r_RandomIndex.Next(256), r_RandomIndex.Next(256));
            BackColor = randomColor;
        }

        private void changeFontStyle()
        {
            int fontStyle = r_RandomIndex.Next(0, 4);
            foreach (Control control in Controls)
            {
                switch(fontStyle)
                {
                    case 1:
                        control.Font = new System.Drawing.Font(control.Font, System.Drawing.FontStyle.Regular);
                        break;
                    case 2:
                        control.Font = new System.Drawing.Font(control.Font, System.Drawing.FontStyle.Bold);
                        break;
                    case 3:
                        control.Font = new System.Drawing.Font(control.Font, System.Drawing.FontStyle.Italic);
                        break;
                    case 4:
                        control.Font = new System.Drawing.Font(control.Font, System.Drawing.FontStyle.Underline);
                        break;
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            startFacebookConnection(eConnectOrLogin.Connect);
        }

        private void startFacebookConnection(eConnectOrLogin i_ConnectionType)
        {
            bool isLoggedIn = false;

            try
            {
                if (i_ConnectionType == eConnectOrLogin.Login)
                {
                    r_AppManager.SignIn(out isLoggedIn);
                }
                else
                {
                    r_AppManager.CheckConnection(out isLoggedIn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (isLoggedIn)
                {
                    populateUIFromFacebookData();
                }
                else
                {
                    if (i_ConnectionType == eConnectOrLogin.Login)
                    {
                        MessageBox.Show("Login Failed!");
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            r_AppManager.SavePreferences();
        }

        private void presentPanel(Panel i_Panel)
        {
            foreach (Panel panel in this.Controls)
            {
                if (panel.Name.Contains("UI") && panel != i_Panel)
                {
                    panel.Hide();
                }
            }

            if (r_AppManager.CurrentUser != null )
            {
                i_Panel.Show();
            }
            else
            {
                m_WelcomePanelUI.Show();
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            startFacebookConnection(eConnectOrLogin.Login);
        }

        private void populateUIFromFacebookData()
        {
            m_ButtonLogin.Visible = false;
            m_WelcomePanelUI.Hide();
            m_PictureBoxProfile.Load(r_AppManager.CurrentUser.PictureNormalURL);
            m_PictureBoxProfile.Visible = true;
            m_ButtonLogout.Visible = true;
            m_CheckBoxRememberMe.Visible = false;
            new Thread(setProfileUI).Start();
            showProfile();
            new Thread(setSocialUI).Start();
            new Thread(setEventsUI).Start();
        }

        private void setSocialUI()
        {
            try
            {
                if (r_AppManager.SocialManagement == null)
                {
                    r_AppManager.FetchSocialData();
                }

                setFriendsList();
                setCheckingsList();
                setAlbumsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setAlbumsList()
        {
            if (r_AppManager.SocialManagement != null && 
                r_AppManager.SocialManagement.AlbumList.Count > 0)
            {
                m_ListBoxAlbums.Invoke(
                    new Action(() =>
                    {
                        m_ListBoxAlbums.Items.Clear();
                        foreach (Album album in r_AppManager.SocialManagement.AlbumList)
                        {
                            m_ListBoxAlbums.Items.Add(album.Name);
                        }
                    }));
            }
        }

        private void setCheckingsList()
        {
            if (r_AppManager.SocialManagement != null &&
                r_AppManager.SocialManagement.LatestChekingsList.Count > 0)
            {
                m_ListBoxLatestCheckings.Invoke(
                    new Action(() =>
                    {
                        m_ListBoxLatestCheckings.Items.Clear();
                        foreach (Checkin checkIn in r_AppManager.SocialManagement.LatestChekingsList)
                        {
                            m_ListBoxLatestCheckings.Items.Add
                                (string.Format(
                                @"Place:{0}, Time:{1}",
                                checkIn.Place.Name,
                                checkIn.CreatedTime));
                        }
                    }));
            }
        }

        private void setFriendsList()
        {
            if (r_AppManager.SocialManagement != null && 
                r_AppManager.SocialManagement.FriendsList.Count > 0)
            {
                m_ListBoxFriendsList.Invoke(
                    new Action(() =>
                    {
                        m_ListBoxFriendsList.Items.Clear();
                        foreach (User friend in r_AppManager.SocialManagement.FriendsList)
                        {
                            m_ListBoxFriendsList.Items.Add(friend.Name);
                        }
                    }));
            }
        }

        private void showProfile()
        {
            presentPanel(m_PanelProfileUI);
        }

        private void setProfileUI()
        {
            if (r_AppManager.ProfileManagement != null &&
                r_AppManager.ProfileManagement.PostsList.Count > 0)
            {
                m_ListBoxProfileStatus.Invoke(
                    new Action(() =>
                    {
                        userBindingSource.DataSource = r_AppManager.CurrentUser;
                        m_ListBoxProfileStatus.Items.Clear();
                        foreach (Post post in r_AppManager.ProfileManagement.PostsList)
                        {
                            if (post.Message != null)
                            {
                                m_ListBoxProfileStatus.Items.Add(post.Message);
                            }
                        }
                    }));
            }
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                showProfile();
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            r_AppManager.LoggingOut += buttonLogout_LoggingOut;
            r_AppManager.DisconnectFacebook();
        }

        private void buttonLogout_LoggingOut()
        {
            m_ButtonLogin.Visible = true;
            m_CheckBoxRememberMe.Visible = true;
            m_ButtonLogout.Visible = false;
            m_PictureBoxProfile.Image = null;
            presentPanel(m_WelcomePanelUI);
        }

        private void buttonSocial_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                presentPanel(m_PanelSocialUI);
            }
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            try
            {
                r_AppManager.PostStatus(m_TextBoxStatus.Text);
                MessageBox.Show("Successfully Posted a new Status!");
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Post your new Status!");
            }
        }

        private void textBoxStatus_Click(object sender, EventArgs e)
        {
            (sender as TextBox).Text = " ";
        }

        private void listBoxProfileStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (r_AppManager.ProfileManagement != null && 
                r_AppManager.ProfileManagement.PostsList.Count > 0)
            {
                Post selectedPost =
                    r_AppManager.ProfileManagement.PostsList[m_ListBoxProfileStatus.SelectedIndex];
                m_ListBoxProfilePostsComments.DisplayMember = "Message";
                m_ListBoxProfilePostsComments.DataSource = selectedPost.Comments;
            }
        }

        private void checkBoxRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            r_AppManager.ApplicationSettings.RememberUser = (sender as CheckBox).Checked;
        }

        private void buttonBestFriend_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                FormBestFriend bestFriendForm = new FormBestFriend();
                this.Hide();
                bestFriendForm.ShowDialog();
                this.Show();
            }
        }

        private void listBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (r_AppManager.EventsManagement != null &&
               r_AppManager.EventsManagement.EventList.Count > 0)
            {
                Event selectedEvent = 
                    r_AppManager.EventsManagement.EventList[m_ListBoxUpcomingEvents.SelectedIndex];
                m_ListBoxEventInfo.DisplayMember = "Message";
                m_ListBoxEventInfo.DataSource = selectedEvent.Description;
            }
        }

        private void setEventsUI()
        {
            try
            {
                if(r_AppManager.EventsManagement == null)
                {
                    r_AppManager.FetchEventsData();
                }

                if(r_AppManager.EventsManagement.EventList.Count > 0)
                {
                    m_ListBoxUpcomingEvents.Invoke(
                    new Action(() =>
                    {
                        m_ListBoxUpcomingEvents.Items.Clear();

                        foreach (Event CurrentEvent in r_AppManager.EventsManagement.EventList)
                        {
                            m_ListBoxUpcomingEvents.Items.Add(CurrentEvent.Name);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                presentPanel(m_PanelEventsUI);
            }
        }

        private void buttonBirthdayFeature_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                this.Hide();
                FormBirthdayFeature form = new FormBirthdayFeature();
                this.Show();
            }
        }
    }
}
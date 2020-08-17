using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using FacebookLogic;
using FacebookUI;

namespace FacebookUI
{
    public partial class FormMain : Form
    {
        private readonly AppManagement r_AppManager = new AppManagement();

        public enum eConnectOrLogin
        {
            Login = 0,
            Connect = 1
        }

        public FormMain()
        {
            InitializeComponent();
            presentPanel(m_WelcomePanelUI);
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
            setProfileUI();
            showProfile();
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
                m_ListBoxAlbums.Items.Clear();

                foreach (Album album in r_AppManager.SocialManagement.AlbumList)
                {
                    m_ListBoxAlbums.Items.Add(album.Name);
                }
            }
        }

        private void setCheckingsList()
        {
            if (r_AppManager.SocialManagement != null &&
                r_AppManager.SocialManagement.LatestChekingsList.Count > 0)
            {
                m_ListBoxLatestCheckings.Items.Clear();

                foreach (Checkin checkIn in r_AppManager.SocialManagement.LatestChekingsList)
                {
                    m_ListBoxLatestCheckings.Items.Add(
                        string.Format(
                        @"Place:{0}, Time:{1}",
                        checkIn.Place.Name,
                        checkIn.CreatedTime));
                }
            }
        }

        private void setFriendsList()
        {
            if (r_AppManager.SocialManagement != null && 
                r_AppManager.SocialManagement.FriendsList.Count > 0)
            {
                m_ListBoxFriendsList.Items.Clear();

                foreach (User friend in r_AppManager.SocialManagement.FriendsList)
                {
                    m_ListBoxFriendsList.Items.Add(friend.Name);
                }
            }
        }

        private void showProfile()
        {
            presentPanel(m_PanelProfileUI);
        }

        private void setProfileUI()
        {
            m_LabelTextFullName.Text = string.Format(
                @"{0} {1}",
                r_AppManager.CurrentUser.FirstName,
                r_AppManager.CurrentUser.LastName);
            m_LabelTextLivesAt.Text = r_AppManager.CurrentUser.Location.Name;
            m_LabelTextBirthday.Text = r_AppManager.CurrentUser.Birthday;
            m_LabelTextEmail.Text = r_AppManager.CurrentUser.Email;

            if (r_AppManager.ProfileManagement != null &&
                r_AppManager.ProfileManagement.PostsList.Count > 0)
            {
                m_ListBoxProfileStatus.Items.Clear();

                foreach (Post post in r_AppManager.ProfileManagement.PostsList)
                {
                    if (post.Message != null)
                    {
                        m_ListBoxProfileStatus.Items.Add(post.Message);
                    }
                }
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
                setSocialUI();
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
                FormBestFriend bestFriendForm = new FormBestFriend(r_AppManager);
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
                    m_ListBoxUpcomingEvents.Items.Clear();

                    foreach (Event CurrentEvent in r_AppManager.EventsManagement.EventList)
                    {
                        m_ListBoxUpcomingEvents.Items.Add(CurrentEvent.Name);
                    }
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
                setEventsUI();
                presentPanel(m_PanelEventsUI);
            }
        }

        private void buttonBirthdayFeature_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                this.Hide();
                FormBirthdayFeature form = new FormBirthdayFeature(r_AppManager);
                this.Show();
            }
        }
    }
}
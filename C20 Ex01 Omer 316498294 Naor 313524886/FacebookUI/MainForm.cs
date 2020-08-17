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
using System.Net.Mail;

namespace FacebookUI
{
    public partial class MainForm : Form
    {
        private readonly AppManagement r_AppManager = new AppManagement();
        private bool m_IsFirstTime = true;
        public MainForm()
        {
            InitializeComponent();
            presentPanel(m_WelcomePanelUI);
        }

        private void presentPanel(Panel i_Panel)
        {
            foreach(Panel panel in this.Controls)
            {
                if(panel.Name.Contains("UI") && panel != i_Panel)
                {
                    panel.Hide();
                }
            }

            i_Panel.Show();
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
            bool isLoggedIn = r_AppManager.SignIn();
            if (isLoggedIn)
            {
                (sender as Button).Visible = false;
                m_WelcomePanelUI.Hide();
                m_PictureBoxProfile.LoadAsync(r_AppManager.CurrentUser.PictureNormalURL);
                m_PictureBoxProfile.Visible = true;
                m_ButtonLogout.Visible = true;
                showProfile();
             //   setEvents();
                setFriendsBirthday();
            }
            else
            {
                MessageBox.Show("Login Failed!");
            }
        }

        private void setFriendsBirthday()
        {
            if(r_AppManager != null)
            {
                foreach (FBFriendData friend in r_AppManager.FriendBdManegment.friendList)
                    m_ListBoxFriendsBirthday.Items.Add(friend.Name);
            }
        }

        private void showProfile()
        {
            if (r_AppManager.CurrentUser != null )
            {
                if(m_IsFirstTime)
                {
                    setProfileData();
                    m_IsFirstTime = false;
                }

                presentPanel(m_PanelProfileUI);
            }
        }

        private void setProfileData()
        {
            m_LabelTextFullName.Text = string.Format(
                @"{0} {1}",
                r_AppManager.CurrentUser.FirstName,
                r_AppManager.CurrentUser.LastName);
            m_LabelTextLivesAt.Text = r_AppManager.CurrentUser.Location.Name;
            m_LabelTextBirthday.Text = r_AppManager.CurrentUser.Birthday;
            m_LabelTextEmail.Text = r_AppManager.CurrentUser.Email;
            foreach(Post post in r_AppManager.ProfileManagement.PostsList)
            {
                m_ListBoxProfileStatus.Items.Add(post.Message);
            }
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            showProfile();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            r_AppManager.LoggingOut += buttonLogout_LoggingOut;
            r_AppManager.DisconnectFacebook();
        }

        private void buttonLogout_LoggingOut()
        {
            m_ButtonLogin.Visible = true;
            m_ButtonLogout.Visible = false;
            m_PictureBoxProfile.Image = null;
            m_IsFirstTime = true;
            FacebookService.Logout(null);
            clearPages();
            presentPanel(m_WelcomePanelUI);
        }

        private void clearPages()
        {
            clearProfilePage();
        }

        private void clearProfilePage()
        {
            foreach (Control profileControls in m_PanelProfileUI.Controls)
            {
                if (profileControls is PictureBox)
                {
                    (profileControls as PictureBox).Image = null;
                }    
                else if (profileControls is Label)
                {
                    if ((profileControls as Label).Name.Contains("Text"))
                    {
                        profileControls.Text = string.Empty;
                    }
                }
                else
                {
                    (profileControls as ListBox).Items.Clear();
                }    
            }
        }

        private void buttonSocial_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                presentPanel(m_PanelSocialUI);
            }
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                presentPanel(m_PanelEventsUI);
            }
        }

        private void listBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_ListBoxEventsData.Items.Clear();
            string curItem = m_ListBoxEvents.SelectedItem.ToString();
            int index = m_ListBoxEvents.FindString(curItem);
            m_ListBoxEventsData.Items.Add(r_AppManager.EventsManegement.EventDataList[index]);
        }
    
        private void setEvents()
        {
            foreach(Event CurrentEvent in r_AppManager.EventsManegement.EventList)
            {
                m_ListBoxEvents.Items.Add(CurrentEvent.Name);
            }
        }

        private void m_Birthdayfriends_Click(object sender, EventArgs e)
        {
            if (r_AppManager.CurrentUser != null)
            {
                presentPanel(m_PanelBirthdayUI);
            }
        }

     

        private void buttonSendMail_Click(object sender, EventArgs e)
        {
            if (m_ListBoxFriendsBirthday.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(m_RichTextBoxMailContent.Text))
                {
                    MessageBox.Show("Put Content to send a mail!");
                }
                else
                {
                    string CurrentFriendFullName = m_ListBoxFriendsBirthday.SelectedItem.ToString();
                    FBFriendData CurrentFriendBD = r_AppManager.FriendBdManegment.SearchFriend(CurrentFriendFullName);
                    if (CurrentFriendBD != null)
                    {
                        r_AppManager.FriendBdManegment.sendMail(r_AppManager.CurrentUser, m_RichTextBoxMailContent.Text, CurrentFriendBD);
                        MessageBox.Show(string.Format("You sent mail to say happy birthday to {0} ", CurrentFriendBD.Name));
                    }
                    else
                    {
                        MessageBox.Show("Somting went Worng..");
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose a Friend.");
            }
        }
    }
}


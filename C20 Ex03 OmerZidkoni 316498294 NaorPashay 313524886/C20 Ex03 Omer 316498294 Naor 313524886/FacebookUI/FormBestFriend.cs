using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookLogic;
using FacebookWrapper.ObjectModel;
using Singleton;

namespace FacebookUI
{
    public partial class FormBestFriend : Form
    {
        private readonly AppManagement r_AppManager = Singleton<AppManagement>.Instance;
        private UserFriend m_SelectedFriend;

        public FormBestFriend()
        {
            InitializeComponent();
            m_PanelMemories.Hide();
            setBestFriendUI();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void setBestFriendUI()
        {
            try
            {
                if (r_AppManager.BestFriend == null)
                {
                    r_AppManager.FetchBestFriendData();
                }

                UserFriend mostCommentsFriend = r_AppManager.GetBestCommentFriend();
                if (mostCommentsFriend != null)
                {
                    m_PictureBoxCommentsBestFriend.Load(mostCommentsFriend.ProfilePhotoURL);
                    m_LabelCommentsBestFriend.Text = mostCommentsFriend.Name;
                    m_ComboBoxMemories.Items.Add(mostCommentsFriend.Name);
                }

                UserFriend mostLikesFriend = r_AppManager.GetBestLikesFriend();
                if (mostLikesFriend != null)
                {
                    m_PictureBoxLikesBestFriend.Load(mostLikesFriend.ProfilePhotoURL);
                    m_LabelLikesBestFriend.Text = mostLikesFriend.Name;
                    m_ComboBoxMemories.Items.Add(mostLikesFriend.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonStartMemories_Click(object sender, EventArgs e)
        {
            if (m_ComboBoxMemories.SelectedItem != null)
            {
                m_PanelBestFriendUI.Hide();
                m_PanelMemories.Show();
                m_SelectedFriend = r_AppManager.BestFriendsList[m_ComboBoxMemories.SelectedIndex];
                setMemoriesUI();
            }
        }

        private void setMemoriesUI()
        {
            try
            {
                setOurChekins();
                setPostsTaggedIn();
                setPictures();
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't set all properties!");
            }
        }

        private void setPostsTaggedIn()
        {
            foreach(Post post in r_AppManager.CurrentUser.Posts)
            {
                foreach (User user in post.TaggedUsers)
                {
                    if(user.Id == m_SelectedFriend.ID)
                    {
                        m_ListBoxTaggedPosts.Items.Add(post.Message);
                    }
                }
            }
        }

        private void setOurChekins()
        {
            foreach(Checkin checkIn in r_AppManager.CurrentUser.Checkins)
            {
                foreach(User user in checkIn.TaggedUsers)
                {
                    if(user.Id == m_SelectedFriend.ID)
                    {
                        m_ListBoxSharedChekins.Items.Add(checkIn.Place.Name);
                    }
                }
            }
        }

        private void setPictures()
        {
            List<string> picturesList = r_AppManager.GetCommonPhotosList(m_SelectedFriend);
            int ourPhotosIndex = 0;
            foreach(PictureBox pictureBox in m_GroupBoxPhotos.Controls)
            {
                if(picturesList.Count > 0 && ourPhotosIndex < picturesList.Count)
                {
                    pictureBox.Load(picturesList[ourPhotosIndex]);
                    ourPhotosIndex++;
                }
                else
                {
                    break;
                }
            }
        }
    }
}

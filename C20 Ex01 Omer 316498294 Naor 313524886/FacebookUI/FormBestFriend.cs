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

namespace FacebookUI
{
    public partial class FormBestFriend : Form
    {
        private readonly AppManagement r_AppManager;
        private readonly List<UserFriend> r_BestFriends = new List<UserFriend>();
        private readonly List<string> r_OurPhotos = new List<string>();
        private UserFriend m_SelectedFriend;

        public FormBestFriend(AppManagement i_AppManager)
        {
            InitializeComponent();
            r_AppManager = i_AppManager;
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
                    r_BestFriends.Add(mostCommentsFriend);
                    m_PictureBoxCommentsBestFriend.Load(mostCommentsFriend.ProfilePhotoURL);
                    m_LabelCommentsBestFriend.Text = mostCommentsFriend.Name;
                    m_ComboBoxMemories.Items.Add(mostCommentsFriend.Name);
                }

                UserFriend mostLikesFriend = r_AppManager.GetBestLikesFriend();
                if (mostLikesFriend != null)
                {
                    r_BestFriends.Add(mostLikesFriend);
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
                m_SelectedFriend = r_BestFriends[m_ComboBoxMemories.SelectedIndex];
                setMemoriesUI();
            }
        }

        private void setMemoriesUI()
        {
            try
            {
                setOurChekins();
                setPostsTaggedIn();
                setOurPhotos();
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

        private void setOurPhotos()
        {
            foreach (Album albums in r_AppManager.CurrentUser.Albums)
            {
                foreach (Photo photo in albums.Photos)
                {
                    if(photo.Tags != null)
                    {
                        foreach (PhotoTag taggedUser in photo.Tags)
                        {
                            if (taggedUser.User.Id == m_SelectedFriend.ID)
                            {
                                r_OurPhotos.Add(photo.PictureNormalURL);
                            }
                        }
                    }    
                }
            }

            setPictures();
        }

        private void setPictures()
        {
            int ourPhotosIndex = 0;
            foreach(PictureBox pictureBox in m_GroupBoxPhotos.Controls)
            {
                if(r_OurPhotos.Count > 0 && ourPhotosIndex < r_OurPhotos.Count)
                {
                    pictureBox.Load(r_OurPhotos[ourPhotosIndex]);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookLogic;
using FacebookWrapper.ObjectModel;
using Singleton;

namespace FacebookUI
{
    public partial class FormBirthdayFeature : Form
    {
        private const int k_MaxFriends = 3;

        private readonly Random r_RandomIndex = new Random();

        private readonly UserFriend r_BirthdayFriend;

        private readonly List<User> r_RandomFriendsList = new List<User>();

        private readonly List<string> r_QuestionsList = new List<string>();

        private readonly AppManagement r_AppManager = Singleton<AppManagement>.Instance;

        private int m_CurrentQuestion;

        private int m_CorrectAnswers;

        private string m_CurrentCorrectAnswer;

        public enum eQuestion
        {
            First = 1,
            Second,
            Third,
        }

        public FormBirthdayFeature()
        {
            InitializeComponent();
            r_AppManager.FetchBirthdayFriendData();
            r_BirthdayFriend = r_AppManager.BirthdayFriend.GetBirthdayFriend();
            initializeUI();
        }

        private void initializeUI()
        {
            if (r_BirthdayFriend != null)
            {
                setQuestions();
                setFriendsList();
                setUI();
            }
            else
            {
                m_PanelAnotherBirthdays.Hide();
                m_GroupBoxQuizz.Hide();
                m_LabelWelcomeFriendBirthday.Hide();
                m_PictureBoxFriend.Hide();
                m_LabelNoFriends.Visible = true;
            }

            this.ShowDialog();
        }

        private void setFriendsList()
        {
            int friendsCounter = 0;
            while (friendsCounter < k_MaxFriends)
            {
                if (r_AppManager.SocialManagement == null)
                {
                    r_AppManager.FetchSocialData();
                }

                User randomFriend = r_AppManager.SocialManagement.GetRandomFriend();

                while (r_RandomFriendsList.Contains(randomFriend)
                        && friendsCounter < r_AppManager.SocialManagement.FriendsList.Count)
                {
                    randomFriend = r_AppManager.SocialManagement.GetRandomFriend();
                }

                r_RandomFriendsList.Add(randomFriend);
                friendsCounter++;
            }
        }

        private void setUI()
        {
            m_PictureBoxFriend.Load(r_BirthdayFriend.ProfilePhotoURL);
            changeQuestion();
            setOthersBirthdayPanel();
        }

        private void setOthersBirthdayPanel()
        {
            int counter;
            foreach (UserFriend friend in r_AppManager.BirthdayFriend)
            {
                if (friend.ID != r_BirthdayFriend.ID)
                {
                    foreach (PictureBox picture in m_PanelAnotherBirthdays.Controls)
                    {
                        counter = 0;
                        if (picture.Image == null && counter == 0)
                        {
                            picture.Load(friend.ProfilePhotoURL);
                            counter++;
                            break;
                        }
                    }
                }
            }
        }

        private void changeQuestion()
        {
            m_GroupBoxQuizz.Text = r_QuestionsList[m_CurrentQuestion];
            setAnswers();
            m_CurrentQuestion++;
        }

        private void setAnswers()
        {
            List<string> answersList = new List<string>();
            int currIndex = m_CurrentQuestion + 1;

            switch (currIndex)
            {
                case (int)eQuestion.First:
                    foreach (User friend in r_RandomFriendsList)
                    {
                        answersList.Add(friend.Name);
                    }

                    answersList.Add(r_BirthdayFriend.Name);
                    m_CurrentCorrectAnswer = r_BirthdayFriend.Name;
                    break;

                case (int)eQuestion.Second:
                    foreach (User friend in r_RandomFriendsList)
                    {
                        answersList.Add(friend.Location.Name);
                    }

                    answersList.Add(r_BirthdayFriend.LivingPlace);
                    m_CurrentCorrectAnswer = r_BirthdayFriend.LivingPlace;
                    break;

                case (int)eQuestion.Third:
                    foreach (User friend in r_RandomFriendsList)
                    {
                        answersList.Add(friend.Checkins[0].Place.Name);
                    }

                    answersList.Add(r_BirthdayFriend.LatestCheckin);
                    m_CurrentCorrectAnswer = r_BirthdayFriend.LatestCheckin;
                    break;
            }

            setRadioButtonsByQuestion(answersList);
        }

        private void setRadioButtonsByQuestion(List<string> i_AnswersList)
        {
            int randIndex;
            foreach (RadioButton rButton in m_GroupBoxQuizz.Controls.OfType<RadioButton>())
            {
                if (i_AnswersList.Count > 0)
                {
                    rButton.Visible = true;
                    randIndex = r_RandomIndex.Next(i_AnswersList.Count);
                    rButton.Text = i_AnswersList[randIndex];
                    i_AnswersList.RemoveAt(randIndex);
                }
            }
        }

        private void setQuestions()
        {
            if (!string.IsNullOrEmpty(r_BirthdayFriend.Name))
            {
                r_QuestionsList.Add("What's he's Name?");
            }

            if (!string.IsNullOrEmpty(r_BirthdayFriend.LivingPlace))
            {
                r_QuestionsList.Add("Where he Lives?");
            }

            if (!string.IsNullOrEmpty(r_BirthdayFriend.LatestCheckin))
            {
                r_QuestionsList.Add("Where did he Checked In Lately?");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            bool isButtonChecked;
            updateCorrectAnswersPoints(out isButtonChecked);
            if (isButtonChecked && m_CurrentQuestion < r_QuestionsList.Count)
            {
                changeQuestion();
            }
            else
            {
                MessageBox.Show(
                    string.Format(
                        @"You answered {0}/{1} correct answers!",
                        m_CorrectAnswers,
                        r_QuestionsList.Count));
                this.Hide();
            }
        }

        private void updateCorrectAnswersPoints(out bool isButtonChecked)
        {
            isButtonChecked = false;
            foreach (RadioButton rButton in m_GroupBoxQuizz.Controls.OfType<RadioButton>())
            {
                if (rButton.Checked)
                {
                    isButtonChecked = true;
                    if (rButton.Text == m_CurrentCorrectAnswer)
                    {
                        m_CorrectAnswers++;
                        break;
                    }
                }
            }
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

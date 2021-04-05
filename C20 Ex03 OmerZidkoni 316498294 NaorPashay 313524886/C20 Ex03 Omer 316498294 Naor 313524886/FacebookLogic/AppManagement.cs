using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class AppManagement
    {
        private const string k_AppID = "604513893795681";

        public BestUserFriend BestFriend { get; set; }

        public FriendsBirthdayFeature BirthdayFriend { get; set; } 

        public AppSettings ApplicationSettings { get; set; }

        public User CurrentUser { get; set; }

        public Profile ProfileManagement { get; set; }

        public Social SocialManagement { get; set; }

        public Events EventsManagement { get; set; }

        public event Action LoggingOut;

        private AppManagement()
        {
        }

        public bool ToRememberUser()
        {
            return ApplicationSettings.RememberUser;
        }

        public List<UserFriend> BestFriendsList
        {
            get { return BestFriend.BestFriendList; }
        }

        public void SignIn(out bool o_LoginSucceed)
        {
            LoginResult result = FacebookService.Login(
                k_AppID,
                "public_profile",
                "email",
                "user_birthday",
                "user_age_range",
                "user_gender",
                "user_tagged_places",
                "user_friends",
                "user_likes",
                "user_location",
                "user_photos",
                "user_posts",
                "user_hometown");
            ApplicationSettings.LatestAccessToken = result.AccessToken;
            CurrentUser = result.LoggedInUser;
            o_LoginSucceed = true;
            ProfileManagement = new Profile(CurrentUser);
        }

        public void SavePreferences()
        {
            if (!ApplicationSettings.RememberUser)
            {
                ApplicationSettings.LatestAccessToken = null;
            }

            ApplicationSettings.SaveToFile();
        }

        public void CheckConnection(out bool o_ToConnect)
        {
            o_ToConnect = false;

            try
            {
                ApplicationSettings = AppSettings.LoadFromFile();
            }
            catch (Exception)
            {
                ApplicationSettings = new AppSettings();
            }

            if (ApplicationSettings != null)
            {
                o_ToConnect = 
                    !string.IsNullOrEmpty(ApplicationSettings.LatestAccessToken) 
                    && ApplicationSettings.RememberUser;
                if (o_ToConnect)
                {
                    o_ToConnect = false;
                    LoginResult result =
                        FacebookService.Connect(ApplicationSettings.LatestAccessToken);
                    o_ToConnect = true;
                    CurrentUser = result.LoggedInUser;
                    ProfileManagement = new Profile(CurrentUser);
                }
            }
        }

        public void FetchSocialData()
        {
            SocialManagement = new Social(CurrentUser);
        }

        public void FetchBestFriendData()
        {
            BestFriend = new BestUserFriend(CurrentUser);
        }

        public void FetchBirthdayFriendData()
        {
            BirthdayFriend = new FriendsBirthdayFeature(
                CurrentUser,
                (day, month) =>
                day == DateTime.Now.Day.ToString() && 
                month == DateTime.Now.Month.ToString());
        }

        public void FetchEventsData()
        {
            EventsManagement = new Events(CurrentUser);
        }

        public void DisconnectFacebook()
        {
            FacebookService.Logout(LoggingOut);
            ApplicationSettings.RememberUser = false;
            CurrentUser = null;
            ProfileManagement = null;
            SocialManagement = null;
            EventsManagement = null;
        }

        public void PostStatus(string text)
        {
            CurrentUser.PostStatus(text);
        }

        public UserFriend GetBestCommentFriend()
        {
            if (BestFriend.BestCommentFriend == null)
            {
                BestFriend.SetFriendWithMostComments();
            }

            return BestFriend.BestCommentFriend;
        }

        public UserFriend GetBestLikesFriend()
        {
            if (BestFriend.BestLikesFriend == null)
            {
                BestFriend.SetFriendWithMostLikes();
            }

            return BestFriend.BestLikesFriend;
        }

        public List<string> GetCommonPhotosList(UserFriend i_BestFriend)
        {
            BestFriend.SetOurCommonPhotos(CurrentUser, i_BestFriend);
            return BestFriend.OurPhotosList;
        }
    }
}

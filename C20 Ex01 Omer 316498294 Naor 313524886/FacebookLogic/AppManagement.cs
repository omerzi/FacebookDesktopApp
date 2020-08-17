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

        public bool ToRememberUser()
        {
            return ApplicationSettings.RememberUser;
        }

        public void SignIn(out bool o_LoginSucceed)
        {
            try
            {
                LoginResult result = FacebookService.Login(
                    k_AppID,
                    "public_profile",
                    "email",
                    "publish_to_groups",
                    "user_birthday",
                    "user_age_range",
                    "user_gender",
                    "user_link",
                    "user_tagged_places",
                    "user_videos",
                    "publish_to_groups",
                    "groups_access_member_info",
                    "pages_read_engagement",
                    "user_friends",
                    "user_events",
                    "user_likes",
                    "user_location",
                    "user_photos",
                    "user_posts",
                    "user_hometown",
                    "pages_show_list");

                ApplicationSettings.LatestAccessToken = result.AccessToken;
                CurrentUser = result.LoggedInUser;
                o_LoginSucceed = true;
                ProfileManagement = new Profile(CurrentUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    try
                    {
                        o_ToConnect = false;
                        LoginResult result =
                            FacebookService.Connect(ApplicationSettings.LatestAccessToken);
                        o_ToConnect = true;
                        CurrentUser = result.LoggedInUser;
                        ProfileManagement = new Profile(CurrentUser);
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
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
            BirthdayFriend = new FriendsBirthdayFeature(CurrentUser);
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
            BestFriend.SetFriendWithMostComments();
            return BestFriend.BestCommentFriend;
        }

        public UserFriend GetBestLikesFriend()
        {
            BestFriend.SetFriendWithMostLikes();
            return BestFriend.BestLikesFriend;
        }
    }
}

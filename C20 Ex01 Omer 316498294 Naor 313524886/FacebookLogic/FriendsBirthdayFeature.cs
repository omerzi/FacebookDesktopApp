using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class FriendsBirthdayFeature
    {
        private readonly List<UserFriend> r_FBFriendListToday = new List<UserFriend>();
        private readonly UserFriend r_CurrentFriendFB = new UserFriend();

        public FriendsBirthdayFeature(User i_CurrentUser)
        {
            setListData(i_CurrentUser);
        }

        private void setListData(User i_CurrentUser)
        {
            bool isToday;
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
          
            foreach (User friend in i_CurrentUser.Friends)
            {
                if (friend.Birthday != null)
                {
                    setFriendDate(friend.Birthday);
                    isToday = day == r_CurrentFriendFB.DayBorn
                               && month == r_CurrentFriendFB.MonthBorn;
                    if (isToday)
                    {
                        setFriendData(friend);
                        r_FBFriendListToday.Add(r_CurrentFriendFB);
                    }
                }
            }
        }

        private void setFriendData(User i_Friend)
        {
            r_CurrentFriendFB.ProfilePhotoURL = i_Friend.PictureNormalURL;
            r_CurrentFriendFB.LivingPlace = i_Friend.Location.Name;
            r_CurrentFriendFB.Name = i_Friend.Name;
            if (i_Friend.Checkins.Count > 0)
            {
                r_CurrentFriendFB.LatestCheckin = i_Friend.Checkins[0].Place.Name;
            }
        }

        public UserFriend GetBirthdayFriend()
        {
            Random randomFriendIndex = new Random();
            UserFriend randomFriend = null;
            if(r_FBFriendListToday.Count > 0)
            {
                randomFriend =
                    r_FBFriendListToday[randomFriendIndex.Next(r_FBFriendListToday.Count)];
            }

            return randomFriend;
        }

        private void setFriendDate(string i_FriendBirthday)
        {
            r_CurrentFriendFB.DayBorn = i_FriendBirthday.Substring(3, 2);
            r_CurrentFriendFB.MonthBorn = i_FriendBirthday.Substring(0, 2);
        }

        public List<UserFriend> BirthdayFriendList
        { 
            get
            {
                return r_FBFriendListToday; 
            }
        }
    }
}
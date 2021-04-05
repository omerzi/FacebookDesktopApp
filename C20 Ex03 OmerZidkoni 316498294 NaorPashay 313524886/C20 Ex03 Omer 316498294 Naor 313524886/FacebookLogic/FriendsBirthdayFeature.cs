using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class FriendsBirthdayFeature : IEnumerable
    {
        private readonly List<UserFriend> r_FBFriendList = new List<UserFriend>();

        public Func<string, string, bool> BirthdayStrategyMethod { get; set; }

        internal FriendsBirthdayFeature(User i_CurrentUser, Func<string, string, bool> i_MyBirthdayStrategy)
        {
            BirthdayStrategyMethod = i_MyBirthdayStrategy;
            setListData(i_CurrentUser);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (UserFriend friend in r_FBFriendList)
            {
                yield return friend;
            }
        }

        private void setListData(User i_CurrentUser)
        {
            foreach (User friend in i_CurrentUser.Friends)
            {
                if (friend.Birthday != null)
                {
                    if (BirthdayStrategyMethod.Invoke(
                        friend.Birthday.Substring(3, 2), friend.Birthday.Substring(0, 2)))
                    {
                        r_FBFriendList.Add(
                            new UserFriend
                            {
                                ProfilePhotoURL = friend.PictureNormalURL,
                                ID = friend.Id,
                                LivingPlace = friend.Location.Name,
                                Name = friend.Name,
                                LatestCheckin =
                                (friend.Checkins.Count == 0) ? null : friend.Checkins[0].Place.Name
                            });
                    }
                }
            }
        }

        public UserFriend GetBirthdayFriend()
        {
            Random randomFriendIndex = new Random();
            UserFriend randomFriend = null;
            if(r_FBFriendList.Count > 0)
            {
                randomFriend =
                    r_FBFriendList[randomFriendIndex.Next(r_FBFriendList.Count)];
            }

            return randomFriend;
        }

        internal List<UserFriend> BirthdayFriendList
        { 
            get
            {
                return r_FBFriendList; 
            }
        }
    }
}
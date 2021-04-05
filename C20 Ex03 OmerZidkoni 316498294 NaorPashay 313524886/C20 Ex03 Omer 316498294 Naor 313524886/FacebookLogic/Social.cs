using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class Social
    {
        private readonly List<Checkin> r_LatestChekingsList = new List<Checkin>();
        private readonly List<Album> r_AlbumsList = new List<Album>();
        private readonly List<User> r_FriendList = new List<User>();

        public Social(User i_User)
        {
            fetchSocialData(i_User);
        }

        private void fetchSocialData(User i_User)
        {
            fetchCheckins(i_User);
            fetchFriends(i_User);
            fetchAlbums(i_User);
        }

        private void fetchAlbums(User i_User)
        {
            foreach (Album album in i_User.Albums)
            {
                r_AlbumsList.Add(album);
            }
        }

        private void fetchFriends(User i_User)
        {
            foreach (User friend in i_User.Friends)
            {
                r_FriendList.Add(friend);
            }
        }

        private void fetchCheckins(User i_User)
        {
            foreach (Checkin checkin in i_User.Checkins)
            {
                r_LatestChekingsList.Add(checkin);
            }
        }

        public List<Checkin> LatestChekingsList
        {
            get
            {
                return r_LatestChekingsList;
            }
        }

        public List<Album> AlbumList
        {
            get
            {
                return r_AlbumsList;
            }
        }

        public List<User> FriendsList
        {
            get
            {
                return r_FriendList;
            }
        }

        public User GetRandomFriend()
        {
            Random randomFriendIndex = new Random();
            User randomFriend = null;
            if (r_FriendList.Count > 0)
            {
                randomFriend =
                    r_FriendList[randomFriendIndex.Next(r_FriendList.Count)];
            }

            return randomFriend;
        }
    }
}

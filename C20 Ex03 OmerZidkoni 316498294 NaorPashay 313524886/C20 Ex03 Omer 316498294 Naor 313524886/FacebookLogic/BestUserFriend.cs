using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class BestUserFriend
    {
        private readonly Dictionary<string, UserFriend> r_FriendDictionary =
            new Dictionary<string, UserFriend>();

        private readonly List<UserFriend> r_BestFriends = new List<UserFriend>();
        private readonly List<string> r_OurPhotos = new List<string>();

        internal List<UserFriend> BestFriendList
        {
            get { return r_BestFriends; }
        }

        internal List<string> OurPhotosList
        {
            get { return r_OurPhotos; }
        }

        internal BestUserFriend(User i_User)
        {
            setBestFriendDictionary(i_User);
        }

        public UserFriend BestCommentFriend { get; set; }

        public UserFriend BestLikesFriend { get; set; }

        internal Dictionary<string, UserFriend> FriendsDictionary
        {
            get
            {
                return r_FriendDictionary;
            }
        }

        private void setBestFriendDictionary(User i_User)
        {
            foreach (Post userPost in i_User.Posts)
            {
                foreach (Comment friendComment in userPost.Comments)
                {
                    if (friendComment.From.Id != i_User.Id)
                    {
                        if (!FriendsDictionary.ContainsKey(friendComment.From.Id))
                        {
                            FriendsDictionary.Add(friendComment.From.Id, new UserFriend());
                            setFriendData(friendComment.From);
                        }

                        FriendsDictionary[friendComment.From.Id].CommentsCount++;
                        if(friendComment.LikedByUser)
                        {
                            FriendsDictionary[friendComment.From.Id].LikesCount++;
                        }
                    }
                }
            }
        }

        private void setFriendData(User i_Friend)
        {
            FriendsDictionary[i_Friend.Id].ID = i_Friend.Id;
            FriendsDictionary[i_Friend.Id].ProfilePhotoURL = i_Friend.PictureNormalURL;
            FriendsDictionary[i_Friend.Id].Name = i_Friend.Name;
        }

        internal void SetFriendWithMostComments()
        {
            int maxComments = 0, currComments;

            foreach (KeyValuePair<string, UserFriend> friend in FriendsDictionary)
            {
                currComments = friend.Value.CommentsCount;
                if(maxComments < currComments)
                {
                    maxComments = currComments;
                    BestCommentFriend = friend.Value;
                }
            }

            r_BestFriends.Add(BestCommentFriend);
        }

        internal void SetFriendWithMostLikes()
        {
            int maxLikes = 0, currLikes;
            foreach (KeyValuePair<string, UserFriend> friend in FriendsDictionary)
            {
                currLikes = friend.Value.CommentsCount;
                if (currLikes > maxLikes)
                {
                    maxLikes = currLikes;
                    BestCommentFriend = friend.Value;
                }
            }

            r_BestFriends.Add(BestLikesFriend);
        }

        internal void SetOurCommonPhotos(User i_LoggedInUser, UserFriend i_BestFriend)
        {
            foreach (Album albums in i_LoggedInUser.Albums)
            {
                foreach (Photo photo in albums.Photos)
                {
                    if (photo.Tags != null)
                    {
                        foreach (PhotoTag taggedUser in photo.Tags)
                        {
                            if (taggedUser.User.Id == i_BestFriend.ID)
                            {
                                r_OurPhotos.Add(photo.PictureNormalURL);
                            }
                        }
                    }
                }
            }
        }
    }
}

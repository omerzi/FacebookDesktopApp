using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class Profile
    {
        private readonly List<Post> r_PostList = new List<Post>();

        internal Profile(User i_User)
        {
            fetchUserPost(i_User);
        }

        public List<Post> PostsList
        {
            get
            {
                return r_PostList;
            }
        }

        private void fetchUserPost(User i_User)
        {
            foreach (Post post in i_User.Posts)
            {
                if (post.Message != null)
                {
                    r_PostList.Add(post);
                }
                else if (post.Caption != null)
                {
                    r_PostList.Add(post);
                }
            }
        }
    }
}

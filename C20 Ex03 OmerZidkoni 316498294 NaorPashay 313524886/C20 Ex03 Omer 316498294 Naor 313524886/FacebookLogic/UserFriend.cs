using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace FacebookLogic
{
    public class UserFriend
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public string ProfilePhotoURL { get; set; }

        public string LivingPlace { get; set; }

        public string DayBorn { get; set; }

        public string LatestCheckin { get; set; }

        public string MonthBorn { get; set; }
    } 
}

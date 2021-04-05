using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class FBFriendData
    {
        private string m_Name;
        private string m_Email;
        private int m_BirthdayDay;
        private int m_BirthdayMonth;
       
        public string Name { get { return m_Name; } set { m_Name = value; } }
        public string Email { get { return m_Email; } set { m_Email = value; } }
        public int BDDay { get { return m_BirthdayDay; } set { m_BirthdayDay = value; } }
        public int BDMonth { get { return m_BirthdayMonth; } set { m_BirthdayMonth = value; } }
    }
}


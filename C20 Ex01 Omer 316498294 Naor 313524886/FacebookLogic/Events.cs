using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class Events
    {
        private readonly List<Event> r_EventsList = new List<Event>();
        private readonly List<string> r_EventDataList = new List<string>();

        public Events(User i_CurrentUser)
        {
            string EventData;

            try
            {
                foreach (Event currentEvent in i_CurrentUser.Events)
                {
                    r_EventsList.Add(currentEvent);
                    EventData = string.Format(
                    @"Name:{0}
                    Date:{1}
                    Location:{2}
                    Link To Facebook:{3}
                    Description:{4}",
                    currentEvent.Name,
                    currentEvent.StartTime,
                    currentEvent.Location,
                    currentEvent.LinkToFacebook,
                    currentEvent.Description);
                    r_EventDataList.Add(EventData);
                }
            }
            catch (Exception)
            {
                throw new Exception("Couldn't Retrieve your Events!");
            }
        }

        public List<Event> EventList
        { 
            get 
            { 
                return r_EventsList;
            } 
        }

        public List<string> EventDataList 
        { 
            get
            {
                return r_EventDataList; 
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace Ex01.FacebookAppLogic
{
    public class StatsCalculationsOnlyMen : FriendsStatistics
    {
        protected override FacebookObjectCollection<User> filterListByChoice(FacebookObjectCollection<User> i_FriendList)
        {
            FacebookObjectCollection<User> returnedList = new FacebookObjectCollection<User>();
            foreach (User friend in i_FriendList)
            {
                if (friend.Gender == User.eGender.male)
                {
                    returnedList.Add(friend);
                }
            }

            return returnedList;
        }
    }
}

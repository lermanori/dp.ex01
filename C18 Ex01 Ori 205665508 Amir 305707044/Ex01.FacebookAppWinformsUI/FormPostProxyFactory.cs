﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex01.FacebookAppLogic;

namespace Ex01.FacebookAppWinformsUI
{
    public static class FormPostProxyFactory
    {

        public static IProxyForm Create(TasksType i_taskToAutomate)
        {
            IProxyForm res = null;
            switch (i_taskToAutomate)
            {
                case TasksType.status:
                    res = FormPostStatusProxy.Create();
                    break;
                case TasksType.photo:
                    res = FormPostPhotoProxy.Create();
                    break;
                case TasksType.link:
                    res = FormPostLinkProxy.Create();
                    break;
            }
            return res;
        }
    }
}
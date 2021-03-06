﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IActorUserControl
    {
        EnumTypes.UserControlTypes GetUserControlType();

        Actor GetActor();

        void Reprint(Object targetPanel);
    }
}

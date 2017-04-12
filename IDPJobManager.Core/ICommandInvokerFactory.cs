﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPJobManager.Core
{
    public interface ICommandInvokerFactory
    {
        TOut Handle<TIn, TOut>(TIn input);
    }
}

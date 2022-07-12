using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.DAL.Managers
{
    public abstract class ManagerBase
    {
        protected readonly TodoContext Context;

        protected ManagerBase(TodoContext context)
        {
            Context = context;
        }
    }
}

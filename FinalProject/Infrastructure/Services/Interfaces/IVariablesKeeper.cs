using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Services.Interfaces
{
    public interface IVariablesKeeper
    {
        void SetVariable(string key, object value);
        object GetVariableByKey(string key);
    }
}

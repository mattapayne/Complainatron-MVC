using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Core.DataAccess
{
    public interface IDbPropertyValues
    {
        TValue GetValue<TValue>(string key);
    }
}

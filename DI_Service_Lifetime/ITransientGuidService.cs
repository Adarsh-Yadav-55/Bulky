using System;
using System.Collections.Generic;
using System.Text;

namespace DI_Service_Lifetime
{
    public interface ITransientGuidService
    {
        string GetGuid();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Computer_.API.Interfaces
{
    public interface IDocumentationMaster
    {
        string GetConfiguration(List<IDocumentation> items);
    }
}

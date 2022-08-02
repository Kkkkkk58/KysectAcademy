using LABA_6__Computer_.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Computer_.API.Models
{
    public class DocumentationMaster : IDocumentationMaster
    {
        public DocumentationMaster() { }
        public string GetConfiguration(List<IDocumentation> items)
        {
            string answer = "";
            foreach (var item in items)
                answer += item.GetConfiguration() + "\r\n";
            return answer;
        }
    }
}

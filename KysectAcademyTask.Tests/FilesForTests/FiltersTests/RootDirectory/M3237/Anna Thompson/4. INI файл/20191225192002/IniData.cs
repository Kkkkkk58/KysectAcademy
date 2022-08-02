using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_LABS.LABA_4.Model
{
    public class IniData
    {
        #region Initialization

        public IniData()
        {
            _sections = new SectionCollection();
        }

        #endregion

        #region Properties

        public PropertyCollection this[string sectionName]
        {
            get
            {
                if (!_sections.ContainsSection(sectionName))
                    return null;

                return _sections[sectionName];
            }
        }

        public SectionCollection Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }

        #endregion

        #region Non-Public Members
        private SectionCollection _sections;
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_LABS.LABA_4.Model
{
    public class Section : ICloneable
    {
        #region Initialization
        public Section(string sectionName)
        {
            SectionName = sectionName;

            if (string.IsNullOrEmpty(sectionName))
                throw new ArgumentException("Section name can not be empty");

            _keyDataCollection = new PropertyCollection();
        }

        public Section(Section x)
        {
            SectionName = x.SectionName;

            _keyDataCollection = new PropertyCollection(x._keyDataCollection);
        }
        #endregion

        #region Properties
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        
        public PropertyCollection Keys
        {
            get { return _keyDataCollection; }
            set { _keyDataCollection = value; }
        }
        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new Section(this);
        }

        #endregion

        #region Non-public members
        private PropertyCollection _keyDataCollection;
        private string _sectionName;
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_LABS.LABA_4.Model
{
    public class IniScheme
    {
        #region Initialization
        public IniScheme()
        {
            CommentString = ";";
            DeviderString = "=";
            SectionStartString = "[";
            SectionEndString = "]";
            ArrayString = "[]";
        }
        #endregion

        #region Properties

        public string SectionStartString
        {
            get { return _sectionStartString; }
            set { _sectionStartString = value; }
        }

        public string SectionEndString
        {
            get { return _sectionEndString; }
            set { _sectionEndString = value; }
        }

        public string CommentString
        {

            get { return _commentString; }
            set { _commentString = value; }
        }

        public string DeviderString
        {
            get { return _deviderString; }
            set { _deviderString = value; }
        }

        public string ArrayString
        {
            get { return _arrayString; }
            set { _arrayString = value; }
        }

        #endregion

        #region Non-public Members
        private string _deviderString = null;
        private string _sectionStartString = null;
        private string _sectionEndString = null;
        private string _commentString = null;
        private string _arrayString = null;
        #endregion
    }
}

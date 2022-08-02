using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OOP_LABS.LABA_4.Model
{
    public class SectionCollection : IEnumerable<Section>
    {
        #region Initialization

        public SectionCollection()
        {
            _sectionData = new Dictionary<string, Section>();
        }

        public SectionCollection(SectionCollection x)
        {
            _sectionData = new Dictionary<string, Section>();
            foreach (var sectionData in x)
            {
                _sectionData.Add(sectionData.SectionName, (Section)sectionData.Clone());
            };
        }

        #endregion

        #region Properties

        public PropertyCollection this[string sectionName]
        {
            get
            {
                if (_sectionData.ContainsKey(sectionName))
                    return _sectionData[sectionName].Keys;

                return null;
            }
        }

        #endregion

        #region Operations

        public bool AddSection(string keyName)
        {
            if (!ContainsSection(keyName))
            {
                _sectionData.Add(keyName, new Section(keyName));
                return true;
            }

            return false;
        }

        public bool ContainsSection(string keyName)
        {
            return _sectionData.ContainsKey(keyName);
        }

        public void Show()
        {
            foreach(var x in _sectionData.Keys)
            {
                Console.WriteLine(x);
            }
        }

        #endregion

        #region IEnumerable<SectionData> Members

        public IEnumerator<Section> GetEnumerator()
        {
            foreach (string sectionName in _sectionData.Keys)
                yield return _sectionData[sectionName];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
      
        #endregion

        #region Non-public Members
        private Dictionary<string, Section> _sectionData;
        #endregion
    }
}

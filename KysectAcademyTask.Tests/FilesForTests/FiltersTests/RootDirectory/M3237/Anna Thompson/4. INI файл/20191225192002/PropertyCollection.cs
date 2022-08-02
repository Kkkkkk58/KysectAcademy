using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OOP_LABS.LABA_4.Model
{
    public class PropertyCollection : IEnumerable<Property>
    {
        #region Initialization
        public PropertyCollection()
        {
            _keyData = new Dictionary<string, Property>();
        }

        public PropertyCollection(PropertyCollection x)
        {
            foreach (Property key in x)
            {
                if (_keyData.ContainsKey(key.Key))
                {
                    _keyData[key.Key] = (Property)key.Clone();
                }
                else
                {
                    _keyData.Add(key.Key, (Property)key.Clone());
                }
            }
        }
        #endregion

        #region Properties

        public Property this[string keyName]
        {
            get
            {
                if (_keyData.ContainsKey(keyName))
                    return _keyData[keyName];

                return null;
            }

            set
            {
                if (!_keyData.ContainsKey(keyName))
                {
                    this.AddKey(keyName);
                }

                _keyData[keyName] = value;

            }
        }

        #endregion

        #region Operations

        public bool AddKey(string key, string value = null)
        {
            if (!ContainsKey(key))
            {
                _keyData.Add(key, new Property(key, value));
                return true;
            }

            return false;
        }

        public bool ContainsKey(string key)
        {
            return _keyData.ContainsKey(key);
        }

        public void Show()
        {
            foreach(var x in _keyData.Keys)
            {
                Console.WriteLine(x);
            }
        }

        #endregion

        #region IEnumerable<KeyData> Members

        public IEnumerator<Property> GetEnumerator()
        {
            foreach (string key in _keyData.Keys)
                yield return _keyData[key];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _keyData.GetEnumerator();
        }

        #endregion

        #region Non-public Members
        private Dictionary<string, Property> _keyData;
        #endregion
    }
}

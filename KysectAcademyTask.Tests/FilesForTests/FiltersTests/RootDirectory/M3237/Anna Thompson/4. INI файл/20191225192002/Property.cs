using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_LABS.LABA_4.Model
{
    public class Property : ICloneable
    {
        #region Initialization

        public Property(string key, string value = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException();

            _value = value;
            _key = key;
        }

        public Property(Property x)
        {
            _value = x._value;
            _key = x._key;
        }

        #endregion

        #region Properties 

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public T GetValue<T>()
        {
            try
            {
                return (T)Convert.ChangeType(_value, typeof(T));
            }
            catch
            {
                throw new Exception("Invalid parameter type");
            }
            return default(T);
        }

        public string Key
        {
            get { return _key; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _key = value;
            }

        }

        #endregion Properties 

        #region ICloneable Members

        public object Clone()
        {
            return new Property(this);
        }

        #endregion

        #region Operations

        public void AddValue(string value)
        {
            Value += $", {value}";
        }

        #endregion

        #region Non-public Members
        private string _value;
        private string _key;
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Core
{
    internal sealed class PropertyMappingInfo
    {
        #region Private Variables

        private string _dataFieldName;
        private object _defaultValue;
        private PropertyInfo _propInfo;

        #endregion

        #region Constructors

        internal PropertyMappingInfo()
            : this(string.Empty, null, null){}

        internal PropertyMappingInfo(string dataFieldName, object defaultValue, PropertyInfo info)
        {
            _dataFieldName = dataFieldName;
            _defaultValue = defaultValue;
            _propInfo = info;
        }

        #endregion

        #region Public Properties

        public string DataFieldName
        {
            get 
            {
                if (string.IsNullOrEmpty(_dataFieldName))
                {
                    _dataFieldName = _propInfo.Name;
                }
                return _dataFieldName;
            }
        }

        public object DefaultValue
        {
            get { return _defaultValue; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return _propInfo; }
        }

        #endregion
    }
}

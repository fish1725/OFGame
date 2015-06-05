#region

using System;
using System.Reflection;

#endregion

namespace Assets.Scripts.Core.UI {
    public class OFUIPropertyAttribute : Attribute {
        #region Constructors

        public OFUIPropertyAttribute(string name, Type uiType) {
            this.name = name;
            this.uiType = uiType;
        }

        #endregion

        #region Properties

        [OFUIProperty("name", typeof (OFUIText))]
        public string name { get; set; }

        public Type uiType { get; set; }

        [OFUIProperty("name", null)]
        public object value { get; set; }

        #endregion

        #region Methods

        public void Save(string propertyName, object propertyValue) {
            PropertyInfo pi = GetType()
                .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (pi != null) {
                object[] attrs = pi.GetCustomAttributes(typeof (OFUIPropertyAttribute), true);
                if (attrs.Length > 0) {
                    OFUIPropertyAttribute property = attrs[0] as OFUIPropertyAttribute;
                    if (property != null) {
                        pi.SetValue(this, propertyValue, null);
                    }
                }
            }
        }

        #endregion
    }
}
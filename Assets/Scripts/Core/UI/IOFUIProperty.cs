#region

using System;

#endregion

namespace Assets.Scripts.Core.UI {
    public interface IOFUIProperty {
        #region Properties

        string propertyName { get; set; }
        Type propertyType { get; set; }
        object propertyValue { get; set; }

        #endregion
    }
}
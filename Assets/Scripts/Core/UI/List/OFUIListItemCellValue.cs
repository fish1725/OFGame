namespace Assets.Scripts.Core.UI.List {
    public class OFUIListItemCellValue : OFUIComponent {
        #region Fields

        protected object _value;

        #endregion

        #region Properties

        public virtual bool canWrite { get; set; }

        public virtual float preferredWidth {
            get { return 10f; }
        }

        public virtual object propertyValue {
            get { return _value; }
            set { _value = value; }
        }

        public object model { get; set; }
        public string propertyName { get; set; }

        #endregion
    }
}
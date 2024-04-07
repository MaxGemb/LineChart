using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartIndicator : BaseObject
    {
        public ChartIndicator(Session session)
            : base(session)
        { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                SetPropertyValue(nameof(id), ref _id, value);
            }
        }
        string _indicator;
        public string indicator
        {
            get
            {
                return _indicator;
            }
            set
            {
                SetPropertyValue(nameof(indicator), ref _indicator, value);
            }
        }
    }
}

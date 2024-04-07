using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartShowLegend : BaseObject
    {
        public ChartShowLegend(Session session)
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
        bool _isNeedValues;
        public bool IsNeedValues
        {
            get
            {
                return _isNeedValues;
            }
            set
            {
                SetPropertyValue(nameof(IsNeedValues), ref _isNeedValues, value);
            }
        }


    }
}

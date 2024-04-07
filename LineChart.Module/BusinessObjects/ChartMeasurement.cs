using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartMeasurement : BaseObject
    {
        public ChartMeasurement(Session session)
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
        string _measurement;
        public string measurement
        {
            get
            {
                return _measurement;
            }
            set
            {
                SetPropertyValue(nameof(measurement), ref _measurement, value);
            }
        }
    }
}

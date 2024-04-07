using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartStrategist : BaseObject
    {
        public ChartStrategist(Session session)
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
        string _strategist;
        public string strategist
        {
            get
            {
                return _strategist;
            }
            set
            {
                SetPropertyValue(nameof(strategist), ref _strategist, value);
            }
        }

 
    }
}

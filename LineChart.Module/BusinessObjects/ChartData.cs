using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartData : BaseObject
    {
        public ChartData(Session session) : base(session)
        { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        string _name;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue(nameof(name), ref _name, value);
            }
        }

        DateTime _date;
        public DateTime date
        {
            get
            {
                return _date;
            }
            set
            {
                SetPropertyValue(nameof(date), ref _date, value);
            }
        }
        double _total;
        public double total
        {
            get
            {
                return _total;
            }
            set
            {
                SetPropertyValue(nameof(total), ref _total, value);
            }
        }
    }
}

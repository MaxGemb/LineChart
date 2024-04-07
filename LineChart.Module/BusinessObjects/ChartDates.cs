using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartDates : BaseObject
    {
        public ChartDates(Session session) : base(session)
        { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        DateTime _dateBegin;
        public DateTime dateBegin
        {
            get
            {
                return _dateBegin;
            }
            set
            {
                SetPropertyValue(nameof(dateBegin), ref _dateBegin, value);
            }
        }
        DateTime _dateEnd;
        public DateTime dateEnd
        {
            get
            {
                return _dateEnd;
            }
            set
            {
                SetPropertyValue(nameof(dateEnd), ref _dateEnd, value);
            }
        }
    }
}

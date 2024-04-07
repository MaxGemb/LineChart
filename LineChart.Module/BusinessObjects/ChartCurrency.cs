using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartCurrency : BaseObject
    {
        public ChartCurrency(Session session)
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
        string _currency;
        public string currency
        {
            get
            {
                return _currency;
            }
            set
            {
                SetPropertyValue(nameof(currency), ref _currency, value);
            }
        }

 
    }
}

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartIndexes : BaseObject
    {
        public ChartIndexes(Session session)
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
        string _index;
        public string index
        {
            get
            {
                return _index;
            }
            set
            {
                SetPropertyValue(nameof(index), ref _index, value);
            }
        }

 
    }
}

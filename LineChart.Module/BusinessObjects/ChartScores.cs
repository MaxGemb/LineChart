using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace LineChart.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChartScores : BaseObject
    {
        public ChartScores(Session session)
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
        string _score;
        public string score
        {
            get
            {
                return _score;
            }
            set
            {
                SetPropertyValue(nameof(score), ref _score, value);
            }
        }

 
    }
}

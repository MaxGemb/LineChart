using DevExpress.ExpressApp;
using LineChart.Module.BusinessObjects;

namespace LineChart.Module.Controllers
{
    public partial class ChartController : ViewController
    {
        public ChartController()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            IObjectSpace objectSpace = View.ObjectSpace;

            var datas = objectSpace.GetObjects(typeof(ChartData));
            if (datas.Count == 0)
            {
                MessageOptions options = new MessageOptions();
                options.Duration = 2000;
                options.Message = string.Format("Для отображения графика необходимо загрузить данные. Перейдите в любой каталог и нажмите \"Загрузить базовые данные\"");
                options.Type = InformationType.Error;
                options.Web.Position = InformationPosition.Right;
                options.Win.Caption = "Success";
                options.Win.Type = WinMessageType.Flyout;
                Application.ShowViewStrategy.ShowMessage(options);
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}

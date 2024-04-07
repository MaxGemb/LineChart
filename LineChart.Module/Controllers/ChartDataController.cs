using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using LineChart.Module.BusinessObjects;
using LineChart.Module.Data;
using System.Xml;

namespace LineChart.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ChartDataController : ObjectViewController<ListView, ChartData>
    {
        public ListView listView { get; set; }
        public SimpleAction importInMainView { get; }
        public ChartDataController()
        {
            importInMainView = new SimpleAction(this, "ImportDataFromXML", PredefinedCategory.RecordEdit);
            importInMainView.TargetViewNesting = Nesting.Root;
            importInMainView.Execute += ImportInMainView_Execute;
            InitializeComponent();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            ChartData chartData = this.ViewCurrentObject;
            ListView listView = this.View;
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void ImportInMainView_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            ImportDataMain();
        }

        public void ImportDataMain()
        {
            if (listView == null)
            {
                listView = this.View;
            }
            IObjectSpace importObjectSpace = null;
            try
            {
                importObjectSpace = Application.CreateObjectSpace(listView.CollectionSource.ObjectTypeInfo.Type);
                using (IObjectSpace nestedImportObjectSpace = importObjectSpace.CreateNestedObjectSpace())
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(XmlContainer.GetXmlString());
                    foreach (XmlNode element in doc.ChildNodes[0].ChildNodes)
                    {
                        ChartData data = nestedImportObjectSpace.CreateObject<ChartData>();
                        data.name = String.Format("{0}", element.ChildNodes[0].InnerText);
                        data.isVisible = true;
                        try
                        {
                            data.date = DateTime.Parse(element.ChildNodes[1].InnerText.ToString());

                        }
                        catch (FormatException)
                        {
                            Tracing.Tracer.LogText($"значение не конвертируется в DateTime: {element.ChildNodes[1].InnerText}");
                        }
                        try
                        {
                            data.total = Convert.ToDouble(element.ChildNodes[2].InnerText.ToString());
                        }
                        catch (FormatException)
                        {
                            Tracing.Tracer.LogText($"значение не конвертируется в double: {element.ChildNodes[2].InnerText}");
                        }


                    };
                    nestedImportObjectSpace.CommitChanges();
                }
                importObjectSpace.CommitChanges();
                listView.ObjectSpace.Refresh();
            }
            catch (Exception commitException)
            {
                try
                {
                    importObjectSpace.Rollback();
                }
                catch (Exception rollBackException)
                {
                    throw new Exception(String.Format("An exception of type {0} was encountered while attempting to roll back the transaction while importing the data of the {1} type.\nError Message:{2}\nStackTrace:{3}", rollBackException.GetType(), View.ObjectTypeInfo.Type, rollBackException.Message, rollBackException.StackTrace), rollBackException);
                }
                throw new UserFriendlyException(String.Format("Importing can't be finished!\nAn exception of type {0} was encountered while importing the data of the {1} type.\nError message = {2}\nStackTrace:{3}\nNo records were imported.", commitException.GetType(), View.ObjectTypeInfo.Type, commitException.Message, commitException.StackTrace));
            }
            finally
            {
                importObjectSpace.Dispose();
                importObjectSpace = null;
            }
        }

    }
}


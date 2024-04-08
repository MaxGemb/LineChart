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
    public partial class SimpleDataController : ViewController<ListView>
    {
        public ListView listView { get; set; }
        public SimpleAction importInMainView { get; }
        public SimpleDataController()
        {
            importInMainView = new SimpleAction(this, "Загрузить базовые данные", PredefinedCategory.RecordEdit);
            importInMainView.TargetViewNesting = Nesting.Root;
            importInMainView.Execute += ImportInMainView_Execute;
            InitializeComponent();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            listView = this.View;
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
                    IObjectSpace objectSpace = View.ObjectSpace;

                    var datas = objectSpace.GetObjects(typeof(ChartData));
                    if (datas.Count == 0)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(XmlContainer.GetXmlString());
                        foreach (XmlNode element in doc.ChildNodes[0].ChildNodes)
                        {
                            ChartData data = nestedImportObjectSpace.CreateObject<ChartData>();
                            data.name = String.Format("{0}", element.ChildNodes[0].InnerText);
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

                    }
                    nestedImportObjectSpace.CommitChanges();
                }
                importObjectSpace.CommitChanges();

                using (IObjectSpace nestedImportObjectSpace = importObjectSpace.CreateNestedObjectSpace())
                {
                    IObjectSpace objectSpace = View.ObjectSpace;
                    var newDatas = new List<ChartData>();
                    var chartDatas = objectSpace.GetObjects(typeof(ChartData));
                    foreach (ChartData data in chartDatas)
                    { newDatas.Add(data); }
                    var datas = objectSpace.GetObjects(typeof(ChartClients));
                    if (datas.Count == 0)
                    {
                        var names = new List<string>();
                        names = newDatas.OrderBy(x => x.name).Select(x => x.name).Distinct().ToList();
                        ChartClients catalog = nestedImportObjectSpace.CreateObject<ChartClients>();
                        int i = 1;
                        catalog.id = 0;
                        catalog.client = "(все)";
                        foreach (var name in names)
                        {
                            catalog = nestedImportObjectSpace.CreateObject<ChartClients>();
                            catalog.id = i++;
                            catalog.client = name;
                        }
                    }
                    datas = objectSpace.GetObjects(typeof(ChartDates));
                    if (datas.Count == 0)
                    {
                        ChartDates catalog = nestedImportObjectSpace.CreateObject<ChartDates>();
                        catalog.dateBegin = newDatas.Min(x=> x.date);
                        catalog.dateEnd = newDatas.Max(x => x.date);
                    }
                    datas = objectSpace.GetObjects(typeof(ChartIndicator));
                    if (datas.Count == 0)
                    {
                        ChartIndicator catalog = nestedImportObjectSpace.CreateObject<ChartIndicator>();
                        catalog.id = 0;
                        catalog.indicator = "Прибыль";
                    }
                    datas = objectSpace.GetObjects(typeof(ChartMeasurement));
                    if (datas.Count == 0)
                    {
                        ChartMeasurement catalog = nestedImportObjectSpace.CreateObject<ChartMeasurement>();
                        catalog.id = 0;
                        catalog.measurement = "Счет";
                    }
                    datas = objectSpace.GetObjects(typeof(ChartScores));
                    if (datas.Count == 0)
                    {
                        ChartScores catalog = nestedImportObjectSpace.CreateObject<ChartScores>();
                        catalog.id = 0;
                        catalog.score = "(все)";
                    }
                    datas = objectSpace.GetObjects(typeof(ChartStrategist));
                    if (datas.Count == 0)
                    {
                        ChartStrategist catalog = nestedImportObjectSpace.CreateObject<ChartStrategist>();
                        catalog.id = 0;
                        catalog.strategist = "(не выбрано)";
                    }
                    datas = objectSpace.GetObjects(typeof(ChartIndexes));
                    if (datas.Count == 0)
                    {
                        ChartIndexes catalog = nestedImportObjectSpace.CreateObject<ChartIndexes>();
                        catalog.id = 0;
                        catalog.index = "IMOEX";
                    }
                    datas = objectSpace.GetObjects(typeof(ChartCurrency));
                    if (datas.Count == 0)
                    {
                        ChartCurrency catalog = nestedImportObjectSpace.CreateObject<ChartCurrency>();
                        catalog.id = 0;
                        catalog.currency = "RUR";
                    }
                    datas = objectSpace.GetObjects(typeof(ChartShowLegend));
                    if (datas.Count == 0)
                    {
                        ChartShowLegend catalog = nestedImportObjectSpace.CreateObject<ChartShowLegend>();
                        catalog.id = 0;
                        catalog.IsNeedValues = true;
                    }
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


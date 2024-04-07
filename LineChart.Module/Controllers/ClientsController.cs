using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using LineChart.Module.BusinessObjects;
using System.Linq;

namespace LineChart.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ClientController : ObjectViewController<ListView, ChartClients>
    {
        public ListView listView { get; set; }
        public SimpleAction ImportDataFromXML { get; }
        public ClientController()
        {
            ImportDataFromXML = new SimpleAction(this, "ImportClientsFromXML", PredefinedCategory.RecordEdit);
            ImportDataFromXML.TargetViewNesting = Nesting.Root;
            ImportDataFromXML.Execute += ImportDataFromXML_Execute;
            InitializeComponent();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            ChartClients chartClients = this.ViewCurrentObject as ChartClients;
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

        private void ImportDataFromXML_Execute(object sender, SimpleActionExecuteEventArgs e)
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
                    var chartDatas = new List<ChartData>();
                    var names = new List<string>();
                    IObjectSpace objectSpace = View.ObjectSpace;
                    var datas = objectSpace.GetObjects(typeof(ChartData));
                    if (datas.Count > 0)
                    {
                        foreach (ChartData data in datas)
                        { chartDatas.Add(data); }
                        names = chartDatas.OrderBy(x => x.name).Select(x => x.name).Distinct().ToList();
                        ChartClients clients = nestedImportObjectSpace.CreateObject<ChartClients>();
                        int i = 1;
                        clients.id = 0;
                        clients.client = "(все)";
                        foreach (var name in names)
                        {
                            clients = nestedImportObjectSpace.CreateObject<ChartClients>();
                            clients.id = i++;
                            clients.client = name;
                        }
                        nestedImportObjectSpace.CommitChanges();

                    }
                    else
                    {
                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = string.Format($"Нет выгруженных данных в таблице \"Данные Диаграммы\".");
                        options.Type = InformationType.Error;
                        options.Web.Position = InformationPosition.Right;
                        options.Win.Caption = "Success";
                        options.Win.Type = WinMessageType.Toast;
                        Application.ShowViewStrategy.ShowMessage(options);
                    }
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


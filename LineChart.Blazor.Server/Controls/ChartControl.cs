using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using LineChart.Blazor.Server.Pages;
using LineChart.Module.BusinessObjects;
using Microsoft.AspNetCore.Components;

namespace LineChart.Blazor.Server.Controls
{

    public class ChartComponentModel : ComponentModelBase
    {
        public IEnumerable<ChartClients> ClientsSource
        {
            get => GetPropertyValue<IEnumerable<ChartClients>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartCurrency> CurrencySource
        {
            get => GetPropertyValue<IEnumerable<ChartCurrency>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartData> DataSource
        {
            get => GetPropertyValue<IEnumerable<ChartData>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartDates> DatesSource
        {
            get => GetPropertyValue<IEnumerable<ChartDates>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartIndexes> IndexesSource
        {
            get => GetPropertyValue<IEnumerable<ChartIndexes>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartIndicator> IndicatorSource
        {
            get => GetPropertyValue<IEnumerable<ChartIndicator>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartMeasurement> MeasurementSource
        {
            get => GetPropertyValue<IEnumerable<ChartMeasurement>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartScores> ScoresSource
        {
            get => GetPropertyValue<IEnumerable<ChartScores>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartShowLegend> ShowLegendSource
        {
            get => GetPropertyValue<IEnumerable<ChartShowLegend>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<ChartStrategist> StrategistSource
        {
            get => GetPropertyValue<IEnumerable<ChartStrategist>>();
            set => SetPropertyValue(value);
        }
    }

    public class ChartControl : IComplexControl, IComponentContentHolder
    {
        private RenderFragment componentContent;
        private ChartComponentModel componentModel = new();
        private IObjectSpace objectSpace;

        public RenderFragment ComponentContent
        {
            get
            {
                componentContent ??= ComponentModelObserver.Create(componentModel, ChartComponent.Create(componentModel));
                return componentContent;
            }
        }

        public void Refresh()
        {
            componentModel.DataSource = objectSpace.GetObjects<ChartData>().AsEnumerable();
            componentModel.ClientsSource = objectSpace.GetObjects<ChartClients>().AsEnumerable();
        }

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            this.objectSpace = objectSpace;
            componentModel.ClientsSource = objectSpace.GetObjects<ChartClients>().AsEnumerable();
            componentModel.CurrencySource = objectSpace.GetObjects<ChartCurrency>().AsEnumerable();
            componentModel.DataSource = objectSpace.GetObjects<ChartData>().AsEnumerable();
            componentModel.DatesSource = objectSpace.GetObjects<ChartDates>().AsEnumerable();
            componentModel.IndexesSource = objectSpace.GetObjects<ChartIndexes>().AsEnumerable();
            componentModel.IndicatorSource = objectSpace.GetObjects<ChartIndicator>().AsEnumerable();
            componentModel.MeasurementSource = objectSpace.GetObjects<ChartMeasurement>().AsEnumerable();
            componentModel.ScoresSource = objectSpace.GetObjects<ChartScores>().AsEnumerable();
            componentModel.ShowLegendSource = objectSpace.GetObjects<ChartShowLegend>().AsEnumerable();
            componentModel.StrategistSource = objectSpace.GetObjects<ChartStrategist>().AsEnumerable();
        }
    }
}

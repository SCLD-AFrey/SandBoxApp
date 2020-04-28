using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpo;
using SandBox.Data;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using DevExpress.Map;
using SandBoxApp.Models;
using DevExpress.Xpf.Map;

namespace SandBoxApp.ViewModels
{
    [MetadataType(typeof(MetaData))]
    public class FeedViewModel
    {
        public class MetaData : IMetadataProvider<FeedViewModel>
        {
            void IMetadataProvider<FeedViewModel>.BuildMetadata
                (MetadataBuilder<FeedViewModel> p_builder)
            {

                p_builder.Property(p_x => p_x.SelectedArea)
                    .OnPropertyChangedCall(p_x => p_x.OnSelectedAreaChanged());
                p_builder.Property(p_x => p_x.SelectedSubArea)
                    .OnPropertyChangedCall(p_x => p_x.OnSelectedSubAreaChanged());
                p_builder.CommandFromMethod(p_x => p_x.OnRefreshData())
                    .CommandName("RefreshData");
            }
        }

        #region Constructors

        protected FeedViewModel()
        {
            session = new UnitOfWork();
            client = new HttpClient();
            GetWorldDataAsync();
            CenterPoint = new GeoPoint(45,18);
            ZoomLevel = 3;

        }

        public static FeedViewModel Create()
        {
            return ViewModelSource.Create(() => new FeedViewModel());
        }

        #endregion

        #region Fields and Properties

        public virtual UnitOfWork session { get; set; }
        public virtual HttpClient client { get; set; }
        public virtual List<Area> AllAreas { get; set; }
        public virtual Area SelectedArea { get; set; }
        public virtual Area SelectedSubArea { get; set; }
        public virtual bool ShowLoading { get; set; } = true;
        public virtual CoordPoint CenterPoint { get; set; }
        public virtual int ZoomLevel { get; set; }
        public virtual string SelectedDisplayName { get; set; }

        public virtual XPCollection<cDataPull> DataPullCollection { get; set; }

        #endregion

        #region Methods

        public void OnRefreshData()
        {
            GetWorldDataAsync();
        }

        public async Task GetWorldDataAsync()
        {
            ShowLoading = true;
            AllAreas = null;
            HttpResponseMessage response = await client.GetAsync("https://bing.com/covid/data");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<Area>(responseBody);
                AllAreas = obj.areas;
                ShowLoading = false;

            }


        }

        public void OnSelectedAreaChanged()
        {
            if (SelectedArea != null)
            {
                CenterPoint = new GeoPoint(SelectedArea.lat, SelectedArea.@long);
                ZoomLevel = 5;
                SelectedDisplayName = SelectedArea.displayName + " - " + SelectedArea.totalConfirmed.ToString();
            }
        }
        public void OnSelectedSubAreaChanged()
        {
            if (SelectedSubArea != null)
            {
                CenterPoint = new GeoPoint(SelectedSubArea.lat, SelectedSubArea.@long);
                ZoomLevel = 7;
                SelectedDisplayName = SelectedSubArea.displayName + " - " + SelectedSubArea.totalConfirmed.ToString();
            }
        }


        #endregion
    }




}

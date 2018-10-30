namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiSerice;
        #endregion
        #region Attributes
        private ObservableCollection<Land> lands;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        #endregion

        #region Constructors
        public LandsViewModel()
        {
            this.apiSerice = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Methods
        private async void LoadLands()
        {
            // Consumir servicio
            var response = await this.apiSerice.GetList<Land>(
                "https://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            //Ya tenemos la lista de paises

            // creamos el grid de lista
            var List = (List<Land>)response.Result;

            this.Lands = new ObservableCollection<Land>(List);
        }
        #endregion
    }
}

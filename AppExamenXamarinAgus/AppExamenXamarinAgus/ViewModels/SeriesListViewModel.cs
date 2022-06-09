using AppCrudXamarin.Base;
using AppExamenXamarinAgus.Models;
using AppExamenXamarinAgus.Services;
using AppExamenXamarinAgus.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppExamenXamarinAgus.ViewModels
{
    public class SeriesListViewModel : ViewModelBase
    {
        private ServiceApiSeries service;

        public SeriesListViewModel(ServiceApiSeries service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadSeriesAsync();
            });
        }

        private async Task LoadSeriesAsync()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            this.Series = new ObservableCollection<Serie>(series);
        }

        private ObservableCollection<Serie> _Series;

        public ObservableCollection<Serie> Series
        {
            get { return this._Series; }
            set
            {
                this._Series = value;
                OnPropertyChanged("Series");
            }
        }

        public Command VerSerie
        {
            get
            {
                return new Command(async (serie) =>
                {
                    Serie s = serie as Serie;
                    SerieView view = new SerieView();
                    SerieViewModel viewmodel = App.ServiceLocator.SerieViewModel;
                    viewmodel.Serie = s;
                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}

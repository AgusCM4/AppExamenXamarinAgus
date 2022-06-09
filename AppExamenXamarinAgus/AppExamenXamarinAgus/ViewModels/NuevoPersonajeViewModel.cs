using AppCrudXamarin.Base;
using AppExamenXamarinAgus.Models;
using AppExamenXamarinAgus.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppExamenXamarinAgus.ViewModels
{
    public class NuevoPersonajeViewModel:ViewModelBase
    {
        private ServiceApiSeries service;

        public NuevoPersonajeViewModel(ServiceApiSeries service)
        {
            this.service = service;  
            this.Personaje = new Personaje();
            this.SerieSeleccionada = new Serie();
            Task.Run(async () =>
            {
                await this.LoadSeriesAsync();
            });
        }

        private async Task LoadSeriesAsync()
        {
            this.Series = await this.service.GetSeriesAsync();
        }

        private List<Serie> _Series;

        public List<Serie> Series
        {
            get { return this._Series; }
            set {
                this._Series = value;
                OnPropertyChanged("Series");
            }
        }

        private Serie _SerieSeleccionada;

        public Serie SerieSeleccionada
        {
            get { return this._SerieSeleccionada; }
            set
            {
                this._SerieSeleccionada = value;
                OnPropertyChanged("SerieSeleccionada");
            }
        }

        private Personaje _Personaje;

        public Personaje Personaje
        {
            get { return this._Personaje; }
            set
            {
                this._Personaje = value;
                OnPropertyChanged("Personaje");
            }
        }



        public Command InsertarPersonaje
        {
            get
            {
                return new Command(async () =>
                {
                    await this.service.InsertPersonajeAsync(this.Personaje.Nombre, this.SerieSeleccionada.IdSerie);
                    await Application.Current.MainPage.DisplayAlert("Alert", "Insertado", "OK");
                });
            }
        }
    }
}

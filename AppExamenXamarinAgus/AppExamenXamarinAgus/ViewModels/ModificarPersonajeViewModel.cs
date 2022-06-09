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
    public class ModificarPersonajeViewModel:ViewModelBase
    {
        private ServiceApiSeries service;

        public ModificarPersonajeViewModel(ServiceApiSeries service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadSeriesPersonajesAsync();
            });
        }

        private async Task LoadSeriesPersonajesAsync()
        {
            this.Series = await this.service.GetSeriesAsync();
            this.Personajes = await this.service.GetPersonajesAsync();
        }

        private List<Serie> _Series;

        public List<Serie> Series
        {
            get { return this._Series; }
            set
            {
                this._Series = value;
                OnPropertyChanged("Series");
            }
        }

        private List<Personaje> _Personajes;

        public List<Personaje> Personajes
        {
            get { return this._Personajes; }
            set
            {
                this._Personajes = value;
                OnPropertyChanged("Personajes");
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

        private Personaje _PersonajeSeleccionado;

        public Personaje PersonajeSeleccionado
        {
            get { return this._PersonajeSeleccionado; }
            set
            {
                this._PersonajeSeleccionado = value;
                OnPropertyChanged("PersonajeSeleccionado");
            }
        }



        public Command ModificarPersonaje
        {
            get
            {
                return new Command(async () =>
                {
                    await this.service.ModificarPersonajeAsync(this.PersonajeSeleccionado.IdPersonaje, this.SerieSeleccionada.IdSerie);
                    await Application.Current.MainPage.DisplayAlert("Alert", "Modificado", "OK");
                });
            }
        }
    }
}

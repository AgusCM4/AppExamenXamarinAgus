using AppCrudXamarin.Base;
using AppExamenXamarinAgus.Models;
using AppExamenXamarinAgus.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppExamenXamarinAgus.ViewModels
{
    public class SerieViewModel: ViewModelBase
    {
        private ServiceApiSeries service;

        public SerieViewModel(ServiceApiSeries service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadPersonajesAsync();
            });
        }

        private async Task LoadPersonajesAsync()
        {
            List<Personaje> personajes = await this.service.GetPersonajesSeriesAsync(this.Serie.IdSerie);            
            this.Personajes = new ObservableCollection<Personaje>(personajes);
        }

        private Serie _Serie;

        public Serie Serie
        {
            get { return this._Serie; }
            set
            {
                this._Serie = value;
                OnPropertyChanged("Serie");
            }
        }

        private ObservableCollection<Personaje> _Personajes;

        public ObservableCollection<Personaje> Personajes
        {
            get { return this._Personajes; }
            set
            {
                this._Personajes = value;
                OnPropertyChanged("Personajes");
            }
        }


    }
}

using AppExamenXamarinAgus.ViewModels;
using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AppExamenXamarinAgus.Services
{
    public class ServiceIoC
    {
        private IContainer container;

        public ServiceIoC()
        {
            this.RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ServiceApiSeries>();
            //VIEWMODELS
            builder.RegisterType<SeriesListViewModel>();
            builder.RegisterType<SerieViewModel>();
            builder.RegisterType<NuevoPersonajeViewModel>();
            builder.RegisterType<ModificarPersonajeViewModel>();
            string resourceName = "AppExamenXamarinAgus.appsettings.json";
            Stream stream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream(resourceName);
            IConfiguration configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Register<IConfiguration>(z => configuration);
            this.container = builder.Build();
        }

        public SeriesListViewModel SeriesListViewModel
        {
            get
            {
                return this.container.Resolve<SeriesListViewModel>();
            }
        }

        public NuevoPersonajeViewModel NuevoPersonajeViewModel
        {
            get
            {
                return this.container.Resolve<NuevoPersonajeViewModel>();
            }
        }

        public ModificarPersonajeViewModel ModificarPersonajeViewModel
        {
            get
            {
                return this.container.Resolve<ModificarPersonajeViewModel>();
            }
        }

        public SerieViewModel SerieViewModel
        {
            get
            {
                return this.container.Resolve<SerieViewModel>();
            }
        }
    }
}

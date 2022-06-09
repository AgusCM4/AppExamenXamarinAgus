using AppCrudXamarin.Code;
using AppExamenXamarinAgus.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppExamenXamarinAgus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuSeries : MasterDetailPage
    {
        public MainMenuSeries()
        {
            InitializeComponent();
            ObservableCollection<MenuPageItem> menu = new ObservableCollection<MenuPageItem>
            {
                new MenuPageItem
                {
                    Titulo="Nuevo Personaje",
                    Icono="favorito.png",
                    TypePage=typeof(NuevoPersonajeView)
                },
                new MenuPageItem
                {
                    Titulo="Modificar Serie",
                    Icono="favorito.png",
                    TypePage=typeof(ModificarPersonajeView)
                },
                new MenuPageItem
                {
                    Titulo="Series",
                    Icono="favorito.png",
                    TypePage=typeof(SeriesListView)
                }
            };
            this.lsvMenu.ItemsSource = menu;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)));
            this.lsvMenu.ItemSelected += LsvMenu_ItemSelected;
        }

        private void LsvMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuPageItem item = e.SelectedItem as MenuPageItem;
            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TypePage));
            IsPresented = false;
        }
    }
}
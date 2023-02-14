using SQLite;
using AppPoolMaui.Models;
namespace AppPoolMaui;

public partial class RegistrosPage : ContentPage
{
	public RegistrosPage()
	{
		InitializeComponent();
        List<Registro> registros = App.RegistroRepo.SeleccionarMesas();
        listaRegistros.ItemsSource = registros;
    }

    private void RefreshView_Refreshing(object sender, EventArgs e)
    {
        List<Registro> registros = App.RegistroRepo.SeleccionarMesas();
        listaRegistros.ItemsSource = registros;
        refreshingView.IsRefreshing = false;
        
    }
}
using AppPoolMaui.Models;
using SQLite;
using CommunityToolkit;
namespace AppPoolMaui;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
        List<Mesa> mesas = App.MesaRepo.SeleccionarMesas();
    }
	
	private async void OnCrearClicked(object sender, EventArgs e)
	{
		await App.MesaRepo.AddNewMesa(pickerNroMesa.Items[pickerNroMesa.SelectedIndex]);
		label1.Text = App.MesaRepo.StatusMessage;
		
		
	}
	private async void OnVerClicked(object sender, EventArgs e)
	{
		List<Mesa> mesas = await App.MesaRepo.GetAllMesas();
		mesasList.ItemsSource = mesas;
		foreach(var mesa in mesas)
		{
			mesa.Imagen = $"poolball{mesa.Numero}.png";
		}
	}

}



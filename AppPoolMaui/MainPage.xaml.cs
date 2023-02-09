using AppPoolMaui.Models;
using SQLite;
namespace AppPoolMaui;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}
	
	private async void OnCrearClicked(object sender, EventArgs e)
	{
		await App.MesaRepo.AddNewMesa(nmesa.Text);
		label1.Text = App.MesaRepo.StatusMessage;
		
		
	}
	private async void OnVerClicked(object sender, EventArgs e)
	{
		List<Mesa> mesas = await App.MesaRepo.GetAllMesas();
		mesasList.ItemsSource = mesas;
	}

}



using AppPoolMaui.Models;
using SQLite;
using CommunityToolkit;
using CommunityToolkit.Maui.Views;

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
        await ListarMesas();


    }
	private async void OnVerClicked(object sender, EventArgs e)
	{
        await ListarMesas();
    }

	private async void OnDeleteClicked (object sender, EventArgs e)
	{
        List<Mesa> mesas = App.MesaRepo.SeleccionarMesas();
        string numeros = string.Empty;
        foreach (var me in mesas)
        {
            numeros = numeros.Insert(0,$"{me.Numero} - ");
        }
        string result = await DisplayPromptAsync("Mesa", "¿Cual Mesa deseas finalizar?", placeholder: $"Usadas: {numeros}" ,keyboard: Keyboard.Numeric);
        //TODO: Metodo que calcule el precio final por mesa
		await App.MesaRepo.DeleteMesa(result);
		await App.OrdenRepo.DeletOrden(result);
		await ListarMesas();
    }

	private async Task<int> ListarMesas()
	{
        List<Mesa> mesas = await App.MesaRepo.GetAllMesas();
        mesasList.ItemsSource = mesas;
        foreach (var mesa in mesas)
        {
            if (string.IsNullOrEmpty(mesa.Imagen))
            {
                mesa.Imagen = $"poolball{mesa.Numero}.png";
            }

        }
		return 0;
    }

    private void pickerNroMesa_Focused(object sender, FocusEventArgs e)
    {
        CrearBtn.IsEnabled= true;
    }
}



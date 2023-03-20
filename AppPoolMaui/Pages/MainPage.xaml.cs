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
		await App.MesaRepo.AddNewMesa(pickerNroMesa.Text);
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
        double totalorden = 0;
        try
        {
            totalorden = App.OrdenRepo.valorTotalOrden(result);
        }
        catch (Exception)
        {

            totalorden = 0;
            
        }
        double preciotiempo = 0;

        try
        {
        preciotiempo = App.MesaRepo.valorTotalTiempo(result);
        await App.RegistroRepo.AddNewRegistro(result, totalorden + preciotiempo, DateTime.Now);
        await App.MesaRepo.DeleteMesa(result);
        }
        catch
        {
            label1.Text = "No eliminado";

        }

        try
        {
            await App.OrdenRepo.DeletOrden(result);
        }
        catch (Exception asd)
        {

           await DisplayAlert("Error", asd.Message, "cancel");
        }
		
        
        if (preciotiempo != 0){
            if ((preciotiempo / 75 < 58))
            {
                await DisplayAlert("TOTAL CONSUMOS",
                $"La mesa {result} tiene: \n * En productos: {totalorden}$ \n * Tiempo de mesa: {preciotiempo / 75} minutos usados",
                "Ok");
            }
            else
            {
                await DisplayAlert("TOTAL GASTOS",
            $"Los gastos de la mesa {result} fueron: \n * En productos: {totalorden}$ \n * En tiempo: {preciotiempo}$ \nTotal = {totalorden + preciotiempo}$",
            "Ok");

            }

            await ListarMesas();
        }
        else
        {
            label1.Text = "Not eliminated";
        }

        
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
        CrearCustomBtn.IsEnabled= true;
    }

    private async void testBtn_Clicked(object sender, EventArgs e)
    {
        List<Mesa> mesas = App.MesaRepo.SeleccionarMesas();
        string numeros = string.Empty;
        foreach (var me in mesas)
        {
            numeros = numeros.Insert(0, $"{me.Numero} - ");
        }
        string result = await DisplayPromptAsync("Mesa", "¿Cual Mesa deseas evaluar?", placeholder: $"Usadas: {numeros}", keyboard: Keyboard.Numeric);
        //TODO: Metodo que calcule el precio final por mesa
        double totalorden = 0;
        try
        {
            totalorden = App.OrdenRepo.valorTotalOrden(result);
        }
        catch (Exception)
        {

            totalorden = 0;

        }

        double preciotiempo = App.MesaRepo.valorTotalTiempo(result);

        if ((preciotiempo / 75 < 58))
        {
            await DisplayAlert("TOTAL CONSUMOS",
            $"La mesa {result} tiene: \n * En productos: {totalorden}$ \n * Tiempo de mesa: {preciotiempo / 75} minutos usados",
            "Ok");
        }
        else
        {
            await DisplayAlert("TOTAL GASTOS",
        $"Los gastos de la mesa {result} fueron: \n * En productos: {totalorden}$ \n * En tiempo: {preciotiempo}$ (Tiempo: {preciotiempo/75})\nTotal = {totalorden + preciotiempo}$",
        "Ok");

        }

        await ListarMesas();

    }
    private async void OnCrearCustomClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Hora", $"Hora Formato:\n DD/MM/AAAA HH:mm:ss AM (o PM)"
            , initialValue:$"{DateTime.Now}");
        await App.MesaRepo.AddNewMesaCustom(pickerNroMesa.Text, result);
        label1.Text = App.MesaRepo.StatusMessage;
        await ListarMesas();
    }
}



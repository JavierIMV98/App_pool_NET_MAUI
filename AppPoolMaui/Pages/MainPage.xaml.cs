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
            if ((preciotiempo / App.MesaRepo.valoresTotalesMesa(result).pminuto < 58))
            {
                await DisplayAlert("TOTAL CONSUMOS",
                $"La mesa {result} tiene: \n * En productos: {totalorden}$ \n * Tiempo de mesa: {preciotiempo / App.MesaRepo.valoresTotalesMesa(result).pminuto} minutos usados",
                "Ok");
            }
            else
            {
                await DisplayAlert("TOTAL GASTOS",
            $"Los gastos de la mesa {result} fueron: \n * En productos: {totalorden}$ \n * En tiempo: {preciotiempo}$ (Tiempo: {preciotiempo / App.MesaRepo.valoresTotalesMesa(result).pminuto} minutos) \nTotal = {totalorden + preciotiempo}$",
            "Ok");

            }
            await App.MesaRepo.DeleteMesa(result);
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
        try
        {
            double preciotiempo = App.MesaRepo.valorTotalTiempo(result);
            if ((preciotiempo / App.MesaRepo.valoresTotalesMesa(result).pminuto < 58))
            {
                await DisplayAlert("TOTAL CONSUMOS",
                $"La mesa {result} tiene: \n * En productos: {totalorden}$ \n * Tiempo de mesa: {preciotiempo / App.MesaRepo.valoresTotalesMesa(result).pminuto} minutos usados",
                "Ok");
            }
            else
            {
                await DisplayAlert("TOTAL GASTOS",
            $"Los gastos de la mesa {result} fueron: \n * En productos: {totalorden}$ \n * En tiempo: {preciotiempo}$ (Tiempo: {preciotiempo / App.MesaRepo.valoresTotalesMesa(result).pminuto} minutos)\nTotal = {totalorden + preciotiempo}$",
            "Ok");

            }
        }
        catch (Exception)
        { label1.Text = "No evaluado"; }
       



        await ListarMesas();

    }
    private async void OnCrearCustomClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Hora", $"Hora Formato:\n DD/MM/AAAA HH:mm:ss AM (o PM)"
            , initialValue:$"{DateTime.Now}");
        double precio = 75;
        switch (pickerprecio.SelectedIndex)
        {
            case (-1): precio = 75;
                break;
            case (0):
                precio = 75;
                break;
            case (1):
                precio = 80;
                break;
            case (2):
                precio = 85;
                break;
            case (3):
                precio = 90;
                break;
            case (4):
                precio = 92;
                break;
            case (5):
                precio = 100;
                break;
            case (6):
                precio = 110;
                break;
            case (7):
                precio = 113;
                break;
            case (8):
                precio = 124;
                break;
            case (9):
                precio = 135;
                break;
        }
        await App.MesaRepo.AddNewMesaCustom(pickerNroMesa.Text, result, precio);
        label1.Text = App.MesaRepo.StatusMessage;
        await ListarMesas();
    }
}



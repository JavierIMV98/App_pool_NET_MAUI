using SQLite;
using AppPoolMaui.Models;
using AppPoolMaui.Repos;
using System.Windows.Input;
using System.Dynamic;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.Xaml;

namespace AppPoolMaui;

public partial class RegistrosPage : ContentPage
{
    private ICommand deleteCommand;

    public ObservableCollection<Registro> DatosCollection { get; private set; }

    //ObservableCollection<Registro> test;
    public ICommand DeleteCommand { get => deleteCommand; private set => deleteCommand = value; }

    public RegistrosPage()
    {
        InitializeComponent();
        DatosCollection = new ObservableCollection<Registro>();
        DeleteCommand = new Command<Registro>(OnDeleteCommand);
        List<Registro> registros = App.RegistroRepo.SeleccionarMesas();
        foreach (var registro in registros)
        {
            DatosCollection.Add(registro);
        }
    }
    private void OnDeleteCommand(Registro o)
    {
        Task.Run(async () => await App.RegistroRepo.DeleteRegistro(o.Id));
        DatosCollection.Remove(o);
        DisplayAlert("Title", "Messaje xd", "cancel");
    }



    private void RefreshView_Refreshing(object sender, EventArgs e)
    {
        List<Registro> registros = App.RegistroRepo.SeleccionarMesas();
        DatosCollection = new ObservableCollection<Registro>();
        foreach (var registro in registros)
        {
            DatosCollection.Add(registro);
        }
        listaRegistros.ItemsSource = DatosCollection;
        refreshingView.IsRefreshing = false;

    }
    private async void DeleteRegistros(object sender, EventArgs e)
    {
        await App.RegistroRepo.DeleteRegistros();
    }
    //pendiente
    private async void DeleteRegistro(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Eliminar", "¿Que registro desea eliminar?", "Ok", "Cancelar", placeholder: "Id: ", keyboard: Keyboard.Numeric);
        try
        {
            await App.RegistroRepo.DeleteRegistro(Double.Parse(result));
        }
        catch (Exception)
        {

            await DisplayAlert("Error","No se realizó", "Ok");
        }
        
    }

}
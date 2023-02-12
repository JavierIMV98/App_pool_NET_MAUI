using AppPoolMaui.Models;
using SQLite;
using System.Collections.Generic;

namespace AppPoolMaui;

public partial class OrdensPage : ContentPage
{
    public OrdensPage()
    {

        InitializeComponent();

        List<Mesa> mesas = App.MesaRepo.SeleccionarMesas();
        pickerMesa.ItemsSource = mesas;

        List<Producto> productos = App.ProductoRepo.SeleccionarProductos();
        pickerProducto.ItemsSource = productos;

    }
    private async void OnAgrOrden(object sender, EventArgs e)
    {
        await App.OrdenRepo.AddNewOrden(pickerMesa.Items[pickerMesa.SelectedIndex],
            pickerProducto.Items[pickerProducto.SelectedIndex], int.Parse(pickerCantidad.Items[pickerCantidad.SelectedIndex]));
        labelOrdenes.Text = App.OrdenRepo.StatusMessage;
    }
    private async void OnVerOrden(object sender, EventArgs e)
    {
        List<Orden> ordenes = await App.OrdenRepo.GetAllOrdenes();
        ordenesList.ItemsSource = ordenes;
        foreach(var orden in ordenes)
        {
            orden.Imagen = $"poolball{orden.NroMesa}";
        }
    }


}
using AppPoolMaui.Models;
using System.Collections.Generic;

namespace AppPoolMaui;

public partial class ProductsPage : ContentPage
{
	public ProductsPage()
	{
		InitializeComponent();
	}
	private async void OnAgrProducto(object sender , EventArgs e)
	{
        await App.ProductoRepo.AddNewProducto(editNombreProducto.Text, double.Parse(editPrecioProducto.Text));
        labelProductos.Text = App.ProductoRepo.StatusMessage;
    }

    private async void OnVerProducto(object sender, EventArgs e)
    {
        List <Producto> productos = await App.ProductoRepo.GetAllProductos();
        productosList.ItemsSource = productos;
        labelProductos.Text = "Desliza!";
    }
}
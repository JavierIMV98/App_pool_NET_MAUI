namespace AppPoolMaui;

public partial class OrdensPage : ContentPage
{
	public OrdensPage()
	{
		InitializeComponent();
	}
    private void OnAgrOrden(object sender, EventArgs e)
    {
        labelOrdenes.Text = "Prueba 1";
    }
    private void OnVerOrden(object sender, EventArgs e)
    {
        labelOrdenes.Text = "Prueba 2";
    }
}
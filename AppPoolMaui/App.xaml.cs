
namespace AppPoolMaui;

public partial class App : Application
{
    public static MesaRepository MesaRepo { get; private set; }
    public static ProductoRepository ProductoRepo { get; private set; }
    public App(MesaRepository repo, ProductoRepository prepo)
	{
		InitializeComponent();

		MainPage = new AppShell();
		MesaRepo= repo;
		ProductoRepo = prepo;
	}
}

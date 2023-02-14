
using AppPoolMaui.Repos;

namespace AppPoolMaui;

public partial class App : Application
{
    public static MesaRepository MesaRepo { get; private set; }
    public static ProductoRepository ProductoRepo { get; private set; }
	public static OrdenRepository OrdenRepo { get; private set; } 
	public static RegistroRepository RegistroRepo { get; private set; }
    public App(MesaRepository repo, ProductoRepository prepo, OrdenRepository orepo, RegistroRepository rrepo)
	{
		InitializeComponent();

		MainPage = new AppShell();
		MesaRepo= repo;
		ProductoRepo = prepo;
		OrdenRepo = orepo;
		RegistroRepo = rrepo;
	}
}

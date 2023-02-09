
namespace AppPoolMaui;

public partial class App : Application
{
    public static MesaRepository MesaRepo { get; private set; }
    public App(MesaRepository repo)
	{
		InitializeComponent();

		MainPage = new AppShell();
		MesaRepo= repo;
	}
}

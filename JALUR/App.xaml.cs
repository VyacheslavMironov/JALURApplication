using JALUR.Pages;

namespace JALUR;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new PointPage();
	}
}

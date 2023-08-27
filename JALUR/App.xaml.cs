using JALUR.Pages;
using System.Runtime.Caching;

namespace JALUR;

public partial class App : Application
{
    MemoryCache cache;

    public App()
	{
		InitializeComponent();

        cache = MemoryCache.Default;

        if (cache.Get("BearerToken") == null)
        {
            MainPage = new PointPage();
        }
        else
        {
            MainPage = new MainPage();
        }
	}
}

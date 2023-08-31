namespace JALUR.Pages;

using System.Runtime.Caching;

public partial class ExtraMenuPage : ContentPage
{
    MemoryCache cache;
    public ExtraMenuPage()
	{
		InitializeComponent();
        cache = MemoryCache.Default;
    }

    public async void menu_Click__Schedule(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new SchedulePage());
    }
    public async void menu_Click__Main(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MainPage());
    }
    public async void menu_Click__CoucherList(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CoucherListPage());
    }
    private async void menu_Click__ExtraMenu(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ExtraMenuPage());
    }
    
    private async void menu_Click__Profile(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ProfilePage());
    }

    public async void Click__LogoutUser(object sender, EventArgs e)
    {
        cache.Remove("Id");
        cache.Remove("FirstName");
        cache.Remove("LastName");
        cache.Remove("Phone");
        cache.Remove("Role");
        cache.Remove("AccessKey");
        await Navigation.PushModalAsync(new PointPage());
    }
}
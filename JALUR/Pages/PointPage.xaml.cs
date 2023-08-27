namespace JALUR.Pages;

using JALUR.Pages;

public partial class PointPage : ContentPage
{
    public PointPage()
	{
		InitializeComponent();
    }

	public async void OpenUserCreatePage_Click(object sender, EventArgs e)
	{
		await Navigation.PushModalAsync(new UserCreatePage());
	}

    public async void OpenAuthPage_Click(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AuthPage());
    }
}
using JALUR.Entity.Pages;
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace JALUR.Pages;

public partial class ShowCoucherPage : ContentPage
{
	public int Id;
    public ConfigEditor config = new ConfigEditor();
    ObservableCollection<ShowCouchEntity> ShowCouch;
    public ShowCoucherPage(int Id)
	{
		this.Id = Id;
        ShowCouch = new ObservableCollection<ShowCouchEntity>();
        InitializeComponent();
        BindingContext = this;
        GetData();
    }

    public async void GetData()
    {
        HttpClient client = new HttpClient();
        ShowCouchEntity response = await client.GetFromJsonAsync<ShowCouchEntity>($"{config.ServerHost}/api/user/show/{Id}");

        FlexLayout flexContent = new FlexLayout
        {
            new StackLayout
            {
                new Image
                {
                    Source = config.ServerHost + "/storage/" + response.Image,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 180
                }
            },
            new StackLayout
            {
                new Label
                {
                    Text = response.FirstName + " " + response.LastName,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.End,
                    TextColor=Color.FromHex("#977585"),
                    FontSize=18,
                    FontAttributes = FontAttributes.Bold,
                },
                new Label
                {
                    Text = "Возраст " + response.Age.ToString(),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    TextColor=Color.FromHex("#977585"),
                    FontSize=14,
                    FontAttributes = FontAttributes.Bold,
                }
            }
        };

        flexContent.Wrap = FlexWrap.Wrap;
        flexContent.JustifyContent = FlexJustify.SpaceAround;

        StackLayout content = new StackLayout
        {
            flexContent,
            new StackLayout
            {
                new Label
                {
                    Text = response.Description,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                    TextColor=Color.FromHex("#977585"),
                    Margin = new Thickness(0, 15, 0, 0),
                    FontSize=14
                }
            }
        };

        xPage.Add(content);
    }

    public async void menu_Click__Schedule(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new SchedulePage());
    }
    public async void ClosePage_Click(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
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
}
using JALUR.Entity.Pages;
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace JALUR.Pages;

public partial class CoucherListPage : ContentPage
{
    public ConfigEditor config = new ConfigEditor();
    ObservableCollection<CoucherListEntity> CoucherList;
    public CoucherListPage()
	{
        CoucherList = new ObservableCollection<CoucherListEntity>();
        InitializeComponent();
        BindingContext = this;
        GetData();
    }

    public async void GetData()
    {
        HttpClient client = new HttpClient();
        var response = await client.GetFromJsonAsync<List<CoucherListEntity>>($"{config.ServerHost}/api/user/show/role/all/Тренер");

        foreach (var item in response)
        {
            Frame frame = new Frame
            {
                BackgroundColor = Color.FromHex("#D9D9D9"),
                Margin = new Thickness(0, 0, 0, 15),
                CornerRadius = 10,
                Padding = new Thickness(8)
            };

            Button btn = new Button
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, -40, 0, 0),
                BackgroundColor = Color.FromHex("#75646C"),
                TextColor = Color.FromHex("#f6f6f6"),
                WidthRequest = 150,
                Text = "Подробнее",
                ZIndex = 99999,
            };

            FlexLayout flexContent = new FlexLayout
                {
                new Image
                {
                    Source = config.ServerHost + "/storage/" + item.Image,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest=80,
                    Margin = new Thickness(0, 0, 0, 0)
                },
                new StackLayout
                {
                    new Label
                    {
                        Text = item.FirstName + " " + item.LastName,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, -40, 0, 10),
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("#75646C")
                    }
                },
            };

            btn.Clicked += (sender, args) => {
                Navigation.PushModalAsync(new ShowCoucherPage(item.Id));
            };

            flexContent.Wrap = FlexWrap.Wrap;
            flexContent.JustifyContent = FlexJustify.SpaceAround;
            flexContent.AlignContent = FlexAlignContent.Center;

            frame.Content = new StackLayout
            {
                flexContent,
                btn
            };
            xCouchers.Add(frame);
        }
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
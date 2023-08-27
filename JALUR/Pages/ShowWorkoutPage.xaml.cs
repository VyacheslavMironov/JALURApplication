using JALUR.Entity.Pages;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace JALUR.Pages;

public partial class ShowWorkoutPage : ContentPage
{
	public int Id;
    public ConfigEditor config = new ConfigEditor();
    ObservableCollection<ShowWorkoutEntity> ShowWorkout;
    public Task<ShowWorkoutEntity> ShowData;

    public ShowWorkoutPage(int Id)
	{
        ShowWorkout = new ObservableCollection<ShowWorkoutEntity>();

        this.Id = Id;
        InitializeComponent();
        BuildPage();
        BindingContext = this;
    }



    public async void ClosePage_Click(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    public async void BuildPage()
    {
        HttpClient client = new HttpClient();
        ShowWorkoutEntity response = await client.GetFromJsonAsync<ShowWorkoutEntity>($"{config.ServerHost}/api/workouts/show/{Id}");


        StackLayout content =
            new StackLayout
            {
                Children =
                {
                    new Image
                    {
                        Source = config.ServerHost + "/storage/" + response.Image,
                        Margin = new Thickness(0, 0, 0, 10)
                    },
                    new Label
                    {
                        Text = response.Name,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        TextColor=Color.FromHex("#977585"),
                        FontSize=18,
                        FontAttributes = FontAttributes.Bold,
                    },
                    new Label
                    {
                        Text = "Тип типа",
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        TextColor=Color.FromHex("#977585"),
                        FontSize=14,
                    },
                    new Label
                    {
                        Text = "Описание",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor=Color.FromHex("#977585"),
                        FontSize=18,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(0, 15, 0, 15),
                    },
                    new Label
                    {
                        Text = response.Description,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        TextColor=Color.FromHex("#977585"),
                        FontSize=14,
                        Margin = new Thickness(0, 0, 0, 15),
                    }
                }
            
            };
        xPage.Add(content);
    }

    public async void menu_Click__Main(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MainPage());
    }
}
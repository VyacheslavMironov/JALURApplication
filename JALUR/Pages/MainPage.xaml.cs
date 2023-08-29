using JALUR.Entity.Pages;
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace JALUR.Pages;

public partial class MainPage : ContentPage
{
    public ConfigEditor config = new ConfigEditor();
    ObservableCollection<WorkoutEntity> Workouts;

    public MainPage()
	{
        Workouts = new ObservableCollection<WorkoutEntity>();
        InitializeComponent();
        
        BindingContext = this;
        buildWorkoutList();
    }



    public async void buildWorkoutList()
    {
        HttpClient client = new HttpClient();
        var response = await client.GetFromJsonAsync<List<WorkoutEntity>>($"{config.ServerHost}/api/workouts/all");
        Workouts.Clear();


        foreach (var item in response)
        {
            var responseType = await client.GetFromJsonAsync<TypeWorkoutEntity>($"{config.ServerHost}/api/type/workouts/show/{item.TypeWorkoutId}");

            Workouts.Add(
                new WorkoutEntity(
                    item.Id,
                    item.TypeWorkoutId,
                    item.Name,
                    item.Description,
                    item.Image
                )
            );
            
            Frame frame = new Frame
            {
                BackgroundColor = Color.FromHex("#E4DBE0"),
                Margin = new Thickness(0, 0, 0, 15),
                CornerRadius = 10,
                Padding = new Thickness(0, 0, 0, 2)
            };
            Button buttonLinkShow = new Button
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 10, 0, 0),
                BackgroundColor = Color.FromHex("#75646C"),
                TextColor = Color.FromHex("#f6f6f6"),
                WidthRequest = 150,
                Text = "Подробнее"
            };

            frame.Content =
                new StackLayout
                {
                    Children =
                    {
                        new Image
                        {
                            Source = config.ServerHost + "/storage/" + item.Image,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Margin = new Thickness(0, 0, 0, 0)
                        },
                        new StackLayout
                        {
                            Padding = new Thickness(10),
                            Children =
                            {
                                new Label
                                {
                                    Text = item.Name,
                                    HorizontalOptions = LayoutOptions.Start,
                                    VerticalOptions = LayoutOptions.Start,
                                    Margin = new Thickness(0, 0, 0, 0),
                                    FontSize = 16,
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.FromHex("#75646C")
                                },
                                new StackLayout
                                {
                                    Margin = new Thickness(0, 25, 0, 0),
                                    Children =
                                    {
                                        new FlexLayout
                                        {
                                            Wrap=FlexWrap.Wrap,
                                            JustifyContent=FlexJustify.SpaceBetween,
                                            Children=
                                            {
                                                new Label
                                                {
                                                    Text = responseType.Name,
                                                    HorizontalOptions = LayoutOptions.Start,
                                                    VerticalOptions = LayoutOptions.Start,
                                                    Margin = new Thickness(0, 10, 0, 0),
                                                    FontSize = 14,
                                                    TextColor = Color.FromHex("#75646C")
                                                },
                                                buttonLinkShow
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

            buttonLinkShow.Clicked += (sender, e) => {
                Navigation.PushModalAsync(new ShowWorkoutPage(item.Id));
            };
            dataList.Add( frame );
        }
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

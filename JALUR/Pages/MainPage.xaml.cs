using JALUR.Entity.Pages;
using Microsoft.Maui.Graphics.Text;
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace JALUR.Pages;

public partial class MainPage : ContentPage
{
    public ConfigEditor config = new ConfigEditor();
    public TypeWorkout typeWorkout = new TypeWorkout();
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

            frame.Content =
                new StackLayout
                {
                    Children =
                    {
                        new Image
                        {
                            Source=config.ServerHost + "/storage/" + item.Image,
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
                                                    Text = "Тип тренировки",
                                                    HorizontalOptions = LayoutOptions.Start,
                                                    VerticalOptions = LayoutOptions.Start,
                                                    Margin = new Thickness(0, 10, 0, 0),
                                                    FontSize = 14,
                                                    TextColor = Color.FromHex("#75646C")
                                                },
                                                new Button
                                                {
                                                    HorizontalOptions = LayoutOptions.End,
                                                    VerticalOptions = LayoutOptions.Start,
                                                    Margin = new Thickness(0, 10, 0, 0),
                                                    BackgroundColor = Color.FromHex("#75646C"),
                                                    TextColor = Color.FromHex("#f6f6f6"),
                                                    WidthRequest = 150,
                                                    Text="Подробнее"
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            
            dataList.Add( frame );
        }
    }
}
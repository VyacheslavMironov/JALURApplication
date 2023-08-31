using Microsoft.Maui.Layouts;

namespace JALUR.Pages;

public partial class SchedulePage : ContentPage
{

	public SchedulePage()
	{
		InitializeComponent();

        Frame calendarFrame = new Frame
        {
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            Padding = 6,
            Margin = new Thickness(0, 0, 0, 0),
            CornerRadius = 0,
            BackgroundColor = Color.FromHex("#75646C")
        };

        StackLayout itemList = new StackLayout();

        string Month = "";

        switch (DateTime.Now.Month)
        {
            case 1:
                Month = "Январь";
                break;
            case 2:
                Month = "Февраль";
                break;
            case 3:
                Month = "Март";
                break;
            case 4:
                Month = "Апрель";
                break;
            case 5:
                Month = "Май";
                break;
            case 6:
                Month = "Июнь";
                break;
            case 7:
                Month = "Июль";
                break;
            case 8:
                Month = "Август";
                break;
            case 9:
                Month = "Сентябрь";
                break;
            case 10:
                Month = "Октябрь";
                break;
            case 11:
                Month = "Ноябрь";
                break;
            case 12:
                Month = "Декабрь";
                break;
        }

        itemList.Add(
            new Label
            {
                Text = Month + DateTime.Now.Year.ToString(),
                TextColor = Color.FromHex("#f6f6f6"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin= new Thickness(0, 14, 0, 8),
                FontAttributes = FontAttributes.Bold,
                FontSize = 15
            }
        );

        FlexLayout dayFlex = new FlexLayout
        {
            new StackLayout
            {
                new Label
                {
                    Text = "Пн",
                    TextColor = Color.FromHex("#f6f6f6"),
                    WidthRequest=24,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14
                }
            },
            new StackLayout
            {
                new Label
                {
                    Text = "Вт",
                    TextColor = Color.FromHex("#f6f6f6"),
                    WidthRequest=24,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14
                }
            },
            new StackLayout
            {
                new Label
                {
                    Text = "Ср",
                    TextColor = Color.FromHex("#f6f6f6"),
                    WidthRequest=24,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14
                }
            },
            new StackLayout
            {
                new Label
                {
                    Text = "Чт",
                    TextColor = Color.FromHex("#f6f6f6"),
                    WidthRequest=24,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14
                }
            },
            new StackLayout
            {
                new Label
                {
                    Text = "Пт",
                    TextColor = Color.FromHex("#f6f6f6"),
                    WidthRequest=24,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14
                }
            },
            new StackLayout
            {
                new Label
                {
                    Text = "Сб",
                    TextColor = Color.FromHex("#f6f6f6"),
                    WidthRequest=24,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14
                }
            },
            new StackLayout
            {
                new Label
                {
                    Text = "Вс",
                    TextColor = Color.FromHex("#f6f6f6"),
                    WidthRequest=24,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14
                }
            }
        };

        dayFlex.Wrap = FlexWrap.Wrap;
        dayFlex.JustifyContent = FlexJustify.SpaceBetween;

        itemList.Add(
            dayFlex
        );

        for (int row = 0; row < 6; row++)
        {
            FlexLayout rowCalendar = new FlexLayout();
            for (int col = 0; col < 7; col++)
            {
                int dayOfMonth = CalendarGenerate()[row, col];
                if (dayOfMonth > 0)
                {
                    StackLayout dayLabel = new StackLayout
                    {
                        new Label
                        {
                            Text = dayOfMonth.ToString().PadLeft(2),
                            TextColor = DateTime.Now.Day.ToString() == dayOfMonth.ToString().PadLeft(2) ? Color.FromHex("#DD3737") : Color.FromHex("#f6f6f6"),
                            WidthRequest=24,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            FontSize = 14
                        }
                    };
                    rowCalendar.Add(dayLabel);
                }
                else
                {
                    rowCalendar.Add(
                        new StackLayout
                        {
                            new Label
                            {
                                Text = "",
                                TextColor = Color.FromHex("#f6f6f6"),
                                WidthRequest=24,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center,
                                FontSize = 14
                            }
                        }
                    );
                }
            }

            rowCalendar.Margin = 5;
            rowCalendar.Padding = 6;
            rowCalendar.Wrap = FlexWrap.Wrap;
            rowCalendar.JustifyContent = FlexJustify.SpaceBetween;

            itemList.Add(rowCalendar);
        }

        calendarFrame.Content = itemList;

        xCalendar.Add(calendarFrame);

    }

    public int[,] CalendarGenerate()
    {
        DateTime currentDate = DateTime.Now;

        // Кол-во дней в текущем месяце
        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

        // День недели первого дня месяца
        DayOfWeek firstDayOfWeek = new DateTime(currentDate.Year, currentDate.Month, 1).DayOfWeek;

        int[,] calendarMatrix = new int[6, 7];
        int day = 1;
        for (int row = 0; row < 6; row++)
        {
            for (int col = (int)firstDayOfWeek - 1; col < 7 && day <= daysInMonth; col++)
            {
                calendarMatrix[row, col] = day;
                day++;
            }
            firstDayOfWeek = DayOfWeek.Monday;
        }
        return calendarMatrix;
    }

    public string GetMonth(DateTime date)
    {
        string month = "";
        switch (date.Month)
        {
            case 1:
                month += "Январь";
                break;
            case 2:
                month += "Февраль";
                break;
            case 3:
                month += "Март";
                break;
            case 4:
                month += "Апрель";
                break;
            case 5:
                month += "Май";
                break;
            case 6:
                month += "Июнь";
                break;
            case 7:
                month += "Июль";
                break;
            case 8:
                month += "Август";
                break;
            case 9:
                month += "Сентябрь";
                break;
            case 10:
                month += "Октябрь";
                break;
            case 11:
                month += "Ноябрь";
                break;
            case 12:
                month += "Декабрь";
                break;
        }
        return month;
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
}
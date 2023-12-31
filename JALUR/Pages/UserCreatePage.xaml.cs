using System.Text;
using Newtonsoft.Json;

namespace JALUR.Pages;

public partial class UserCreatePage : ContentPage
{
    ConfigEditor config;
    public UserCreatePage()
	{
		InitializeComponent();
        config = new ConfigEditor();
        Phone.TextChanged += (s, e) =>
        {
            if (e.NewTextValue.Length >= Phone.Mask.Length)
            {
                Phone.CursorPosition = Phone.Mask.Length;
            }
            else
            {
                Phone.CursorPosition = e.NewTextValue.Length;
            }
        };
    }

    public async void ClosePage_Click(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    public async void CreateUser_Click(object sender, EventArgs e)
    {
        if (FirstName.Text.Length > 0 && LastName.Text.Length > 0 && Weight.Text.Length > 0 &&
            Height.Text.Length > 0 && Age.Text.Length > 0 && Phone.Text.Length > 0)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{config.ServerHost}/api/user/create");
            string json = JsonConvert.SerializeObject(new
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Weight = Weight.Text,
                Height = Height.Text,
                Age = Age.Text,
                Gender = "�������",
                Phone = Phone.Text,
                Role = "������"
            });
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("�������!", "�� ������� ������������������, ������ �� ������ ����� � ������ �������.", "��");
            }
            else
            {
                await DisplayAlert("��������!", "����� ������������ ��� ����������!", "��");
            }
        }
    }   
}
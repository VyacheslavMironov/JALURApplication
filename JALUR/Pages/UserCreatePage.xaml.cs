using System.Text;
using Newtonsoft.Json;

namespace JALUR.Pages;

public partial class UserCreatePage : ContentPage
{
	public UserCreatePage()
	{
		InitializeComponent();
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://89.108.77.131:5000/api/user/create");
            string json = JsonConvert.SerializeObject(new
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Weight = Weight.Text,
                Height = Height.Text,
                Age = Age.Text,
                Gender = "Женщина",
                Phone = Phone.Text,
                Role = "Клиент"
            });
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Отлично!", "Вы успешно зарегистрировались, теперь вы можете войти в личный кабинет.", "Ок");
            }
            else
            {
                await DisplayAlert("Внимание!", "Такой пользователь уже существует!", "Ок");
            }
        }
    }   
}
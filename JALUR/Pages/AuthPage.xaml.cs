using System.Text;
using Android.Hardware.Lights;
using Newtonsoft.Json;

namespace JALUR.Pages;

public partial class AuthPage : ContentPage
{
	public AuthPage()
	{
		InitializeComponent();
	}

	public async void ClosePage_Click(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}

    public async void SetCodeUser_Click(object sender, EventArgs e)
    {
        if (Phone.Text.Length > 0)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://89.108.77.131:5000/api/user/code");
            string json = JsonConvert.SerializeObject(new
            {
                Phone = Phone.Text
            });
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                SetUserCode.IsVisible = false;
                SetUserAuth.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Внимание!", "Такой пользователь не найден!", "Ок");
            }
        }
    }

    public async void AuthUser_Click(object sender, EventArgs e)
    {
        if (Phone.Text.Length > 0 && Password.Text.Length > 0)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://89.108.77.131:5000/api/user/auth");
            string json = JsonConvert.SerializeObject(new
            {
                Phone = Phone.Text,
                Password = Password.Text
            });
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                // Сохранить в файл Bearer <key>
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Внимание!", "Такой пользователь не найден!", "Ок");
            }
        }
    }
}
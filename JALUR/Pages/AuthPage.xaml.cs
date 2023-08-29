using System.Text;
using System.Runtime.Caching;
using Newtonsoft.Json;
using JALUR.Entity.Pages;

namespace JALUR.Pages;

public partial class AuthPage : ContentPage
{
    MemoryCache cache;
    ConfigEditor config;
    AuthEntity Auth;

    public AuthPage()
	{
		InitializeComponent();
        cache = MemoryCache.Default;
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

    public async void SetCodeUser_Click(object sender, EventArgs e)
    {
        if (Phone.Text.Length > 0)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{config.ServerHost}/api/user/code");
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{config.ServerHost}/api/user/auth");
            string json = JsonConvert.SerializeObject(new
            {
                Phone = Phone.Text,
                Password = Password.Text
            });
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            string jsonContext = await response.Content.ReadAsStringAsync();
            var userContext = JsonConvert.DeserializeObject<AuthEntity>(jsonContext);
            if (response.IsSuccessStatusCode)
            {
                // Сохранить Информацию о клиенте
                cache.Add("Id", userContext.Id, DateTimeOffset.Now.AddDays(365));
                cache.Add("FirstName", userContext.FirstName, DateTimeOffset.Now.AddDays(365));
                cache.Add("LastName", userContext.LastName, DateTimeOffset.Now.AddDays(365));
                cache.Add("Phone", userContext.Phone, DateTimeOffset.Now.AddDays(365));
                cache.Add("Role", userContext.Role, DateTimeOffset.Now.AddDays(365));
                // Сохранить Bearer <key>
                cache.Add("AccessKey", userContext.AccessKey, DateTimeOffset.Now.AddDays(365));
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Внимание!", "Такой пользователь не найден!", "Ок");
            }
        }
    }
}
namespace JALUR.Pages;

using System.Text;
using Newtonsoft.Json;
using System.Runtime.Caching;
using JALUR.Entity.Response;
using System.Net.Http.Headers;

public partial class ProfilePage : ContentPage
{
    MemoryCache cache;
    ConfigEditor config;
    public ProfilePage()
	{
		InitializeComponent();
        cache = MemoryCache.Default;
        config = new ConfigEditor();

        Phone.Text = (string)cache.Get("Phone");


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
    
    public async void Close_Send_DataFormPhone(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Phone.Text) || Phone.Text.Length > 0)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{config.ServerHost}/api/user/update");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cache.Get("AccessKey").ToString());
            string json = JsonConvert.SerializeObject(new
            {
                Id = cache.Get("Id"),
                Phone = Phone.Text
            });
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            string jsonContext = await response.Content.ReadAsStringAsync();
            var userContext = JsonConvert.DeserializeObject<UpdatePhoneResponseEntity>(jsonContext);
            if (response.IsSuccessStatusCode && userContext.Phone != null)
            {
                // Сохранить Информацию о клиенте
                cache.Add("Phone", userContext.Phone, DateTimeOffset.Now.AddDays(365));

                await DisplayAlert("Успех!", "Номер не успешно обновлён!", "Ок");
            }
            else
            {
                await DisplayAlert("Внимание!", "Номер не указан, укажите свой номер и повторите запрос!", "Ок");
            }
        }
        else
        {
            await DisplayAlert("Внимание!", "Номер не указан, укажите свой номер и повторите запрос!", "Ок");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class AdminAPIController1 : Controller
    {
        public async Task<IActionResult> IndexAdmin()
        {
            string apiUrl = "https://mocki.io/v1/d33691f7-1eb5-45aa9642-8d538f6c5ebd";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

                }


            }
            return View();
        }

    }
}

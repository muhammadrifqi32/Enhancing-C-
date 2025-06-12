using FridayAssignments.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AccountsDTO input)
        {
            var client = _clientFactory.CreateClient();
            var apiUrl = "https://localhost:7079/api/Accounts/login";

            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        success = false,
                        message = JsonDocument.Parse(responseContent).RootElement.GetProperty("message").GetString()
                    });
                }

                // Set session (jika login berhasil)
                var jsonDoc = JsonDocument.Parse(responseContent);
                var email = jsonDoc.RootElement.GetProperty("data").GetProperty("email").GetString();
                var fullName = jsonDoc.RootElement.GetProperty("data").GetProperty("fullName").GetString();

                // Simpan ke session
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("FullName", fullName);

                return Json(new
                {
                    success = true,
                    message = "Login berhasil"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Terjadi kesalahan: " + ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Accounts");
        }
    }
}

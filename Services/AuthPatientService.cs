using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDNetCore.DTOs.Patient;
using DDDSample1.Domain.Shared;
using System.Net;
using System.IO;
using System.Net.Http.Json;

using Microsoft.Extensions.Configuration;

namespace TodoApi.Services
{
    public class AuthServicePatient
    {
        private readonly HttpClient _httpClient;
        private readonly PatientService _patientService;
        private readonly string _googleClientId;
        private readonly string _googleClientSecret;
        private readonly string _redirectUri;

        public AuthServicePatient(HttpClient httpClient, PatientService patientService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _patientService = patientService;

            // Retrieve values from configuration
            _googleClientId = configuration["Authentication:Google:ClientId"];
            _googleClientSecret = configuration["Authentication:Google:ClientSecret"];
            _redirectUri = configuration["Authentication:Google:RedirectUri"];
        }

        public async Task<string?> AuthenticateUser()
        {
            // Construct Google OAuth 2.0 URL
            var authorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                                   $"client_id={_googleClientId}" +
                                   $"&redirect_uri={_redirectUri}" +
                                   $"&response_type=code" +
                                   $"&scope=email profile openid" +
                                   $"&access_type=offline" +
                                   $"&prompt=consent";

            Console.WriteLine($"Redirecting to Google for authentication: {authorizationUrl}");

            // Wait for the callback and get the authorization code
            string? code = await WaitForCodeAsync();
            if (string.IsNullOrEmpty(code)) return null;

            // Exchange the code for an access token
            var tokenUrl = "https://oauth2.googleapis.com/token";
            var tokenPayload = new
            {
                client_id = _googleClientId,
                client_secret = _googleClientSecret,
                code,
                redirect_uri = _redirectUri,
                grant_type = "authorization_code"
            };

            var response = await _httpClient.PostAsJsonAsync(tokenUrl, tokenPayload);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonDocument.Parse(result).RootElement;
                return tokenResponse.GetProperty("id_token").GetString();
            }

            Console.WriteLine($"Failed to authenticate user: {response.StatusCode}");
            return null;
        }

        private async Task<string> WaitForCodeAsync()
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add($"{_redirectUri}/");
                listener.Start();
                Console.WriteLine("Waiting for Google authentication...");

                var context = await listener.GetContextAsync();
                var code = context.Request.QueryString["code"];

                using (var writer = new StreamWriter(context.Response.OutputStream))
                {
                    context.Response.StatusCode = 200;
                    writer.WriteLine("Authentication successful. You can close this window.");
                }

                return code;
            }
        }
    }
}

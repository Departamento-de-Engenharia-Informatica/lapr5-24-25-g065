using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DDDNetCore.Services
{
    public class AuthServicePatient
    {
        private readonly HttpClient _httpClient;
        private readonly string _googleClientId;
        private readonly string _googleClientSecret;
        private readonly string _redirectUri;

        public AuthServicePatient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            var googleKeys = configuration.GetSection("GoogleKeys");
            _googleClientId = googleKeys["ClientId"];
            _googleClientSecret = googleKeys["ClientSecret"];
            _redirectUri = googleKeys["RedirectUri"];
        }

        public async Task<string?> AuthenticateUser()
        {
            try
            {
                // Step 1: Construct Google OAuth URL
                var authorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                                       $"client_id={_googleClientId}" +
                                       $"&redirect_uri={_redirectUri}" +
                                       $"&response_type=code" +
                                       $"&scope=email profile openid" +
                                       $"&access_type=offline" +
                                       $"&prompt=consent";

                Console.WriteLine($"Redirecting to Google for authentication: {authorizationUrl}");

                // Step 2: Wait for the callback and get the authorization code
                string? code = await WaitForCodeAsync();
                if (string.IsNullOrEmpty(code)) return null;

                // Step 3: Exchange the code for an access token
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error during authentication: {ex.Message}");
                return null;
            }
        }

        private async Task<string> WaitForCodeAsync()
        {
            using (var listener = new System.Net.HttpListener())
            {
                listener.Prefixes.Add($"{_redirectUri}/");
                listener.Start();
                Console.WriteLine("Waiting for Google authentication...");

                var context = await listener.GetContextAsync();
                var code = context.Request.QueryString["code"];

                using (var writer = new System.IO.StreamWriter(context.Response.OutputStream))
                {
                    context.Response.StatusCode = 200;
                    writer.WriteLine("Authentication successful. You can close this window.");
                }

                return code ?? string.Empty;
            }
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            // Example logic for verifying an email token
            // Replace with your actual validation logic
            if (string.IsNullOrEmpty(token)) return false;

            // Simulating token verification
            return token == "valid-token"; // Replace with actual implementation
        }

        public async Task<string?> GetUserEmailFromTokenAsync(string token)
        {
            try
            {
                // Decode the token and extract the email
                // This example assumes the token is a JWT; adjust for actual implementation
                var payload = await DecodeJwtAsync(token);
                if (payload == null || !payload.TryGetValue("email", out var email))
                {
                    return null;
                }

                return email?.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting email from token: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GenerateEmailVerificationTokenAsync(string email)
        {
            // Example logic to generate a dummy email verification token
            // Replace with your actual token generation logic
            return Guid.NewGuid().ToString(); // Example: Use a GUID as the token
        }

        private async Task<Dictionary<string, object>?> DecodeJwtAsync(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length < 3) return null;

                var payload = parts[1];
                var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error decoding JWT: {ex.Message}");
                return null;
            }
        }
    }
}

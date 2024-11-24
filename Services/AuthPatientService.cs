using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DDDSample1.Domain.Passwords;
using DDDSample1.Domain.Users;
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
            _googleClientId = googleKeys["893040379732-le0ofpg38advhvckken7foj9tggr43u0.apps.googleusercontent.com"];
            _googleClientSecret = googleKeys["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"];
            _redirectUri = googleKeys["https://localhost:5001/auth/callback"];
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

        public async Task<string> GetUserEmailFromTokenAsync(string token)
        {
            try
            {
                var userInfoUrl = "https://www.googleapis.com/oauth2/v3/userinfo";
                var request = new HttpRequestMessage(HttpMethod.Get, userInfoUrl);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var userInfo = await response.Content.ReadFromJsonAsync<JsonElement>();
                    return userInfo.GetProperty("email").GetString() ?? string.Empty;
                }

                Console.WriteLine($"Failed to retrieve user email: {response.StatusCode}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user email: {ex.Message}");
                return string.Empty;
            }
        }

        public async Task<string> GenerateEmailVerificationTokenAsync(string email)
        {
            // Generate a simple mock token for email verification
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Console.WriteLine($"Generated email verification token for {email}: {token}");
            return await Task.FromResult(token);
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            // Example logic for verifying an email token
            // Replace with your actual validation logic
            if (string.IsNullOrEmpty(token)) return false;

            // Simulating token verification
            return token == "valid-token";
        }

        public async Task SendEmailVerificationAsync(string email)
        {
            // Simulate sending email verification
            Console.WriteLine($"Sending email verification to {email}");
            await Task.CompletedTask; // Replace with actual email sending logic
        }

        public async Task<User?> CreateUserFromVerifiedEmailAsync(string token)
        {
            // Simulating user creation after email verification
            if (string.IsNullOrEmpty(token)) return null;

            // Use the static Create method to create a new User instance
            return User.Create("testuser", "test@example.com", Role.Patient, new Password("password"));
        }
    }
}

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Blazored.LocalStorage;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace FSLInvesting.App.Services;

public class AuthenticationService : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ISyncLocalStorageService _localStorageService;

    public AuthenticationService(HttpClient httpClient, ISyncLocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;

        var accessToken = localStorageService.GetItem<string>("accessToken");
        if (accessToken != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        var result = await _httpClient.GetAsync("manage/info");
        if (result.IsSuccessStatusCode)
        {
            var strResult = await result.Content.ReadAsStringAsync();
            var json = JsonNode.Parse(strResult);
            var email = json!["email"]!.ToString();

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email),
                new(ClaimTypes.Email, email)
            };

            // set the principal
            var identity = new ClaimsIdentity(claims, "Token");
            user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        return new AuthenticationState(user);
    }

    public async Task<LoginResult> Login(UserLogin userLogin)
    {
        var json = JsonSerializer.Serialize(userLogin);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _httpClient.PostAsync("/login", content);
        if (result.IsSuccessStatusCode)
        {
            var strResult = await result.Content.ReadAsStringAsync();
            var jsonResult = JsonNode.Parse(strResult);
            var accessToken = jsonResult?["accessToken"]?.ToString();
            var refreshToken = jsonResult?["refreshToken"]?.ToString();

            _localStorageService.SetItem("accessToken", accessToken);
            _localStorageService.SetItem("refreshToken", refreshToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // need to refresh auth state
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return new LoginResult() { Success = true };
        }

        return new LoginResult() { Success = false, Errors = ["Invalid email or password."] };
    }

    public void Logout()
    {
        _localStorageService.RemoveItem("accessToken");
        _localStorageService.RemoveItem("refreshToken");
        _httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
namespace Spotify_Playlist_Sync.common.GetAccessToken;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Spotify_Playlist_Sync.models.TokenResponse;
using Spotify_Playlist_Sync.common.JsonHandler;


public class GetAccessToken{
         
    public static async Task GetTokenAsync(){

        var jsonHandler = new JsonHandler("secrets.json");
        var secret = jsonHandler.JsonToObject();
        
        string clientId = secret.ClientId;
        string secretId = secret.SecretId;
        string accessTokenEndpoint = secret.AccessTokenEndpoint;  
        string redirectUri = "http://localhost:3000";  

        string authorizationUrl = $"https://accounts.spotify.com/authorize?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope=user-read-private%20user-read-email";

        Console.WriteLine("Please visit the following URL to authorize your application:");
        Console.WriteLine(authorizationUrl);

        Console.Write("Enter the authorization code from the URL: ");
        string authorizationCode = Console.ReadLine();

        var client = new HttpClient();

        var tokenRequest = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("code", authorizationCode),
            new KeyValuePair<string, string>("redirect_uri", redirectUri),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", secretId),
        });

        var response = await client.PostAsync(accessTokenEndpoint, tokenRequest);
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);


        var access_token = JsonDocument.Parse(responseBody).RootElement.GetProperty("access_token").GetString();

        Console.WriteLine(access_token);

        string profileEndpoint = "https://api.spotify.com/v1/me";

        client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {access_token}");

        response = await client.GetAsync(profileEndpoint);

        if (response.IsSuccessStatusCode){
            responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        else {
            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }
}
using Spotify_Playlist_Sync.common.GetAccessToken;


var token = await GetAccessToken.GetTokenAsync();
Console.WriteLine(token.access_token);

var client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.access_token}");

var response = await client.GetAsync("https://api.spotify.com/v1/me");
Console.WriteLine(response);
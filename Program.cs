

using Spotify_Playlist_Sync.common.JsonHandler;

var jsonHandler = new JsonHandler("secrets.json");
var secret = jsonHandler.JsonToObject();
Console.WriteLine(secret.ClientId);
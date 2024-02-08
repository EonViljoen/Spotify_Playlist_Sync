using Spotify_Playlist_Sync.common.GetAccessToken;
using static Spotify_Playlist_Sync.common.GetArtistInfo;
using Spotify_Playlist_Sync.models.Artist;


var token = await GetAccessToken.GetTokenAsync();
var id = "1Ffb6ejR6Fe5IamqA5oRUF";

var artist = await GetArtist(token, id);

Console.WriteLine(artist.name);
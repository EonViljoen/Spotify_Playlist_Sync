using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_Playlist_Sync.models.Token;
using Spotify_Playlist_Sync.models.Artist;
using Newtonsoft.Json;


namespace Spotify_Playlist_Sync.common
{
    public class GetArtistInfo
    {
        public static async Task<Artist> GetArtist(Token bearerToken, string Id){

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken.access_token}");
            Console.WriteLine(Id);
            var response = await client.GetAsync($"https://api.spotify.com/v1/artists/{Id}");

            if (response.IsSuccessStatusCode){

                var jsonResponse = await response.Content.ReadAsStringAsync();
                // Console.WriteLine(response);

                return JsonConvert.DeserializeObject<Artist>(jsonResponse);
            }
            else {
                Console.WriteLine($"Failed to get artist. Status code: {response.StatusCode}");
                return null;

            }

            
        }
    }
}
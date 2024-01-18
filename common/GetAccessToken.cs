using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Spotify_Playlist_Sync.models.Token;
using Spotify_Playlist_Sync.common.JsonReader;
﻿using Newtonsoft.Json;

namespace Spotify_Playlist_Sync.common.GetAccessToken
{
    public class GetAccessToken{
            
        public static async Task<Token> GetTokenAsync(){

            //Load json with secrets
            var JsonHandler = new JsonHandler("resources/secrets.json");
            var secret = JsonHandler.JsonToObject();

            //Encode authorization string
            var authString = secret.ClientId + ":" + secret.SecretId;
            var authTextBytes = System.Text.Encoding.UTF8.GetBytes(authString);
            var auth64Encoding = System.Convert.ToBase64String(authTextBytes);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth64Encoding}");
            // client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded"));
            var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            });

            var response = await client.PostAsync(secret.AccessTokenEndpoint, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            return (JsonConvert.DeserializeObject<Token>(responseBody));
        }
    }
}
using Spotify_Playlist_Sync.models.Secret;
using Newtonsoft.Json;
namespace Spotify_Playlist_Sync.common.JsonReader
{
    public class JsonHandler{
    private readonly string _jsonFilePath;

    public JsonHandler(string jsonFilePath){
        _jsonFilePath = jsonFilePath;
    }

    public Secret JsonToObject(){
        using StreamReader reader = new StreamReader(_jsonFilePath);
        var json = reader.ReadToEnd();
        var secret = JsonConvert.DeserializeObject<Secret>(json);

        return secret;
    }
}
}


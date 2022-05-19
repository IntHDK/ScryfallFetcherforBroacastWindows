using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScryfallFetch
{
    public class ParentObject
    {
        [JsonPropertyName("object")]
        public string Object { get; set; } = "";
    }
    public class CardObject : ParentObject
    {
        //Core Card Fields
        [JsonPropertyName("id")]    public string Id { get; set; } = "";
        [JsonPropertyName("lang")]  public string Lang { get; set; } = "";

        //Gameplay Fields
        [JsonPropertyName("card_faces")] public List<CardFace>? Card_faces { get; set; }

        //Print Fields
        [JsonPropertyName("highres_image")] public bool Highres_image { get; set; } = false;
        [JsonPropertyName("image_status")]  public string Image_status { get; set; } = "highres_scan";
        [JsonPropertyName("image_uris")]    public CardImagery? Image_uris { get; set; }
    }
    public class CardImagery
    {
        [JsonPropertyName("png")]           public string Png { get; set; } = "";
        [JsonPropertyName("border_crop")]   public string Border_crop { get; set; } = "";
        [JsonPropertyName("art_crop")]      public string Art_crop { get; set; } = "";
        [JsonPropertyName("large")]         public string Large { get; set; } = "";
        [JsonPropertyName("normal")]        public string Normal { get; set; } = "";
        [JsonPropertyName("small")]         public string Small { get; set; } = "";
    }
    public class CardFace : ParentObject
    {
        [JsonPropertyName("image_uris")] public CardImagery? Image_uris { get; set; }
    }
}

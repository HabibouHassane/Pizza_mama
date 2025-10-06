using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizza_mama.Models
{
    public class Pizza
    {
        [JsonIgnore]
        public int PizzaId { get; set; }
        [Display(Name = "Nom")]
        public string nom { get; set; }
        [Display(Name = "Prix(€)")]
        public float prix { get; set; }
        [Display(Name = "Végétarienne")]
        public bool vegetarienne { get; set; }
        [Display(Name = "Ingrédients")]
        
        [JsonIgnore]
        public string? ingredients { get; set; }

        [NotMapped]
        [JsonPropertyName("ingredients")]
        public string[] listIngredients
        { 
            get
            {
                if((ingredients == null) || (ingredients.Count() == 0))
                {
                    return null;
                }

                return ingredients.Split(",");
            }
        }
    }
}

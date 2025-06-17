using System.ComponentModel.DataAnnotations;

namespace ParkingOnline.UI.Models;

public class VagaModel : BaseModel<int>
{
    [Display(Name = "Localização")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "A localização da vaga é obrigatória.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "A localização da vaga deve ter exatos 3 caracteres.")]
    [RegularExpression(@"^[EI]\d{2}$", ErrorMessage = "A localização da vaga deve começar com 'E' ou 'I' seguido de dois números.")]
    public required string Localizacao { get; set; }

    public bool Ocupada { get; set; }
}
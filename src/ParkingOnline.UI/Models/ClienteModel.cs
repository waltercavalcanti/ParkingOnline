using System.ComponentModel.DataAnnotations;

namespace ParkingOnline.UI.Models;

public class ClienteModel : BaseModel<int>
{
    public string? Nome { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O telefone do cliente é obrigatório.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O telefone do cliente deve ter exatos 11 caracteres.")]
    //[RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = "O telefone deve estar no formato (XX) XXXXX-XXXX.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O telefone deve conter apenas números e ter exatos 11 dígitos.")]
    public required string Telefone { get; set; }

    [Display(Name = "Veículo")]
    public VeiculoModel? Veiculo { get; set; }
}
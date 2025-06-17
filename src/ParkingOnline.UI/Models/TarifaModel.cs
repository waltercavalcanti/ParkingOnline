using System.ComponentModel.DataAnnotations;

namespace ParkingOnline.UI.Models;

public class TarifaModel : BaseModel<int>
{
    [Display(Name = "Valor Inicial")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "O valor inicial da tarifa é obrigatório.")]
    public decimal ValorInicial { get; set; }

    [Display(Name = "Valor por Hora")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "O valor por hora da tarifa é obrigatório.")]
    public decimal ValorPorHora { get; set; }
}
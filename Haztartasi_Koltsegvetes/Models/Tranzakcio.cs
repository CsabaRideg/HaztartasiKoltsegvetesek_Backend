using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haztartasi_Koltsegvetes.Models;

public class Tranzakcio : IValidatableObject
{
    public int Id { get; set; }
    [ForeignKey("KategoriaId")]
    public int KategoriaId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Az összegnek pozitívnak kell lennie.")]
    [DisplayFormat(DataFormatString = "{0:0.############################}", ApplyFormatInEditMode = true)]
    public decimal Osszeg { get; set; }

    public DateTime Datum { get; set; }
    [StringLength(200, ErrorMessage = "A megjegyzés maximum 200 karakter lehet.")]
    public string? Megjegyzes { get; set; }
    
    public Kategoria? Kategoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Datum.Date > DateTime.Today)
        {
            yield return new ValidationResult(
                "A dátum nem lehet jövőbeli.",
                new[] { nameof(Datum) });
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haztartasi_Koltsegvetes.Models;

public class Tranzakcio
{
    public int Id { get; set; }
    [ForeignKey("KategoriaId")]
    public int KategoriaId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Az összegnek pozitívnak kell lennie.")]
    public decimal Osszeg { get; set; }

    public DateTime Datum { get; set; }
    public string Megjegyzes { get; set; } = string.Empty;
    
    public Kategoria Kategoria { get; set; }
}
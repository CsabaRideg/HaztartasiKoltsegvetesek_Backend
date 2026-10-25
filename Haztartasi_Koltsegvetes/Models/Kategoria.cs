using System.ComponentModel.DataAnnotations;

namespace Haztartasi_Koltsegvetes.Models;

public class Kategoria
{
    public int Id { get; set; }
    [Required(ErrorMessage = "A kategória megadása kötelező.")]
    [MaxLength(60, ErrorMessage = "A kategória maximum 60 karakter lehet.")]
    public string Nev { get; set; } = string.Empty;
    public bool Tipus { get; set; } //true: bevétel, false: kiadás
    public ICollection<Tranzakcio> Tranzakcio { get; set; } = new List<Tranzakcio>();
}
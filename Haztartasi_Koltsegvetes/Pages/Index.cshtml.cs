using System.Globalization;
using Haztartasi_Koltsegvetes.Data;
using Haztartasi_Koltsegvetes.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Haztartasi_Koltsegvetes.Pages;

public class IndexModel : PageModel
{
    private readonly HaztartasDbContext _context;

    public IndexModel(HaztartasDbContext context)
    {
        _context = context;
    }

    public decimal Bevetel { get; private set; }
    public decimal Kiadas { get; private set; }
    public decimal Egyenleg { get; private set; }

    public string AktualisHonap { get; private set; } = string.Empty;

    public IList<HaviKategoriaOsszesitoDto> KategoriaOsszesites { get; private set; }
        = new List<HaviKategoriaOsszesitoDto>();

    public async Task OnGetAsync()
    {
        var ma = DateTime.Today;
        var honapKezdete = new DateTime(ma.Year, ma.Month, 1);
        var kovetkezoHonapKezdete = honapKezdete.AddMonths(1);

        AktualisHonap = ma.ToString(
            "yyyy. MMMM",
            CultureInfo.GetCultureInfo("hu-HU"));

        var aktualisHaviTranzakciok = _context.Tranzakcio
            .Where(t => t.Datum >= honapKezdete && t.Datum < kovetkezoHonapKezdete);

        Bevetel = await aktualisHaviTranzakciok
            .Where(t => t.Kategoria != null && t.Kategoria.Tipus)
            .SumAsync(t => (decimal?)t.Osszeg) ?? 0m;

        Kiadas = await aktualisHaviTranzakciok
            .Where(t => t.Kategoria != null && !t.Kategoria.Tipus)
            .SumAsync(t => (decimal?)t.Osszeg) ?? 0m;

        Egyenleg = Bevetel - Kiadas;

        KategoriaOsszesites = await aktualisHaviTranzakciok
            .GroupBy(t => new
            {
                t.KategoriaId,
                KategoriaNev = t.Kategoria!.Nev,
                t.Kategoria.Tipus
            })
            .Select(csoport => new HaviKategoriaOsszesitoDto
            {
                KategoriaNev = csoport.Key.KategoriaNev,
                Tipus = csoport.Key.Tipus,
                Osszeg = csoport.Sum(t => t.Osszeg)
            })
            .OrderBy(osszesites => osszesites.KategoriaNev)
            .ToListAsync();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AsesoriaLegalGlobal.Pages;

public class CerrarSesionModel : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.Clear();
        return Page();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AsesoriaLegalGlobal.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "Ingresá tu nombre de usuario")]
    public string NombreUsuario { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "Ingresá tu contraseña")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = "";

    public string MensajeError { get; set; } = "";

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        string? contrasenaEnCookie = Request.Cookies["usuario_" + NombreUsuario];

        if (contrasenaEnCookie == null)
        {
            ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            return Page();
        }

        // Decodificamos el valor guardado en la cookie
        string contrasenaGuardada = Uri.UnescapeDataString(contrasenaEnCookie);

        if (contrasenaGuardada != Contrasena)
        {
            ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            return Page();
        }

        HttpContext.Session.SetString("usuario", NombreUsuario);

        string? emailEnCookie = Request.Cookies["email_" + NombreUsuario];
        if (emailEnCookie != null)
        {
            HttpContext.Session.SetString("email", Uri.UnescapeDataString(emailEnCookie));
        }

        return RedirectToPage("/MisDocumentos");
    }
}

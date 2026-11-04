using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AsesoriaLegalGlobal.Pages;

public class RegistroModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
    public string Nombre { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres")]
    public string Apellido { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9_\-]+$", ErrorMessage = "El nombre solo puede tener letras, números, guión o guión bajo")]
    public string NombreUsuario { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string Email { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "La edad es obligatoria")]
    [Range(16, 120, ErrorMessage = "Debés tener más de 15 años para registrarte")]
    public int? Edad { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{6,}$",
        ErrorMessage = "La contraseña debe tener al menos 6 caracteres, una mayúscula y un número")]
    public string Contrasena { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "Confirmá la contraseña")]
    [DataType(DataType.Password)]
    [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmarContrasena { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [RegularExpression(@"^\d{2,3}-\d{4}-\d{4}$",
        ErrorMessage = "El teléfono debe tener el formato 011-4567-8900 o 11-4567-8900")]
    public string Telefono { get; set; } = "";

    public string MensajeExito { get; set; } = "";

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var opciones = new CookieOptions
        {
            IsEssential = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.Now.AddDays(7)
        };

        Response.Cookies.Append("usuario_" + NombreUsuario, Uri.EscapeDataString(Contrasena), opciones);
        Response.Cookies.Append("email_" + NombreUsuario, Uri.EscapeDataString(Email), opciones);

        HttpContext.Session.SetString("usuario", NombreUsuario);
        HttpContext.Session.SetString("email", Email);
        HttpContext.Session.SetString("nombreCompleto", Nombre + " " + Apellido);

        return RedirectToPage("/MisDocumentos");
    }
}

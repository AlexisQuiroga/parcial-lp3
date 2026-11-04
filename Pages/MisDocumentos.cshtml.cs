using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AsesoriaLegalGlobal.Pages;

public class MisDocumentosModel : PageModel
{
    private readonly IWebHostEnvironment _env;

    public MisDocumentosModel(IWebHostEnvironment env)
    {
        _env = env;
    }

    [BindProperty]
    [Required(ErrorMessage = "Seleccioná un archivo")]
    public IFormFile? Archivo { get; set; }

    public bool EstaLogueado { get; set; }
    public string NombreUsuario { get; set; } = "";
    public List<string> Archivos { get; set; } = new List<string>();
    public string MensajeSubida { get; set; } = "";
    public bool SubidaExitosa { get; set; }

    public void OnGet()
    {
        CargarDatos();
    }

    public IActionResult OnPost()
    {
        CargarDatos();

        if (!EstaLogueado)
        {
            return RedirectToPage("/Login");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        string extension = Path.GetExtension(Archivo!.FileName).ToLower();
        if (extension != ".pdf" && extension != ".doc" && extension != ".docx")
        {
            ModelState.AddModelError("Archivo", "Solo se permiten archivos PDF, DOC o DOCX");
            return Page();
        }

        string carpetaUsuario = Path.Combine(_env.WebRootPath, "documentos", NombreUsuario);
        if (!Directory.Exists(carpetaUsuario))
        {
            Directory.CreateDirectory(carpetaUsuario);
        }

        string nombreArchivo = Path.GetFileName(Archivo.FileName);
        string rutaCompleta = Path.Combine(carpetaUsuario, nombreArchivo);

        if (System.IO.File.Exists(rutaCompleta))
        {
            ModelState.AddModelError("Archivo", "Ya existe un archivo con ese nombre. Renombralo antes de subir.");
            return Page();
        }

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            Archivo.CopyTo(stream);
        }

        MensajeSubida = "Archivo subido correctamente.";
        SubidaExitosa = true;
        Archivos = ObtenerArchivos();
        Archivo = null;
        ModelState.Clear();

        return Page();
    }

    private void CargarDatos()
    {
        string? usuario = HttpContext.Session.GetString("usuario");
        EstaLogueado = !string.IsNullOrEmpty(usuario);

        if (EstaLogueado)
        {
            NombreUsuario = usuario!;
            Archivos = ObtenerArchivos();
        }
    }

    private List<string> ObtenerArchivos()
    {
        string carpeta = Path.Combine(_env.WebRootPath, "documentos", NombreUsuario);
        if (!Directory.Exists(carpeta))
        {
            return new List<string>();
        }
        var rutas = Directory.GetFiles(carpeta);
        var nombres = new List<string>();
        foreach (var ruta in rutas)
        {
            nombres.Add(Path.GetFileName(ruta));
        }
        return nombres;
    }
}

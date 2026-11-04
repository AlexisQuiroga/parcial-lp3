# Asesoría Legal Global

Aplicación web desarrollada con ASP.NET Core Razor Pages para el parcial de Laboratorio de Programación 3.

## Descripción

Sistema web para una firma de asesoría legal que permite:
- Registro de usuarios con validaciones
- Login y manejo de sesión
- Gestión de documentos por usuario

## Tecnologías

- ASP.NET Core Razor Pages (.NET 10)
- Bootstrap 5
- jQuery Validation
- Cookies y Session de ASP.NET Core

## Funcionalidades

### Master Page (_Layout.cshtml)
- Navbar responsivo con Bootstrap
- Logo de la empresa
- Pie de página con datos de contacto

### Registro de usuarios
- Validaciones: email, edad mayor a 15 años, contraseña con mayúscula y número, confirmación de contraseña, formato de teléfono
- Almacenamiento en cookie (contraseña) y sesión (nombre de usuario)

### Login
- Verificación de credenciales contra cookie guardada
- Inicio de sesión con sesión activa

### Mis Documentos
- Requiere sesión activa
- Subida de archivos PDF, DOC, DOCX
- Lista de documentos subidos por usuario
- Almacenamiento en `wwwroot/documentos/{usuario}/`

## Ejecución

```bash
dotnet run
```

Acceder a `https://localhost:5001`

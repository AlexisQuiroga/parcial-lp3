var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        // Traducción de mensajes de error de binding al español
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "El campo es obligatorio.");
        options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(_ => "El valor ingresado no es válido.");
        options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(_ => "El campo debe ser un número.");
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((v, _) => $"El valor '{v}' no es válido.");
        options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "El campo debe ser un número.");
        options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(v => $"El valor '{v}' no es válido.");
        options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(_ => "El valor ingresado no es válido.");
        options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(_ => "El campo es obligatorio.");
    });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();

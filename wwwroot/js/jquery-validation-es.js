$.extend($.validator.messages, {
    required: "Este campo es obligatorio.",
    email: "Ingresá una dirección de correo electrónico válida.",
    number: "Ingresá un número válido.",
    digits: "Ingresá solo dígitos.",
    equalTo: "Los valores no coinciden.",
    range: $.validator.format("Ingresá un valor entre {0} y {1}."),
    max: $.validator.format("Ingresá un valor menor o igual a {0}."),
    min: $.validator.format("Ingresá un valor mayor o igual a {0}."),
    maxlength: $.validator.format("No ingresés más de {0} caracteres."),
    minlength: $.validator.format("Ingresá al menos {0} caracteres."),
    rangelength: $.validator.format("Ingresá un valor de entre {0} y {1} caracteres."),
    date: "Ingresá una fecha válida.",
    url: "Ingresá una URL válida."
});

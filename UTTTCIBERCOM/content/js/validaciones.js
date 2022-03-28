function validaNumeros(evt) {
    //valida que solo se ingresan números en la caja de texto
    var code = (evt.which) ? evt.which : evt.keyCode;
    if (code == 8) {
        return true;
    } else if (code >= 48 && code <= 57) {
        return true;
    } else {
        return false;
    }
}

function validaLetras(e) {
    //valida quer solo ingreses letras y algunos caracteres especiales
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "áéíóúàèìòùüïabcdefghijklmnñopqrstuvwxyz ";
    especiales = "8-37-39-46";
    tecla_especial = false;
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}

function validaAlfanumericos(e) {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (!regex.test(key)) {
        e.preventDefault();
        return false;
    }
}

function validaUser(e) {
    var regex = new RegExp("^[a-zA-Z0-9!#$%&'*/=?^_+-`{|}~]+$");
    var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (!regex.test(key)) {
        e.preventDefault();
        return false;
    }
}

function error() {
    const Toast = Swal.mixin({
        toast: true, position: 'top-end', showConfirmButton: false, timer: 3000, timerProgressBar: true, didOpen: (toast) => { toast.addEventListener('mouseenter', Swal.stopTimer); toast.addEventListener('mouseleave', Swal.resumeTimer) }
    })

    Toast.fire({
        icon: 'error',
        title: 'El usuario o contraseña no coinciden'
    })
}
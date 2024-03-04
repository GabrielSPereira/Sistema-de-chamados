function AvisoNecessarioSelecionarRegistro(data) {
    if (!data || !data.ID || data.ID <= 0) {
        Swal.fire({
            text: "E necessario selecionar um registro.",
            type: "warning",
        });

        return true;
    }

    return false;
}
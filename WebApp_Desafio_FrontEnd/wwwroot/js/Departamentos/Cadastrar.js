$(document).ready(function () {
    $('#form').validate({
        rules: {
            Descricao: {
                required: true,
                maxlength: 100,
            }
        },
        messages: {
            Descricao: {
                required: "Por favor, preencha este campo.",
                maxlength: "O campo Assunto deve ter no máximo 100 caracteres.",
            }
        }
    });

    $('#btnCancelar').click(function () {
        Swal.fire({
            html: "Deseja cancelar essa operação? O registro não será salvo.",
            type: "warning",
            showCancelButton: true,
        }).then(function (result) {
            if (result.value) {
                history.back();
            } else {
                console.log("Cancelou a inclusão.");
            }
        });
    });

    $('#btnSalvar').click(function () {

        if ($('#form').valid() != true) {
            FormularioInvalidoAlert();
            return;
        }

        let chamado = SerielizeForm($('#form'));
        let url = $('#form').attr('action');
        //debugger;

        $.ajax({
            type: "POST",
            url: url,
            data: chamado,
            success: function (result) {

                Swal.fire({
                    type: result.Type,
                    title: result.Title,
                    text: result.Message,
                }).then(function () {
                    window.location.href = config.contextPath + result.Controller + '/' + result.Action;
                });

            },
            error: function (result) {

                Swal.fire({
                    text: result.responseJSON.Message,
                    confirmButtonText: 'OK',
                    icon: 'error'
                });

            },
        });
    });

});

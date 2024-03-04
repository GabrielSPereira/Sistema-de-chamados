$(document).ready(function () {
    $('#form').validate({
        rules: {
            Assunto: {
                required: true,
                maxlength: 100
            },
            Solicitante: {
                required: true,
                maxlength: 100,
                pattern: /^[a-zA-Z ]+/
            },
            Departamento: {
                required: true
            },
            DataAbertura: {
                required: true
            }
        },
        messages: {
            Assunto: {
                required: "Por favor, preencha este campo.",
                maxlength: "O campo Assunto deve ter no máximo 100 caracteres.",
            },
            Solicitante: {
                required: "Por favor, preencha este campo.",
                maxlength: "O campo Solicitante deve ter no máximo 100 caracteres.",
                pattern: "O campo Solicitante deve conter apenas letras."
            },
            Departamento: {
                required: "Por favor, preencha este campo.",
            },
            DataAbertura: {
                required: "Por favor, preencha este campo.",
            }
        }
    });

    $('.glyphicon-calendar').closest("div.date").datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: false,
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'pt-BR'
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

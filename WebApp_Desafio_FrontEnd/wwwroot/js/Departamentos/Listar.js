$.getScript('../js/Comum.js', function () {
});

$(document).ready(function () {

    const table = $('#dataTables-Departamentos').DataTable({
        paging: false,
        ordering: false,
        info: false,
        searching: false,
        processing: true,
        serverSide: true,
        ajax: config.contextPath + 'Departamentos/Datatable',
        columns: [
            { data: 'ID' },
            { data: 'Descricao', title: 'Descrição' },
        ],
    });

    $('#dataTables-Departamentos tbody').on('dblclick', 'tr', function () {
        const data = table.row(this).data();
        window.location.href = config.contextPath + 'Departamentos/Editar/' + data.ID;
    });

    $('#dataTables-Departamentos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $('#btnRelatorio').click(function () {
        window.location.href = config.contextPath + 'Departamentos/Report';
    });

    $('#btnAdicionar').click(function () {
        window.location.href = config.contextPath + 'Departamentos/Cadastrar';
    });

    $('#btnEditar').click(function () {
        const data = table.row('.selected').data();
        if (AvisoNecessarioSelecionarRegistro(data)) {
            return;
        }

        window.location.href = config.contextPath + 'Departamentos/Editar/' + data.ID;
    });

    $('#btnExcluir').click(function () {
        const data = table.row('.selected').data();
        if (AvisoNecessarioSelecionarRegistro(data)) {
            return;
        }

        const idRegistro = data.ID;

        if (idRegistro) {
            Swal.fire({
                text: "Tem certeza de que deseja excluir " + data.Descricao + " ?",
                type: "warning",
                showCancelButton: true,
            }).then(function (result) {

                if (result.value) {
                    $.ajax({
                        url: config.contextPath + 'Departamentos/Excluir/' + idRegistro,
                        type: 'DELETE',
                        success: function (result) {
                            console.log(result);

                            Swal.fire({
                                type: result.Type,
                                title: result.Title,
                                text: result.Message,
                            }).then(function () {
                                table.draw();
                            });
                        },
                        error: function (result) {
                            console.log(result);
                            Swal.fire({
                                text: result.responseJSON.Message,
                                confirmButtonText: 'OK',
                                icon: 'error'
                            });

                        }
                    });
                } else {
                    console.log("Cancelou a exclusão.");
                }

            });
        }
    });    
});
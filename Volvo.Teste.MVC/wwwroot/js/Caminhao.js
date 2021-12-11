$("#cmbMarca").change(function () {

    var Id = $("#cmbMarca").val();

    if (Id != "0") {

        BuscarModelos(parseInt(Id));

    }

});

function BuscarModelos(Id) {

    $.ajax({
        url: '/Home/ListarModeloAjax',
        type: 'POST',
        data: { prmIdMarca: Id },
        dataType: "json",
        success: function (data) {

            $("#cmbModelo").empty();

            if (data.length > 0) {

                $.each(data, function (i) {

                    $("#cmbModelo").append(
                        $('<option>', {
                            value: data[i].value,
                            text: data[i].text
                        }, '<option/>')
                    )

                });
            } else {
                $("#cmbModelo").append(
                    $('<option  value="" hidden disabled selected>', {
                        value: "0",
                        text: "Selecione o modelo..."
                    }, '<option/>')
                )
            }

        },
        error: function () {
            alert("Falha")
        }
    })

}
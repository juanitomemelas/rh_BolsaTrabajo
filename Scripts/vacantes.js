function abreVacante(id, titulo) {
    var posicion = "#POS_" + id;
    $("#elIDFrame").contents().find("body").html('');
    miURL = "/rh/vacantes/resultadoVacante.aspx?vacante=" + id+"&titulo="+titulo;
    $("#elIDFrame").attr("src", miURL);
    $('#dialog').dialog({ title: 'Vacante ' + id + ': ' + titulo,
        width: 550,
        height: 550,
        autoOpen: true, 
        modal: true,
        position: { my: "left",
                    at: "top",
                    of: posicion
                }
            });
            $('#dialog').load(acomoda());
        }
        function abreFormaCorreo(id,descripcion,numeroEmpleado,nombreEmpleado,correo) {
            $("#elIDFrame").contents().find("body").html('');
            miURL = "/rh/vacantes/envioCorreo.aspx?vacante=" + id + "&descripcion=" + descripcion
                    +"&numEmpleado="+numeroEmpleado+"&nombreEmpleado="+nombreEmpleado+"&correo="+correo;
            $("#elIDFrame").attr("src", miURL);
            $('#dialog').on('shown', function () {
                $("#elIDFrame input").first().focus();
            });
            $('#dialog').dialog({ title: id + " - " + descripcion,
            width: 550,
            height: 550,
            autoOpen: true,
            modal: true
            /*,
            position: { my: "left",
                    at: "top",
                    of: "center"
                                    }*/
            });
            $('#dialog').load(acomoda());
        }
function abreVacanteCompleta(id) {
    $("#elIDFrame").contents().find("body").html('');
    miURL = "/rh/vacantes/resultadoVacanteCompleta.aspx?vacante=" + id;
    $("#elIDFrame").attr("src", miURL);
    $('#dialog').dialog({ title: 'Vacante ' + id, width: 550, height: 550, autoOpen: true, modal: true });
    $('#dialog').load(acomoda());
}
function abreCV(id) {
    $("#elIDFrame").contents().find("body").html('');
    miURL = "/rh/vacantes/Curriculum.aspx?candidato=" + id;
    $("#elIDFrame").attr("src", miURL);
    $('#dialog').dialog({ title: 'Vacante ' + id, width: 500, height: 550, autoOpen: true, modal: true });
    $('#dialog').load(acomoda());
}
function manipulaDivcontenedorDerecho(laCarpeta) {
    var laURL = "";
    sigue = true;
    //Validamos hacia donde vamos a mandar la información, le concatenamos una N para que se convierta en string
    switch ("n" + laCarpeta) {
        case "n10001":
            laURL = "/rh/vacantes/NuevaVacante.aspx";
            break;
        case "n10002":
            laURL = "/rh/vacantes/cerrarVacantes.aspx";
            break;
        case "n10003":
            laURL = "/rh/vacantes/Candidatos.aspx";
            break;
        case "n10004":
            laURL = "/rh/vacantes/Principal.aspx";
            break;
        case "n20001":
            laURL = "/rh/vacantes/NuevaVacanteDatos.aspx";
            break;
        case "n10005":
            laURL = "/rh/Administracion/administracion.aspx";
            break;      
        default:
            sigue = false;
            break;
    }
    if (sigue) {
        $("#elframePrincipal").attr("src", laURL);
    }
}

function regresaDatos(elID, elObjeto) {
    var url = "/rh/servicios/MyService.asmx/CambiosVacantes";
    var losDatos = {};
    losDatos.id = elID;
    losDatos.puesto = $('#' + elID + '_02').val();
    losDatos.descripcion = $('#' + elID + '_03').val();
    losDatos.competencias = $('#' + elID + '_04').val();
    losDatos.ubicacion = $('#' + elID + '_05').find('option:selected').val();
    losDatos.contrato = $('#' + elID + '_06').val();
    losDatos.rango = $('#' + elID + '_07').val();
    losDatos.sexo = $('#' + elID + '_08').val();
    losDatos.status = $('#' + elID + '_09').find('option:selected').val();  


    //Hacemos la petición hacia el servidor con los datos del combobox seleccionado
    $.ajax({
        type: "POST",
        url: url,
        data: losDatos,
        datatype: "xml",
        success: function (msg) {
            var datos = $.parseJSON($(msg).find('string').text());
            agregaFilaActualizada(elID, elObjeto, datos)

        },
        error: function (res, status) {
            if (status === "error") {
                var errorMessage = $.parseJSON(res.responseText);
                alert(errorMessage.Message);
            }
        }
    })
}

function agregaFilaNoEditable(elID, elObjeto) {
    //10 celdas
    elObjetoOriginal = elObjeto.parent().parent();
    var $tds = elObjetoOriginal.find('td');
    //Buscamos la fila de la tabla en la que estamos ya que la ASP no genera un ID para estos elementos
    elObjeto.parent().parent().replaceWith($('<tr>')
        .append($('<td>').text(elID))
        .append($('<td>').append())
        .append($('<td>').append())
        .append($('<td>').append())
        .append($('<td>').append())
        .append($('<td>').append())
        .append($('<td>').append())
        .append($('<td>').append())
        .append($('<td>').append())
        .append($('<td>')
            .append($('<img>')
                .attr('src', '/rh/Imagenes/IconoModificar.png')
                .text('aceptar')
                .attr('class', 'imagenIconoMano')
                .click(function () { agregaFilaEditable(elID ,$(this)) })
            )
        )
		)
}
function agregaFilaActualizada(elID, elObjeto,elJotoson) {
    //10 celdas
    elObjetoOriginal = elObjeto.parent().parent();
    var $tds = elObjetoOriginal.find('td');
    //Buscamos la fila de la tabla en la que estamos ya que la ASP no genera un ID para estos elementos
    elObjeto.parent().parent().replaceWith($('<tr>')
        .append($('<td>').text(elID))
        .append($('<td>').text(elJotoson.Puesto))
        .append($('<td>').text(elJotoson.Descripcion))
        .append($('<td>').text(elJotoson.Competencias))
        .append($('<td>').text(elJotoson.Ubicacion))
        .append($('<td>').text(elJotoson.Tipo_Contrato))
        .append($('<td>').text(elJotoson.Rango_Edad))
        .append($('<td>').text(elJotoson.Sexo))
        .append($('<td>').text(elJotoson.Status))
        .append($('<td>')
            .append($('<img>')
                .attr('src', '/rh/Imagenes/IconoModificar.png')
                .text('aceptar')
                .attr('class', 'imagenIconoMano')
                .click(function () { agregaFilaEditable(elID, $(this)) })
            )
        )
		)
}
function generaInput(elID, elTexto) {
    var elInput = $('<input>');
    elInput.attr({
        type: 'text',
        id: elID,
        name: 'bar',
        class: 'textboxSanchez095',
        value: elTexto
    });
    return elInput;
}
function generaTextArea(elID, elTexto) {
    var elInput = $('<textarea>');
    elInput.attr({
        cols: '40',
        rows: '5',
        id: elID,
        class: 'textboxSanchez160',
        name: 'bar'
    }).val(elTexto);
    return elInput;
}
//Función para generar un combobox dinámico dependiento de los parámetros enviados que son: ID y Valor.
function generaSelect(nameID, elArreglo,elValor) {
    var combo = $('<select>');
    combo.attr('id', nameID);
    combo.attr('class','textboxSanchez095');
    for (var x in elArreglo) {
        combo.append(
        $('<option />')
            .text(elArreglo[x].valor)
            .val(elArreglo[x].id)
    )
    }
    combo.val(elValor);
    
    return combo;
}
function confirmaAccion(elID,elObjeto) {
    regresaDatos(elID, elObjeto);
}

function regresaLocaciones() {
    var url = "/rh/servicios/MyService.asmx/obtieneLocaciones";
    var losDatos = {};
    var datos;
    $.ajax({
        type: "POST",
        url: url,
         async: false,
        datatype: "xml",
        success: function (msg) {
            datos = $.parseJSON($(msg).find('string').text());
            console.log(datos);
        },
        error: function (res, status) {
            if (status === "error") {
                var errorMessage = $.parseJSON(res.responseText);
                alert(errorMessage.Message);
            }
        }
    });
    return datos;

}
function agregaFilaEditable(elID, elObjeto) {

    //10 celdas
    elObjetoOriginal = elObjeto.parent().parent();
    var $tds = elObjetoOriginal.find('td');
    //Para la parte del combo de estatus de la vacante
    var comboEstatus = [];
    comboEstatus[0] = { id: "V", valor: "Vigente" };
    comboEstatus[1] = { id: "C", valor: "Cerrado" };
    var comboSexo = [];
    comboSexo[0] = { id: "I", valor: "Indistinto" };
    comboSexo[1] = { id: "M", valor: "Masculino" };
    comboSexo[2] = { id: "F", valor: "Femenino" };
    var comboContrato = [];
    var valorDefaultContrato = "";
    if ("Indefinido" == $tds.eq(5).text()) {
        valorDefaultContrato = "001";
    }else if("Medio Tiempo" == $tds.eq(5).text()){
    valorDefaultContrato = "002";
    } else if ("Temporal" == $tds.eq(5).text()) {
    valorDefaultContrato = "003";
    }
    
    comboContrato[0] = { id: "001", valor: "Indefinido" };
    comboContrato[1] = { id: "002", valor: "Medio Tiempo" };
    comboContrato[2] = { id: "003", valor: "Temporal" };
    var comboUbicacion = [];
    var locaciones = regresaLocaciones();
    var valorDefaultLocaciones = "";
    for (var i = 0; i < locaciones.length; i++) {
        comboUbicacion[i] = { id: locaciones[i].Id, valor: locaciones[i].Descripcion }
        if ($tds.eq(4).text() == locaciones[i].Descripcion) { valorDefaultLocaciones = locaciones[i].Id }
    }
    valorDefaultLocaciones
    //Buscamos la fuila de la tabla en la que estamos

    elObjeto.parent().parent().replaceWith($('<tr>')
        .append($('<td>').text(elID))
        .append($('<td>').append(generaInput(elID+"_02", $tds.eq(1).text())))
        .append($('<td>').append(generaTextArea(elID+"_03", $tds.eq(2).text())))
        .append($('<td>').append(generaTextArea(elID + "_04", $tds.eq(3).text())))
        .append($('<td>').append(generaSelect(elID + "_05", comboUbicacion,valorDefaultLocaciones)))
        .append($('<td>').append(generaSelect(elID + "_06", comboContrato, valorDefaultContrato)))
        .append($('<td>').append(generaInput(elID + "_07", $tds.eq(6).text())))
        .append($('<td>').append(generaSelect(elID + "_08", comboSexo, $.trim($tds.eq(7).text()).charAt(0))))
        .append($('<td>').append(generaSelect(elID + "_09",comboEstatus, $.trim($tds.eq(8).text()).charAt(0))))
        .append($('<td>')
            .append($('<img>')
                .attr('src', '/rh/Imagenes/aceptar.png')
                .text('aceptar')
                .attr('class', 'imagenIconoMano')
                .click(function () { confirmaAccion(elID,$(this)) })
            )
        )
		)
}
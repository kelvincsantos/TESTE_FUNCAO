
$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #CPF').val(obj.CPF);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
    }

    if (benef) {
        $('#modalBeneficiarios #Nome_Benef').val(benef.Nome);
        $('#modalBeneficiarios #CPF_Benef').val(benef.CPF);
    };


    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "CPF": $(this).find("#CPF").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val()
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                    window.location.href = urlRetorno;
                }
        });
    })


    $('#benef-modal').click(function () {
        var texto = '        <div id="modalBeneficiarios" class="modal fade">																													' +
            '            <div class="modal-dialog">                                                                                                                                    ' +
            '                <div class="modal-content">                                                                                                                               ' +
            '                    <div class="modal-header">                                                                                                                            ' +
            '                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>                                                            ' +
            '                        <h4 class="modal-title">Beneficiários</h4>                                                                                                        ' +
            '                    </div>                                                                                                                                                ' +
            '                                                                                                                                                                          ' +
            '                    <div class="modal-body">                                                                                                                              ' +
            '                        <div class="row">                                                                                                                                 ' +
            '                            <div class="col-md-4">                                                                                                                        ' +
            '                                <div class="form-group">                                                                                                                  ' +
            '                                    <label for="CPF">CPF:</label>                                                                                                         ' +
            '                                    <input required="required" type="text" class="form-control" id="CPF_Benef" name="CPF" placeholder="Ex.: 010.011.111-00" maxlength="14">     ' +
            '                                </div>                                                                                                                                    ' +
            '                            </div>                                                                                                                                        ' +
            '                            <div class="col-md-4">                                                                                                                        ' +
            '                                <div class="form-group">                                                                                                                  ' +
            '                                    <label for="Nome">Nome:</label>                                                                                                       ' +
            '                                    <input required="required" type="text" class="form-control" id="Nome_Benef" name="Nome" placeholder="Ex.: José da Silva" maxlength="255">	' +
            '                                </div>                                                                                                                                    ' +
            '                            </div>                                                                                                                                        ' +
            '                            <div class="col-md-4">                                                                                                                        ' +
            '                                <div class="form-group">                                                                                                                  ' +
            '                                    <label for="Empty"> </label>                                                                                                       ' +
            '                                    <button type="button" onclick="IncluirBeneficiario(' + obj.Id + ')" class="btn btn-success btn-sm">Incluir</button>                           ' +
            '                                </div>                                                                                                                                    ' +
            '                            </div>                                                                                                                                        ' +
            '                        </div>                                                                                                                                            ' +
            '                        <div class="row">                                                                                                                                 ' +
            '                            <div class="col-md-12">                                                                                                                        ' +
            '                                <div class="form-group">                                                                                                                  ' +
            '                                   <table id="gridBeneficiarios" class="table">                                                                                          ' +
            '                                       <thead>                                                                                          ' +
            '                                           <tr>                                                                                          ' +
            '                                           <th>CPF</th>                                                                                          ' +
            '                                           <th>Nome</th>                                                                                          ' +
            '                                           <th></th>                                                                                          ' +
            '                                           </tr>                                                                                          ' +
            '                                       </thead>                                                                                          ' +
            '                                       <tbody id="bodyBeneficiarios">                                                                                          ' +
            '                                       </tbody>                                                                                          ' +
            '                                   </table>                                                                                          ' +
            '                                </div>                                                                                                                                    ' +
            '                            </div>                                                                                                                                        ' +
            '                        </div>                                                                                                                                            ' +
            '                    </div>                                                                                                                                                ' +
            '                                                                                                                                                                          ' +
            '                    <div class="modal-footer">                                                                                                                            ' +
            '                        <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>                                                                ' +
            '                    </div>                                                                                                                                                ' +
            '                </div>                                                                                                                                                    ' +
            '            </div>                                                                                                                                                        ' +
            '        </div>                                                                                                                                                            ';

        $('body').html(texto);

        PegarBeneficiarios(obj.Id);

        $('#modalBeneficiarios').modal('show'); 
        
    });
})

function IncluirBeneficiario(ID_Cliente) {
    $.ajax({
        url: urlBenefInclusao,
        method: 'POST',
        data:
        {
            "IDCliente": ID_Cliente,
            "Nome": $('#modalBeneficiarios').find('#Nome_Benef').val(),
            "CPF": $('#modalBeneficiarios').find('#CPF_Benef').val(),

        },
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {
                ModalDialog("Sucesso!", r);

                window.location.href = urlAlteracao + '/' + ID_Cliente;
                
                //Load student list from server
                PegarBeneficiarios(ID_Cliente);
            }
    })
}

function PegarBeneficiarios(idcliente) {
    $.ajax({
        url:urlBeneficiariosList,
        method: 'POST',
        data: {
            "idCliente": idcliente,
        },
       success: function (r) {
            if (r == null || r == undefined || r.length == 0) {
                var message = '';
                message += '<tr>';
                message += '<td class="colspan="4"">Beneficiários não encontrados!</td>';
                message += '</tr>';

                $('#bodyBeneficiarios').html(message);
            }
            else {
                var listaItens = '';
                $.each(r.Records, function (index, item) {
                    listaItens += '<tr>';
                    listaItens += '<td width="40%">' + item.Nome + '</td>';
                    listaItens += '<td width="30%">' + item.CPF + '</td>';
                    listaItens += '<td><button type="button" onclick="AlterarBeneficiario(' + item + ')" class="btn btn-success btn-sm pull-right">Alterar</button>'
                    listaItens += '<button type="button" onclick="ExcluirBeneficiario(' + item.Id + ', ' + idcliente + ')" class="btn btn-danger btn-sm pull-right">Excluir</button></td>';
                    listaItens += '</tr>';    
                });

                $('#bodyBeneficiarios').html(listaItens);
            }
        },
        error:
            function () {
                ModalDialog(e.Result, r.message);
            }

    });
}

function AlterarBeneficiario(Benef) {
    alert(benef.Id);
    alert(benef.Nome);
    alert(benef.CPF);

    $('#modalBeneficiarios').find('#Nome_Benef').val(benef.Nome);
    $('#modalBeneficiarios').find('#CPF_Benef').val(benef.CPF);
}

function ExcluirBeneficiario(id, idcliente) {
    $.ajax({
        url: urlBenefExclusao,
        method: 'POST',
        data: {
            "idCliente": id,
        },
        success: function (r) {
            ModalDialog('Sucesso!', r);         
            
        },
        error:
            function () {
                ModalDialog(e.Result, r.message);
            }

    });

    PegarBeneficiarios(id_cliente);
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

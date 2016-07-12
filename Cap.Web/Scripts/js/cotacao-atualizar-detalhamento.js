
function atualizarDetalhamento(id) {
    $.ajax({
        url: '/Erp/CotacaoFornecedor/Detalhamento/',
        type: 'GET',
        data: { 'id': id },
        success: function (result) {
            $('#detalhamento').html(result);
        },
        error: function (e) {
            $('#detalhamento').html(e.error);
        }
    });
};


function ShowMessage(msg) {
    $('#modal').load('/Erp/ModalInfo/Index/', { 'msg': msg }, function () {
        $('#modal').modal('show');
    });
};
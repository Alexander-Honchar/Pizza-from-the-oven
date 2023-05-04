var OrderStatus = /** @class */ (function () {
    function OrderStatus() {
    }
    return OrderStatus;
}());
var IsStatus = /** @class */ (function () {
    function IsStatus() {
    }
    return IsStatus;
}());
function setOrderStatus(id, status) {
    var newOrderStatus = new OrderStatus();
    newOrderStatus.id = id;
    newOrderStatus.status = status;
    $.ajax({
        type: "GET",
        url: "/Home/InWorkAjax",
        data: newOrderStatus,
        success: function (data) {
            if (data != null) {
                var newStatus = new IsStatus();
                newStatus = data;
                if (newStatus.isSuccess) {
                    changeStatus(newStatus.status);
                }
            }
        }
    });
}
$('a').click(function () {
    var clickId = Number($(this).attr('id'));
    var name = $(this).attr('name');
    setOrderStatus(clickId, name);
});
function changeStatus(newStatus) {
    $(".statusOrder").text("".concat(newStatus));
    $(".StatusInWork").attr("class", 'btn btn-primary disabled StatusInWork');
}
//# sourceMappingURL=InWork.js.map
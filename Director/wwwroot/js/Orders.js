//class OrderCount {
//    menuName: string
//    count: number
//}
var OrderViewForDirector = /** @class */ (function () {
    function OrderViewForDirector() {
    }
    return OrderViewForDirector;
}());
var orderStatusAccepted = "Принят";
var orderStatusInWork = "В работе";
var orderStatusReady = "Готов";
var orderStatusASent = "Отправлен";
function card(newOrders) {
    if (newOrders.length > 0) {
        for (var i = 0; i < newOrders.length; i++) {
            var _card = document.createElement("div");
            _card.setAttribute('id', "".concat(newOrders[i].idOrderHeader));
            _card.setAttribute('class', 'taleForDirector card p-2');
            _card.setAttribute('style', "border:2px solid #808080; border-radius: 10px;");
            var _cardheader = document.createElement('div');
            _cardheader.setAttribute('class', 'card-header  text-light');
            _cardheader.setAttribute('style', "background-color:#222222;height:150px");
            var _cardheaderdiv1 = document.createElement('div');
            _cardheaderdiv1.setAttribute('class', 'row text-center');
            var _cardheaderdiv1div2_1 = document.createElement('div');
            _cardheaderdiv1div2_1.setAttribute('class', 'col-2');
            var _cardheaderdiv1div2_1h5 = document.createElement('h5');
            _cardheaderdiv1div2_1h5.innerHTML = "".concat(newOrders[i].numberOrder);
            _cardheaderdiv1div2_1.appendChild(_cardheaderdiv1div2_1h5);
            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_1);
            var _cardheaderdiv1div2_2 = document.createElement('div');
            _cardheaderdiv1div2_2.setAttribute('class', 'col-2');
            var _cardheaderdiv1div2_2h5 = document.createElement('h5');
            _cardheaderdiv1div2_2h5.innerHTML = "".concat(newOrders[i].dateCreateOrder);
            _cardheaderdiv1div2_2.appendChild(_cardheaderdiv1div2_2h5);
            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_2);
            var _cardheaderdiv1div2_3 = document.createElement('div');
            _cardheaderdiv1div2_3.setAttribute('class', 'col-2');
            for (var j = 0; j < newOrders[i].menuName.length; j++) {
                var _cardheaderdiv1div2_3h5 = document.createElement('h5');
                _cardheaderdiv1div2_3h5.innerHTML = "".concat(newOrders[i].menuName[j]);
                _cardheaderdiv1div2_3.appendChild(_cardheaderdiv1div2_3h5);
            }
            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_3);
            var _cardheaderdiv1div2_4 = document.createElement('div');
            /*_cardheaderdiv1div2_4.setAttribute('id', `${newOrders[i].idOrderHeader}`);*/
            _cardheaderdiv1div2_4.setAttribute('class', 'col-2');
            var _cardheaderdiv1div2_4h5 = document.createElement('h5');
            _cardheaderdiv1div2_4h5.setAttribute('id', "".concat(newOrders[i].idOrderHeader));
            _cardheaderdiv1div2_4h5.setAttribute('class', 'manager');
            _cardheaderdiv1div2_4h5.innerHTML = "".concat(newOrders[i].manager);
            _cardheaderdiv1div2_4.appendChild(_cardheaderdiv1div2_4h5);
            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_4);
            var _cardheaderdiv1div2_5 = document.createElement('div');
            /* _cardheaderdiv1div2_5.setAttribute('id', `${newOrders[i].idOrderHeader}`);*/
            _cardheaderdiv1div2_5.setAttribute('class', 'col-2');
            var _cardheaderdiv1div2_5h5 = document.createElement('h5');
            _cardheaderdiv1div2_5h5.setAttribute('id', "".concat(newOrders[i].idOrderHeader));
            _cardheaderdiv1div2_5h5.setAttribute('class', 'cook');
            _cardheaderdiv1div2_5h5.innerHTML = "".concat(newOrders[i].cook);
            _cardheaderdiv1div2_5.appendChild(_cardheaderdiv1div2_5h5);
            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_5);
            var _cardheaderdiv1div2_6 = document.createElement('div');
            /*_cardheaderdiv1div2_6.setAttribute('id', `${newOrders[i].idOrderHeader}`);*/
            _cardheaderdiv1div2_6.setAttribute('class', 'col-2');
            var _cardheaderdiv1div2_6h5 = document.createElement('h5');
            _cardheaderdiv1div2_6h5.setAttribute('id', "".concat(newOrders[i].idOrderHeader));
            _cardheaderdiv1div2_6h5.setAttribute('class', 'status');
            if (newOrders[i].orderStatus == null) {
                _cardheaderdiv1div2_6h5.innerHTML = "Не принят";
            }
            else {
                _cardheaderdiv1div2_6h5.innerHTML = "".concat(newOrders[i].orderStatus);
            }
            _cardheaderdiv1div2_6.appendChild(_cardheaderdiv1div2_6h5);
            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_6);
            _cardheader.appendChild(_cardheaderdiv1);
            _card.appendChild(_cardheader);
            document.getElementById("cardOrders").appendChild(_card);
        }
    }
}
function getAllOrders() {
    $.ajax({
        type: "GET",
        url: "/api/Home",
        success: function (data) {
            if (data != null) {
                var oldArrayViewModels = getOldArrayViewModels();
                var arrayViewModels = [];
                for (var i = 0; i < data.length; i++) {
                    arrayViewModels.push(data[i]);
                }
                if (oldArrayViewModels.length == 0) {
                    card(arrayViewModels);
                }
                if (oldArrayViewModels.length > 0) {
                    checkColumsManager(arrayViewModels);
                    checkColumsCook(arrayViewModels);
                    checkColumsStatus(arrayViewModels);
                    if (arrayViewModels.length > oldArrayViewModels.length) {
                        var addNewViewModels = checkOrdersForAdd(oldArrayViewModels, arrayViewModels);
                        addingOrders(addNewViewModels);
                    }
                    if (arrayViewModels.length < oldArrayViewModels.length) {
                        var idForRemovedOrders = checkOrdersForRemoved(oldArrayViewModels, arrayViewModels);
                        deletingOrders(idForRemovedOrders);
                    }
                }
            }
        }
    });
}
function getOldArrayViewModels() {
    var oldListOrders = [];
    if ($(".taleForDirector").length > 0) {
        $(".taleForDirector.card").each(function (index, element) {
            oldListOrders.push(Number($(element).attr("id")));
        });
    }
    return oldListOrders;
}
function checkColumsManager(arrayViewModels) {
    if (arrayViewModels.length > 0) {
        $(".manager").each(function (index, element) {
            for (var i = 0; i < arrayViewModels.length; i++) {
                if (arrayViewModels[i].idOrderHeader == Number($(element).attr("id"))) {
                    $(element).text("".concat(arrayViewModels[i].manager));
                }
            }
        });
    }
}
function checkColumsStatus(arrayViewModels) {
    if (arrayViewModels.length > 0) {
        $(".status").each(function (index, element) {
            for (var i = 0; i < arrayViewModels.length; i++) {
                if (arrayViewModels[i].idOrderHeader == Number($(element).attr("id"))) {
                    if (arrayViewModels[i].orderStatus == null) {
                        $(element).text("Не принят");
                    }
                    else {
                        $(element).text("".concat(arrayViewModels[i].orderStatus));
                    }
                }
            }
        });
    }
}
function checkColumsCook(arrayViewModels) {
    if (arrayViewModels.length > 0) {
        $(".cook").each(function (index, element) {
            for (var i = 0; i < arrayViewModels.length; i++) {
                if (arrayViewModels[i].idOrderHeader == Number($(element).attr("id"))) {
                    $(element).text("".concat(arrayViewModels[i].cook));
                }
            }
        });
    }
}
function checkOrdersForAdd(oldArrayViewModels, arrayViewModels) {
    if (arrayViewModels.length > oldArrayViewModels.length) {
        for (var i = 0; i < oldArrayViewModels.length; i++) {
            for (var j = 0; j < arrayViewModels.length; j++) {
                if (oldArrayViewModels[i] == arrayViewModels[j].idOrderHeader) {
                    var removedObject = arrayViewModels.splice(j, 1);
                }
            }
        }
        return arrayViewModels;
    }
}
function checkOrdersForRemoved(oldArrayViewModels, arrayViewModels) {
    for (var i = 0; i < arrayViewModels.length; i++) {
        for (var j = 0; j < oldArrayViewModels.length; j++) {
            if (oldArrayViewModels[j] == arrayViewModels[i].idOrderHeader) {
                var removedObject = oldArrayViewModels.splice(j, 1);
            }
        }
    }
    return oldArrayViewModels;
}
function deletingOrders(idOrder) {
    $(".taleForDirector").each(function (index, element) {
        for (var i = 0; i < idOrder.length; i++) {
            if (Number($(element).attr("id")) == idOrder[i]) {
                $(element).remove();
            }
        }
    });
}
function addingOrders(newViewModels) {
    card(newViewModels);
}
//function test() {
//    getAllOrders();
//}
setInterval(getAllOrders, 1200);
//# sourceMappingURL=Orders.js.map
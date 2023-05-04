
//class OrderCount {
//    menuName: string
//    count: number
//}

class OrderViewForDirector {
    idOrderHeader: number
    numberOrder: number
    dateCreateOrder:string
    menuName: string[]
    manager: string
    cook: string
    orderStatus: string
}

const orderStatusAccepted: string = "Принят";
const orderStatusInWork: string = "В работе";
const orderStatusReady: string = "Готов";
const orderStatusASent: string = "Отправлен";



function card(newOrders: OrderViewForDirector[]) {

    if (newOrders.length>0) {

        for (var i = 0; i < newOrders.length; i++) {

            const _card = document.createElement("div");
            _card.setAttribute('id', `${newOrders[i].idOrderHeader}`);
            _card.setAttribute('class', 'taleForDirector card p-2');
            _card.setAttribute('style', "border:2px solid #808080; border-radius: 10px;");


            const _cardheader = document.createElement('div');
            _cardheader.setAttribute('class', 'card-header  text-light');
            _cardheader.setAttribute('style', "background-color:#222222;height:150px");

            const _cardheaderdiv1 = document.createElement('div');
            _cardheaderdiv1.setAttribute('class', 'row text-center');

            const _cardheaderdiv1div2_1 = document.createElement('div');
            _cardheaderdiv1div2_1.setAttribute('class', 'col-2');

            const _cardheaderdiv1div2_1h5 = document.createElement('h5');
            _cardheaderdiv1div2_1h5.innerHTML = `${newOrders[i].numberOrder}`;
            _cardheaderdiv1div2_1.appendChild(_cardheaderdiv1div2_1h5);

            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_1)

            const _cardheaderdiv1div2_2 = document.createElement('div');
            _cardheaderdiv1div2_2.setAttribute('class', 'col-2');

            const _cardheaderdiv1div2_2h5 = document.createElement('h5');
            _cardheaderdiv1div2_2h5.innerHTML = `${newOrders[i].dateCreateOrder}`;
            _cardheaderdiv1div2_2.appendChild(_cardheaderdiv1div2_2h5);

            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_2)

            const _cardheaderdiv1div2_3 = document.createElement('div');
            _cardheaderdiv1div2_3.setAttribute('class', 'col-2');

            for (var j = 0; j < newOrders[i].menuName.length; j++) {

                const _cardheaderdiv1div2_3h5 = document.createElement('h5');
                _cardheaderdiv1div2_3h5.innerHTML = `${newOrders[i].menuName[j]}`;
                _cardheaderdiv1div2_3.appendChild(_cardheaderdiv1div2_3h5);

            }

            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_3);

            const _cardheaderdiv1div2_4 = document.createElement('div');
            /*_cardheaderdiv1div2_4.setAttribute('id', `${newOrders[i].idOrderHeader}`);*/
            _cardheaderdiv1div2_4.setAttribute('class', 'col-2');

            const _cardheaderdiv1div2_4h5 = document.createElement('h5');
            _cardheaderdiv1div2_4h5.setAttribute('id', `${newOrders[i].idOrderHeader}`);
            _cardheaderdiv1div2_4h5.setAttribute('class', 'manager');
            _cardheaderdiv1div2_4h5.innerHTML = `${newOrders[i].manager}`;
            _cardheaderdiv1div2_4.appendChild(_cardheaderdiv1div2_4h5);

            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_4);

            const _cardheaderdiv1div2_5 = document.createElement('div');
           /* _cardheaderdiv1div2_5.setAttribute('id', `${newOrders[i].idOrderHeader}`);*/
            _cardheaderdiv1div2_5.setAttribute('class', 'col-2');

            const _cardheaderdiv1div2_5h5 = document.createElement('h5');
            _cardheaderdiv1div2_5h5.setAttribute('id', `${newOrders[i].idOrderHeader}`);
            _cardheaderdiv1div2_5h5.setAttribute('class', 'cook');
            _cardheaderdiv1div2_5h5.innerHTML = `${newOrders[i].cook}`;
            _cardheaderdiv1div2_5.appendChild(_cardheaderdiv1div2_5h5);

            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_5)

            const _cardheaderdiv1div2_6 = document.createElement('div');
            /*_cardheaderdiv1div2_6.setAttribute('id', `${newOrders[i].idOrderHeader}`);*/
            _cardheaderdiv1div2_6.setAttribute('class', 'col-2');

            const _cardheaderdiv1div2_6h5 = document.createElement('h5');
            _cardheaderdiv1div2_6h5.setAttribute('id', `${newOrders[i].idOrderHeader}`);
            _cardheaderdiv1div2_6h5.setAttribute('class', 'status');
            if (newOrders[i].orderStatus==null) {
                _cardheaderdiv1div2_6h5.innerHTML = "Не принят";
            }
            else {
                _cardheaderdiv1div2_6h5.innerHTML = `${newOrders[i].orderStatus}`;
            }
            _cardheaderdiv1div2_6.appendChild(_cardheaderdiv1div2_6h5);

            _cardheaderdiv1.appendChild(_cardheaderdiv1div2_6)

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

                const arrayViewModels: OrderViewForDirector[] = []

                for (var i = 0; i < data.length; i++) {

                    arrayViewModels.push(data[i]);

                }

                if (oldArrayViewModels.length==0) {

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

    var oldListOrders: number[] = [];
    if ($(".taleForDirector").length > 0) {
        $(".taleForDirector.card").each(function (index, element) {
            oldListOrders.push(Number($(element).attr("id")));
        });
    }
    return oldListOrders;
}





function checkColumsManager(arrayViewModels: OrderViewForDirector[]) {

    if (arrayViewModels.length>0) {

        $(".manager").each(function (index, element) {
            for (var i = 0; i < arrayViewModels.length; i++) {
                if (arrayViewModels[i].idOrderHeader == Number($(element).attr("id")) ) {

                    $(element).text(`${arrayViewModels[i].manager}`);

                }
            }
        });
    }
}




function checkColumsStatus(arrayViewModels: OrderViewForDirector[]) {

    if (arrayViewModels.length > 0) {

        $(".status").each(function (index, element) {
            for (var i = 0; i < arrayViewModels.length; i++) {
                if (arrayViewModels[i].idOrderHeader == Number($(element).attr("id"))) {

                    if (arrayViewModels[i].orderStatus==null) {
                        $(element).text("Не принят");
                    }
                    else {
                        $(element).text(`${arrayViewModels[i].orderStatus}`);
                    }

                    

                }
            }
        });
    }
}






function checkColumsCook(arrayViewModels: OrderViewForDirector[]) {

    if (arrayViewModels.length > 0) {

        $(".cook").each(function (index, element) {
            for (var i = 0; i < arrayViewModels.length; i++) {
                if (arrayViewModels[i].idOrderHeader == Number($(element).attr("id"))) {

                    $(element).text(`${arrayViewModels[i].cook}`);

                }
            }
        });
    }
}









function checkOrdersForAdd(oldArrayViewModels: number[], arrayViewModels: OrderViewForDirector[]) {

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





function checkOrdersForRemoved(oldArrayViewModels: number[], arrayViewModels: OrderViewForDirector[]) {

    for (var i = 0; i < arrayViewModels.length; i++) {

        for (var j = 0; j < oldArrayViewModels.length; j++) {

            if (oldArrayViewModels[j] == arrayViewModels[i].idOrderHeader) {
                var removedObject = oldArrayViewModels.splice(j, 1);
            }

        }

    }

    return oldArrayViewModels;

}









function deletingOrders(idOrder: number[]) {

    $(".taleForDirector").each(function (index, element) {

        for (var i = 0; i < idOrder.length; i++) {
            if (Number($(element).attr("id")) == idOrder[i]) {
                $(element).remove();
            }
        }

    });

}





function addingOrders(newViewModels: OrderViewForDirector[]) {

    card(newViewModels);

}




//function test() {
//    getAllOrders();
//}


setInterval(getAllOrders, 1200);










class Status {
    id: number
    status: string
}






interface OrderDetailsIndex {
    idOrderHeader: number
    numberOrder: number
    orderStatus: string
    dateCreatedOrder: string
    menuName: string[]
    firstName: string
    lastName: string
    phoneNumber: string
    street: string
    house: string
    entrance: string
    apartment: string
    floor: string
}



const arrayOrderDetails: OrderDetailsIndex[] = [];

const orderStatusAccepted: string = "Принят";
const orderStatusInWork: string = "В работе";
const orderStatusReady: string = "Готов";
const orderStatusASent: string = "Отправлен";



function table(response: OrderDetailsIndex[]) {

    



    const _table = document.createElement('table');
    _table.setAttribute('id', "table");
    _table.setAttribute('class', "table-bordered ");
    _table.setAttribute('width', '100%');

    const _thead = document.createElement('thead');
    const _trhead = document.createElement('tr');
    _trhead.setAttribute('class', 'text-center');

        const _th1 = document.createElement('th');
        _th1.innerHTML ="Номер Заказа";
        _trhead.appendChild(_th1);

        const _th2 = document.createElement('th');
        _th2.innerHTML = "Принят";
        _trhead.appendChild(_th2);;
    
        const _th3 = document.createElement('th');
        _th3.innerHTML = "Клиент";
        _trhead.appendChild(_th3);;

        const _th4 = document.createElement('th');
        _th4.innerHTML = "Телефон";
        _trhead.appendChild(_th4);

        const _th5 = document.createElement('th');
        _th5.innerHTML = "Адрес";
        _trhead.appendChild(_th5);;

        const _th6 = document.createElement('th');
        _th6.innerHTML = "Заказ";
        _trhead.appendChild(_th6);;

        const _th7 = document.createElement('th');
        _th7.innerHTML = "Статус";
        _trhead.appendChild(_th7);;

        const _th8 = document.createElement('th');
        _trhead.appendChild(_th8);;


    _thead.appendChild(_trhead);
    _table.appendChild(_thead);




    const _tbody = document.createElement('tbody');
    _tbody.setAttribute('id','tbody')
    
    for (var i = 0; i < response.length; i++) {

        let tablestring = [];
        tablestring[0] = response[i].street == null ? '' : " " + String(response[i].street);
        tablestring[1] = response[i].house == null ? '' : " дом." + String(response[i].house);
        tablestring[2] = response[i].entrance == null ? '' : " под." + String(response[i].entrance)
        tablestring[3] = response[i].floor == null ? '' : " эт." + String(response[i].floor);
        tablestring[4] = response[i].apartment == null ? '' : " кв." + String(response[i].apartment);




        const _trbody = document.createElement('tr');
        _trbody.setAttribute('id', `${response[i].idOrderHeader}`);
        _trbody.setAttribute('class', 'text-center tr');

            const _td1 = document.createElement('td');
            _td1.setAttribute('width', '5%');
            _td1.innerHTML =String(response[i].numberOrder);
            _trbody.appendChild(_td1);

            const _td2 = document.createElement('td');
            _td2.setAttribute('width', '7%');
            _td2.innerHTML = String(response[i].dateCreatedOrder);
            _trbody.appendChild(_td2);

            const _td3 = document.createElement('td');
            _td3.setAttribute('width', '10%');
            _td3.innerHTML = String(response[i].firstName) + " " + String(response[i].lastName);
            _trbody.appendChild(_td3);

            const _td4 = document.createElement('td');
            _td4.setAttribute('width', '10%');
            _td4.innerHTML = String(response[i].phoneNumber);
            _trbody.appendChild(_td4);

            const _td5 = document.createElement('td');
            _td5.setAttribute('width', '10%');
            var resultstring = '';
            for (var k = 0; k < tablestring.length; k++) {
                resultstring += tablestring[k];
            }
             _td5.innerHTML = resultstring;                               
            _trbody.appendChild(_td5);

            const _td6 = document.createElement('td');
            _td6.setAttribute('width', '15%');
            for (var j = 0; j < response[i].menuName.length; j++) {
                var _p = document.createElement('p');
                _p.textContent = response[i].menuName[j];
                _td6.appendChild(_p);
            }
            _trbody.appendChild(_td6);

            const _td7 = document.createElement('td');
            _td7.setAttribute('width', '5%');
            if (response[i].orderStatus == null) {
                _td7.innerHTML = `<i id="${response[i].idOrderHeader}" name="${null}" class="bi bi-bookmark-plus-fill" style="color:goldenrod;border-radius: 50%;padding: 6px 10px; border:1px;"></i>`;
            }
            if (response[i].orderStatus == orderStatusAccepted) {
                _td7.innerHTML = `<i id="${response[i].idOrderHeader}" name="${orderStatusAccepted}" class="bi bi-bookmark-plus-fill" style="color:forestgreen;border-radius: 50%;padding: 6px 10px; border:1px;"></i>`;
            }
            if (response[i].orderStatus == orderStatusInWork) {
                _td7.innerHTML = `<i id="${response[i].idOrderHeader}" name="${orderStatusInWork}" class="bi bi-fire" style="color:green;border-radius: 50%;padding: 6px 10px; border:1px;"></i>`;
            }
            if (response[i].orderStatus == orderStatusReady) {
                _td7.innerHTML = `<i id="${response[i].idOrderHeader}" name="${orderStatusReady}" class="bi bi-fire"  style="color:red;border-radius: 50%;padding: 6px 10px; border:1px;"></i>`;
                }
            if (response[i].orderStatus == orderStatusASent) {
                _td7.innerHTML = `<i id="${response[i].idOrderHeader}" name="${orderStatusASent}" class="bi bi-car-front-fill" style="color:green;border-radius: 50%;padding: 6px 10px; border:1px;"></i>`;
                }
            _trbody.appendChild(_td7);

            const _td8 = document.createElement('td');
            _td8.setAttribute('width', '10%');
                const _div1 = document.createElement('div');
                _div1.setAttribute('class', 'col btn-group text-end" role="group');
                    const _div1_1 = document.createElement('div');
                    _div1_1.setAttribute('class', 'col');
                    if (response[i].orderStatus == null || response[i].orderStatus == orderStatusAccepted || response[i].orderStatus == orderStatusInWork) {
                        _div1_1.innerHTML = `<a id="${response[i].idOrderHeader}"  onclick="requestChangeStatus(${response[i].idOrderHeader},'${orderStatusAccepted}')"   class="btn statusAccepted" style="color:forestgreen;border-radius: 50%;padding: 6px 10px; border:1px;"><i class="bi bi-bookmark-plus-fill"></i></a>`;
                    }
                    if (response[i].orderStatus == orderStatusReady || response[i].orderStatus == orderStatusASent ) {
                        _div1_1.innerHTML = `<a id="${response[i].idOrderHeader}"  onclick="requestChangeStatus(${response[i].idOrderHeader},'${orderStatusASent}')"  class="btn statusSent" style="color:green;border-radius: 50%;padding: 6px 10px; border:1px;"><i class="bi bi-car-front-fill"></i></a>`;
                    }
                    _div1.appendChild(_div1_1)

        
                    const _div1_2 = document.createElement('div');
                    _div1_2.setAttribute('class', 'col');
                    _div1_2.innerHTML = `<a href="Home/Edit/${response[i].idOrderHeader}"  class="btn" style="color:royalblue;border-radius: 50%;padding: 6px 10px;"><i class="bi bi-pencil-square"></i></a>`;
                    _div1.appendChild(_div1_2)

                    const _div1_3 = document.createElement('div');
                    _div1_3.setAttribute('class', 'col');
                    _div1_3.innerHTML = ` <a href="Home/Delete/${response[i].idOrderHeader}"  class="btn " style="color:red;border-radius: 50%;padding: 6px 10px;"><i class="bi bi-trash3-fill"></i></a>`;
                    _div1.appendChild(_div1_3)


                _td8.appendChild(_div1);
            _trbody.appendChild(_td8);

        
        _tbody.appendChild(_trbody);
        
    }
    
    
    _table.appendChild(_tbody);

    document.getElementById('mytable').appendChild(_table);

}




function getAllStatus() {

    $.ajax({
        type: "GET",
        url: "/Home/GetAllStatus",
        

        success: function (data) {
            if (data != null) {

                const arrayAllStatus: Status[] = [];

                for (var i = 0; i < data.length; i++) {

                    var status = new Status();
                    status = data[i];
                    arrayAllStatus.push(status);

                }

                checkAllStatus(arrayAllStatus);

            }
        }

    });

}


function checkAllStatus(arrayAllStatus: Status[]) {

    $("[id][name]").each(function (index, element) {

        for (var i = 0; i < arrayAllStatus.length; i++) {
            if (arrayAllStatus[i].id == Number($(element).attr("id"))) {
                $(element).attr("name", `${arrayAllStatus[i].status}`)
            }

        }

    });

    $("[id][name]").each(function (index, element) {

        for (var i = 0; i < arrayAllStatus.length; i++) {
            if ($(element).attr("name") == orderStatusAccepted) {
                $(element).attr("class", "bi bi-bookmark-plus-fill");
                $(element).attr("style", "color:forestgreen;border-radius: 50%;padding: 6px 10px; border:1px;");
            }

            if ($(element).attr("name") == orderStatusInWork) {
                $(element).attr("class", "bi bi-fire");
                $(element).attr("style", "color:green;border-radius: 50%;padding: 6px 10px; border:1px;");
            }

            if ($(element).attr("name") == orderStatusReady) {
                $(element).attr("class", "bi bi-fire");
                $(element).attr("style", "color:red;border-radius: 50%;padding: 6px 10px; border:1px;");
            }

            if ($(element).attr("name") == orderStatusASent) {
                $(element).attr("class", "bi bi-car-front-fill");
                $(element).attr("style", "color:green;border-radius: 50%;padding: 6px 10px; border:1px;");
            }

        }
    });

    for (var i = 0; i < arrayAllStatus.length; i++) {
        if (arrayAllStatus[i].status == orderStatusInWork) {
            checkAllButtons(arrayAllStatus[i].id);
        }
    }

}



function checkAllButtons(id: number) {

    $(".statusAccepted").each(function (index, element) {

        if (id == Number($(element).attr("id"))) {
            $(element).attr("onclick", `requestChangeStatus(${id},'${orderStatusASent}')`);
            $(element).attr("class", "btn statusSent");
            $(element).attr("style", "color:green;border-radius: 50%;padding: 6px 10px; border:1px;");
            $(element).find("i").attr("class", "bi bi-car-front-fill");
        }

    });


}






function requestChangeStatus(id: number,status:string) {

    var statusId = new Status();

    statusId.id = id;
    statusId.status = status;

    $.ajax({


        type: "GET",
        url: "/Home/ChangeStatus",
        data: statusId,

        success: function (data) {
            if (data != null) {

            }
        }

    });
}





function getAllOrders() {
    $.ajax({

        type: "GET",
        url: "/Home/IndexAjax",


        success: function (data) {
            if (data != null) {

                var newArrayOrder = [];
                if (arrayOrderDetails.length==0) {
                    for (var i = 0; i < data.length; i++) {

                        arrayOrderDetails.push(data[i]);
                    }
                    table(arrayOrderDetails);
                }
                if (arrayOrderDetails.length > 0) {
                    for (var i = 0; i < data.length; i++) {

                        newArrayOrder.push(data[i]);
                    }
                    changeArrayOrders(newArrayOrder);
                }
                
               
            }
        }

    });
}



function changeArrayOrders(newArray: OrderDetailsIndex[]) {
    if (newArray.length > arrayOrderDetails.length) {
        for (var i = 0; i < arrayOrderDetails.length; i++) {
            for (var j = 0; j < newArray.length; j++) {
                if (arrayOrderDetails[i].idOrderHeader == newArray[j].idOrderHeader) {
                    var removedObject = newArray.splice(j, 1);
                }
            }
        }
        plusNewOrder(newArray);
    }
}


function plusNewOrder(response: OrderDetailsIndex[]) {

    


    for (var i = 0; i < response.length; i++) {

        let tablestring = [];
        tablestring[0] = response[i].street == null ? '' : " " + String(response[i].street);
        tablestring[1] = response[i].house == null ? '' : " дом." + String(response[i].house);
        tablestring[2] = response[i].entrance == null ? '' : " под." + String(response[i].entrance)
        tablestring[3] = response[i].floor == null ? '' : " эт." + String(response[i].floor);
        tablestring[4] = response[i].apartment == null ? '' : " кв." + String(response[i].apartment);

        const _trbody = document.createElement('tr');
        _trbody.setAttribute('id', `${response[i].idOrderHeader}`);
        _trbody.setAttribute('class', 'text-center tr');

        const _td1 = document.createElement('td');
        _td1.setAttribute('width', '5%');
        _td1.innerHTML = String(response[i].numberOrder);
        _trbody.appendChild(_td1);

        const _td2 = document.createElement('td');
        _td2.setAttribute('width', '7%');
        _td2.innerHTML = String(response[i].dateCreatedOrder);
        _trbody.appendChild(_td2);

        const _td3 = document.createElement('td');
        _td3.setAttribute('width', '10%');
        _td3.innerHTML = String(response[i].firstName) + " " + String(response[i].lastName);
        _trbody.appendChild(_td3);

        const _td4 = document.createElement('td');
        _td4.setAttribute('width', '10%');
        _td4.innerHTML = String(response[i].phoneNumber);
        _trbody.appendChild(_td4);

        const _td5 = document.createElement('td');
        _td5.setAttribute('width', '10%');
        var resultstring = '';
        for (var k = 0; k < tablestring.length; k++) {
            resultstring += tablestring[k];
        }
        _td5.innerHTML = resultstring;
        _trbody.appendChild(_td5);

        const _td6 = document.createElement('td');
        _td6.setAttribute('width', '15%');
        for (var j = 0; j < response[i].menuName.length; j++) {
            var _p = document.createElement('p');
            _p.textContent = response[i].menuName[j];
            _td6.appendChild(_p);
        }
        _trbody.appendChild(_td6);

        const _td7 = document.createElement('td');
        _td7.setAttribute('width', '5%');
        _td7.setAttribute('width', '5%');
        if (response[i].orderStatus == null || response[i].orderStatus == orderStatusAccepted) {
            _td7.innerHTML = `<i id="${response[i].idOrderHeader}" name="${orderStatusAccepted}" class="bi bi-bookmark-plus-fill" style="color:goldenrod;border-radius: 50%;padding: 6px 10px; border:1px;"></i>`;
        }
        _trbody.appendChild(_td7);

        const _td8 = document.createElement('td');
        _td8.setAttribute('width', '10%');
        const _div1 = document.createElement('div');
        _div1.setAttribute('class', 'col btn-group text-end" role="group');
        const _div1_1 = document.createElement('div');
        _div1_1.setAttribute('class', 'col');
        /*_div1_1.innerHTML = `<a onclick="requestChangeStatus(${response[i].idOrderHeader},'${orderStatusAccepted}')"  class="btn " style="color:forestgreen;border-radius: 50%;padding: 6px 10px; border:1px;"><i class="bi bi-bookmark-plus-fill"></i></a>`;*/
        _div1_1.innerHTML = `<a  class="btn " style="color:forestgreen;border-radius: 50%;padding: 6px 10px; border:1px;"><i class="bi bi-bookmark-plus-fill"></i></a>`;
        _div1.appendChild(_div1_1)


        const _div1_2 = document.createElement('div');
        _div1_2.setAttribute('class', 'col');
        _div1_2.innerHTML = `<a href="Home/Edit/${response[i].idOrderHeader}"  class="btn" style="color:royalblue;border-radius: 50%;padding: 6px 10px;"><i class="bi bi-pencil-square"></i></a>`;
        _div1.appendChild(_div1_2)

        const _div1_3 = document.createElement('div');
        _div1_3.setAttribute('class', 'col');
         _div1_3.innerHTML = ` <a href="Home/Delete/${response[i].idOrderHeader}"  class="btn " style="color:red;border-radius: 50%;padding: 6px 10px;"><i class="bi bi-trash3-fill"></i></a>`;
        _div1.appendChild(_div1_3)


        _td8.appendChild(_div1);
        _trbody.appendChild(_td8);

        document.getElementById('tbody').appendChild(_trbody);
    }
    for (var i = 0; i < response.length; i++) {
            arrayOrderDetails.push(response[i]);
        }
}



function minusNewOrder(id: number) {
    $("tr").each(function (index, element) {
        if (Number($(element).attr('id')) == id) {
            $(element).remove();
        } 
    });
    for (var i = 0; i < arrayOrderDetails.length; i++) {
        if (arrayOrderDetails[i].idOrderHeader == id) {
            var removed = arrayOrderDetails.splice(i, 1);
        }
    }
}




setInterval(getAllOrders, 2500);

setInterval(getAllStatus, 3500);

//function test() {
//    getAllOrders();
//    getAllStatus();
//}






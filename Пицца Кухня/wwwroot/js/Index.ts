
class OrderCount {
    menuName: string
    count: number
}

class ViewModelForIndex {
    idOrderHeader: number
    menuList: OrderCount[]
    orderStatus: string
    dateCreatedOrder: string
}

const orderStatusAccepted: string = "Принят";
const orderStatusInWork: string = "В работе";
const orderStatusReady: string = "Готов";
const orderStatusASent: string = "Отправлен";



function card(newOrders: ViewModelForIndex[]) {

    for (var i = 0; i < newOrders.length; i++) {

        const _card = document.createElement("div");
        _card.setAttribute('id', `${newOrders[i].idOrderHeader}`);
        _card.setAttribute('class', 'card p-2');
        _card.setAttribute('style', "border:2px solid #808080; border-radius: 10px;");

            // cardheader
            const _cardheader = document.createElement('div');
            _cardheader.setAttribute('class', 'card-header  text-light');
            _cardheader.setAttribute('style', "background-color:#222222;height:150px");

                const _cardheaderdiv1_1 = document.createElement('div');
                _cardheaderdiv1_1.setAttribute('class', 'row');

                    const _cardheaderdiv1_1h3 = document.createElement('h3');
                    _cardheaderdiv1_1h3.innerHTML = "Наряд-Заказ";
                    _cardheaderdiv1_1.appendChild(_cardheaderdiv1_1h3);

                    const _cardheaderdiv1_1hr = document.createElement('hr');
                    _cardheaderdiv1_1.appendChild(_cardheaderdiv1_1hr);


                _cardheader.appendChild(_cardheaderdiv1_1);

                const _cardheaderdiv1_2 = document.createElement('div');
                _cardheaderdiv1_2.setAttribute('class', 'row text-center');

                    const _cardheaderdiv1_2div2_1 = document.createElement('div');
                    _cardheaderdiv1_2div2_1.setAttribute('class', 'col-6');

                        const _cardheaderdiv1_2div2_1div3 = document.createElement('div');
                        _cardheaderdiv1_2div2_1div3.setAttribute('class', 'row');
                

                            const _cardheaderdiv1_2div2_1div3div4_1 = document.createElement('div');
                            _cardheaderdiv1_2div2_1div3div4_1.setAttribute('class', 'col-8');

                                const _cardheaderdiv1_2div2_1div3div4_1h5_1 = document.createElement('h5');
                                _cardheaderdiv1_2div2_1div3div4_1h5_1.innerHTML = "Название";
                                _cardheaderdiv1_2div2_1div3div4_1.appendChild(_cardheaderdiv1_2div2_1div3div4_1h5_1);

                            _cardheaderdiv1_2div2_1div3.appendChild(_cardheaderdiv1_2div2_1div3div4_1);


                            const _cardheaderdiv1_2div2_1div3div4_2 = document.createElement('div');
                            _cardheaderdiv1_2div2_1div3div4_2.setAttribute('class', 'col-4');

                                const _cardheaderdiv1_2div2_1div3div4_2h5_2 = document.createElement('h5');
                                _cardheaderdiv1_2div2_1div3div4_2h5_2.innerHTML = "Кол-ство";
                                _cardheaderdiv1_2div2_1div3div4_2.appendChild(_cardheaderdiv1_2div2_1div3div4_2h5_2);

                            _cardheaderdiv1_2div2_1div3.appendChild(_cardheaderdiv1_2div2_1div3div4_2);

                
                        _cardheaderdiv1_2div2_1.appendChild(_cardheaderdiv1_2div2_1div3);


                    _cardheaderdiv1_2.appendChild(_cardheaderdiv1_2div2_1);
                    //  
                    //
                    const _cardheaderdiv1_2div2_2 = document.createElement('div');
                    _cardheaderdiv1_2div2_2.setAttribute('class', 'col-4');

                        const _cardheaderdiv1_2div2_2h5 = document.createElement('h5');
                        _cardheaderdiv1_2div2_2h5.innerHTML = "Время";
                        _cardheaderdiv1_2div2_2.appendChild(_cardheaderdiv1_2div2_2h5);

                    _cardheaderdiv1_2.appendChild(_cardheaderdiv1_2div2_2);
                    //

                    //
                    const _cardheaderdiv1_2div2_3 = document.createElement('div');
                    _cardheaderdiv1_2div2_3.setAttribute('class', 'col-2');

                        const _cardheaderdiv1_2div2_3h5 = document.createElement('h5');
                        _cardheaderdiv1_2div2_3h5.innerHTML = "Статус";
                        _cardheaderdiv1_2div2_3.appendChild(_cardheaderdiv1_2div2_3h5);

                    _cardheaderdiv1_2.appendChild(_cardheaderdiv1_2div2_3);
                    //
                    const _cardheaderdiv1_2div2_4hr = document.createElement('hr');
                    _cardheaderdiv1_2.appendChild(_cardheaderdiv1_2div2_4hr);


                _cardheader.appendChild(_cardheaderdiv1_2);



            _card.appendChild(_cardheader);


        // cardbody
        const _cardbody = document.createElement('div');
        _cardbody.setAttribute('class', 'card-body');
        const _cardbodydiv1 = document.createElement('div');
        _cardbodydiv1.setAttribute('class', 'row text-center');


        const _cardbodydiv1div2_1 = document.createElement('div');
        _cardbodydiv1div2_1.setAttribute('class', 'col-6');

        for (var j = 0; j < newOrders[i].menuList.length; j++) {

            const _cardbodydiv1div2_1div3 = document.createElement('div');
            _cardbodydiv1div2_1div3.setAttribute('class', 'row');
            //
                const _cardbodydiv1div2_1div3div4_1 = document.createElement('div');
                _cardbodydiv1div2_1div3div4_1.setAttribute('class', 'col-8');

                    const _cardbodydiv1div2_1div3div4_1h4 = document.createElement('h4');
                    _cardbodydiv1div2_1div3div4_1h4.innerHTML = `${newOrders[i].menuList[j].menuName}`;
                    _cardbodydiv1div2_1div3div4_1.appendChild(_cardbodydiv1div2_1div3div4_1h4);

                    const _cardbodydiv1div2_1div3div4_1br = document.createElement('br');
                    _cardbodydiv1div2_1div3div4_1.appendChild(_cardbodydiv1div2_1div3div4_1br);

                _cardbodydiv1div2_1div3.appendChild(_cardbodydiv1div2_1div3div4_1);

            //

            //
                const _cardbodydiv1div2_1div3div4_2 = document.createElement('div');
                _cardbodydiv1div2_1div3div4_2.setAttribute('class', 'col-4');

                    const _cardbodydiv1div2_1div3div4_2h4 = document.createElement('h4');
                    _cardbodydiv1div2_1div3div4_2h4.innerHTML = `${newOrders[i].menuList[j].count}`;
                    _cardbodydiv1div2_1div3div4_2.appendChild(_cardbodydiv1div2_1div3div4_2h4);

                    const _cardbodydiv1div2_1div3div4_2br = document.createElement('br');
                    _cardbodydiv1div2_1div3div4_2.appendChild(_cardbodydiv1div2_1div3div4_2br);

                _cardbodydiv1div2_1div3.appendChild(_cardbodydiv1div2_1div3div4_2);

            //
            _cardbodydiv1div2_1.appendChild(_cardbodydiv1div2_1div3);
        }
        

        _cardbodydiv1.appendChild(_cardbodydiv1div2_1);


            const _cardbodydiv1div2_2 = document.createElement('div');
            _cardbodydiv1div2_2.setAttribute('class', 'col-4');

                const _cardbodydiv1div2_2h4 = document.createElement('h4');
                _cardbodydiv1div2_2h4.innerHTML = `${newOrders[i].dateCreatedOrder}`;
                _cardbodydiv1div2_2.appendChild(_cardbodydiv1div2_2h4);

            _cardbodydiv1.appendChild(_cardbodydiv1div2_2);



            const _cardbodydiv1div2_3 = document.createElement('div');
            _cardbodydiv1div2_3.setAttribute('class', 'col-2');

                const _cardbodydiv1div2_3h4 = document.createElement('h4');
                _cardbodydiv1div2_3h4.setAttribute('id', `${newOrders[i].idOrderHeader}`);
                _cardbodydiv1div2_3h4.setAttribute('class', "statusOrder");
                _cardbodydiv1div2_3h4.innerHTML = `${newOrders[i].orderStatus}`;
                _cardbodydiv1div2_3.appendChild(_cardbodydiv1div2_3h4);

            _cardbodydiv1.appendChild(_cardbodydiv1div2_3);



        _cardbody.appendChild(_cardbodydiv1);
        _card.appendChild(_cardbody);


        // cardhfooter
        const _cardfooter = document.createElement('div');
        _cardfooter.setAttribute('class', 'card-footer');
                const _cardfooterdiv1 = document.createElement('div');
                _cardfooterdiv1.setAttribute('class', 'row text-center');

                    const _cardfooterdiv1div2 = document.createElement('div');
                    _cardfooterdiv1div2.innerHTML = ` <a id="${newOrders[i].idOrderHeader}"  href="Home/InWork/${newOrders[i].idOrderHeader}"  class="btn btn-primary buttonOrder" style="width:250px;border-radius: 20px;">Взять в работу</a>`;
                    _cardfooterdiv1.appendChild(_cardfooterdiv1div2);

                _cardfooter.appendChild(_cardfooterdiv1);


                const _cardfooterhr = document.createElement('hr');
                _cardfooter.appendChild(_cardfooterhr);

        _card.appendChild(_cardfooter);


        document.getElementById("cardOrder").appendChild(_card);

    }

}








function getAllOrders() {

    $.ajax({

        type: "GET",
        url: "/Home/IndexAjax",

        success: function (data) {
            if (data != null) {

                var oldArrayViewModels = SetOldArrayViewModels();

                const arrayViewModels: ViewModelForIndex[] = []

                for (var i = 0; i < data.length; i++) {

                    arrayViewModels.push(data[i]);

                }

                checkStatusOrders(arrayViewModels);

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

    });
}



function SetOldArrayViewModels() {

    const oldListOrders: number[] = [];

    $(".card.p-2").each(function (index, element) {

        oldListOrders.push(Number($(element).attr("id"))); 

    });


    return oldListOrders;
}





function checkOrdersForAdd(oldArrayViewModels: number[], arrayViewModels: ViewModelForIndex[]) {

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





function checkOrdersForRemoved(oldArrayViewModels: number[], arrayViewModels: ViewModelForIndex[]) {

    for (var i = 0; i < arrayViewModels.length; i++) {

        for (var j = 0; j < oldArrayViewModels.length; j++) {

            if (oldArrayViewModels[j] == arrayViewModels[i].idOrderHeader) {
                var removedObject = oldArrayViewModels.splice(j, 1);
            }

        }

    }

    return oldArrayViewModels;

}






function checkStatusOrders(arrayViewModels: ViewModelForIndex[]) {

    $(".statusOrder").each(function (index, element) {
        for (var i = 0; i < arrayViewModels.length; i++) {

            if (Number($(element).attr("id")) == arrayViewModels[i].idOrderHeader) {

                $(element).text(`${arrayViewModels[i].orderStatus}`);
                if ($(element).text() == orderStatusInWork) {

                    $(".buttonOrder").each(function (index, element2) {

                        if (Number($(element2).attr("id")) == arrayViewModels[i].idOrderHeader) {
                            $(element2).attr("class", "btn btn-primary disabled buttonOrder");
                            $(element2).text("Уже в работе");
                        }

                    });
                }

            }

        }
  
    });

}







function deletingOrders(idOrder: number[]) {

    $(".card").each(function (index, element) {

        for (var i = 0; i < idOrder.length; i++) {
            if (Number($(element).attr("id")) == idOrder[i]) {
                $(element).remove();
            }
        }

    });

}





function addingOrders(newViewModels: ViewModelForIndex[]) {

    card(newViewModels);

}







setInterval(getAllOrders, 1000);
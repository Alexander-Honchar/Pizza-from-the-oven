


class OrderStatus {
    id: number
    status: string
}

class IsStatus {
    isSuccess: boolean
    status: string
}





function setOrderStatus(id: number, status: string) {

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




function changeStatus(newStatus: string) {

    $(".statusOrder").text(`${newStatus}`);
    $(".StatusInWork").attr("class",'btn btn-primary disabled StatusInWork')

}



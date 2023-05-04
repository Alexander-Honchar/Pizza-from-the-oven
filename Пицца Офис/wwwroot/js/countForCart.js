class Cart {
}
var responsObject = new Array();
function changeCount(listOrderDetails, IsChangeCount) {
    if (IsChangeCount) {
        const oldListOrders = [];
        $(".count").each(function (index, element) {
            var oldOrder = new Cart();
            oldOrder.id = Number($(element).attr("id"));
            oldOrder.count = Number($(element).text());
            oldListOrders.push(oldOrder);
        });
        for (var i = 0; i < listOrderDetails.length; i++) {
            for (var j = 0; j < oldListOrders.length; j++) {
                if (oldListOrders[j].id == listOrderDetails[i].id) {
                    var removed = oldListOrders.splice(j, 1);
                }
            }
        }
        for (var i = 0; i < listOrderDetails.length; i++) {
            $(".count").each(function (index, element) {
                if (Number($(element).attr("id")) == listOrderDetails[i].id) {
                    var count = listOrderDetails[i].count;
                    $(element).text(count);
                }
            });
        }
        for (var i = 0; i < oldListOrders.length; i++) {
            $(".count").each(function (index, element) {
                if (Number($(element).attr("id")) == oldListOrders[i].id) {
                    $(element).text("X").css('color', 'red');
                }
            });
        }
    }
}
$('a').click(function () {
    var clickId = Number($(this).attr('id'));
    var name = $(this).attr('name');
    var valueCount = 0;
    /* var IsValueCountZero = false;*/
    if (name == 'plus') {
        $('.count').each(function (index, element) {
            var countId = Number($(element).attr('id'));
            var valueCountThis = Number($(element).text());
            if (clickId == countId) {
                valueCount = valueCountThis + 1;
                //sessionStorage.setItem("SaveCount", String(valueCount));
                //sessionStorage.setItem("SaveIdOfClick", String(clickId));
            }
        });
    }
    if (name == 'minus') {
        $('.count').each(function (index, element) {
            var countId = Number($(element).attr('id'));
            var valueCountThis = Number($(element).text());
            if (clickId == countId) {
                valueCount = valueCountThis - 1;
                //if (valueCount == 0) {
                //    IsValueCountZero = true;
                //}
                //sessionStorage.setItem("SaveCount", String(valueCount));
                //sessionStorage.setItem("SaveIdOfClick", String(clickId));
            }
        });
    }
    if (name == 'trash') {
        $('.count').each(function (index, element) {
            var countId = Number($(element).attr('id'));
            if (clickId == countId) {
                valueCount = 0;
                //sessionStorage.setItem("SaveCount", String(valueCount));
                //sessionStorage.setItem("SaveIdOfClick", String(clickId));
                //IsValueCountZero = true;
            }
        });
    }
    /*sessionStorage.setItem("IsValueCountZero", String(IsValueCountZero));*/
    /*setNewCount(clickId, Number(sessionStorage.getItem("SaveCount")));*/
    setNewCount(clickId, valueCount);
});
function setNewCount(id, count) {
    var newCount = new Cart();
    newCount.id = id;
    newCount.count = count;
    $.ajax({
        type: "GET",
        url: "/Home/Count",
        data: newCount,
        success: function (data) {
            if (data != null) {
                const listOrderDetails = [];
                responsObject = data.arrayOrderDetails;
                for (var i = 0; i < responsObject.length; i++) {
                    listOrderDetails.push(responsObject[i]);
                }
                var IsResult = data.isSuccess;
                changeCount(listOrderDetails, IsResult);
            }
        }
    });
}
//# sourceMappingURL=countForCart.js.map

var databool = false;
function countFunc(databool) {
    if (databool) {
        $(".count").each(function (index, element) {
            var clickId = sessionStorage.getItem("SaveIdOfClick");
            var count = sessionStorage.getItem("SaveCount");
            var countId = $(element).attr('id');
            if (countId == clickId) {

                $(element).text(count);
            }
        });
    }
  
}




$(function Count() {
    $('a').click(function () {

        var clickId = $(this).attr('id');
        sessionStorage.setItem("SaveIdOfClick", clickId);
        var name = $(this).attr("name");
        
        if (name == 'plus')
        {
            var countForPlus;

            $.each($(".count") ,function (index, element) {

                var countId = $(element).attr('id');
                var valueCount = Number($(element).text()) ;
                if (countId == clickId) {

                    countForPlus = valueCount + 1;
                    sessionStorage.setItem("SaveCount", countForPlus);
                }
                
            });

            
            getCount(clickId, countForPlus);
           
        }
        //if (name == 'minus')
        //{
        //    minus(clickId);
        //}
        //if (name == 'trash')
        //{
        //    trash(clickId);
        //}

        //alert(name + " " + clickId);
    });
});





function getCount(id,count) {
    
    var orderDetalis = new Object();
    orderDetalis.Id = id;
    orderDetalis.Count = count;

    $.ajax({

        type: "GET",
        url: "/Home/Count",
        data: orderDetalis,
        success: function (data) {

            if (data != null) {

                countFunc(data);
            }
            
        },
        failure: function (err) {
            alert(err);
        }

    });


    /*alert("plus" + id)*/
}






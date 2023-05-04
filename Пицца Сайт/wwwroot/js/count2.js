

var globalCount;


$(function getCount() {
	globalCount = $("#MenuItem_Count").val();
});



$("#plus").click(function plus() {
	if (typeof globalCount === 'undefined') {
		globalCount = 1;
	}
	globalCount++;
	$("#MenuItem_Count").val(globalCount);
	$("#Count").val(globalCount);
	$("#count").text('Количество  ' + globalCount + ' шт.');
	$
});



$("#minus").click(function minus() {
	
	globalCount--;
	$("#MenuItem_Count").val(globalCount);
	$("#Count").val(globalCount);
	if (globalCount > 0) {
		$("#count").text('Количество  ' + globalCount + ' шт.');
	}
	else {
		globalCount = 1;
	}
});


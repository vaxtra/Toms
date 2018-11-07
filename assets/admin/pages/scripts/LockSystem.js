var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    Preload();
    
    $(".LockSystem").click(function () {
        bootbox.confirm("Are you sure ?", function (result) {
            if (result) {
                LockSystem();
            }
        });
        
    });

    setTimeout(function () {
        // that's enough of that
        bootbox.hideAll();
    }, 2000);
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            //$.each(result.d.data.EmailConfiguration, function (indexInArray, valueOfElement) {
            //    $("input[name=" + indexInArray + "]").val(valueOfElement);
            //});
            var SystemStatus = result.d.data.SystemStatus;
            if (SystemStatus == "true") {
                $(".SystemStatusMessage").html("<i class='fa fa-lock'></i> LOCKED");
                $(".LockSystem").text("UNLOCK SYSTEM");
                $(".LockSystem").removeClass("red");
                $(".LockSystem").addClass("green");
            }
            else {
                $(".SystemStatusMessage").html("<i class='fa fa-unlock'></i> UNLOCKED");
                $(".LockSystem").text("LOCK SYSTEM");
                $(".LockSystem").removeClass("green");
                $(".LockSystem").addClass("red");
            }
        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'RequestData': ['SystemStatus']
        }
    });
}

function LockSystem() {
    REST.onSuccess = function (result) {
        bootbox.alert(result.d.message, function (e) {
            window.location = 'Default.aspx';
        });
    };
    REST.sendRequest({
        'c': 'beconf',
        'm': 'lock',
        'data': ''
    });
}
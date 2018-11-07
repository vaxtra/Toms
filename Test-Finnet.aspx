<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test-Finnet.aspx.cs" Inherits="Test_Finnet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h5 class="status"></h5>
    <label class="result"></label>
    <script src="assets/frontend/js/jquery.min.js"></script>

    <script>
        $(document).ready(function () {
            $.ajax({
                url: "/modules/Finnet/FinnetHandler.ashx",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    c: "finnet",
                    m: "transaction",
                    data: {
                        sof_id: "finpaytsel",
                        sof_type: "pay"
                    }
                }),
                beforeSend: function () {
                    //Metronic.blockUI({
                    //    boxed: true
                    //});
                },
                success: function (result) {
                    if (result.success) {
                        if (result.data.success) {
                            //$(".status").text
                        }
                        else {
                            //bootbox.alert(result.d.message);
                            bootbox.alert(result.data.message, function () {
                                location.reload();
                            });
                        }
                    }
                },
                complete: function () {
                    Metronic.unblockUI();
                },
                error: function (result) {
                    bootbox.alert(result.d.message, function () {
                        location.reload();
                    });
                }
            });
        });

    </script>
</body>
</html>

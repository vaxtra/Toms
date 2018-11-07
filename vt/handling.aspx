<%@ Page Language="C#" AutoEventWireup="true" CodeFile="handling.aspx.cs" Inherits="vt_handling" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </form>

<%--    <script src="/assets/frontend/js/jquery.min.js"></script>
    <script src="/assets/frontend/js/bootstrap.min.js"></script>

    <script>
        $(document).ready(function () {
            $.ajax({
                url: "/vt/handling.aspx/HandlerVA",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    status_code: "200",
                    status_message: "Veritrans payment notification",
                    transaction_id: "c62bd648-8699-440c-abf1-0ab8913b49f8",
                    order_id: "0316-0000834",
                    payment_type: "credit_card",
                    transaction_time: "2016-03-17 14:06:07 +0700",
                    transaction_status: "capture",
                    fraud_status: "ACCEPT",
                    masked_card: "481111-1114",
                    gross_amount: "609000.00",
                    signature_key: "19178dacf50667a72ab8707cfd98c9aebb6c55ca570e94fda0b37539d88b346131eef8c5af6116676ca65a51747bf385afd265aac19c8851f0628a8f62ce0914"
                }),
                success: function (result) {
                    if (result.success) {
                        if (result.data.data) {
                            alert(result.data.message);
                        }
                        else {
                            alert(result.data.message);
                        }
                    }
                },
                error: function (result) {
                    $(".error").html(result.message);
                }
            });
        });
    </script>--%>
</body>
</html>

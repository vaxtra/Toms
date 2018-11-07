<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WCP.aspx.cs" Inherits="api_WCP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>

        <script src="/assets/frontend/js/jquery.min.js"></script>
    <script src="/assets/frontend/js/jquery-ui.js"></script>


    <script src="/assets/frontend/js/bootstrap.min.js"></script>
    <script src="/assets/frontend/js/owl.carousel.js"></script>
    <script src='/assets/frontend/js/jquery.themepunch.tools.min.js' type='text/javascript'></script>
    <script src='/assets/frontend/js/jquery.themepunch.revolution.js' type='text/javascript'></script>
    <script src='/assets/frontend/js/settings.js' type='text/javascript'></script>
    <script src="/assets/frontend/js/jquery.fancybox.js"></script>
    <script src="/assets/frontend/js/isotope.min.js"></script>
    <script src="/assets/frontend/js/masonry.js"></script>
    <script src="/assets/frontend/js/jquery.stellar.js"></script>
    <script src="/assets/frontend/js/wow.js"></script>
    <script src="/assets/frontend/js/scripts.js"></script>
    <script src="/assets/frontend/js/my-instagram-gallery.js"></script>
    <script src="/assets/frontend/js/wit.js"></script>

    <script>
        $(document).ready(function () {
            $.ajax({
                url: "./WCP.aspx/GetProductWCP",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    token: "d1ulb8GmKVLGSSAWlEzQRFfwi2a93v3ChH2+JmM8m3hDbalH99e3Vq95zR9LSEylpvxxLVevziB44q/wEnVZxg==",
                    clientKey: "witxdemo"
                }),
                success: function (result) {
                    document.write("success");
                },
                error: function (result) {
                    document.write("error");
                }
            });
        });
    </script>
</body>
</html>

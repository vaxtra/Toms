<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="adminwitcommerce_Login" %>

<!DOCTYPE html>
<html lang="en" class="no-js">
<head>
    <meta charset="utf-8" />
    <title>Administration Panel - WIT. Commerce</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/simple-line-icons/simple-line-icons.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/uniform/css/uniform.default.css" />

    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/select2/select2.css" />
    <link href="/assets/admin/pages/css/login.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="/assets/global/css/components.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/css/plugins.css" />
    <link rel="stylesheet" type="text/css" href="/assets/admin/layout/css/layout.css" />
    <link rel="stylesheet" type="text/css" href="/assets/admin/layout/css/themes/default.css" />
    <link rel="stylesheet" type="text/css" href="/assets/admin/layout/css/custom.css" />
    <link rel="shortcut icon" href="/assets/global/img/favicon.png" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body class="login">
    <div class="logo">
        <img src="/assets/admin/layout/img/logo-big.png" alt="" />
    </div>
    <div class="menu-toggler sidebar-toggler">
    </div>
    <div class="content">
        <form class="login-form" action="index.html" method="post">
            <h3 class="form-title">Administrator Panel</h3>
            <div class="alert alert-danger display-hide">
                <button class="close" data-close="alert"></button>
                <span>Enter any username and password. </span>
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Username</label>
                <div class="input-icon">
                    <i class="fa fa-envelope"></i>
                    <input class="form-control placeholder-no-fix" type="text" autocomplete="off" placeholder="Username" name="username" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Password</label>
                <div class="input-icon">
                    <i class="fa fa-key"></i>
                    <input class="form-control placeholder-no-fix" type="password" autocomplete="off" placeholder="Password" name="password" />
                </div>
            </div>
            <div class="form-actions">
                <div class="form-group">
                    <label class="checkbox">
                        <input type="checkbox" name="remember" value="1" />
                        Remember me
                    </label>

                    <span class="checkbox pull-right">
                        <a href="javascript:;" id="forget-password">Loss Password</a>
                    </span>

                    <button type="submit" class="btn btn-block btn-primary">
                        <i class="fa fa-check"></i>&nbsp;Sign In
                    </button>
                </div>
            </div>
        </form>
        <form class="forget-form" action="index.html" method="post">
            <h3 class="form-title">Forgot your password?</h3>
            <div class="alert alert-danger">
                <span>In order to receive your access code by email, please enter the address you provided during the registration process. </span>
            </div>
            <div class="form-group">
                <div class="input-icon">
                    <i class="fa fa-envelope"></i>
                    <input class="form-control placeholder-no-fix" type="text" autocomplete="off" placeholder="Email" name="email" />
                </div>
            </div>
            <div class="form-actions">
                <button type="button" id="back-btn" class="btn btn-sm btn-default">
                    <i class="fa fa-angle-double-left"></i>&nbsp;Back
                </button>
                <button type="submit" class="btn btn-sm btn-primary pull-right">
                    Send <i class="fa fa-angle-double-right"></i>
                </button>
            </div>
        </form>
    </div>
    <div class="copyright">
        2014 &copy; WIT. Commerce
    </div>
    <script src="/assets/global/plugins/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>

    <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/assets/global/plugins/select2/select2.min.js"></script>

    <script src="/assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="/assets/admin/layout/scripts/layout.js" type="text/javascript"></script>
    <script src="/assets/admin/pages/scripts/login.js" type="text/javascript"></script>

    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Login.init();
        });
    </script>
</body>
</html>

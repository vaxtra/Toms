<%@ Page Title="Collection - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="CollectionDetail.aspx.cs" Inherits="CollectionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="about-us wow fadeInUp">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 title-box">
                    <h2><asp:Label ID="LabelTitlePost" runat="server" Text=""></asp:Label></h2>
                </div>
                <div class="col-md-12 padtopbot50">
                    <asp:Label ID="Labelcontent" runat="server" Text=""></asp:Label>
                </div>
            <!-- end row -->
        </div>
            </div>
        <!-- end container -->
    </section>
    <section>
        <asp:ListView ID="lvCollection" runat="server">
            <ItemTemplate>
                <div class="floatleft wow fadeInUp">
                    <img src="assets/images/post/<%# Eval("MediaUrl") %>" class="full-img" alt="Image">
                </div>
            </ItemTemplate>
        </asp:ListView>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script type="text/javascript">
        $(function () {
            /* 
             * just for this demo:
             */
            $('#showcode').toggle(
                function () {
                    $(this).addClass('up').removeClass('down').next().slideDown();
                },
                function () {
                    $(this).addClass('down').removeClass('up').next().slideUp();
                }
            );
            $('#panel').toggle(
                function () {
                    $(this).addClass('show').removeClass('hide');
                    $('#overlay').stop().animate({ left: -$('#overlay').width() + 20 + 'px' }, 300);
                },
                function () {
                    $(this).addClass('hide').removeClass('show');
                    $('#overlay').stop().animate({ left: '0px' }, 300);
                }
            );

            var $container = $('#am-container'),
                $imgs = $container.find('img').hide(),
                totalImgs = $imgs.length,
                cnt = 0;

            $imgs.each(function (i) {
                var $img = $(this);
                $('<img/>').load(function () {
                    ++cnt;
                    if (cnt === totalImgs) {
                        $imgs.show();
                        $container.montage({
                            fillLastRow: true,
                            alternateHeight: true,
                            alternateHeightRange: {
                                min: 90,
                                max: 240
                            },
                            margin: 0
                        });

                        /* 
                         * just for this demo:
                         */
                        $('#overlay').fadeIn(500);
                    }
                }).attr('src', $img.attr('src'));
            });
        });
		</script>
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>


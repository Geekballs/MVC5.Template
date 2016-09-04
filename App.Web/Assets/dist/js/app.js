/* -------------------------------------------------- */
/* variables                                          */
/* -------------------------------------------------- */
var ajaxLoader = "#ajax-loader";
var loadTimeout;
var timeoutValue = 2000;
var alertContainer = 'alert-container';
var alertFloat = '.alert-float';
var message = 'Network busy or large data requested! Please wait...';
/* -------------------------------------------------- */

/* -------------------------------------------------- */
/* bootstrap alerts                                   */
/* -------------------------------------------------- */
function BootstrapAlert(data) {

    var alertContainer = 'alert-container';
    var alertFloat = '.alert-float';
    var danger = 1;
    var info = 2;
    var success = 3;
    var warning = 4;

    /* danger */
    if (data.type === danger) {
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-danger\'><button type=\'button\' class=\'close\' data-dismiss=\'alert\'>×</button>' + '<p> <span class=\'fa fa-times-circle fa-fw fa-lg\'></span>' + data.message + '<p/></div>';
        window.setTimeout(function () {
            $(alertFloat).fadeOut(2000, 0).slideUp(2000, function () {
                $(this).remove();
            });
        }, 2000);
    }

        /* info */
    else if (data.type === info) {
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-info\'><button type=\'button\' class=\'close\' data-dismiss=\'alert\'>×</button>' + '<p> <span class=\'fa fa-info-circle fa-fw fa-lg\'></span>' + data.message + '<p/></div>';
        window.setTimeout(function () {
            $(alertFloat).fadeOut(2000, 0).slideUp(2000, function () {
                $(this).remove();
            });
        }, 2000);
    }

        /* success */
    else if (data.type === success) {
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-success\'><button type=\'button\' class=\'close\' data-dismiss=\'alert\'>×</button>' + '<p> <span class=\'fa fa-check-circle fa-fw fa-lg\'></span>' + data.message + '<p/></div>';
        window.setTimeout(function () {
            $(alertFloat).fadeOut(2000, 0).slideUp(2000, function () {
                $(this).remove();
            });
        }, 2000);
    }

        /* Warning */
    else if (data.type === warning) {
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-warning\'><button type=\'button\' class=\'close\' data-dismiss=\'alert\'>×</button>' + '<p> <span class=\'fa fa-exclamation-triangle fa-fw fa-lg\'></span>' + data.message + '<p/></div>';
        window.setTimeout(function () {
            $(alertFloat).fadeOut(2000, 0).slideUp(2000, function () {
                $(this).remove();
            });
        }, 2000);
    }
}
/* -------------------------------------------------- */

/* -------------------------------------------------- */
/* ajax helper                                        */
/* -------------------------------------------------- */
function AjaxHelper(url, dataType, type, async, element) {
    $.ajax({
        url: url,
        dataType: dataType,
        type: type,
        async: async,
        success: function (data) {
            $(element).html(data);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert('Error!');
        }
    });
}
/* -------------------------------------------------- */

/* -------------------------------------------------- */
/* ajax begin                                         */
/* -------------------------------------------------- */
$(document).ajaxStart(function () {
    $(ajaxLoader).removeClass("fa fa-fw fa-lg fa-bolt");
    $(ajaxLoader).addClass("fa fa-fw fa-lg fa-cog fa-spin");
    loadTimeout = setTimeout(function () {
        $(ajaxLoader).addClass("fa fa-fw fa-lg fa-cog fa-spin");
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-warning\'>' + '<p> <span class=\'fa fa-fw fa-lg fa-info-circle\'></span>' + message + '<p/></div>';
    }, timeoutValue);

    console.log("Ajaxt Request Started - " + new Date($.now()));
});
/* -------------------------------------------------- */

/* -------------------------------------------------- */
/* ajax complete                                      */
/* -------------------------------------------------- */
$(document).ajaxComplete(function () {
    clearTimeout(loadTimeout);
    setTimeout(function () {
        $(ajaxLoader).removeClass("fa fa-fw fa-lg fa-cog fa-spin");
        $(ajaxLoader).addClass("fa fa-fw fa-lg fa-bolt");
        $(alertFloat).fadeOut(500, 0).slideUp(500, function () {
            $(this).remove();
        });
        console.log("Ajax Request Completed - " + new Date($.now()));
    }, 250);
});
/* -------------------------------------------------- */

/* -------------------------------------------------- */
/* sub nav                                            */
/* -------------------------------------------------- */
function ActivateSubNav(enabled) {
    if (enabled === true) {
        $('.toggle-sub-nav')
            .click(function () {
                $(this).find('i').toggleClass('fa-caret-down fa-caret-up');
                $('.sub-nav-content').slideToggle(250);
                return false;
            });
    } else {
        $('.toggle-sub-nav').find('i').removeClass('fa-caret-down fa-caret-up');
        $('.toggle-sub-nav').removeAttr('href');
    }
};
/* -------------------------------------------------- */

/* -------------------------------------------------- */
/* bootsrap tooltip                                   */
/* -------------------------------------------------- */
$(function () { $('[data-toggle="tooltip"]').tooltip({ 'delay': { show: 1000, hide: 100 } }); });
/* -------------------------------------------------- */

/* -------------------------------------------------- */
/* bootsrap popover                                   */
/* -------------------------------------------------- */
$(function () { $("[data-toggle=popover]").popover({ trigger: "hover" }); });
/* -------------------------------------------------- */


/* -------------------------------------------------- */
/* scroll to top                                      */
/* -------------------------------------------------- */
var fixed = false;
$(document).scroll(function () {
    if ($(this).scrollTop() > 250) {
        if (!fixed) {
            fixed = true;
            $('#scroll-to-top').show("slow", function () {
                $('#scroll-to-top').css({
                    position: 'fixed',
                    display: 'block'
                });
            });
        }
    } else {
        if (fixed) {
            fixed = false;
            $('#scroll-to-top').hide("slow", function () {
                $('#scroll-to-top').css({
                    display: 'none'
                });
            });
        }
    }
});
/* -------------------------------------------------- */
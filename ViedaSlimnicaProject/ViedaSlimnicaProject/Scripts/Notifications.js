//$(document).ready(function () {
//    var chat   = $.connection.messagesHub;
//    var username = $('#username').html()
//    $.connection.messagesHub.qs = { 'username' : username };
//});

$(function () {
    var chat = $.connection.messagesHub;
    $.connection.hub.logging = true;
    chat.client.receiveNotification = function (message,count) {
        console.log('hi' + message);
        $('#notification-body').html(message);
        $('#notification').css('display', 'block');
        $('#notification').animate({
            top: '70px',
            opacity: '1'
        });
        $('#zinojumiNav').html('Ziņojumi(' + count + ')');
        document.title = 'Jauna ziņa!';
    }
    chat.client.updateMessageCount = function (count) {
        console.log(count);
        $('#zinojumiNav').html('Ziņojumi(' + count + ')');
    }
    $.connection.hub.start().done(function () {
        chat.server.messageUpdate();
    });
});

function closeNotif() {
    $('#notification').animate({
        top: '-=100px',
        opacity: '0'
    }, 400, function () { $('#notification').css('display', 'none'); });
}
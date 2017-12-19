function ShowBooks()
{
    $("#lbWait").css("display", "inherit");
    $.ajax({
        
        url: "http://localhost:51746/api/books",
        type: "GET",
        dataType: "json",
        success: function (data) {
            if (Array.isArray(data)) {
                $("#tableBooks tbody").empty();
                for (var i = 0; i < data.length; i++) {
                    $('#tableBooks').children("tbody").append("<tr class=\"book-row\">" +
                                                                  "<td>" + data[i].TitleBook + "</td>" +
                                                                  "<td>" + data[i].Genre +     "</td>" +
                                                                  "<td>" + data[i].Author + "</td>" +
                                                                  "<td>" + new Date(data[i].DateRealise).getFullYear() + "</td>" +
                                                              "</tr>");
                }
                $("#lbWait").css("display", "none");
            } else {
                $("#lb_Error").html("Некорректный ответ от веб-сервиса.");
            }
        },
        error: function()
        { $("#lb_Error").html("Ошибка соединения с веб-сервисом."); }
    });
    
}
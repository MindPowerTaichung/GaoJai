    function showChangePasswordWindow(userName) {

        var wnd = $("#workspace_changePassword").data("kendoWindow"),
            detailsTemplate;
        debugger;
        if (wnd == undefined)
            wnd = $("#workspace_changePassword")
                            .kendoWindow({
                                title: "使用者名稱與密碼",
                                modal: true,
                                visible: false,
                                resizable: false,
                                width: 300
                            }).data("kendoWindow");

        detailsTemplate = kendo.template($("#changePassword-template").html());

        wnd.content(detailsTemplate({ "userName": userName}));
        wnd.center().open();
    }

    function closeChangePasswordWindow() {
        $("#workspace_changePassword").data("kendoWindow").close();
    }

    function changePassword() {
        var password = $("#txtPassword").val();
        var userName = $("#txtUserName").val();

        var data = {
            "userName": userName,
            "password": password
        };

        var menus_apiUrl = "Membership/Users/";
        $.ajax({
            type: "PUT",
            url: menus_apiUrl + "/" + userName,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status, jqXHR) {
                //alert("PUT success..." + data);
                closeChangePasswordWindow();
            },
            error: function (xhr) {
                alert(xhr.responseText);
            }
        });

    }

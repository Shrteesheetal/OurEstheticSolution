if (!window.app) {
    window.app = {};
}

window.app.service = (function ($) {
    debugger;
    var initUi = function () {
        GetService();
        GetServiceList();
        $('#UpdateService').hide();
    };

    var initEvent = function () {
        debugger;
        $("#CreateService").on("click", InsertService);
        $("#UpdateService").on("click", UpdateService);
        $("#deleteservice").on("click", DeleteService);

        $(document).on("click", ".editservice", function () {
            debugger;
            var id = $(this).data('id');
            GetServiceById(id);
        });

        $(document).on("click", ".deleteservice", function () {
            var id = $(this).data('id');
            $('#DeleteServiceId').val(id);
        });

        $(document).on("click", "input[type='reset']", Reset);
    };

    function getFormData() {
        debugger;
        return {
            Id: $('#ServiceId').val(),
            Name: $('#Name').val(),
            TimePeriod: $('#TimePeriod').val(),
            TotalCost: $('#TotalCost').val(),
            Tools: $('#Tools').val(),
            Description: $('#Description').val(),
            CreatedBy: $('#CreatedBy').val()
        };
    }

    var InsertService = function (e) {
        debugger;
        e.preventDefault();
        var formData = getFormData();
        $.ajax({
            type: "POST",
            url: '/Service/Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    Reset();
                    GetServiceList();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to create service.");
            }
        });
    };

    var UpdateService = function (e) {
        debugger;
        e.preventDefault();
        var formData = getFormData();
        $.ajax({
            type: "POST",
            url: '/Service/UpdateService',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    Reset();
                    GetServiceList();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to update service.");
            }
        });
    };

    var Reset = function () {
        debugger;
        $('#ServiceId').val('');
        $('#DeleteServiceId').val('');
        $('#Name').val('');
        $('#TimePeriod').val('');
        $('#TotalCost').val('');
        $('#Tools').val('');
        $('#Description').val('');
        $('#CreatedBy').val('');
        $('#CreateService').show();
        $('#UpdateService').hide();
    };

    var GetServiceList = function () {
        debugger;
        $.ajax({
            url: '/Service/AllService',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var rows = '';
                var i = 1;
                $.each(data, function (index, item) {
                    rows += '<tr>';
                    rows += '<td>' + i + '</td>';
                    rows += '<td>' + item.name + '</td>';
                    rows += '<td>' + item.timePeriod + '</td>';
                    rows += '<td>' + item.totalCost + '</td>';
                    rows += '<td>' + item.tools + '</td>';
                    rows += '<td>' + item.description + '</td>';
                    rows += '<td>' + item.createdBy + '</td>';
                    rows += '<td>';
                    rows += '<button class="btn btn-info editservice" data-id="' + item.id + '">Edit</button> ';
                    rows += '<button class="btn btn-danger deleteservice" data-id="' + item.id + '" data-bs-toggle="modal" data-bs-target="#DeleteModal">Delete</button>';
                    rows += '</td>';
                    rows += '</tr>';
                    i++;
                });
                $('#serviceTableBody').html(rows);
            },
            error: function () {
                alert("Unable to retrieve service list.");
            }
        });
    };
    var GetService = function () {
        debugger;
        $.ajax({
            url: '/Service/AllService',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var listItems = '';
                var i = 1;
                $.each(data, function (index, item) {
                    listItems += `  <li><a class="dropdown-item" href="#"> ${item.name}</a></li> `;
                    i++;
                });

                $('#dropdoenlabelservice').html(listItems);
            },
            error: function () {
                alert("Unable to retrieve employee list.");
            }
        });
    };
    var GetServiceById = function (id) {
        debugger;
        $.ajax({
            url: '/Service/DetailService/' + id,
            method: 'GET',
            datatype: 'json',
            success: function (item) {
               
                $('#ServiceId').val(item.id);
                $('#DeleteServiceId').val(item.id);
                $('#Name').val(item.name);
                $('#TimePeriod').val(item.timeperiod);
                $('#TotalCost').val(item.totalcost);
                $('#Tools').val(item.tools);
                $('#Description').val(item.description);
                $('#CreatedBy').val(item.createdby);
                $('#UpdateService').show();
                $('#CreateService').hide();
            },
            error: function () {
                alert("unable to retrieve service.");
            }
        });
    };

    var DeleteService = function () {
        debugger;
        var id = $('#DeleteServiceId').val();
        $.ajax({
            url: '/Service/DeleteServices/' + id,
            method: 'POST',
            dataType: 'json',
            success: function (item) {
                alert(item.message);
                GetServiceList();
            },
            error: function () {
                alert("Unable to delete service.");
            }
        });
    };

  
       





    var onDocumentReady = function () {
        initUi();
        initEvent();
    };

    return {
        onDocumentReady: onDocumentReady,
        GetServiceById: GetServiceById
    };

}(jQuery));

jQuery(app.service.onDocumentReady);

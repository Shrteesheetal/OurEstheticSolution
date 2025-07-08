if (!window.app) {
    window.app = {};
}

window.app.appointment = (function ($) {

    var initUi = function () {

        debugger;
        GetAppointmentList();
        $('#UpdateAppointment').hide();
    };

    var initEvent = function () {
        debugger;
        $("#CreateAppointment").on("click", InsertAppointment);
        $("#UpdateAppointment").on("click", UpdateAppointment);
        $("#deleteAppointment").on("click", DeleteAppointment);

        $(document).on("click", ".editappointment", function () {
            var id = $(this).data('id');
            GetAppointmentById(id);
        });

        $(document).on("click", ".deleteappointment", function () {
            var id = $(this).data('id');
            $('#DeleteAppointmentId').val(id);
        });

        $(document).on("click", "input[type='reset']", Reset);
    };

    function getFormData() {
        debugger;
        return {
            Id: $('#AppointmentId').val(),
            Title: $('#Title').val(),
            CustomerId: $('#CustomerId').val(),
            EmployeeId: $('#EmployeeId').val(),
            ServiceId: $('#ServiceId').val(),
            AppointmentDate: $('#AppointmentDate').val(),
            AppointmentTime: $('#AppointmentTime').val(),
            CreatedBy: $('#CreatedBy').val()
        };
    }

    var InsertAppointment = function (e) {
        debugger;
        e.preventDefault();
        var formData = getFormData();
        $.ajax({
            type: "POST",
            url: '/Appointment/Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    Reset();
                    GetAppointmentList();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to create appointment.");
            }
        });
    };

    var UpdateAppointment = function (e) {
        debugger;
        e.preventDefault();
        var formData = getFormData();
        $.ajax({
            type: "POST",
            url: '/Appointment/UpdateAppointment',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    Reset();
                    GetAppointmentList();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to update appointment.");
            }
        });
    };

    var Reset = function () {
        $('#AppointmentId').val('');
        $('#DeleteAppointmentId').val('');
        $('#Title').val('');
        $('#CustomerId').val('');
        $('#EmployeeId').val('');
        $('#ServiceId').val('');
        $('#AppointmentDate').val('');
        $('#AppointmentTime').val('');
        $('#CreatedBy').val('');
        $('#CreateAppointment').show();
        $('#UpdateAppointment').hide();
    };

    var GetAppointmentList = function () {
        debugger;
        $.ajax({
            url: '/Appointment/AllAppointments',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var rows = '';
                var i = 1;
                $.each(data, function (index, item) {
                    rows += '<tr>';
                    rows += '<td>' + i + '</td>';
                    rows += '<td>' + item.title + '</td>';
                    rows += '<td>' + item.customerId + '</td>';
                    rows += '<td>' + item.employeeId + '</td>';
                    rows += '<td>' + item.serviceId + '</td>';
                    rows += '<td>' + item.appointmentDate + '</td>';
                    rows += '<td>' + item.appointmentTime + '</td>';
                    rows += '<td>' + item.createdBy + '</td>';
                    rows += '<td>';
                    rows += '<button class="btn btn-info editappointment" data-id="' + item.id + '">Edit</button> ';
                    rows += '<button class="btn btn-danger deleteappointment" data-id="' + item.id + '" data-bs-toggle="modal" data-bs-target="#DeleteModal">Delete</button>';
                    rows += '</td>';
                    rows += '</tr>';
                    i++;
                });
                $('#appointmentTableBody').html(rows);
            },
            error: function () {
                alert("Unable to retrieve appointment list.");
            }
        });
    };

    var GetAppointmentById = function (id) {
        debugger;
        $.ajax({
            url: '/Appointment/DetailAppointment/' + id,
            method: 'GET',
            dataType: 'json',
            success: function (item) {
                $('#AppointmentId').val(item.id);
                $('#Title').val(item.title);
                $('#CustomerId').val(item.customerId);
                $('#EmployeeId').val(item.employeeId);
                $('#ServiceId').val(item.serviceId);
                $('#AppointmentDate').val(item.appointmentDate);
                $('#AppointmentTime').val(item.appointmentTime);
                $('#CreatedBy').val(item.createdBy);
                $('#UpdateAppointment').show();
                $('#CreateAppointment').hide();
            },
            error: function () {
                alert("Unable to retrieve appointment.");
            }
        });
    };

    var DeleteAppointment = function () {
        debugger;
        var id = $('#DeleteAppointmentId').val();
        $.ajax({
            url: '/Appointment/DeleteAppointments/' + id,
            method: 'POST',
            dataType: 'json',
            success: function (response) {
                alert(response.message);
                GetAppointmentList();
            },
            error: function () {
                alert("Unable to delete appointment.");
            }
        });
    };

    var onDocumentReady = function () {
        initUi();
        initEvent();
    };

    return {
        onDocumentReady: onDocumentReady,
        GetAppointmentById: GetAppointmentById
    };

}(jQuery));

jQuery(app.appointment.onDocumentReady);

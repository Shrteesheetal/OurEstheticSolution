if (!window.app) {
    window.app = {};
}

window.app.employee = (function ($) {

    var initUi = function () {
        GetEmployee();
        GetEmployeeList();
       

         $('#UpdateEmployee').hide();
    };

    var initEvent = function () {
        $("#CreateEmployee").on("click", InsertEmployee);
        $("#UpdateEmployee").on("click", UpdateEmployee);
        $("#deleteEmployee").on("click", DeleteEmployee);

        $(document).on("click", ".editemployee", function () {
            var id = $(this).data('id');
            GetEmployeebyID(id);
        });

        $(document).on("click", ".deleteemployee", function () {
            var id = $(this).data('id');
            $('#DeleteEmpId').val(id);
        });

        $(document).on("click", "input[type='reset']", Reset);
    };
    function getFormData() {
        return {
            Id: $('#EmpId').val(),
            Name: $('#Name').val(),
            Address: $('#Address').val(),
            Email: $('#Email').val(),
            Designation: $('#Designation').val()
        };
    }

    var InsertEmployee = function (e) {
        e.preventDefault();
        var formData = getFormData();
        $.ajax({
            type: "POST",
            url: '/Employee/Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                   
                    alert(response.message);
                    Reset();
                    GetEmployeeList();
                   
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to create employee.");
            }
        });
    };

    var UpdateEmployee = function (e) {
        e.preventDefault();
        var formData = getFormData();
        $.ajax({
            type: "POST",
            url: '/Employee/UpdateEmployee',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    Reset();
                    GetEmployeeList();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to update employee.");
            }
        });
    };

    var Reset = function () {
        $('#DeleteEmpId').val('');
        $('#EmpId').val('');
        $('#Name').val('');
        $('#Address').val('');
        $('#Email').val('');
        $('#Designation').val('');
        $('#CreateEmployee').show();
        $('#UpdateEmployee').hide();
    };

    var GetEmployeeList = function () {
        $.ajax({
            url: '/Employee/AllEmployee',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var rows = '';
                var i = 1;
                $.each(data, function (index, item) {
                    rows += '<tr>';
                    rows += '<td>' + i + '</td>';
                    rows += '<td>' + item.name + '</td>';
                    rows += '<td>' + item.address + '</td>';
                    rows += '<td>' + item.email + '</td>';
                    rows += '<td>' + item.designation + '</td>';
                    rows += '<td>';
                    rows += '<button class="btn btn-info editemployee" data-id="' + item.id + '">Edit</button> ';
                    rows += '<button class="btn btn-danger deleteemployee" data-id="' + item.id + '" data-bs-toggle="modal" data-bs-target="#DeleteModal">Delete</button>';
                    rows += '</td>';
                    rows += '</tr>';
                    i++;
                });
                $('#employeeTableBody').html(rows);
            },
            error: function () {
                alert("Unable to retrieve employee list.");
            }
        });
    };
    var GetEmployee = function () {
        debugger;
        $.ajax({
            url: '/Employee/AllEmployee',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var listItems = '';
                var i = 1;
                $.each(data, function (index, item) {
                    listItems += `  <li><a class="dropdown-item" href="#"> ${item.name}</a></li> `;
                    i++;
                });

                $('#dropdoenlabelemp').html(listItems);
            },
            error: function () {
                alert("Unable to retrieve employee list.");
            }
        });
    };

    var GetEmployeebyID = function (id) {
        $.ajax({
            url: '/Employee/DetailEmployee/' + id,
            method: 'GET',
            dataType: 'json',
            success: function (item) {
                $('#EmpId').val(item.id);
                $('#DeleteEmpId').val(item.id);
                $('#Name').val(item.name);
                $('#Address').val(item.address);
                $('#Email').val(item.email);
                $('#Designation').val(item.designation);
                $('#UpdateEmployee').show();
                $('#CreateEmployee').hide();
            },
            error: function () {
                alert("Unable to retrieve employee.");
            }
        });
    };


    var DeleteEmployee = function () {
       var id=  $('#DeleteEmpId').val();
        debugger;
        $.ajax({
            url: '/Employee/DeleteEmployees/' + id,
            method: 'POST',
            dataType: 'json',
            success: function (item) {
                alert(item.message);
                GetEmployeeList();
            },
            error: function () {
                alert("Unable to retrieve employee.");
            }
        });
    };
    

    var onDocumentReady = function () {
        initUi();
        initEvent();
    };

    return {
        onDocumentReady: onDocumentReady,
        GetEmployeebyID: GetEmployeebyID
    };

}(jQuery));

jQuery(app.employee.onDocumentReady);

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

        var formData = new FormData();

        // Get the image file
        var image = document.getElementById("imgup").files[0];
        if (image) {
            formData.append("PostImage", image); // Key name must match server parameter
        }

        // Append text fields
        formData.append("Id", $('#ServiceId').val());
        formData.append("Name", $('#Name').val());
        formData.append("TimePeriod", $('#TimePeriod').val());
        formData.append("TotalCost", $('#TotalCost').val());
        formData.append("Tools", $('#Tools').val());
        formData.append("Description", $('#Description').val());
    
        //formData.append("Imagepath", $('#Imagepath').val());

        //// Append new file if selected
        //var imageFile = $('#ImageFile')[0].files[0];
        //if (imageFile) {
        //    formData.append("ImageFile", imageFile); // key must match parameter in controller
        //}


        return formData;
    }

 

    var InsertService = function (e) {
        e.preventDefault();
        var formData = getFormData();

        $.ajax({
            type: "POST",
            url: '/Service/Create',
            data: formData,
            contentType: false,
            processData: false,
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
        e.preventDefault();

        var formData = getFormData();

        $.ajax({
            type: "POST",
            url: '/Service/UpdateService',
            data: formData,
            processData: false,        // Prevent jQuery from processing data
            contentType: false,        // Prevent jQuery from setting content type
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
        $('#Imagepath').val('');
        $('#CreateService').show();
        $('#UpdateService').hide();
        const button = document.querySelector('.remove-image'); // or pass button ref if available
        if (button) {
            removeUpload(button); // call your removeUpload function
        }

        $('#PreviewImage').attr('src', '');
    };
    var removeUpload = function (button) {
        var $wrapper = $(button).closest('.file-upload');
        var $input = $wrapper.find('.file-upload-input');

        $input.replaceWith($input.clone()); // Clear input
        $wrapper.find('.file-upload-content').hide();
        $wrapper.find('.image-upload-wrap').show();
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
                    rows += '<td><img src="' + item.imagepath + '" alt="Service Image" width="80" height="60"/></td>';
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
                debugger;
                $('#ServiceId').val(item.id);
                $('#DeleteServiceId').val(item.id);
                $('#Name').val(item.name);
                $('#TimePeriod').val(item.timePeriod);
                $('#TotalCost').val(item.totalCost);
                $('#Tools').val(item.tools);
                $('#Description').val(item.description);
                $('#CreatedBy').val(item.createdBy);
               
                // Show existing image in preview
                if (item.imagepath) {
                    $('#PreviewImage').attr('src',  item.imagepath); // adjust path if needed
                } else {
                    $('#PreviewImage').attr('src', '');
                }


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

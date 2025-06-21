if (!window.app) {
    window.app = {};
}

window.app.product = (function ($) {
  
    var initUi = function () {
      
        GetProductList();
        $('#UpdateProduct').hide();
      
    };
    var initEvent = function () {
      
        $("#CreateProduct").on("click", InsertProduct);
        $("#UpdateProduct").on("click", UpdateProduct);
        debugger;
        $("#deleteProduct").on("click", DeleteProduct);
        $(document).on("click", ".editproduct", function () {
            var id = $(this).data('id');
            GetProductByID(id);
        });
        $(document).on("click", ".deleteproduct", function () {
            var id = $(this).data('id');
            $('#DeleteProdId').val(id);
        });
       
        $(document).on("click", "input[type='reset']", Reset);
    };
    function getFormData() {
        return {   
            Id: $('#ProductId').val(),
            Name: $('#Name').val(),
            Description: $('#Description').val(),
            ExpiryDate: $('#ExpiryDate').val(),
            ManufacturedBy: $('#ManufacturedBy').val(),
            ServiceId: $('#ServiceId').val()
        };
    }
    var InsertProduct = function (e) {
        e.preventDefault();
     
        var formData = getFormData();
        $.ajax({
            type: "POST",
            url: '/Product/Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    Reset();
                    GetProductList();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to create product.");
            }
        });
    };

    var GetProductList = function () {
   
        $.ajax({

            url: '/Product/AllProduct',
            method: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',

            success: function (data) {
                var rows = '';

                var i = 1;
             
                $.each(data, function (index, item) {
                    rows += '<tr>';
                    rows += '<td>' + i + '</td>';
                    rows += '<td>' + item.name + '</td>';
                    rows += '<td>' + item.description + '</td>';
                    rows += '<td>' + item.expiryDate + '</td>';
                    rows += '<td>' + item.manufacturedBy + '</td>';
                    rows += '<td>' + item.serviceId + '</td>';
                    rows += '<td>';
                    rows += '<button class="btn btn-info editproduct" data-id="' + item.id + '">Edit</button> ';
                    rows += '<button class="btn btn-danger deleteproduct" data-id="' + item.id + '" data-bs-toggle="modal" data-bs-target="#DeleteModal">Delete</button>';
                    rows += '</td>';
                    rows += '</tr>';
                    i++;
                });
                $('#productTableBody').html(rows);
            },
            error: function () {
                alert("Unable to retrieve product list.");
            }
        });
    };

    var UpdateProduct = function (e) {
        debugger;
        e.preventDefault();
        var formData = getFormData(); // Ensure this collects product form fields properly
        $.ajax({
            type: "POST",
            url: '/Product/UpdateProduct', 
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    Reset(); // Ensure Reset() clears the product form
                    GetProductList(); // Replace with your actual function to refresh the product list
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Failed to update product.");
            }
        });
    };


    var Reset = function () {
        debugger;
        $('#DeleteProdId').val('');
        $('#Id').val('');
        $('#Name').val('');
        $('#Description').val('');
        $('#ExpiryDate').val('');
        $('#ManufacturedBy').val('');
        $('#ServiceId').val('');
        $('#CreatedAt').val('');
        $('#UpdatedAt').val('');
        $('#CreateProduct').show();
        $('#UpdateProduct').hide();
    };



    var GetProductByID = function (id) {
        debugger;
        $.ajax({
            url: '/Product/DetailProduct/'+id,
            method: 'GET',
            data: { id: id },
            dataType: 'json',
            success: function (data) {
                    
                $('#ProductId').val(data.id);
                $('#Name').val(data.name);
                $('#Description').val(data.description);
                $('#ExpiryDate').val(data.expiryDate);
                $('#ManufacturedBy').val(data.manufacturedBy);
                $('#ServiceId').val(data.serviceId);
                $('#CreateProduct').hide();
                $('#UpdateProduct').show();
            },
            error: function () {
                alert("Unable to retrieve product details.");
            }
        });
    }
    var DeleteProduct = function () {
        var id = $('#DeleteProdId').val();
        debugger;
        $.ajax({
            url: '/Product/DeleteProducts/' + id,
            method: 'POST',
            dataType: 'json',
            success: function (item) {
                alert(item.message);
                GetProductList(); // Refresh product list after deletion
            },
            error: function () {
                alert("Unable to delete product.");
            }
        });
    };


    // Additional functions like GetProductList, GetProductByID, UpdateProduct, DeleteProduct, and Reset would be defined here.
    var onDocumentReady = function () {
        initUi();
        initEvent();
    };


    return {
        onDocumentReady: onDocumentReady
        , GetProductByID: GetProductByID
       
    };

}(jQuery));
jQuery(app.product.onDocumentReady);
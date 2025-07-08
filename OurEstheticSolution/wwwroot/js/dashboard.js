if (!window.app) {
    window.app = {};
}

window.app.dashboard = (function ($) {

    var initUi = function () {
        GetEmployee();
        GetService();
        GetServiceList();
       

       
    };

    var initEvent = function () {
       

      
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
                    rows += `
                    <div class="col-md-4 mb-4">
                        <div class="card shadow-sm">
                            <img src="/images/image/img.jpg" class="card-img-top" height="200px" width="200px" alt="Service Image">
                            <div class="card-body">
                                <h5 class="card-title">${item.name  || 'No Title'}</h5>
                             
                              <a href="/Service/ServiceDetail/${item.id}">View Detail</a>

                            
                            </div>
                        </div>
                    </div>`;
                    i++;
                });
                $('#cardService').html(rows);
            },
            error: function () {
                alert("Unable to retrieve service list.");
            }
        });
    };

    

    var onDocumentReady = function () {
        initUi();
        initEvent();
    };

    return {
        onDocumentReady: onDocumentReady,
      
    };

}(jQuery));

jQuery(app.dashboard.onDocumentReady);

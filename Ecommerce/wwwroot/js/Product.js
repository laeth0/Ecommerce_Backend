function loadDataTable() {
    let dataTable = $('#DT_load').dataTable({
        "ajax": {
            "url": "Product/getall",
            "type": "GET",
            "datatype": "json",
            contentType: "application/json",
        },
        "columns": [
            { "data": "ProductId" },
            { "data": "ProductName" },
            { "data": "ImageURL" },
            { "data": "Category.CategoryName" },
            {
                data: "ProductId",
                render: function (data, type, row) {
                  
                    return `<a href="/Admin/Product/Update/${data.ProductId}" class="btn btn-info mx-2" >
                                <i class="far fa-edit"></i> Edit
                            </a>
                            <a href="/Admin/Product/Delete/${data.ProductId}" class="btn btn-danger" >
                                <i class="far fa-trash-alt"></i> Delete
                            </a>`;
                }
            }
        ],
    });

}
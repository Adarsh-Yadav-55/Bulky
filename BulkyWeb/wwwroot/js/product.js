$(document).ready(function (){
    loadDataTable();
})

function Delete(url) {
    Swal.fire({
        title: 'Are you sure!',
        text: "you won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, Delete it!'
    }).then((result) => {
        console.log(result);
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: { url: '/admin/Product/getAll' },
        columns: [
            {data:'title'},
            {data:'author'},
            {data:'isbn'},
            {data:'listPrice'},
            { data: 'category.name' },
            {
                data: 'id',
                render: function (data) {
                    return ` <div class="w-75 btn-group" role="group">
                        <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-fill"></i>Edit
                        </a>
                        <a onclick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i>Delete
                        </a>
                    </div>`
                }
            }
        ]
    })
}
//$(document).ready(function () {
//    bindDatatable();
//});

//function bindDatatable() {
//    datatable = $('#tbDepartments')
//        .DataTable({
//            "sAjaxSource": "https://localhost:7079/api/Departments",
//            "bServerSide": true,
//            "bProcessing": true,
//            "bSearchable": true,
//            "order": [[1, 'asc']],
//            "language": {
//                "emptyTable": "No record found.",
//                "processing":
//                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
//            },
//            "columns": [
//                {
//                    "data": "dept_Id",
//                    "autoWidth": true,
//                    "searchable": true
//                },
//                {
//                    "data": "name",
//                    "autoWidth": true,
//                    "searchable": true
//                },
//                {
//                    "render": function (data, type, row) {
//                        return '<button class="btn btn-warning" data-toggle="tooltip" data-placement="left" title="Edit" onclick="return GetById(\'' + row.dept_Id + '\')"><i class="fas fa-pencil-alt"></i></button >' + '&nbsp;' +
//                            '<button class="btn btn-danger" data-toggle="tooltip" data-placement="right" title="Delete" onclick="return Delete(\'' + row.dept_Id + '\')"><i class="fas fa-trash-alt"></i></button >'
//                    }
//                }
//            ]
//        });
//}

//$(document).ready(function () {
//    $('#tbDepartments').DataTable({
//        "paging": true,
//        "lengthChange": true,
//        "searching": true,
//        "ordering": true,
//        "info": true,
//        "autoWidth": false,
//        "responsive": true,
//        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
//        "ajax": {
//            url: "https://localhost:7079/api/Departments",
//            type: "GET",
//            "datatype": "json",
//            "dataSrc": "data",
//        },
//        columnDefs: [{
//            "defaultContent": "-",
//            "targets": "_all"
//        }],
//        "columns": [
//            {
//                render: function (data, type, row, meta) {
//                    return meta.row + meta.settings._iDisplayStart + 1 + "."
//                }
//            },
//            { "data": "name" },
//            {
//                "render": function (data, type, row) {
//                    return '<button class="btn btn-warning" data-toggle="tooltip" data-placement="left" title="Edit" onclick="return GetById(\'' + row.dept_Id + '\')"><i class="fas fa-pencil-alt"></i></button >' + '&nbsp;' +
//                        '<button class="btn btn-danger" data-toggle="tooltip" data-placement="right" title="Delete" onclick="return Delete(\'' + row.dept_Id + '\')"><i class="fas fa-trash-alt"></i></button >'
//                }
//            }
//        ],
//        "initComplete": function (settings, json) {
//            $('[data-toggle="tooltip"]').tooltip();
//        }
//    });
//})

//function Save() {
//    var Department = new Object(); //set your object name
//    Department.Name = $('#DepartmentName').val(); //set the value, and it's source
//    $.ajax({
//        type: 'POST', // API Method
//        url: 'https://localhost:7079/api/Departments', // your API's URL
//        data: JSON.stringify(Department), //must exist on Insert method
//        contentType: "application/json; charset=utf-8", //must exist on Insert method
//    }).then((result) => { //the result depends on what you write on your API's
//        if (result.status == 200) {
//            alert(result.message);
//        }
//        else {
//            alert(result.message);
//        }
//    })
//}

//function GetById(dept_Id) {
//    $.ajax({
//        url: "https://localhost:7079/api/Departments/" + dept_Id,
//        //remember to give / at the end of your URL.
//        //because your format is /api/controllername/{key}
//        //adjust the key by what you provide on your API's
//        type: "GET", // API Method
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var obj = result.data;
//            //adjust with what's your API return
//            $('#Id').val(obj.dept_Id);
//            //set the column first
//            //then set what the value is
//            $('#DepartmentName').val(obj.name);
//            $('#exampleModal').modal('show');
//            //we only used one modal
//            //so set the modal value with what you get from API
//        }
//    });
//}

//function Update() {
//    debugger;
//    var Department = new Object();
//    Department.dept_Id = $('#Id').val();
//    Department.Name = $('#DepartmentName').val();
//    $.ajax({
//        url: 'https://localhost:7079/api/Departments/',
//        type: 'PUT',
//        data: JSON.stringify(Department), //must exist on Insert method
//        contentType: "application/json; charset=utf-8", //must exist on Insert method
//    }).then((result) => {
//        if (result.status == 200) {
//            alert(result.message);
//            table.ajax.reload();
//        }
//        else {
//            alert(result.message);
//        }
//    });
//}

//function Delete(dept_Id) {
//    $.ajax({
//        url: "https://localhost:7079/api/Departments/" + dept_Id,
//        type: "DELETE",
//        dataType: "json",
//    }).then((result) => {
//        if (result.status == 200) {
//            alert(result.message);
//        }
//        else {
//            alert(result.message);
//        }
//    })
//}

var table = null;
$(document).ready(function () {
    table = $('#tbDepartments').DataTable({
        "paging": true,
        "lengthChange": true,
        lengthMenu: [[5, 10, 15, -1], ['5', '10', '15', 'Show All']],
        "pageLength": 10,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "processing": true,
        "serverSide": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "ajax": {
            url: "https://localhost:7079/api/Departments/paging", // Your API endpoint
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: function (d) {
                d.order = d.order[0];
                return JSON.stringify(d);
            }
        },
        columnDefs: [
            {
                "defaultContent": "-",
                "targets": "_all"
            },
            {
                "searchable": false,
                "orderable": false,
                "targets": [0, 2]
            }
        ],
        "columns": [
            {
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1 + "."
                }
            },
            { "data": "name" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning" data-toggle="tooltip" data-placement="left" title="Edit" onclick="return GetById(\'' + row.dept_Id + '\')"><i class="fas fa-pencil-alt"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-toggle="tooltip" data-placement="right" title="Delete" onclick="return Delete(\'' + row.dept_Id + '\')"><i class="fas fa-trash-alt"></i></button >'
                }
            }
        ],
        "initComplete": function (settings, json) {
            $('[data-toggle="tooltip"]').tooltip({
                trigger: 'hover'
            });
        }
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
})

function ClearScreen() {
    $('#Id').val('');
    $('#DepartmentName').val('');
    $('#Update').hide();
    $('#Save').show();
}

function Save() {
    if ($('#DepartmentName').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Department Name',
            showConfirmButton: false,
            timer: 1500
        });
    } else {
        debugger;
        var Department = new Object(); //set your object name
        Department.Name = $('#DepartmentName').val(); //set the value, and it's source
        $.ajax({
            type: 'POST', // API Method
            url: 'https://localhost:7079/api/Departments', // your API's URL
            data: JSON.stringify(Department), //must exist on Insert method
            contentType: "application/json; charset=utf-8", //must exist on Insert method
        }).then((result) => {
            if (result.status == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                })
                table.ajax.reload();
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                })
                table.ajax.reload();
                ClearScreen();
            }
        });
    }
}

function GetById(dept_Id) {
    $.ajax({
        url: "https://localhost:7079/api/Departments/" + dept_Id,
        //remember to give / at the end of your URL.
        //because your format is /api/controllername/{key}
        //adjust the key by what you provide on your API's
        type: "GET", // API Method
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var obj = result.data;
            //adjust with what's your API return
            $('#Id').val(obj.dept_Id);
            //set the column first
            //then set what the value is
            $('#DepartmentName').val(obj.name);
            $('#exampleModal').modal('show');
            //we only used one modal
            //so set the modal value with what you get from API
            $('#Update').show();
            $('#Save').hide();
        }
    });
}

function Update() {
    if ($('#DepartmentName').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Department Name',
            showConfirmButton: false,
            timer: 1500
        });
    } else {
        var Department = new Object();
        Department.dept_Id = $('#Id').val();
        Department.Name = $('#DepartmentName').val();
        $.ajax({
            url: 'https://localhost:7079/api/Departments/',
            type: 'PUT',
            data: JSON.stringify(Department), //must exist on Insert method
            contentType: "application/json; charset=utf-8", //must exist on Insert method
        }).then((result) => {
            debugger;
            if (result.status == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                })
                table.ajax.reload();
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                })
                table.ajax.reload();
                ClearScreen();
            }
        });
    }
}

function Delete(dept_Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7079/api/Departments/" + dept_Id,
                type: "DELETE",
                dataType: "json",
            }).then((result) => {
                if (result.status == 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: result.message,
                        showConfirmButton: false,
                        timer: 1500
                    })
                    table.ajax.reload();
                }
                else {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: result.message,
                        showConfirmButton: false,
                        timer: 1500
                    })
                    table.ajax.reload();
                }
            })
        }
    })
}
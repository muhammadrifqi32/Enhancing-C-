let table = null;

$(document).ready(function () {
    initDepartmentTable();
});

function initDepartmentTable() {
    table = $('#tbDepartments').DataTable({
        paging: true,
        lengthChange: true,
        lengthMenu: [[5, 10, 15, -1], ['5', '10', '15', 'Show All']],
        pageLength: 10,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true,
        processing: true,
        serverSide: true,
        ajax: {
            url: "https://localhost:7079/api/Departments/paging",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: function (d) {
                d.order = d.order[0]; // flatten order for backend
                return JSON.stringify(d);
            },
            error: handleAjaxError
        },
        language: {
            processing: "Memuat data...",
            emptyTable: "Tidak ada data tersedia"
        },
        columnDefs: [
            { defaultContent: "-", targets: "_all" },
            { searchable: false, orderable: false, targets: [0, 2] }
        ],
        columns: [
            {
                render: function (data, type, row, meta) {
                    return `${meta.row + meta.settings._iDisplayStart + 1}.`;
                }
            },
            { data: "name" },
            {
                render: renderActionButtons
            }
        ],
        drawCallback: function () {
            $('[data-toggle="tooltip"]').tooltip({ trigger: 'hover' });
        }
    });

    // Auto update numbering on reorder / search
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' })
            .nodes()
            .each(function (cell, i) {
                cell.innerHTML = `${i + 1}.`;
            });
    });
}

function renderActionButtons(data, type, row) {
    return `
        <button class="btn btn-warning" title="Edit" onclick="GetById('${row.dept_Id}')">
            <i class="fas fa-pencil-alt"></i>
        </button>
        &nbsp;
        <button class="btn btn-danger" title="Delete" onclick="Delete('${row.dept_Id}')">
            <i class="fas fa-trash-alt"></i>
        </button>`;
}

function handleAjaxError(xhr, status, error) {
    const message = xhr.responseJSON?.message || error || 'Gagal memuat data';
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: message,
        showConfirmButton: true
    });
}

function ClearScreen() {
    $('#Id').val('');
    $('#DepartmentName').val('');
    $('#Update').hide();
    $('#Save').show();
    $('#exampleModal').modal('hide');
}

function Save() {
    const departmentName = $('#DepartmentName').val();

    if (!departmentName || departmentName.trim() === "") {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Department name cannot be empty',
            showConfirmButton: false,
            timer: 1500
        });
        return;
    }

    const department = {
        Name: departmentName.trim()
    };

    $.ajax({
        type: 'POST',
        url: 'https://localhost:7079/api/Departments',
        data: JSON.stringify(department),
        contentType: "application/json; charset=utf-8",
    }).done((result) => {
        Swal.fire({
            position: 'center',
            icon: 'success',
            title: result.message,
            showConfirmButton: false,
            timer: 1500
        });
        table.ajax.reload();
        ClearScreen();
    }).fail((xhr) => {
        if (xhr.status === 400 && xhr.responseJSON?.errors) {
            const errorMessages = Object.values(xhr.responseJSON.errors).flat();
            const errorMessage = errorMessages[0] || "Validation failed";

            Swal.fire({
                position: 'center',
                icon: 'error',
                title: errorMessage,
                showConfirmButton: false,
                timer: 2000
            });
        } else {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Something went wrong',
                showConfirmButton: false,
                timer: 2000
            });
        }
    });
}

function GetById(dept_Id) {
    if (!dept_Id) {
        Swal.fire({
            icon: 'error',
            title: 'Invalid department ID',
            showConfirmButton: false,
            timer: 1500
        });
        return;
    }

    $.ajax({
        url: `https://localhost:7079/api/Departments/${dept_Id}`,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done((result) => {
        const obj = result.data;
        if (!obj) {
            Swal.fire({
                icon: 'error',
                title: 'Data not found',
                showConfirmButton: false,
                timer: 1500
            });
            return;
        }

        // Populate modal form
        $('#Id').val(obj.dept_Id);
        $('#DepartmentName').val(obj.name);

        // Show modal and toggle buttons
        $('#exampleModal').modal('show');
        $('#Update').show();
        $('#Save').hide();
    }).fail((xhr) => {
        const errorMsg = xhr.responseJSON?.message || 'Failed to fetch department data';
        Swal.fire({
            icon: 'error',
            title: errorMsg,
            showConfirmButton: false,
            timer: 2000
        });
    });
}


function Update() {
    const departmentId = $('#Id').val();
    const departmentName = $('#DepartmentName').val();

    if (!departmentName || departmentName.trim() === "") {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Department name cannot be empty',
            showConfirmButton: false,
            timer: 1500
        });
        return;
    }

    const department = {
        dept_Id: departmentId,
        name: departmentName.trim()
    };

    $.ajax({
        url: 'https://localhost:7079/api/Departments/',
        type: 'PUT',
        data: JSON.stringify(department),
        contentType: "application/json; charset=utf-8",
    }).done((result) => {
        Swal.fire({
            position: 'center',
            icon: 'success',
            title: result.message,
            showConfirmButton: false,
            timer: 1500
        });
        table.ajax.reload();
        ClearScreen();
    }).fail((xhr) => {
        if (xhr.status === 400 && xhr.responseJSON?.errors) {
            const errorMessages = Object.values(xhr.responseJSON.errors).flat();
            const errorMessage = errorMessages[0] || "Validation failed";

            Swal.fire({
                position: 'center',
                icon: 'error',
                title: errorMessage,
                showConfirmButton: false,
                timer: 2000
            });
        } else {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Something went wrong',
                showConfirmButton: false,
                timer: 2000
            });
        }
    });
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
            }).done((result) => {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                });
                table.ajax.reload();
            }).fail((xhr) => {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Failed to delete the department',
                    showConfirmButton: false,
                    timer: 2000
                });
            });
        }
    });
}

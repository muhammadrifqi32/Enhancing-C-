let table = null;

$(document).ready(function () {
    initEmployeeTable();
    loadDepartments();
});

function initEmployeeTable() {
    table = $('#tbEmployees').DataTable({
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
        serverSide: false,
        ajax: {
            url: "https://localhost:7079/api/Employees",
            type: "GET",
            dataType: "json",
            dataSrc: json => json.data,
            error: xhr => showErrorAlert(xhr.responseJSON?.message || 'Gagal memuat data')
        },
        language: {
            processing: "Memuat data...",
            emptyTable: "Tidak ada data tersedia"
        },
        columnDefs: [
            { defaultContent: "-", targets: "_all" },
            { searchable: false, orderable: false, targets: [0, 7] }
        ],
        columns: [
            { render: (data, type, row, meta) => `${meta.row + meta.settings._iDisplayStart + 1}.` },
            { data: "nik" },
            { data: null, render: row => `${row.firstName} ${row.lastName}` },
            { data: "email" },
            { data: "phoneNumber" },
            { data: "address" },
            { data: "departmentName" },
            { render: renderActionButtons }
        ],
        drawCallback: () => $('[data-toggle="tooltip"]').tooltip({ trigger: 'hover' })
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each((cell, i) => {
            cell.innerHTML = `${i + 1}.`;
        });
    });
}

function renderActionButtons(data, type, row) {
    return `
        <button class="btn btn-warning" title="Edit" onclick="GetById('${row.nik}')">
            <i class="fas fa-pencil-alt"></i>
        </button>
        &nbsp;
        <button class="btn btn-danger" title="Delete" onclick="Delete('${row.nik}')">
            <i class="fas fa-trash-alt"></i>
        </button>`;
}

function loadDepartments() {
    $.ajax({
        url: 'https://localhost:7079/api/Departments',
        method: 'GET'
    }).done(response => {
        const dropdown = $('#departmentName');
        dropdown.empty().append('<option value="">-- Pilih Department --</option>');
        response.data.forEach(d => dropdown.append(`<option value="${d.dept_Id}">${d.name}</option>`));
    }).fail(() => showErrorAlert("Gagal memuat department"));
}

function ClearScreen() {
    $('#nik').val('');
    $('#firstName').val('');
    $('#lastName').val('');
    $('#email').val('');
    $('#phoneNumber').val('');
    $('#address').val('');
    $('#isActive').val('');
    $('#departmentName').val('');

    $('#formGroupEmail').hide();
    $('#Update').hide();
    $('#Save').show();
    $('#exampleModal').modal('hide');
}

function Save() {
    const departmentId = $('#departmentName').val();
    if (!departmentId) return showErrorAlert('Pilih department terlebih dahulu');

    const employee = {
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        phoneNumber: $('#phoneNumber').val(),
        address: $('#address').val(),
        dept_Id: departmentId
    };

    $.ajax({
        url: 'https://localhost:7079/api/Employees',
        type: 'POST',
        data: JSON.stringify(employee),
        contentType: 'application/json; charset=utf-8'
    }).done(result => {
        showSuccessAlert(result.message);
        table.ajax.reload();
        ClearScreen();
    }).fail(xhr => {
        const errorMessage = xhr.responseJSON?.errors
            ? Object.values(xhr.responseJSON.errors).flat()[0]
            : "Terjadi kesalahan";
        showErrorAlert(errorMessage);
    });
}

function GetById(nik) {
    if (!nik) return showErrorAlert('Invalid NIK');

    $.ajax({
        url: `https://localhost:7079/api/Employees/${nik}`,
        type: "GET"
    }).done(result => {
        const obj = result.data;
        if (!obj) return showErrorAlert('Data not found');

        $('#nik').val(obj.nik);
        $('#firstName').val(obj.firstName);
        $('#lastName').val(obj.lastName);
        $('#email').show();
        $('#email').val(obj.email);
        $('#phoneNumber').val(obj.phoneNumber);
        $('#address').val(obj.address);
        $('#isActive').val(obj.isActive);
        $('#departmentName').val(obj.dept_Id);

        $('#formGroupEmail').show();
        $('#exampleModal').modal('show');
        $('#Update').show();
        $('#Save').hide();
    }).fail(xhr => {
        showErrorAlert(xhr.responseJSON?.message || 'Gagal mengambil data');
    });
}

function Update() {
    const departmentId = $('#departmentName').val();
    if (!departmentId) return showErrorAlert('Pilih department terlebih dahulu');

    const employee = {
        nik: $('#nik').val(),
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        phoneNumber: $('#phoneNumber').val(),
        address: $('#address').val(),
        dept_Id: departmentId,
        isActive: $('#isActive').val()
    };

    $.ajax({
        url: 'https://localhost:7079/api/Employees',
        type: 'PUT',
        data: JSON.stringify(employee),
        contentType: 'application/json; charset=utf-8'
    }).done(result => {
        showSuccessAlert(result.message);
        table.ajax.reload();
        ClearScreen();
    }).fail(xhr => {
        const errorMessage = xhr.responseJSON?.errors
            ? Object.values(xhr.responseJSON.errors).flat()[0]
            : "Terjadi kesalahan";
        showErrorAlert(errorMessage);
    });
}

function Delete(nik) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Data akan dihapus!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                url: `https://localhost:7079/api/Employees/${nik}`,
                type: "PATCH"
            }).done(result => {
                showSuccessAlert(result.message);
                table.ajax.reload();
            }).fail(() => {
                showErrorAlert('Gagal menghapus data');
            });
        }
    });
}

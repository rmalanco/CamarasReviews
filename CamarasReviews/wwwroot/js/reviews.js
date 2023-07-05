$(document).ready(function () {
  $("#tblReviews").DataTable({
    ajax: {
      url: "/Author/Reviews/GetAll",
      type: "GET",
      datatype: "json",
    },
    columns: [
      { data: "title" },
      { data: "author" },
      {
        data: "createdDate",
        render: function (data) {
          return moment(data).format("DD/MM/YYYY HH:mm:ss");
        },
        width: "10%",
      },
      {
        data: "reviewId",
        render: function (data) {
          return `
                            <div class="text-center">
                                <button class="btn btn-primary" onclick="window.location.href = '/Author/Reviews/Edit/${data}'">
                                    <i class="fas fa-edit"></i>
                                </button>
                                <button class="btn btn-danger" onclick="Delete('/Author/Reviews/Delete/${data}')">
                                    <i class="fas fa-trash-alt"></i>
                                </button>
                            </div>
                    `;
        },
        width: "10%",
      },
    ],
    language: {
      emptyTable: "No hay registros",
      lengthMenu: "Mostrar _MENU_ registros por pagina",
      zeroRecords: "No se encontró ningún registro",
      info: "Mostrando la pagina _PAGE_ de _PAGES_",
      infoEmpty: "No hay registros disponibles",
      infoFiltered: "(filtrado de _MAX_ registros totales)",
      search: "Buscar:",
      paginate: {
        first: "Primero",
        last: "Ultimo",
        next: "Siguiente",
        previous: "Anterior",
      },
    },
    width: "100%",
  });
});

function Delete(url) {
  Swal.fire({
    title: "¿Estas seguro de eliminar el registro?",
    text: "No podrás revertir esta acción",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    cancelButtonText: "Cancelar",
    confirmButtonText: "Si, eliminar",
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        url: url,
        type: "DELETE",
        success: function (response) {
          if (response.success) {
            $("#tblReviews").DataTable().ajax.reload();
            toastr.success(response.message);
          } else {
            toastr.error(response.message);
          }
        },
        error: function (error) {
          console.log(error);
        },
      });
    }
  });
}

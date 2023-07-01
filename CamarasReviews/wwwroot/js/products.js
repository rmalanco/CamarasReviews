$("#tablaProducto").DataTable({
    ajax: {
        url: "/Author/Products/GetAllActiveProducts"
    },
    columns: [
        { data: "name" },
        { data: "brand.name" },
        { data: "category.name" },
        { data: "sku", width: "10%" },
        {
            data: "price",
            render: $.fn.dataTable.render.number(',', '.', 2, '$')
        },
        {
            data: "createdDate",
            render: function (data) {
                return moment(data).format('DD/MM/YYYY HH:mm:ss');
            }
        },
        {
            data: "productId",
            render: function (data) {
                return `
                                <div class="text-center">
                                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalImagenes" onclick="GetImagesProductById('${data}')">
                                        <i class="fas fa-images"></i>
                                    </button>
                                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalAgregarProducto" onclick="GetProductById('/Author/Products/GetProductById/${data}')">
                                        <i class="fas fa-edit"></i>
                                    </button>
                                    <button type="button" class="btn btn-danger" onclick="Delete('/Author/Products/Delete/${data}')">
                                        <i class="fas fa-trash-alt"></i>
                                    </button>
                                </div>
                        `;
            }
        }
    ],
    language: {
        decimal: "",
        emptyTable: "No hay información",
        info: "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
        infoEmpty: "Mostrando 0 to 0 of 0 Entradas",
        infoFiltered: "(Filtrado de _MAX_ total entradas)",
        infoPostFix: "",
        thousands: ",",
        lengthMenu: "Mostrar _MENU_ Entradas",
        loadingRecords: "Cargando...",
        processing: "Procesando...",
        search: "Buscar:",
        zeroRecords: "Sin resultados encontrados",
        paginate: {
            first: "Primero",
            last: "Ultimo",
            next: "Siguiente",
            previous: "Anterior",
        },
    },
    order: [[5, "desc"]],
});

let filesList = [];
const classDragOver = "drag-over";
const fileInputMulti = document.querySelector("#multi-selector-uniq #files");
const multiSelectorUniqPreview = document.querySelector("#multi-selector-uniq #preview");

function getIndexOfFileList(name, list) {
    return list.reduce(
        (position, file, index) => (file.name === name ? index : position),
        -1
    );
}

async function encodeFileToText(file) {
    return file.text().then((text) => {
        return text;
    });
}

async function getUniqFiles(newFiles, currentListFiles) {
    return new Promise((resolve) => {
        Promise.all(newFiles.map((inputFile) => encodeFileToText(inputFile))).then(
            (inputFilesText) => {
                Promise.all(
                    currentListFiles.map((savedFile) => encodeFileToText(savedFile))
                ).then((savedFilesText) => {
                    let newFileList = currentListFiles;
                    inputFilesText.forEach((inputFileText, index) => {
                        if (!savedFilesText.includes(inputFileText)) {
                            newFileList = newFileList.concat(newFiles[index]);
                        }
                    });
                    resolve(newFileList);
                });
            }
        );
    });
}

function renderPreviews(currentFileList, target, inputFile) {
    target.innerHTML = "";
    currentFileList.forEach((file, index) => {
        const cardContainer = document.createElement("div");
        cardContainer.classList.add("col-md-4", "mb-3", "d-flex", "align-items-stretch");

        const card = document.createElement("div");
        card.classList.add("card");

        const cardBody = document.createElement("div");
        cardBody.classList.add("card-body", "d-flex", "align-items-center", "justify-content-center");

        const myImg = document.createElement("img");
        myImg.src = URL.createObjectURL(file);
        myImg.classList.add("card-img-top", "img-thumbnail", "w-100", "h-100");
        myImg.draggable = true;
        myImg.dataset.key = file.name;
        myImg.addEventListener("drop", eventDrop);
        myImg.addEventListener("dragover", eventDragOver);

        const myButtonRemove = document.createElement("button");
        myButtonRemove.textContent = "Eliminar";
        myButtonRemove.classList.add("btn", "btn-danger", "mt-2");
        myButtonRemove.addEventListener("click", () => {
            filesList = deleteArrayElementByIndex(currentFileList, index);
            inputFile.files = arrayFilesToFileList(filesList);
            renderPreviews(filesList, target, inputFile);
        });

        cardBody.appendChild(myImg);
        card.appendChild(cardBody);
        card.appendChild(myButtonRemove);
        cardContainer.appendChild(card);
        target.appendChild(cardContainer);
    });
}

function deleteArrayElementByIndex(list, index) {
    return list.filter((item, itemIndex) => itemIndex !== index);
}

function arrayFilesToFileList(filesList) {
    return filesList.reduce(function (dataTransfer, file) {
        dataTransfer.items.add(file);
        return dataTransfer;
    }, new DataTransfer()).files;
}

function arraySwapIndex(firstIndex, secondIndex, list) {
    const tempList = list.slice();
    const tmpFirstPos = tempList[firstIndex];
    tempList[firstIndex] = tempList[secondIndex];
    tempList[secondIndex] = tmpFirstPos;
    return tempList;
}

fileInputMulti.addEventListener("input", async () => {
    const newFilesList = Array.from(fileInputMulti.files);
    filesList = await getUniqFiles(newFilesList, filesList);
    renderPreviews(filesList, multiSelectorUniqPreview, fileInputMulti);
    fileInputMulti.files = arrayFilesToFileList(filesList);
});

let myDragElement = undefined;
document.addEventListener("dragstart", (event) => {
    myDragElement = event.target;
});

function eventDragOver(event) {
    event.preventDefault();
    multiSelectorUniqPreview
        .querySelectorAll("li")
        .forEach((item) => item.classList.remove(classDragOver));
    event.target.classList.add(classDragOver);
}

function eventDrop(event) {
    const myDropElement = event.target;
    filesList = arraySwapIndex(
        getIndexOfFileList(myDragElement.dataset.key, filesList),
        getIndexOfFileList(myDropElement.dataset.key, filesList),
        filesList
    );
    fileInputMulti.files = arrayFilesToFileList(filesList);
    renderPreviews(filesList, multiSelectorUniqPreview, fileInputMulti);
}

$("#btnAgregarProducto").on("click", function () {
    // cambiar el titulo del modal
    $("#modalAgregarProductoLabel").html("Agregar Producto");
    // mostrar el files input
    //$("#multi-selector-uniq").show();
    // limpiar el formulario
    $("#productId").val("");
    $("#fromUpsert")[0].reset(); // Reinicia el formulario
    filesList = []; // Reinicia la lista de archivos seleccionados
    renderPreviews(filesList, multiSelectorUniqPreview, fileInputMulti); // Limpia las vistas previas de archivos
});

$("#btnAgregarProductoModal").on("click", function (event) {
    event.preventDefault();
    let productName = $("#nombreProducto").val();
    let productDescription = $("#descripcionProducto").val();
    let productPrice = $("#precioProducto").val();
    let productSKU = $("#skuProducto").val();
    let productBrandId = $("#marcaProducto").val();
    let productCategoryId = $("#categoriaProducto").val();
    let productFeature = $("#featureName").val();
    let data = new FormData();
    data.append("Name", productName);
    data.append("Description", productDescription);
    data.append("Price", productPrice);
    data.append("SKU", productSKU);
    data.append("BrandId", productBrandId);
    data.append("CategoryId", productCategoryId);
    data.append("FeatureDescription", productFeature);
    if ($("#productId").val() == "" || $("#productId").val() == null) {
        let url = "/Author/Products/Create";
        let files = $("#files").get(0).files;
        for (let i = 0; i < files.length; i++) {
            data.append("Files", files[i]);
        }
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                    $("#modalAgregarProducto").modal("hide");
                    $("#tablaProducto").DataTable().ajax.reload();
                    toastr.success(response.message);
                } else {
                    toastr.error(response.message);
                }
            }
        });
    } else {
        let url = "/Author/Products/Update";
        data.append("ProductId", $("#productId").val());
        let files = $("#files").get(0).files;
        for (let i = 0; i < files.length; i++) {
            data.append("Files", files[i]);
        }
        $.ajax({
            type: "PUT",
            url: url,
            data: data,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                    $("#modalAgregarProducto").modal("hide");
                    $("#tablaProducto").DataTable().ajax.reload();
                    toastr.success(response.message);
                } else {
                    toastr.error(response.message);
                }
            }
        });
    }
});

function GetProductById(url) {
    // cambiar el titulo del modal
    $("#modalAgregarProductoLabel").html("Editar Producto");
    $("#fromUpsert")[0].reset();
    filesList = [];
    renderPreviews(filesList, multiSelectorUniqPreview, fileInputMulti);
    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        success: function (data) {
            let product = data.data;
            $("#productId").val(product.productId);
            $("#nombreProducto").val(product.name);
            $("#descripcionProducto").val(product.description);
            $("#precioProducto").val(product.price);
            $("#skuProducto").val(product.sku);
            $("#marcaProducto").val(product.brandId.toUpperCase());
            $("#categoriaProducto").val(product.categoryId.toUpperCase());
            $("#featureName").val(product.featureDescription);
            //$("#multi-selector-uniq").hide();
            $("#modalAgregarProducto").modal("show");
        },
        error: function (error) {
            console.log(error);
        },
    });
}

function GetImagesProductById(id) {
    $("#productIdForImages").val(id);
    $.ajax({
        url: "/Author/Products/GetImagesProductById/" + id,
        type: "GET",
        dataType: "json",
        success: function (data) {
            let imagesProduct = data.data;
            const targetImagesProduct = document.querySelector("#ProductImages");
            targetImagesProduct.innerHTML = "";
            if (imagesProduct.length == 0) {
                targetImagesProduct.innerHTML += `
                        <div class="col-md-12">
                            <div class="alert alert-warning" role="alert">
                                <h4 class="alert-heading">No hay imagenes</h4>
                                <p>No hay imagenes para este producto.</p>
                            </div>
                        </div>
                        `;
            } else {
                imagesProduct.forEach((imagen) => {
                    targetImagesProduct.innerHTML += `
                            <div class="col-md-3">
                                <div class="card">
                                    <img src="${imagen.urlImagen}" class="card-img-top" alt="...">
                                    <div class="card-body">
                                        <button class="btn btn-danger btn-sm" onclick="DeleteImageProduct('${imagen.productImageId}')">Eliminar</button>
                                    </div>
                                </div>
                            </div>
                            `;
                });
            }
        },
        error: function (error) {
            console.log(error);
        },
    });
}

function DeleteImageProduct(id) {
    Swal.fire({
        title: "¿Estás seguro de eliminar la imagen?",
        text: "¡No podrás revertir esto!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        cancelButtonText: "Cancelar",
        confirmButtonText: "Sí, eliminar",
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Author/Products/DeleteImageProduct/" + id,
                type: "DELETE",
                success: function (response) {
                    console.log(response);
                    if (response.success) {
                        GetImagesProductById($("#productIdForImages").val());
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

function Delete(url) {
    Swal.fire({
        title: "¿Estás seguro de eliminar el producto?",
        text: "¡No podrás revertir esto!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        cancelButtonText: "Cancelar",
        confirmButtonText: "Sí, eliminar",
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (response) {
                    if (response.success) {
                        $("#tablaProducto").DataTable().ajax.reload();
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
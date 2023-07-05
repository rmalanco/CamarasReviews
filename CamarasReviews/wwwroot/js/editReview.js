let isReview = false;

$(document).ready(function () {
  imageReview();
  imageProduct();
});

$("#btnAgregarImagenesReview").click(function () {
  $("#modalImagenesLabel").text("Imagenes de la review");
  isReview = true;
  $("#Files").val("");
});
$("#btnAgregarImagenesProducto").click(function () {
  $("#modalImagenesLabel").text("Imagenes del producto");
  isReview = false;
  $("#Files").val("");
});

$("#btnAgregarImagenes").click(function () {
  let files = $("#Files").prop("files");
  let data = new FormData();
  if (isReview) {
    data.append("reviewId", $("#reviewId").val());
    for (let i = 0; i < files.length; i++) {
      data.append("files", files[i]);
    }
    $.ajax({
      url: "/Author/Reviews/UploadImagesReview",
      type: "POST",
      data: data,
      contentType: false,
      processData: false,
      success: function (data) {
        imageReview();
        $("#btnCerrarModal").click();
      },
    });
  } else {
    data.append("productId", $("#productId").val());
    for (let i = 0; i < files.length; i++) {
      data.append("files", files[i]);
    }
    $.ajax({
      url: "/Author/Products/UploadImagesProduct",
      type: "POST",
      data: data,
      contentType: false,
      processData: false,
      success: function (data) {
        imageProduct();
        $("#btnCerrarModal").click();
      },
    });
  }
});

function imageReview() {
  let reviewId = $("#reviewId").val();
  $.ajax({
    url: "/Author/Reviews/GetImagesReview",
    type: "POST",
    data: { reviewId: reviewId },
    dataType: "json",
    success: function (data) {
      let imagesReview = data.data;
      const targetImagesReview = document.querySelector("#ReviewPreview");
      targetImagesReview.innerHTML = "";
      if (imagesReview.length == 0) {
        const alert = document.createElement("div");
        alert.classList.add("alert", "alert-warning");
        const title = document.createElement("h4");
        title.textContent = "No hay imagenes";
        const paragraph = document.createElement("p");
        paragraph.textContent = "No hay imagenes para mostrar";
        alert.appendChild(title);
        alert.appendChild(paragraph);
        targetImagesReview.appendChild(alert);
      } else {
        imagesReview.forEach((imagen) => {
          const cardContainer = document.createElement("div");
          cardContainer.classList.add(
            "col-md-3",
            "mb-3",
            "d-flex",
            "align-items-stretch"
          );
          const card = document.createElement("div");
          card.classList.add("card");
          const cardBody = document.createElement("div");
          cardBody.classList.add(
            "card-body",
            "d-flex",
            "align-items-center",
            "justify-content-center"
          );
          const myImg = document.createElement("img");
          myImg.src = imagen.urlImagen;
          myImg.classList.add("img-fluid");
          const myButtonRemove = document.createElement("button");
          myButtonRemove.textContent = "Eliminar";
          myButtonRemove.classList.add("btn", "btn-danger", "mt-2");
          myButtonRemove.addEventListener("click", (event) => {
            event.preventDefault();
            Swal.fire({
              title: "¿Estas seguro de eliminar esta imagen?",
              text: "La imagen se eliminara permanentemente",
              icon: "warning",
              showCancelButton: true,
              confirmButtonColor: "#3085d6",
              cancelButtonColor: "#d33",
              confirmButtonText: "Si, eliminar",
              cancelButtonText: "Cancelar",
            }).then((result) => {
              if (result.isConfirmed) {
                $.ajax({
                  url: "/Author/Reviews/DeleteImageReview",
                  type: "POST",
                  data: { reviewImageId: imagen.reviewImageId },
                  dataType: "json",
                  success: function (data) {
                    if (data.success) {
                      toastr.success(data.message);
                      imageReview();
                    } else {
                      toastr.error(data.message);
                      imageReview();
                    }
                  },
                  error: function (error) {
                    console.log(error);
                  },
                });
              }
            });
          });
          cardBody.appendChild(myImg);
          card.appendChild(cardBody);
          card.appendChild(myButtonRemove);
          cardContainer.appendChild(card);
          targetImagesReview.appendChild(cardContainer);
        });
      }
    },
    error: function (error) {
      console.log(error);
    },
  });
}

function imageProduct() {
  let productId = $("#productId").val();
  $.ajax({
    url: "/Author/Products/GetImagesProduct",
    type: "POST",
    data: { productId: productId },
    dataType: "json",
    success: function (data) {
      let imagesProduct = data.data;
      const targetImagesProduct = document.querySelector("#ProductPreview");
      targetImagesProduct.innerHTML = "";
      if (imagesProduct.length == 0) {
        const alert = document.createElement("div");
        alert.classList.add("alert", "alert-warning");
        const title = document.createElement("h4");
        title.textContent = "No hay imagenes";
        const paragraph = document.createElement("p");
        paragraph.textContent = "No hay imagenes para mostrar";
        alert.appendChild(title);
        alert.appendChild(paragraph);
        targetImagesProduct.appendChild(alert);
      } else {
        imagesProduct.forEach((imagen) => {
          const cardContainer = document.createElement("div");
          cardContainer.classList.add(
            "col-md-3",
            "mb-3",
            "d-flex",
            "align-items-stretch"
          );
          const card = document.createElement("div");
          card.classList.add("card");
          const cardBody = document.createElement("div");
          cardBody.classList.add(
            "card-body",
            "d-flex",
            "align-items-center",
            "justify-content-center"
          );
          const myImg = document.createElement("img");
          myImg.src = imagen.urlImagen;
          myImg.classList.add("img-fluid");
          const myButtonRemove = document.createElement("button");
          myButtonRemove.textContent = "Eliminar";
          myButtonRemove.classList.add("btn", "btn-danger", "mt-2");
          myButtonRemove.addEventListener("click", (event) => {
            event.preventDefault();
            Swal.fire({
              title: "¿Estas seguro de eliminar esta imagen?",
              text: "La imagen se eliminara permanentemente",
              icon: "warning",
              showCancelButton: true,
              confirmButtonColor: "#3085d6",
              cancelButtonColor: "#d33",
              confirmButtonText: "Si, eliminar",
              cancelButtonText: "Cancelar",
            }).then((result) => {
              if (result.isConfirmed) {
                $.ajax({
                  url: "/Author/Reviews/DeleteImageProduct",
                  type: "POST",
                  data: { productImageId: imagen.productImageId },
                  dataType: "json",
                  success: function (data) {
                    if (data.success) {
                      toastr.success(data.message);
                      imageProduct();
                    } else {
                      toastr.error(data.message);
                      imageProduct();
                    }
                  },
                  error: function (error) {
                    console.log(error);
                  },
                });
              }
            });
          });
          cardBody.appendChild(myImg);
          card.appendChild(cardBody);
          card.appendChild(myButtonRemove);
          cardContainer.appendChild(card);
          targetImagesProduct.appendChild(cardContainer);
        });
      }
    },
    error: function (error) {
      console.log(error);
    },
  });
}

function createImagePreview(containerId, inputId, previewId) {
  let filesList = [];
  const classDragOver = "drag-over";
  const fileInputMulti = document.querySelector(`#${containerId} #${inputId}`);
  const multiSelectorUniqPreview = document.querySelector(
    `#${containerId} #${previewId}`
  );

  function getIndexOfFileList(name, list) {
    return list.reduce(
      (position, file, index) => (file.name === name ? index : position),
      -1
    );
  }

  async function encodeFileToText(file) {
    return file.text().then((text) => text);
  }

  async function getUniqFiles(newFiles, currentListFiles) {
    return new Promise((resolve) => {
      Promise.all(
        newFiles.map((inputFile) => encodeFileToText(inputFile))
      ).then((inputFilesText) => {
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
      });
    });
  }

  function renderPreviews(currentFileList, target, inputFile) {
    target.innerHTML = "";
    currentFileList.forEach((file, index) => {
      const cardContainer = document.createElement("div");
      cardContainer.classList.add(
        "col-md-4",
        "mb-3",
        "d-flex",
        "align-items-stretch"
      );

      const card = document.createElement("div");
      card.classList.add("card");

      const cardBody = document.createElement("div");
      cardBody.classList.add(
        "card-body",
        "d-flex",
        "align-items-center",
        "justify-content-center"
      );

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
}

createImagePreview("MultiImages", "Files", "ImagesPreview");

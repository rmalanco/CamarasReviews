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

createImagePreview("MultiReviewImages", "reviewFiles", "ReviewImagesPreview");
createImagePreview(
  "MultiProductImages",
  "productFiles",
  "ProductImagesPreview"
);

function downloadPhieu2(url) {
  $.ajax({
    async: false,
    method: "GET",
    dataType: "json", //'json'
    url: url,
    contentType: "application/json",
  })
    .done(function (data) {
      const byteCharacters = atob(data.fileContents);
      const byteNumbers = new Array(byteCharacters.length);
      for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }

      const byteArray = new Uint8Array(byteNumbers);
      const blob = new Blob([byteArray], { type: data.contentType });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.href = url;
      a.download = data.fileName;
      document.body.appendChild(a);
      a.click();
      window.URL.revokeObjectURL(url);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
      // If fail
      alert(textStatus + ": " + errorThrown);
    });
}

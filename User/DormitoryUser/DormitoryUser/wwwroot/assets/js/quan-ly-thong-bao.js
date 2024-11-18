$(document).ready(function () {
  function checkInput() {
    var header_area = $("#header").val();
    var content_area = $("#noi-dung-thong-bao").val();
    if (header_area != "" && content_area != "") {
      $("#submit-button").removeClass("disabled");
    } else {
      $("#submit-button").addClass("disabled");
    }
  }
  setInterval(checkInput, 300);

  var token = sessionStorage.getItem("token");

  var getRequestOptions = {
    method: "GET",
    credentials: "omit",
    headers: {
      Authorization: "Bearer " + token,
      "Content-Type": "text/plain",
    },
    redirect: "follow",
  };

  fetch("http://25.43.134.201:8080/lv1/listnoti", getRequestOptions)
    .then((response) => response.json())
    .then((result) => {
      if (result.message == "Get all notification OK") {
        var notiList = result.list_notification;
        for (var i = 0; i < Object.keys(notiList).length; i++) {
          notiList[i].CreatedAt = notiList[i].CreatedAt.substring(0, 10);
          CreatedAt_split = notiList[i].CreatedAt.split("-");
          notiList[i].CreatedAt =
            CreatedAt_split[2] +
            "/" +
            CreatedAt_split[1] +
            "/" +
            CreatedAt_split[0];

          notiList[i].UpdatedAt = notiList[i].UpdatedAt.substring(0, 10);
          UpdatedAt_split = notiList[i].UpdatedAt.split("-");
          notiList[i].UpdatedAt =
            UpdatedAt_split[2] +
            "/" +
            UpdatedAt_split[1] +
            "/" +
            UpdatedAt_split[0];
        }
        handleTable(notiList);
      } else {
        alert("Có lỗi xảy ra");
      }
    })
    .catch((error) => {
      console.log("Không kết nối được tới máy chủ", error);
      alert("Không kết nối được tới máy chủ");
    });

  function handleTable(data) {
    var notiTable = new Tabulator("#noti-table", {
      height: 400, // set height of table (in CSS or here), this enables the Virtual DOM and improves render speed dramatically (can be any valid css height value)
      data: data, //assign data to table
      virtualDom: true,
      layout: "fitColumns", //fit columns to width of table (optional)
      initialSort: [
        { column: "ID", dir: "desc" }, //sort by this first
      ],
      columns: [
        //Define Table Columns
        {
          title: "Mã",
          field: "ID",
          width: "80",
          hozAlign: "center",
          sorter: "number",
        },
        { title: "Ngày đăng", width: 150, field: "CreatedAt", sorter: "date" },
        { title: "Ngày cập nhật", field: "UpdatedAt", sorter: "date", visible: false },
        { title: "Tiêu đề", field: "title", sorter: "string" },
        {
          title: "Nội dung",
          field: "content",
          sorter: "string",
          visible: false,
        },
        
      ],
      rowClick: function (e, row) {
        var ID = row.getData().ID;
        var title = row.getData().title;
        var CreatedAt = row.getData().CreatedAt;
        var UpdatedAt = row.getData().UpdatedAt;
        var content = row.getData().content;
        $("#ID").text(ID);
        $("#title").text(title);
        $("#CreatedAt").text(CreatedAt);
        $("#UpdatedAt").text(UpdatedAt);
        $("#content").text(content);
        $("#noti-modal").modal("show");
        
      },
    });

    var fieldEl = document.getElementById("filter-field");
    var typeEl = document.getElementById("filter-type");
    var valueEl = document.getElementById("filter-value");

    //Custom filter example
    function customFilter(data) {
      return data.car && data.rating < 3;
    }

    //Trigger setFilter function with correct parameters
    function updateFilter() {
      var filterVal = fieldEl.options[fieldEl.selectedIndex].value;
      var typeVal = typeEl.options[typeEl.selectedIndex].value;

      var filter = filterVal == "function" ? customFilter : filterVal;

      if (filterVal == "function") {
        typeEl.disabled = true;
        valueEl.disabled = true;
      } else {
        typeEl.disabled = false;
        valueEl.disabled = false;
      }

      if (filterVal) {
        notiTable.setFilter(filter, typeVal, valueEl.value);
      }
    }

    //Update filters on value change
    document
      .getElementById("filter-field")
      .addEventListener("change", updateFilter);
    document
      .getElementById("filter-type")
      .addEventListener("change", updateFilter);
    document
      .getElementById("filter-value")
      .addEventListener("keyup", updateFilter);
  }

  $("#submit-button").on("click", function () {
      let title = $("#header").val();
      let content = $("#noi-dung-thong-bao").val();
      submitRequest(title, content);
    });
    
    function submitRequest(title, content) {
      var raw =
        '{\n  "title" : "' + title + '",\n  "content" : "' + content + '"\n   \n}';
      var requestOptions = {
        method: "POST",
        credentials: "omit",
        headers: {
          Authorization: "Bearer " + token,
          "Content-Type": "text/plain",
        },
        body: raw,
        redirect: "follow",
      };
    
      fetch("http://25.43.134.201:8080/lv1/newnoti", requestOptions)
        .then((response) => response.json())
        .then((result) => {
          if (result.message == "Create new notification OK") {
            alert("Đăng thông báo thành công");
            window.location.reload();
          } else {
            alert("Không gửi được thông báo");
          }
        })
        .catch((error) => {
          console.log("Không kết nối được tới máy chủ", error);
        });
    }
  });

  

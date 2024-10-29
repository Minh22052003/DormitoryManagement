var token = sessionStorage.getItem("token");
var myHeaders = new Headers();
//myHeaders.append("Content-Type", "text/plain", bearer);

var requestsRequestOption = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

fetch("http://25.43.134.201:8080/lv1/listrequest", requestsRequestOption)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Get list requests OK") {
      var request = 0;
      console.log(result.list_request);
      for (var i = 0; i < Object.keys(result.list_request).length; i++) {
        if (result.list_request[i].status == "new request") {
          request++;
        }
      }
      $("#request").text(request);
      let requestList = result.list_request;
      handleRequestList(requestList);
    } else {
      alert("Có lỗi xảy ra");
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });

function handleRequestList(req) {
  var reqLength = Object.keys(req).length;

  for (i = reqLength - 1; i >= 0; i--) {
    req[i].CreatedAt = req[i].CreatedAt.substring(0, 10);
    posted_date_split = req[i].CreatedAt.split("-");
    req[i].CreatedAt =
      posted_date_split[2] +
      "/" +
      posted_date_split[1] +
      "/" +
      posted_date_split[0];

    req[i].UpdatedAt = req[i].UpdatedAt.substring(0, 10);
    reply_date_split = req[i].UpdatedAt.split("-");
    req[i].UpdatedAt =
      reply_date_split[2] +
      "/" +
      reply_date_split[1] +
      "/" +
      reply_date_split[0];

    if (req[i].status == "replied") {
      req[i].status = "Đã phản hồi";
    } else {
      req[i].status = "Chưa phản hồi";
    }
  }

  var studentsTable = new Tabulator("#student-parking-table", {
    height: 400, // set height of table (in CSS or here), this enables the Virtual DOM and improves render speed dramatically (can be any valid css height value)
    data: req, //assign data to table
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
        sorter: "string",
      },
      { title: "Tiêu đề", field: "title", sorter: "string" },
      { title: "Nội dung", field: "message", sorter: "string", visible: false },
      { title: "Phản hồi", field: "reply", sorter: "string", visible: false },
      { title: "MSSV", width: "130", field: "student_id", sorter: "number" },
      {
        title: "Ngày gửi",
        field: "CreatedAt",
        width: "200",
        sorter: "date",
        sorterParams: {
          format: "DD-MM-YYYY",
          alignEmptyValues: "top",
        },
      },
      { title: "Tình trạng", width: "150", field: "status", sorter: "string" },
    ],
    rowClick: function (e, row) {
      var ID = row.getData().ID;
      var title = row.getData().title;
      var message = row.getData().message;
      var student_id = row.getData().student_id;
      var CreatedAt = row.getData().CreatedAt;
      var status = row.getData().status;
      var reply = row.getData().reply;
      console.log(status);

      $("#id").text(ID);
      $("#title").text(title);
      $("#message").text(message);
      $("#student_id").text(student_id);
      $("#CreatedAt ").text(CreatedAt);
      $("#request-status").text(status);
      $("#reply").val(reply);
      $("#student-request-modal").modal("show");
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
      studentsTable.setFilter(filter, typeVal, valueEl.value);
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

$("#send-button").on("click", function () {
  var reply = $("#reply").val();
  var requestID = $("#id").text();
  console.log(reply);
  submitRequest(reply, requestID);
});

function submitRequest(reply, requestID) {
  var raw =
    '{\n  "request_id":' + requestID + ',\n  "reply":"' + reply + '"\n   \n}';
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

  fetch("http://25.43.134.201:8080/lv1/replyrequest", requestOptions)
    .then((response) => response.json())
    .then((result) => {
      if (result.message == "Reply request OK") {
        alert("Trả lời yêu cầu thành công");
        window.location.reload();
      } else {
        alert("Không gửi được câu trả lời");
      }
    })
    .catch((error) => {
      console.log("Không kết nối được tới máy chủ", error);
    });
}

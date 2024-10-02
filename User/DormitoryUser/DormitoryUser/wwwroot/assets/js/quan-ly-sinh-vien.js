var token = sessionStorage.getItem("token");
var myHeaders = new Headers();

var getRequestOptions = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

var postRequestOptions = {
  method: "POST",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

fetch("http://25.43.134.201:8080/lv1/getallmoneymanage", requestOptions)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Get all money manage OK") {
      var unpaid = 0;
      var list = result.list_money_manage;
      for (var i = 0; i < Object.keys(list).length; i++) {
        if (list[i].status == "Unpaid") {
          unpaid++;
        }
      }
      var paid = 0;
      var list = result.list_money_manage;
      for (var i = 0; i < Object.keys(list).length; i++) {
        if (list[i].status == "Paid") {
          paid++;
        }
      }
      $("#unpaid-student").text(unpaid);
      $("#paid-student").text(paid);
      fetchStatus(list);
    } else {
      alert("Có lỗi xảy ra");
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });

function fetchStatus(paymentList) {
  fetch("http://25.43.134.201:8080/lv1/allstudent", getRequestOptions)
    .then((response) => response.json())
    .then((result) => {
      if (result.message == "Get list student OK") {
        var studentsList = result.list_student;
        for (var i = 0; i < Object.keys(studentsList).length; i++) {
          studentsList[i].stt = i + 1;
          if (typeof paymentList[i] == "undefined") {
            studentsList[i].status = "";
          } else {
            if (paymentList[i].status == "Unpaid") {
              studentsList[i].status = "Chưa thanh toán";
            } else if (paymentList[i].status == "Paid") {
              studentsList[i].status = "Đã thanh toán";
            } else {
              studentsList[i].status = "";
            }
          }
        }
        var studentsTotal = Object.keys(studentsList).length;
        $("#total-student").text(studentsTotal);

        handleTable(studentsList);
      } else {
        alert("Có lỗi xảy ra");
      }
    })
    .catch((error) => {
      console.log("Không kết nối được tới máy chủ", error);
      alert("Không kết nối được tới máy chủ");
    });
}

function handleTable(data) {
  //create Tabulator on DOM element with id "example-table"
  var studentsTable = new Tabulator("#student-table", {
    height: 400, // set height of table (in CSS or here), this enables the Virtual DOM and improves render speed dramatically (can be any valid css height value)
    data: data, //assign data to table
    virtualDom: true,
    layout: "fitColumns", //fit columns to width of table (optional)
    columns: [
      //Define Table Columns
      {
        title: "STT",
        field: "stt",
        width: 80,
        hozAlign: "center",
        sorter: "number",
      },
      { title: "Họ và tên", field: "name", sorter: "string" },
      { title: "MSSV", width: 100, field: "studentid", sorter: "number" },
      { title: "Phòng", width: 100, field: "room", sorter: "string" },
      {
        title: "SĐT",
        field: "contact",
        sorter: "number",
      },
      { title: "Tiền KTX", field: "status", sorter: "string" },
      {
        title: "Ngày cập nhật",
        field: "UpdatedAt",
        sorter: "string",
        visible: false,
      },
      {
        title: "Ngày đăng kí",
        field: "CreatedAt",
        sorter: "string",
        visible: false,
      },
      { title: "Địa chỉ", field: "address", sorter: "string", visible: false },
      { title: "Ngày sinh", field: "dob", sorter: "date", visible: false },
    ],
    rowClick: function (e, row) {
      var name = row.getData().name;
      var studentid = row.getData().studentid;
      var room = row.getData().room;
      var dob = row.getData().dob;
      var address = row.getData().address;
      var contact = row.getData().contact;
      var status = row.getData().status;
      var CreatedAt = row.getData().CreatedAt;
      var UpdatedAt = row.getData().UpdatedAt;

      CreatedAt = CreatedAt.substring(0, 10);
      CreatedAt_split = CreatedAt.split("-");
      CreatedAt =
        CreatedAt_split[2] +
        "/" +
        CreatedAt_split[1] +
        "/" +
        CreatedAt_split[0];

      UpdatedAt = UpdatedAt.substring(0, 10);
      UpdatedAt_split = UpdatedAt.split("-");
      UpdatedAt =
        UpdatedAt_split[2] +
        "/" +
        UpdatedAt_split[1] +
        "/" +
        UpdatedAt_split[0];

      $("#name").text(name);
      $("#studentid").text(studentid);
      $("#room").text(room);
      $("#dob").text(dob);
      $("#address").text(address);
      $("#contact").text(contact);
      $("#status").val(status).change();
      $("#CreatedAt").text(CreatedAt);
      $("#UpdatedAt").text(UpdatedAt);
      $("#student-table-modal").modal("show");
    },
  });

  //Define variables for input elements
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

  document
    .getElementById("download-xlsx")
    .addEventListener("click", function () {
      studentsTable.download("xlsx", "danhsachsinhvienKTX.xlsx", {
        sheetName: "Danh sách",
      });
    });
}

$("#save-button").on("click", function () {
  var id = $("#studentid").text();
  var status = $("#status").val();
  if (status == "Chưa thanh toán") {
    status = "Unpaid";
  } else if (status == "Đã thanh toán") {
    status = "Paid";
  }
console.log(status)
  var raw =
    '{\n  "money_manage_id":' +
    id +
    ',\n  "status":"' +
    status +
    '"\n   \n}';

  var requestOptions = {
    method: "POST",
    credentials: "omit",
    headers: {
      Authorization: "Bearer " + token,
      "Content-Type": "text/plain",
    },
    redirect: "follow",
    body: raw,
  };

  fetch("http://25.43.134.201:8080/lv1/updatepayment", requestOptions)
    .then((response) => response.json())
    .then((result) => {
      console.log(result);
      if (result.message == "Update payment status OK") {
        alert("Cập nhật trạng thái đóng tiền thành công!");
        location.reload();
        return false;
      } else {
        alert("Có lỗi xảy ra");
      }
    })
    .catch((error) => {
      console.log("Không kết nối được tới máy chủ", error);
      alert("Không kết nối được tới máy chủ");
    });
});

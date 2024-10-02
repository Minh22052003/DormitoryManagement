var token = sessionStorage.getItem("token");
var myHeaders = new Headers();
//myHeaders.append("Content-Type", "text/plain", bearer);

var roomRequestOptions = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

fetch("http://25.43.134.201:8080/lv0/roominfo", roomRequestOptions)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Get room info successfully") {
      $("#student-room").text(result.room.roomID);
      $("#room-fee").text(result.room.roomPrice.toLocaleString());
    } else {
      alert("Có lỗi xảy ra");
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });

var roomMoneyRequestOptions = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

var tabledata;

fetch("http://25.43.134.201:8080/lv0/dormmoney", roomMoneyRequestOptions)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Dorm money history") {
      tabledata = result.money_list;

      if (result.message == "Dorm money history") {
        var money_history = result.money_list;
        console.log(money_history[Object.keys(money_history).length - 1]);
        if (
          money_history[Object.keys(money_history).length - 1].status ==
          "Unpaid"
        ) {
          $("#fee-status").text("Chưa thanh toán");
        } else {
          $("#fee-status").text("Đã thanh toán");
        }
      } else {
        alert("Có lỗi xảy ra");
      }

      for (var i = 0; i < Object.keys(tabledata).length; i++) {
        tabledata[i].stt = i + 1;
        if (typeof tabledata[i] == "undefined") {
          tabledata[i].status = "";
        } else {
          if (tabledata[i].status == "Unpaid") {
            tabledata[i].status = "Chưa thanh toán";
          } else if (tabledata[i].status == "Paid") {
            tabledata[i].status = "Đã thanh toán";
          } else {
            tabledata[i].status = "";
          }
        }
      }
      handleTable(tabledata);
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });
let table;
function handleTable(data) {
  table = new Tabulator("#table", {
    height: 205,
    data: data,
    layout: "fitColumns",
    columns: [
      { title: "Tháng", field: "month", sorter: "number" },
      { title: "Năm", field: "year", sorter: "number" },
      { title: "Số tiền", field: "money", sorter: "number" },
      { title: "Trạng thái", field: "status", sorter: "string" },
    ],
  });
}
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
    table.setFilter(filter, typeVal, valueEl.value);
  }
}

document
  .getElementById("filter-field")
  .addEventListener("change", updateFilter);
document.getElementById("filter-type").addEventListener("change", updateFilter);
document.getElementById("filter-value").addEventListener("keyup", updateFilter);


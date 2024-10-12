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

fetch("http://25.43.134.201:8080/lv1/listroom", roomRequestOptions)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Get list room OK") {
      var roomList = result.list_rooms;
      for (var i = 0; i < Object.keys(roomList).length; i++) {
        roomList[i].stt = i + 1;
        roomList[i].roomPrice = roomList[i].roomPrice.toLocaleString();
        roomList[i].empty = roomList[i].roomMax - roomList[i].occupied;
      }
      handleRoomTable(roomList);
    } else {
      alert("Có lỗi xảy ra");
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });

function handleRoomTable(roomList) {
  var roomsTable = new Tabulator("#room-table", {
    height: 400, // set height of table (in CSS or here), this enables the Virtual DOM and improves render speed dramatically (can be any valid css height value)
    data: roomList, //assign data to table
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
      {
        title: "Phòng",
        field: "roomID",
        width: 120,
        hozAlign: "center",
        sorter: "number",
        hozAlign: "center",
      },
      {
        title: "Giá",
        field: "roomPrice",
        hozAlign: "center",
        sorter: "number",
      },
      {
        title: "Giới hạn",
        field: "roomMax",
        hozAlign: "center",
        sorter: "number",
      },
      {
        title: "Đang có",
        field: "occupied",
        hozAlign: "center",
        sorter: "number",
      },
      {
        title: "Còn trống",
        field: "empty",
        hozAlign: "center",
        sorter: "number",
      },
      {
        title: "Ngày cập nhật",
        field: "update_date",
        hozAlign: "center",
        sorter: "string",
        visible: false,
      },
      {
        title: "Ngày đăng kí",
        field: "register_date",
        sorter: "string",
        visible: false,
      },
    ],
    rowClick: function (e, row) {
      var room = row.getData().roomID;
      var limit = row.getData().roomMax;
      var empty = row.getData().empty;
      var price = row.getData().roomPrice;
      var occupied = row.getData().occupied;
      var update_date = row.getData().UpdatedAt;
      var register_date = row.getData().CreatedAt;

      fetch(
        "http://25.43.134.201:8080/lv1/studentbyroom?id=" + room,
        roomRequestOptions
      )
        .then((response) => response.json())
        .then((result) => {
          if (result.message == "Get list student OK") {
            var studentsList = result.list_students;
            var table = new Tabulator("#students-in-room", {
              data: studentsList,
              layout: "fitDataStretch",
              columns: [
                //define the table columns
                {
                  title: "Tên",
                  field: "name",
                  sorter: "number",
                },
                {
                  title: "MSSV",
                  field: "studentid",
                  sorter: "number",
                },
                {
                  title: "Ngày sinh",
                  field: "dob",
                  sorter: "date",
                },
                {
                  title: "SĐT",
                  field: "contact",
                  sorter: "number",
                },
                {
                  title: "Địa chỉ",
                  field: "address",
                  sorter: "date",
                },
              ],
            });
          } else {
            alert("Có lỗi xảy ra");
          }
        })
        .catch((error) => {
          console.log("Không kết nối được tới máy chủ", error);
          alert("Không kết nối được tới máy chủ");
        });

      register_date = register_date.substring(0, 10);
      register_date_split = register_date.split("-");
      register_date =
        register_date_split[2] +
        "/" +
        register_date_split[1] +
        "/" +
        register_date_split[0];

      update_date = update_date.substring(0, 10);
      update_date_split = update_date.split("-");
      update_date =
        update_date_split[2] +
        "/" +
        update_date_split[1] +
        "/" +
        update_date_split[0];

      $("#room").text(room);
      $("#price").text(price);
      $("#limit").text(limit);
      $("#occupied").text(occupied);
      $("#empty").text(empty);
      $("#register_date").text(register_date);
      $("#update_date").text(update_date);

      fetch("http://25.43.134.201:8080/lv1/listfac/" + room, roomRequestOptions)
        .then((response) => response.json())
        .then((result) => {
          if (result.message == "Get list facilities by roomid OK") {
            var facilitiesList = result.list_facilities;
            var table = new Tabulator("#facilities-in-room", {
              data: facilitiesList,
              layout: "fitColumns",
              columns: [
                //define the table columns
                {
                  title: "ID",
                  field: "ID",
                  sorter: "number",
                },
                {
                  title: "Tên",
                  field: "facility_name",
                  sorter: "string",
                },
                {
                  title: "SL hiện tại",
                  field: "quantity",
                  sorter: "number",
                },
                {
                  title: "SL ban đầu",
                  field: "default",
                  sorter: "number",
                },
              ],
              rowClick: function (e, row) {
                var facilityID = row.getData().ID;
                fetch("http://25.43.134.201:8080/lv1/fac/"+facilityID, roomRequestOptions)
                .then((response) => response.json())
                .then((result) => {
                  if (result.message == "Get facility info success") {
                    $("#fac_des").text(result.facility.description);
                  } else {
                    alert("Có lỗi xảy ra");
                  }
                })
                .catch((error) => {
                  console.log("Không kết nối được tới máy chủ", error);
                  alert("Không kết nối được tới máy chủ");
                });

                var quantity = row.getData().quantity;
                $("#fac_quantity").val(quantity);
                $("#fac_ID").text(facilityID);

                $("#facility-table-modal").modal("show");

                function UpdateFacility(facilityID, updateValue) {
                  fetch(
                    "http://25.43.134.201:8080/lv1/updatefacmng?fmngid=" +
                      facilityID +
                      "&quantity=" +
                      updateValue,
                    roomRequestOptions
                  )
                    .then((response) => response.json())
                    .then((result) => {
                      if (result.message == "Update facility manage OK") {
                        alert("Cập nhật CSVC thành công");
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
                }

                $("#facility-update-button").on("click", function () {
                  let newAmount = $("#fac_quantity").val();
                  UpdateFacility(facilityID, newAmount);
                });
              },
            });
          } else {
            alert("Có lỗi xảy ra");
          }
        })
        .catch((error) => {
          console.log("Không kết nối được tới máy chủ", error);
          alert("Không kết nối được tới máy chủ");
        });

      $("#room-table-modal").modal("show");
    },
  });
  var fieldEl = document.getElementById("filter_field");
  var typeEl = document.getElementById("filter_type");
  var valueEl = document.getElementById("filter_value");

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
      roomsTable.setFilter(filter, typeVal, valueEl.value);
    }
  }

  //Update filters on value change
  document
    .getElementById("filter_field")
    .addEventListener("change", updateFilter);
  document
    .getElementById("filter_type")
    .addEventListener("change", updateFilter);
  document
    .getElementById("filter_value")
    .addEventListener("keyup", updateFilter);

  document
    .getElementById("download-room-list-xlsx")
    .addEventListener("click", function () {
      roomsTable.download("xlsx", "danhsachsinhvienKTX.xlsx", {
        sheetName: "Danh sách",
      });
    });
}

var requestOptions = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};
//Amount of beds
fetch("http://25.43.134.201:8080/lv1/listroom", requestOptions)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Get list room OK") {
      $("#total-room").text(Object.keys(result.list_rooms).length)
      $("#used-beds").text(result.occupied);
      $("#empty-beds").text(result.max - result.occupied);
      $("#total-beds").text(result.max);
    } else {
      alert("Có lỗi xảy ra");
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });

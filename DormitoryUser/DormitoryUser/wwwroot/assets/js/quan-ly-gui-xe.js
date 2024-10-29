var studentsParkingList = [
  {
    name: "Trần Đình Vũ",
    student_id: "20176126",
    room: "201",
    type_of_vehicle: "Xe máy",
    plate: "1111-1111",
  },
  {
    name: "Trần Đình A",
    student_id: "20176122",
    room: "201",
    type_of_vehicle: "Xe máy",
    plate: "1111-1111",
  },
  {
    name: "Trần Đình B",
    student_id: "20175343",
    room: "202",
    type_of_vehicle: "Xe đạp",
    plate: "Không có",
  },
  {
    name: "Trần Đình C",
    student_id: "201761512",
    room: "101",
    type_of_vehicle: "Xe máy",
    plate: "1111-2221",
  },
  {
    name: "Trần Đình D",
    student_id: "20176006",
    room: "201",
    type_of_vehicle: "Xe đạp điện",
    plate: "1111-1111",
  },
  {
    name: "Trần Mai Mai",
    student_id: "20176226",
    room: "205",
    type_of_vehicle: "Xe đạp điện",
    plate: "0931-2341",
  },
  {
    name: "Trần Đình Tiến",
    student_id: "20176516",
    room: "301",
    type_of_vehicle: "Xe đạp",
    plate: "Không có",
  },
  {
    name: "Trần Đình Trần",
    student_id: "20176426",
    room: "105",
    type_of_vehicle: "Xe đạp",
    plate: "Không có",
  },
];

for (var i = 0; i < Object.keys(studentsParkingList).length; i++) {
  studentsParkingList[i].stt = i + 1;
}

var studentsTable = new Tabulator("#student-parking-table", {
  height: 400, // set height of table (in CSS or here), this enables the Virtual DOM and improves render speed dramatically (can be any valid css height value)
  data: studentsParkingList, //assign data to table
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
    { title: "MSSV", field: "student_id", sorter: "number" },
    { title: "Phòng", field: "room", sorter: "string" },
    { title: "Loại xe", field: "type_of_vehicle", sorter: "string" },
    { title: "Biển xe", field: "plate", sorter: "string" },
    {
      title: "Ngày cập nhật",
      field: "update_date",
      visible: false,
      sorter: "string",
    },
    {
      title: "Ngày đăng kí",
      field: "register_date",
      visible: false,
      sorter: "string",
    },
  ],
  rowClick: function (e, row) {
    var id = row.getData().id;
    var name = row.getData().name;
    var student_id = row.getData().student_id;
    var room = row.getData().room;
    var type_of_vehicle = row.getData().type_of_vehicle;
    var update_date = row.getData().update_date;
    var plate = row.getData().plate;
    var register_date = row.getData().register_date;
    $("#student-table-modal").modal("show");
    $('input[id="student-name"]').val(name);
    $('input[id="student-id"]').val(student_id);
    $('input[id="student-room"]').val(room);
    $('input[id="type_of_vehicle"]').val(type_of_vehicle);
    $('input[id="plate"]').val(plate);
    $("#update_date").text(update_date);
    $("#register_date").text(register_date);
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
document.getElementById("filter-type").addEventListener("change", updateFilter);
document.getElementById("filter-value").addEventListener("keyup", updateFilter);

document.getElementById("download-xlsx").addEventListener("click", function () {
  studentsTable.download("xlsx", "danhsachsinhvienguixe.xlsx", {
    sheetName: "Danh sách",
  });
});

$("#add-student").on("click",function() {
  $("#add-student-table-modal").modal("show");
})

$("#change-student-button").click(function() {
  //gui request;
})

$("#add-student-button").click(function() {
  //gui-request;
})

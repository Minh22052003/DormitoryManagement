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
      $("#current-number").text(result.room.occupied);
      $("#max-number").text(result.room.roomMax);
    } else {
      alert("Có lỗi xảy ra");
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });

var friendsRequestOptions = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

let tabledata;

fetch("http://25.43.134.201:8080/lv0/friends", friendsRequestOptions)
.then((response) => response.json())
.then((result) => {
    console.log(result);
    tabledata = result.friend_list;
    for (var i = 0; i < Object.keys(tabledata).length; i++) {
      tabledata[i].stt = i + 1;
    }
    handleTable();
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
  });



function handleTable() {
  let table = new Tabulator("#roomate-table", {
    height: "100%", // set height of table (in CSS or here), this enables the Virtual DOM and improves render speed dramatically (can be any valid css height value)
    data: tabledata, //assign data to table
    layout: "fitDataFill", //fit columns to width of table (optional)
    columns: [
      //Define Table Columns
      {
        title: "STT",
        field: "stt",
        width: 70,
        hozAlign: "center",
        headerSort: false,
      },
      { title: "Họ và tên", field: "name", headerSort: false, width: 200 },
      { title: "MSSV", field: "studentid", headerSort: false },
      { title: "Ngày sinh", field: "dob", headerSort: false },
      {
        title: "SĐT",
        field: "contact",
        headerSort: false,
      },
      { title: "Địa chỉ", field: "address", headerSort: false },
    ],
  });
}

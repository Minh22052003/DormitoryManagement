var token = sessionStorage.getItem("token");
var myHeaders = new Headers();
//myHeaders.append("Content-Type", "text/plain", bearer);
var requestOptions = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

fetch("http://25.43.134.201:8080/lv0/usrinfo", requestOptions)
  .then((response) => response.json())
  .then((result) => {
    var student_name = result.student_info.name;
    sessionStorage.setItem("username", student_name);

    var register_date = result.student_info.CreatedAt.substring(0, 10);
    register_date_split = register_date.split("-");
    register_date =
      register_date_split[2] +
      "/" +
      register_date_split[1] +
      "/" +
      register_date_split[0];

    var update_date = result.student_info.UpdatedAt.substring(0, 10);

    update_date_split = update_date.split("-");
    update_date =
      update_date_split[2] +
      "/" +
      update_date_split[1] +
      "/" +
      update_date_split[0];

    
    $("#student-id").text(result.student_info.studentid);
    $("#student-birth").text(result.student_info.dob);
    $("#student-address").text(result.student_info.address);
    $("#student-contact").text(result.student_info.contact);
    $("#student-room").text(result.student_info.room);
    $("#student-register-date").text(register_date);
    $("#student-update-date").text(update_date);
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
  });

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

fetch("http://25.43.134.201:8080/lv0/dormmoney", roomMoneyRequestOptions)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Dorm money history") {
      var money_history = result.money_list;
      console.log(money_history[Object.keys(money_history).length-1])
      if(money_history[Object.keys(money_history).length-1].status=="unpaid"){
        $("#fee-status").text("Chưa thanh toán");
      }else{
        $("#fee-status").text("Đã thanh toán");
      }
      
    } else {
      alert("Có lỗi xảy ra");
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });

// var token = sessionStorage.getItem("token");
// var myHeaders = new Headers();
// //myHeaders.append("Content-Type", "text/plain", bearer);

// var requestOptions = {
//   method: "GET",
//   credentials: "omit",
//   headers: {
//     Authorization: "Bearer " + token,
//     "Content-Type": "text/plain",
//   },
//   redirect: "follow",
// };
// //Amount of beds
// fetch("http://25.43.134.201:8080/lv1/listroom", requestOptions)
//   .then((response) => response.json())
//   .then((result) => {
//     if (result.message == "Get list room OK") {
//       $("#used-beds").text(result.occupied);
//       $("#empty-beds").text(result.max - result.occupied);
//       $("#total-beds").text(result.max);
//     } else {
//       alert("Có lỗi xảy ra");
//     }
//   })
//   .catch((error) => {
//     console.log("Không kết nối được tới máy chủ", error);
//     alert("Không kết nối được tới máy chủ");
//   });

//   fetch("http://25.43.134.201:8080/lv1/listrequest", requestOptions)
//   .then((response) => response.json())
//   .then((result) => {
    
//     if (result.message == "Get list requests OK") {
//       var request = 0;
//       console.log(result.list_request);
//       for (var i = 0; i < Object.keys(result.list_request).length; i++) {
//         if(result.list_request[i].status =="new request"){
//           request++;
//         }
        
//       }
//       $("#request").text(request);
//     } else {
//       alert("Có lỗi xảy ra");
//     }
//   })
//   .catch((error) => {
//     console.log("Không kết nối được tới máy chủ", error);
//     alert("Không kết nối được tới máy chủ");
//   });
// //Amount of students
//   fetch("http://25.43.134.201:8080/lv1/allstudent", requestOptions)
//   .then((response) => response.json())
//   .then((result) => {
//     if (result.message == "Get list student OK") {
//       var studentsTotal =  Object.keys(result.list_student).length
//       $("#total-student").text(studentsTotal);
//     } else {
//       alert("Có lỗi xảy ra");
//     }
//   })
//   .catch((error) => {
//     console.log("Không kết nối được tới máy chủ", error);
//     alert("Không kết nối được tới máy chủ");
//   });
//   fetch("http://25.43.134.201:8080/lv1/getallmoneymanage", requestOptions)
//   .then((response) => response.json())
//   .then((result) => {
//     if (result.message == "Get all money manage OK") {
//       var unpaid =0;
//       var list = result.list_money_manage;
//       for (var i = 0; i < Object.keys(list).length; i++) {
//         if(list[i].status =="Unpaid"){
//           unpaid++;
//         }
//       }
//       var paid =0;
//       var list = result.list_money_manage;
//       for (var i = 0; i < Object.keys(list).length; i++) {
//         if(list[i].status =="Paid"){
//           paid++;
//         }
//       }
//       $("#unpaid-student").text(unpaid);
//       $("#paid-student").text(paid);
//     } else {
//       alert("Có lỗi xảy ra");
//     }
//   })
//   .catch((error) => {
//     console.log("Không kết nối được tới máy chủ", error);
//     alert("Không kết nối được tới máy chủ");
//   });

var role = sessionStorage.getItem("role");
var token = sessionStorage.getItem("token");

if (token == null) {
  alert("Vui lòng đăng nhập để truy cập trang này");
  window.location.href = "/app/frontend/pages/dang-nhap.html";
} else {
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

  fetch("http://25.43.134.201:8080/lv1/studentinfo?id=1", requestOptions)
    .then((response) => response.json())
    .then((result) => {
      if (result.message == "Invalid authorization, please login again") {
        alert("Vui lòng đăng nhập để truy cập trang này");
        window.location.href = "/app/frontend/pages/dang-nhap.html";
      } else {
        if (role == "1") {
          return;
        } else {
          alert("Bạn không có quyền truy cập trang này, vui lòng đăng nhập lại");
          window.location.href = "/app/frontend/pages/dang-nhap.html";
        }
      }
    })
    .catch((error) => {
      alert("Không kết nối được tới máy chủ", error);
      window.stop();
    });
}

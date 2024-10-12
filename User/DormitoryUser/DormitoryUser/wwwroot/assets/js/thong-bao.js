var token = sessionStorage.getItem("token");
var myHeaders = new Headers();
//myHeaders.append("Content-Type", "text/plain", bearer);

var newsRequestOptions = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

fetch("http://25.43.134.201:8080/lv0/listnoti", newsRequestOptions)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Get all notification OK") {
      let notifications = result.list_notification;
      handleNotifications(notifications);
    } else {
      alert("Có lỗi xảy ra");
    }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
    alert("Không kết nối được tới máy chủ");
  });

function handleNotifications(noti) {
  var notiLength = Object.keys(noti).length;

  for (i = notiLength - 1; i >= 0; i--) {
    var posted_date = noti[i].CreatedAt.substring(0, 10);
    posted_date_split = posted_date.split("-");
    posted_date =
      posted_date_split[2] +
      "/" +
      posted_date_split[1] +
      "/" +
      posted_date_split[0];

    var htmlTemp =
      '<div class="row">' +
      '<div class="col-12">' +
      '<article class="article article-style-b">' +
      '<div class="article-details">' +
      '<div class="article-title">' +
      '<h2><a href="#">' +
      noti[i].title +
      "</a></h2> " +
      "</div>" +
      "<div>" +
      "<p>" +
      "<small><span>" +
      posted_date +
      "</span></small>" +
      "</p>" +
      '<p class="article-general">' +
      noti[i].content +
      "</p>" +
      "</div>" +
      '<div class="article-cta">' +
      '<a id="xemthem' +
      noti[i].ID +
      '" href="#">Xem thêm <i class="fas fa-chevron-right"></i></a>' +
      "</div>" +
      "</div>" +
      "</article>" +
      "</div>" +
      "</div>";

    $("#body").append(htmlTemp);
    var modal = "#xemthem" + noti[i].ID;
    $(modal).fireModal({
      size: "modal-lg",
      animation: true,
      center: true,
      title: noti[i].title,
      body: noti[i].content,
    });
  }
}



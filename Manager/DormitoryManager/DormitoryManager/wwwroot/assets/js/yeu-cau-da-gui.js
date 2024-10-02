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

fetch("http://25.43.134.201:8080/lv0/listreq", requestsRequestOption)
  .then((response) => response.json())
  .then((result) => {
    if (result.message == "Get list request successfully") {
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
    var posted_date = req[i].CreatedAt.substring(0, 10);
    posted_date_split = posted_date.split("-");
    posted_date =
      posted_date_split[2] +
      "/" +
      posted_date_split[1] +
      "/" +
      posted_date_split[0];

    var reply_date = req[i].UpdatedAt.substring(0, 10);
    reply_date_split = reply_date.split("-");
    reply_date =
      reply_date_split[2] +
      "/" +
      reply_date_split[1] +
      "/" +
      reply_date_split[0];

    var htmlTemp =
      '<div class="row">' +
      '<div class="col-12">' +
      '<div class="card">' +
      '<div class="card-header">' +
      "<h4>" +
      req[i].title +
      "</h4>" +
      "</div>" +
      '<div class="card-body">' +
      '<div class="media">' +
      '<div class="media-body">' +
      '<h5 class="mt-0">' +
      sessionStorage.getItem("username") +
      "</h5>" +
      "<p>" +
      "<small><span>" +
      posted_date +
      "</span></small>" +
      "</p>" +
      "<p>" +
      req[i].message +
      "</p>" +
      "<hr> ";
    if (req[i].reply != "") {
      var replyTemp =
        "\n" +
        '<div class="media mt-3">' +
        '<a class="pr-3" href="#">' +
        "</a>" +
        '<div class="media-body">' +
        '<h5 class="mt-0"><i class="fas fa-share"></i>   Quản lý</h5>' +
        "<p>" +
        "<small><span>" +
        reply_date +
        "</span></small>" +
        "</p>" +
        '<p class="mb-0">' +
        req[i].reply +
        "</p>" +
        "</div></div></div></div></div></div></div></div>";

      htmlTemp = htmlTemp.concat(replyTemp);
    }
    $("#body").append(htmlTemp);
  }
}

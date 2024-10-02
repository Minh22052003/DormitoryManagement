var token = sessionStorage.getItem("token");
var myHeaders = new Headers();



var facilityRequestOptions = {
  method: "GET",
  credentials: "omit",
  headers: {
    Authorization: "Bearer " + token,
    "Content-Type": "text/plain",
  },
  redirect: "follow",
};

let tabledata;

fetch("http://25.43.134.201:8080/lv0/listfac", facilityRequestOptions)
.then((response) => response.json())
.then((result) => {
  if(result.message =="Get list facilities by roomid OK"){
    var studentFacilityList = result.list_facilities;
    console.log(studentFacilityList);
    handleTable(studentFacilityList);
  }else{
    alert("Có lỗi xảy ra");
  }
  })
  .catch((error) => {
    console.log("Không kết nối được tới máy chủ", error);
  });
function handleTable(data){
  var studentsTable = new Tabulator("#facility-table", {
    height: 400, // set height of table (in CSS or here), this enables the Virtual DOM and improves render speed dramatically (can be any valid css height value)
    data: data, //assign data to table
    virtualDom: true,
    layout: "fitColumns", //fit columns to width of table (optional)
    columns: [
      //Define Table Columns
      {
        title: "STT",
        field: "ID",
        width: 80,
        hozAlign: "center",
        sorter: "number",
      },
      { title: "Tên CSVC", field: "facility_name", sorter: "string" },
      { title: "Số lượng hiện tại", field: "quantity", sorter: "number" },
      { title: "Số lượng ban đầu", field: "default", sorter: "number" },
    ],
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
}






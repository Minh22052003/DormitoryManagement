let token = sessionStorage.getItem('token');
if (token != null){
    //redirect to page
}
else{
    alert("Vui lòng đăng nhập để truy cập trang!");
    window.location.href = "/Account/Login";
}
$(document).ready(function () {
    $('#myTable').DataTable();
});

function submitText() {
    var html = "Name: " + $("#fname").val() + " " + $("#lname").val() +
        "<br>Email: " + $("#email").val() + "<br>Total price: "
        + calcPrice();
    $("#bodyModal").html(html);
}
function calcPrice() {
    return $("#bookedticks").val() * $("#price").val();
}



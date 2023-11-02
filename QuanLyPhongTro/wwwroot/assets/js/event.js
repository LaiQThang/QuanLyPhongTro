$('#change_password').change(function(){
    let status = !$(this).is(":checked");
    $("#password").attr("readonly", status);
    $("#password_confirm").attr("readonly", status);
});

$('.delete_user').click(function(){
    console.log(123);
    return confirm('Do you want to delete the user?');
})
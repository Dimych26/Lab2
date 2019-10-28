function disp(fir) {
    if (fir.style.display == "none" && sec.style.display == "none") {
        fir.style.display = "block";
        sec.style.display = "block";
    } else {
        fir.style.display = "none";
        sec.style.display = "none";
    }
}
$(document).ready(function () {
 
    $('.basket').click(function () {
        $(this).parent().children('div.box').toggle('normal');
        return false;

    });
   
    $('.hto100').click(function () {
        $(this).parent().children('div.box').toggle('normal');
        return false;

    });
   
  
    $('.hto100').click(function () {
        $(this).parent().children('.nestoit').show('normal');
        return false;

    });
   
    $('.basket').click(function () {
        $(this).parent().children('.nestoit').show('normal');
        return false;

    });
    $('.nestoit').click(function () {
        $(this).parent().children('div.box').hide('normal');
        return false;

    });
    $('.nestoit').click(function () {
        $(this).parent().children('.nestoit').hide('normal');
        return false;

    });
   
});
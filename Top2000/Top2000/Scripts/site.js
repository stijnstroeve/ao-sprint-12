$(document).ready(function () {

    // Get the selected year from the url path
    const selectedYear = window.location.pathname.split('/')[1];

    // Select the current year when the page loaded
    $('.list-head > select').val(selectedYear);

    // Update the year when the select input changed
    $('.list-head > select').on('change', (element) => window.location.href = element.target.value)
});
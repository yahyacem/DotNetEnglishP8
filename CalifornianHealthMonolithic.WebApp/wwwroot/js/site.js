// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// $(document).ready(function () {

//     // Get the element
//     var calendarElement = document.getElementById("my-calendar");
//     // Create the calendar
//     var myCalendar = jsCalendar.new(calendarElement);
    
//     $('#DropConsultants').change(function () {

//         // read the dropdown selection
//         var id = $(this).val();  
//         console.log(id);

//         $.get("/api/" + id + "/calendar", function(data, status){
//             console.log("Status:");
//             console.log(status);
//             console.log("Data:");
//             console.log(data);
//             $(data).forEach((e) => {

//             })
//         });
//     });
//         myCalendar.select([
//             "15/07/2023"
//         ]);
    
//     // Add events
//     myCalendar.onDateClick(function(event, date){
//         console.log(date);
//     });
//     myCalendar.onMonthChange(function(event, date){
//         console.log(date);
//     });

// });
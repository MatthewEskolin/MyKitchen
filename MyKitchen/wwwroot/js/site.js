import { Calendar } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin, { Draggable }  from '@fullcalendar/interaction';


//* ADD ALL FULLCALENDAR CODE HERE *//
var theCalendar = null;
var eventSources = [
    {
        url: "/calendar/GetEventsFeed",
        method: 'GET',
        failure: function () {
            alert('could not load items');
        },
        color: 'blue',
        textColor: 'black'
    }
];


function delete_mouseover(imgElement) {
    $(imgElement).attr("src", "/images/red-trash-delete-icon-hover.svg")
}

function delete_mouseout(imgElement) {
    $(imgElement).attr("src", "/images/red-trash-delete.svg")
}

function delete_event_click(imgElement, event, fullCalendarEvent) {

    //clicking delete should only trigger the click event for the trash can icon here, and should not trigger the click event for the entire event which would bring user to details view.
    event.stopPropagation();

    //remove event from calendar
    theCalendar.getEventById(fullCalendarEvent.id).remove();

    //grab eventId from DOM
    //var eventID = $(info.el).find(".custom-event").data("eventid");

    //get class name that starts with data-event- and extract the number after the dash like this data-event-234
    var eventID = $(info.el)
        .find(".custom-event")
        .attr("class")
        .split(" ")
        .find(className => className.startsWith("data-event-"))
        ?.split("-")[2];

    if (fullCalendarEvent.extendedProps.eventID !== undefined)
        eventID = fullCalendarEvent.extendedProps.eventID;

    //also remove from backend stroage
    $.ajax({
        url: '/calendar/RemoveEvent/',
        data: JSON.stringify({
            Subject: fullCalendarEvent.title,
            Start: fullCalendarEvent.start,
            IsFullDay: "true",
            EventID: eventID
        }),
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        success: function (json) {
            console.log("Delete Event returned");
        },
        failure: function (json) {
            console.log("Delete Event failed.");
        }

    });


}



$(document).ready(function () {
    var events = [];


    GenerateCalendar(eventSources);

    var form = $("#fmSearchItems");
    var formData = form.serialize();

    //Get Available Food Items & Meals
    $.ajax({
        type: "POST",
        url: "/calendar/SearchForItems",
        data: formData,
        success: function (data) {
            ResultsReceived(data);
        },
        error: function (error) {
            // Handle errors gracefully
            alert('Failed: ' + error.statusText);
        }
    });

    //Add Search Event Handler 
    $("#fmSearchItems").submit(function (event) {

        var formData = $(this).serialize();

        $.ajax({
            type: "POST",
            url: "/calendar/SearchForItems",
            data: formData
        }).done(function (data) {
            ResultsReceived(data);
        });

        //do not do a normal form submit - ajax call replaces normal http post
        event.preventDefault();
    });




    //Create Calendar
    function GenerateCalendar(eventSources) {

        var calendarEl = document.getElementById('calendar');
        var draggableEL = document.getElementById('itemContainer');
        //var draggableEL2 = document.getElementById('itemContainer2');
        //var Draggable = interactionPlugin.Draggable;

        theCalendar = new Calendar(calendarEl,
            {
                plugins: [dayGridPlugin, interactionPlugin],
                eventSources: eventSources,
                editable: true,
                droppable: true,
                fixedWeekCount: false,
                height: 600,
                displayEventTime: false,
                footer: {
                    left: 'custom1'
                },
                eventDidMount: function (info) {

                    //console.log("eventDidMount: " + info.event.extendedProps.eventID);
                    //$(info.el).find('.custom-event').append("<img style='width:28px;height:28px;float:right;' id='svg-delete-event' src='/images/red-trash-delete-icon.svg' />");

                    if (info.event.extendedProps.mealID != null || info.event.extendedProps.itemType == "MEAL") {
                        info.el.style.backgroundColor = "orange"
                        info.el.style.color = "white";
                    }
                    else {
                        info.el.style.backgroundColor = "blue"
                        info.el.style.color = "white";
                    }

                    //$(info.el).find("#svg-delete-event").click(function (event) {

                    //    //clicking delete should only trigger the click event for the trash can icon here, and should not trigger the click event for the entire event which would bring user to details view.
                    //    event.stopPropagation();
                    //    //remove event from calendar
                    //    info.event.remove();

                    //    //grab eventId from DOM
                    //    //var eventID = $(info.el).find(".custom-event").data("eventid");

                    //    //get class name that starts with data-event- and extract the number after the dash like this data-event-234
                    //    var eventID = $(info.el)
                    //        .find(".custom-event")
                    //        .attr("class")
                    //        .split(" ")
                    //        .find(className => className.startsWith("data-event-"))
                    //        ?.split("-")[2];

                    //    if (info.event.extendedProps.eventID !== undefined)
                    //    eventID = info.event.extendedProps.eventID;

                    //    //also remove from backend stroage
                    //    $.ajax({
                    //        url: '/calendar/RemoveEvent/',
                    //        data: JSON.stringify({
                    //            Subject: info.event.title,
                    //            Start: info.event.start,
                    //            IsFullDay: "true",
                    //            EventID: eventID
                    //        }),
                    //        type: "POST",
                    //        contentType: 'application/json; charset=utf-8',
                    //        success: function (json) {
                    //            console.log("Delete Event returned");
                    //        },
                    //        failure: function (json) {
                    //            console.log("Delete Event failed.");
                    //        }

                    //    });

                    //});



                    //$(info.el).find("#svg-delete-event").mouseover(function () {

                    //    $(this).attr("src", "/images/red-trash-delete-icon-hover.svg")
                    //});


                    //$(info.el).find("#svg-delete-event").mouseout(function () {

                    //    $(this).attr("src", "/images/red-trash-delete-icon.svg")
                    //});

                    //$(info.el).find("#svg-delete-event").hide();

                },

                eventClassNames: function (info) {


                    //add the data
                    if (info.event.extendedProps.eventID !== undefined) {
                        var className = "data-event-" + info.event.extendedProps.eventID;
                        return [className];
                    }
                },

                eventContent: function (eventInfo) {

                    //if eventID is defined, add as data attribute to custom-event div
                    var dataEvent = "";
                    if (typeof eventInfo.event.extendedProps.eventID !== undefined) {
                        dataEvent = `data-eventid="${eventInfo.event.extendedProps.eventID }"`
                    }


                    var eventContent = `<div class="custom-event" ${dataEvent}>
                        <div class="square">
                            <img src="/images/tang48.png" alt="Icon">
                        </div>
                        <div class="text-rectangle">${eventInfo.event.title}
                        <img style='display:none;width:28px;height:28px;float:right;' id='svg-delete-event' src='/images/red-trash-delete-icon.svg' onclick='delete_event_click(this,event,${JSON.stringify(eventInfo.event)})' onmouseover='delete_mouseover(this)' onmouseout='delete_mouseout(this)' /> 

                        </div>

                    </div>`

                    return { html: eventContent };
                },



                eventClick: function (info) {


                    //if food item, redirect to food item
                    if (info.event.extendedProps.itemType == "FOOD ITEM") {

                        window.location.href = "/FoodItems/Details/" + info.event.extendedProps.itemId;
                    }
                    else if (info.event.extendedProps.itemType == "MEAL") {

                        window.location.href = "/MealBuilder/MealDetails/" + info.event.extendedProps.itemId;;
                    }
                    else {

                        window.location.href = "/FoodItems/Details/" + info.event.extendedProps.foodItemID;

                        //if meal, redirect to meal
                        //TODO NOT IMPLEMENTED
                    }
                },
                eventMouseEnter: function (info) {

                    //find remove button and make it visible
                    $(info.el).find('#svg-delete-event').show();
                },
                eventMouseLeave: function (info) {

                    //find remove button and make it visible
                    $(info.el).find('#svg-delete-event').hide();
                },
                eventDrop: function (info) {

                    console.log("eventdrop:" + info.event.extendedProps.eventID);

                    $.ajax({
                        url: '/calendar/UpdateEvent',
                        data: JSON.stringify({
                            Subject: info.event.title,
                            Start: info.event.start,
                            IsFullDay: "true",
                            EventID: info.event.extendedProps.eventID,


                        }),
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        success: function (json) {
                            console.log("Update Event returned");
                        },
                        failure: function (json) {
                            console.log("Update Event failed.");
                        }

                    });

                },
                eventReceive: function (info) {

                    console.log("eventReceive: " + info.event.extendedProps.eventID);

                    

                    var mealId, foodItemId;

                    var sitemId = info.event.extendedProps.itemId
                    var foodItem = window.availableItems.find(x => x.itemId == sitemId);

                    if (foodItem.itemType == "FOOD ITEM") {
                        foodItemId = foodItem.itemId;
                        mealId = null;


                    }
                    else if (foodItem.itemType == "MEAL") {
                        mealId = foodItem.itemId;
                        foodItemId = null;

                    }

                    $.ajax({
                        url: '/calendar/SaveNewEvent',
                        data: JSON.stringify({
                            Subject: foodItem.itemName,
                            Start: info.event.start,
                            IsFullDay: "true",
                            FoodItemId: foodItemId,
                            MealID: mealId
                        }),
                        type: "POST",
                        //contentType: 'application/x-www-form-urlencoded',
                        contentType: 'application/json; charset=utf-8',
                        //contentType:'json',
                        success: function (json) {
                            console.log("SaveNewEvent returned");
                            //info.event.setExtendedProp("eventID", json)

                        },
                        failure: function (json) {
                            console.log("SaveNewEvent failed.");
                        }
                    });
                },
                customButtons: {
                    custom1: {
                        text: 'Clear Month',
                        click: function (whatisthis) {

                            var curMonth = theCalendar.getDate().getMonth();


                            //call ajax to remove all events in the current month
                            $.ajax({
                                url: '/calendar/ClearMonth',
                                data: JSON.stringify({
                                    Month: curMonth

                                }),
                                type: "POST",
                                contentType: 'application/json; charset=utf-8',
                                success: function (json) {
                                    console.log("Clear month returned");
                                    theCalendar.getEventSources()[0].refetch();

                                },
                                failure: function (json) {
                                    console.log("Clear month failed")
                                }

                            });


                            //reload calendar from db


                            alert('clicked custom button does this trigger build? DEBUG TEST!');
                        }
                    }
                }


            });

        theCalendar.render();

        new Draggable(draggableEL,
            {
                itemSelector: '.draggableitem2',
                longPressDelay: 200

            });

        //new Draggable(draggableEL2,
        //    {
        //        itemSelector: '.draggableitem2',
        //        longPressDelay: 200
        //    });




    }


    //Calendar Helper Methods


});         





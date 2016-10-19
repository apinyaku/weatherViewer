(function ($) {


    $(document).ready(
    function () {

        //Initialize scrolling pages and menus
        $('#fullpage').fullpage({
            sectionsColor: ['#1bbc9b', '#4BBFC3', '#7BAABE', 'whitesmoke', '#ccddff'],
            anchors: ['firstPage', 'secondPage', '3rdPage', '4thpage', 'lastPage'],
            menu: '#menu',
            continuousVertical: true
        });

        //Add trigger event to Country Input
        $("#txtCountryName").keypress(function (event) {

            if (event.keyCode == 13) {
                $(location).attr('href', "#secondPage");
                var cName = $("#txtCountryName").val();
                getCountryCities(cName);

            }

        });
    });

    //Trigger Ajax - GetCountryCities Action
    getCountryCities = function (countryName) {
        var dataParams = JSON.stringify({ cName: countryName });
        $.ajax({
            type: "POST",
            url: "/Weather/GetCountryCities",
            data: dataParams,
            dataType: "json",
            success: function (result) {
                updateDlist(result, countryName);
            },
            contentType: "application/json",
            cache: false
        }).fail(function (xhr, textStatus, error) { updateDlist(error, countryName); });
    }

    //Trigger Ajax - GetCityWeather Action
    getCityWeather = function (cityName, countryName) {
        var dataParams = JSON.stringify({ cName: cityName });
        $.ajax({
            type: "POST",
            url: "/Weather/GetCityWeather",
            data: dataParams,
            dataType: "json",
            success: function (result) {
                showCityWeather(result, cityName, countryName);
            },
            contentType: "application/json",
            cache: false
        }).fail(function (xhr, textStatus, error) { showCityWeatherError(error); });
    }

    //Update Cities Page
    updateDlist = function (result, countryName) {
        var list = $('#list')
        list.empty();

        var sfText = $("#shuffleText");
        sfText.shuffleLetters({
            "text": countryName
        });

        if (result != "") {
            $.each(result, function () {
                var cityName = this.CityName;
                $('<a />', {
                    text: cityName + " ",
                    title: cityName,
                    href: '#',
                    click: function () { $(location).attr('href', "#3rdPage"); getCityWeather(cityName, countryName); return false; }
                }).appendTo(list);
            });

            genRandomFontInLinks('#list');
        }
    }
    
    //Update Weather details Page on success
    showCityWeather = function (result, cityName, countryName) {
        if (result != "") {
            var j = jQuery.parseJSON(result);
            
            var date = new Date(j.dt * 1000);
            $('#H1Country').html('Weather in ' + cityName + ', ' + countryName);
            $('#wLocation').html("(" + cityName + " [lon:" + j.coord.lon + " lat:" + j.coord.lat + "])");
            $('#wImgStatus').html("<img src='http://openweathermap.org/img/w/" + j.weather[0].icon + ".png' align='middle'>");
            $('#wTemperature').html(j.main.temp + ' °C');
            $('#wSkyConditions').html(j.weather[0].description);
            $('#wTime').html('get at ' + date);

            $('#wDetails').html('wind:<b>' + j.wind.speed + ' m/s</b> visibility:<b>' + j.main.grnd_level + '</b> humidity:<b>' + j.main.humidity + '%</b> pressure:<b>' + j.main.pressure + ' hpa</b>');
        }
        else {
            $('#H1Country').html("Cannot locate city");
            $('#wLocation').html("Service did not returned data");
             
        }
    }

    //Update Weather details Page on error
    showCityWeatherError = function (errormessage) {
        $('#H1Country').html("City Weather not found");
        $('#wLocation').html(errormessage);
        $('#wImgStatus').empty();
        $('#wTemperature').empty();
        $('#wSkyConditions').empty();
        $('#wTime').empty();
        $('#wDetails').empty();
    }

})(jQuery);
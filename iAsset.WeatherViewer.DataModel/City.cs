using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace iAsset.WeatherViewer.DataModel
{
    public class City : ICity
    {
        [Column(Name = "CityName", CanBeNull = false)]
        public string CityName { get; set; }
    }
}

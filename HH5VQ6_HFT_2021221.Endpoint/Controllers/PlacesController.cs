using HH5VQ6_HFT_2021221.Endpoint.Services;
using HH5VQ6_HFT_2021221.Logic;
using HH5VQ6_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlacesController
    {
        IPlaceLogic placeLogic;
        IHubContext<SignalRHub> hub;

        public PlacesController(IPlaceLogic placeLogic, IHubContext<SignalRHub> hub)
        {
            this.placeLogic = placeLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Place> GetPlaces()
        {
            var places = placeLogic.getAllPlaces();
            return places;
        }

        [HttpGet("{id}")]
        public Place Get(int id)
        {
            return placeLogic.getPlaceById(id);
        }

        [HttpPost]
        public void Post([FromBody] Place place)
        {
            placeLogic.addPlace(place);
            hub.Clients.All.SendAsync("PlaceCreated", place);
        }

        [HttpPut]
        public void Put([FromBody] Place place)
        {
            placeLogic.changePlace(place.PlaceId, place.PlaceName);
            hub.Clients.All.SendAsync("PlaceUpdated", place);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var placeToDelete = placeLogic.getPlaceById(id);
            placeLogic.removePlace(id);
            hub.Clients.All.SendAsync("PlaceDeleted", placeToDelete);
        }

        ////non-crud
        //[HttpGet("players/{id}")]
        //public string InWhichCityPlayerDied(int id)
        //{
        //    return placeLogic.inWhichCityPlayerDied(id);
        //}

        [HttpGet("inwhichcityplayerdied/{playerId}")]
        public Place inWhichCityPlayerDied(int playerId)
        {
            return placeLogic.inWhichCityPlayerDied(playerId);
        }
    }
}

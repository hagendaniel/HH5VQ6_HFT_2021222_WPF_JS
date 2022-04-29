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
    public class SeasonsController
    {
        ISeasonLogic seasonLogic;
        IHubContext<SignalRHub> hub;

        public SeasonsController(ISeasonLogic seasonLogic, IHubContext<SignalRHub> hub)
        {
            this.seasonLogic = seasonLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Season> GetSeasons()
        {
            var seasons = seasonLogic.getAllSeasons();
            return seasons;
        }

        [HttpGet("{id}")]
        public Season Get(int id)
        {
            return seasonLogic.getSeasonById(id);
        }

        [HttpPost]
        public void Post([FromBody] Season season)
        {
            seasonLogic.newSeason(season);
            hub.Clients.All.SendAsync("SeasonCreated", season);
        }

        [HttpPut]
        public void Put([FromBody] Season season)
        {
            seasonLogic.changeName(season.SeasonId, season.SeasonNickname);
            hub.Clients.All.SendAsync("SeasonUpdated", season);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var seasonToDelete = seasonLogic.getSeasonById(id);
            seasonLogic.removeSeason(id);
            hub.Clients.All.SendAsync("SeasonDeleted", seasonToDelete);
        }

        [HttpGet("inwhichseasonfirstgameingivenplace/{placeName}")]
        public Season whichSeasonFirstGameInGivenPlace(string placeName)
        {
            return seasonLogic.whichSeasonFirstGameInGivenPlace(placeName);
        }

        [HttpGet("whichseasonwonbygivenplayer/{playerId}")]
        public Season whichSeasonWonByGivenPlayer(int playerId)
        {
            return seasonLogic.whichSeasonWonByGivenPlayer(playerId);
        }
    }
}

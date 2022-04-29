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
    public class PlayersController
    {
        IPlayerLogic playerLogic;
        IHubContext<SignalRHub> hub;

        public PlayersController(IPlayerLogic playerLogic, IHubContext<SignalRHub> hub)
        {
            this.playerLogic = playerLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Player> GetPlayers()
        {
            var players = playerLogic.getAllPlayers();
            return players;
        }

        [HttpGet("{id}")]
        public Player Get(int id)
        {
            return playerLogic.getPlayerById(id);
        }

        [HttpPost]
        public void Post([FromBody] Player player)
        {
            playerLogic.registerNewPlayer(player);
            hub.Clients.All.SendAsync("PlayerCreated", player);
        }

        [HttpPut]       //------------------------------------------------------------------
        public void Put([FromBody] Player player)
        {
            playerLogic.changeStatus(player.PlayerId, player.AliveOrDead, Convert.ToInt32(player.EliminatedOnMap_MapId));
            hub.Clients.All.SendAsync("PlayerUpdated", player);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var playerToDelete = playerLogic.getPlayerById(id);
            playerLogic.removePlayer(id);
            hub.Clients.All.SendAsync("PlayerDeleted", playerToDelete);
        }

        [HttpGet("whowongivenseason/{seasonId}")]
        public Player whoWonGivenSeason(int seasonId)
        {
            return playerLogic.whoWonGivenSeason(seasonId);
        }
    }
}

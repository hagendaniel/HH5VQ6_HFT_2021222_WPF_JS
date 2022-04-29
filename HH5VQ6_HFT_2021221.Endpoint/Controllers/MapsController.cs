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
    public class MapsController : ControllerBase
    {
        /*private readonly*/ IMapLogic _mapLogic;
        IHubContext<SignalRHub> hub;

        public MapsController(IMapLogic mapLogic, IHubContext<SignalRHub> hub)
        {
            _mapLogic = mapLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Map> GetMaps()
        {
            var maps = _mapLogic.getAllMaps();

            return maps;
        }

        [HttpGet("{id}")]
        public Map Get(int id)
        {
            return _mapLogic.getMapById(id);
        }

        [HttpPost]
        public void Post([FromBody] Map map)
        {
            _mapLogic.addMap(map);
            hub.Clients.All.SendAsync("MapCreated", map);
        }

        [HttpPut]
        public void Put([FromBody] Map map)
        {
            _mapLogic.renameMap(map.MapId, map.MapName);
            hub.Clients.All.SendAsync("MapUpdated", map);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var mapToDelete = _mapLogic.getMapById(id);
            _mapLogic.deleteMap(id);
            hub.Clients.All.SendAsync("MapDeleted", mapToDelete);
        }

        //[HttpPost]
        //public IActionResult AddMap([FromBody] Map map)
        //{
        //    _mapLogic.addMap(map);

        //    return Ok();
        //}

        [HttpGet("thekillermap/{seasonname}")]
        public Map TheKillerMap(string seasonName)
        {
            return _mapLogic.TheKillerMap(seasonName);
        }
    }
}

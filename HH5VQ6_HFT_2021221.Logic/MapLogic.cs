using HH5VQ6_HFT_2021221.Models;
using HH5VQ6_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    public class MapLogic : IMapLogic
    {
        IMapRepository mapRepository;
        IPlayerRepository playerRepository;
        ISeasonRepository seasonRepository;

        public MapLogic(IMapRepository repository, IPlayerRepository _playerRepository, ISeasonRepository _seasonRepository) //dependency injection
        {
            this.mapRepository = repository;
            playerRepository = _playerRepository;
            seasonRepository = _seasonRepository;
        }
        public void addMap(/*string mapName, int difficulty*/Map map)
        {
            mapRepository.addMap(/*mapName, difficulty*/map);
        }

        public void deleteMap(int id)
        {
            mapRepository.removeMap(id);
        }

        public IList<Map> getAllMaps()
        {
            return mapRepository.GetAll().ToList();
        }

        public Map getMapById(int id)
        {
            return mapRepository.GetOne(id);
        }

        public void renameMap(int id, string newName)
        {
            mapRepository.renameMap(id, newName);
            //should add delete to the maprepository - a repot ez nem éri el, csak a maprepot
        }

        //non-crud

        public Map TheKillerMap(string seasonName) //Which map killed most of the players in the given season
        {
            IQueryable<Season> seasons = seasonRepository.GetAll();
            Season season = seasons.Where(x => x.SeasonNickname == seasonName).FirstOrDefault();
            ICollection<Player> players = playerRepository.GetAll().Where(x => x.SeasonId == season.SeasonId).ToList();
            season.Players = players;

            var groupByElimination = players.GroupBy(x => x.EliminatedOnMap_MapId);
            var most = groupByElimination.OrderByDescending(x => x.Count()).Select(x => x.Key).First();
            Map map = mapRepository.GetOne(Convert.ToInt32(most));


            return map;
        }
    }
}

using HH5VQ6_HFT_2021221.Models;
using HH5VQ6_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    public class PlayerLogic : IPlayerLogic
    {
        IPlayerRepository playerRepository;
        ISeasonRepository seasonRepository;

        public PlayerLogic(IPlayerRepository repository, ISeasonRepository _seasonRepository)
        {
            this.playerRepository = repository;
            seasonRepository = _seasonRepository;
        }

        public void changeStatus(int id, bool newStatus, int eliminatedOnMapId)
        {
            playerRepository.changeStatus(id, newStatus, eliminatedOnMapId);
        }

        //public void customId(int id, int customId)
        //{
        //    playerRepository.customId(id, customId);
        //}

        public IList<Player> getAllPlayers()
        {
            return playerRepository.GetAll().ToList();
        }

        public Player getPlayerById(int id)
        {
            return playerRepository.GetOne(id);
        }

        public void registerNewPlayer(/*string name, DateTime dateTime, int debt*/Player player)
        {
            playerRepository.registerNewPlayer(/*new Player() { PlayerName = name, Born = dateTime, Debt = debt }*/ player);
        }

        public void removePlayer(int id)
        {
            playerRepository.removePlayer(id);
        }

        //non-crud

        public Player whoWonGivenSeason(int seasonId)
        {
            IQueryable<Season> seasons = seasonRepository.GetAll();
            ICollection<Player> players = playerRepository.GetAll().Where(x => x.SeasonId == seasonId).ToList();
            Season season = seasons.Where(x => x.SeasonId==seasonId).FirstOrDefault();
            season.Players = players;

            if (season.Players.Count(x => x.AliveOrDead == true) > 1)
                throw new SeasonNotFinishedException();
            else
            {
                Player toReturn = season.Players.Where(x => x.AliveOrDead == true).First();
                return toReturn;
            }
        }
    }
}

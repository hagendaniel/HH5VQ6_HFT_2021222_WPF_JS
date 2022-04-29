using HH5VQ6_HFT_2021221.Models;
using HH5VQ6_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    public class SeasonLogic : ISeasonLogic
    {
        ISeasonRepository seasonRepository;
        IPlaceRepository placeRepository;
        IPlayerRepository playerRepository;

        public SeasonLogic(ISeasonRepository repository, IPlaceRepository _placeRepository, IPlayerRepository _playerRepository)
        {
            this.seasonRepository = repository;
            placeRepository = _placeRepository;
            playerRepository = _playerRepository;
        }

        public void changeName(int id, string newName)
        {
            seasonRepository.changeName(id, newName);
        }

        public IList<Season> getAllSeasons()
        {
            return seasonRepository.GetAll().ToList();
        }

        public Season getSeasonById(int id)
        {
            return seasonRepository.GetOne(id);
        }

        public void newSeason(/*string seasonName, int placeId*/ Season season)
        {
            seasonRepository.newSeason(/*seasonName, placeId*/season);
        }

        public void removeSeason(int id)
        {
            seasonRepository.removeSeason(id);
        }

        //non-crud

        public Season whichSeasonFirstGameInGivenPlace(string placeName) //In which season was the game hold in the given city first
        {
            ICollection<Season> seasons = seasonRepository.GetAll().ToList();
            IQueryable<Place> places = placeRepository.GetAll();

            Place place = places.Where(x => x.PlaceName == placeName).FirstOrDefault();
            place.Seasons = seasons.Where(x => x.PlaceId == place.PlaceId).ToList();

            //int toReturn = place.PlaceId;
            Season toReturn = seasons.Where(x => x.PlaceId == place.PlaceId).FirstOrDefault();

            if (place is null)
                throw new InvalidPlaceException();
            else
                return toReturn;
        }


        public Season whichSeasonWonByGivenPlayer(int playerId)
        {
            Player player = playerRepository.GetOne(playerId);
            if (player.EliminatedOnMap_MapId != null)
            {
                throw new PlayerAlreadyDeadException();
            }
            else if (player is null)
            {
                throw new PlayerDoesNotExistException();
            }
            else
            {
                IQueryable<Season> seasons = seasonRepository.GetAll();
                Season toReturn = seasons.Where(x => x.SeasonId == player.SeasonId).FirstOrDefault();
                return toReturn;
            }
        }
    }
}

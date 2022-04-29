using HH5VQ6_HFT_2021221.Models;
using HH5VQ6_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    public class PlaceLogic : IPlaceLogic
    {
        IPlaceRepository placeRepository;
        IPlayerRepository playerRepository;
        ISeasonRepository seasonRepository;

        public PlaceLogic(IPlaceRepository repository, IPlayerRepository _playerRepository, ISeasonRepository _seasonRepository)
        {
            this.placeRepository = repository;
            playerRepository = _playerRepository;
            seasonRepository = _seasonRepository;
        }

        public void addPlace(/*string placeName, string country*/ Place place)
        {
            placeRepository.addPlace(/*placeName, country*/place);
        }

        public void changePlace(int id, string newPlace)
        {
            placeRepository.changePlace(id, newPlace);
        }

        public IList<Place> getAllPlaces()
        {
            return placeRepository.GetAll().ToList();
        }

        public Place getPlaceById(int id)
        {
            return placeRepository.GetOne(id);
        }

        public void removePlace(int id)
        {
            placeRepository.removePlace(id);
        }

        //non-crud

        //public string inWhichCityPlayerDied(int playerId)
        //{
        //    Player player = playerRepository.GetOne(playerId);
        //    if (player.EliminatedOnMap_MapId == null)
        //    {
        //        throw new PlayerNotDeadException();
        //    }
        //    else if (player is null)
        //    {
        //        throw new PlayerDoesNotExistException();
        //    }
        //    else
        //    {
        //        IQueryable<Season> seasons = seasonRepository.GetAll();
        //        IQueryable<Place> places = placeRepository.GetAll();
        //        ICollection<Player> players = playerRepository.GetAll().ToList();

        //        Season season = seasons.Where(x => x.SeasonId == player.SeasonId).FirstOrDefault();
        //        string toReturnCountry = places.Where(x => x.PlaceId == season.PlaceId).Select(x => x.PlaceName).FirstOrDefault();
        //        return toReturnCountry;
        //    }
        //}

        public Place inWhichCityPlayerDied(int playerId)
        {
            try
            {

                Player player = playerRepository.GetOne(playerId);
                if (player.EliminatedOnMap_MapId == null)
                {
                    throw new PlayerNotDeadException();
                }
                else if (player is null)
                {
                    throw new PlayerDoesNotExistException();
                }
                else
                {
                    IQueryable<Season> seasons = seasonRepository.GetAll();
                    IQueryable<Place> places = placeRepository.GetAll();
                    ICollection<Player> players = playerRepository.GetAll().ToList();

                    Season season = seasons.Where(x => x.SeasonId == player.SeasonId).FirstOrDefault();
                    Place place = places.Where(x => x.PlaceId == season.PlaceId).FirstOrDefault();
                    return place;
                }
            }
            catch (PlayerNotDeadException)
            {
                //Console.WriteLine("The player is still alive");
                return null;
            }
            catch (PlayerDoesNotExistException)
            {
                //Console.WriteLine("The player with the given id does not exist");
                return null;
            }
        }
    }
}

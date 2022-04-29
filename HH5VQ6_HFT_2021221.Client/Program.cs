using ConsoleTools;
using HH5VQ6_HFT_2021221.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
//using HH5VQ6_HFT_2021221.Data; //For testing, also Data should be added in Project reference


namespace HH5VQ6_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World, Loading!");

            RestService rest = new RestService("http://localhost:27989");
            /*
            rest.Post<Map>(new Map()
            {
                //MapId = 5,
                MapName = "Trainsurf",
                Difficulty = 4
            }, "maps");

            var maps = rest.Get<Map>("maps");

            foreach (var map in maps)
            {
                Console.WriteLine(map.MapName);
            }
            */

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------

            var mapMenu = new ConsoleMenu()
                .Add("Create new map", () => CreateMap(rest))
                .Add("Show all maps", () => GetAllMaps(rest))
                .Add("Show a map by id", () => GetMap(rest))
                .Add("Rename a map", () => RenameMap(rest))
                .Add("Delete a map", () => DeleteMap(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var placeMenu = new ConsoleMenu()
                .Add("Create new place", () => CreatePlace(rest))
                .Add("Show all places", () => GetAllPlaces(rest))
                .Add("Show a place by id", () => GetPlace(rest))
                .Add("Change place to a new one in the same country", () => ChangePlace(rest))
                .Add("Delete a place", () => DeletePlace(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var playerMenu = new ConsoleMenu()
                .Add("Register new player", () => CreatePlayer(rest))
                .Add("Show all players", () => GetAllPlayers(rest))
                .Add("Show a player by id", () => GetPlayer(rest))
                .Add("Eliminate a player", () => EliminatePlayer(rest))
                .Add("Delete a player", () => DeletePlayer(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var seasonMenu = new ConsoleMenu()
                .Add("Create new season", () => CreateSeason(rest))
                .Add("Show all seasons", () => GetAllSeasons(rest))
                .Add("Show a season by its id", () => GetSeason(rest))
                .Add("Rename a season", () => RenameSeason(rest))
                .Add("Delete a season", () => DeleteSeason(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var noncrudMenu = new ConsoleMenu()
                .Add("In which city does a player got eliminated?", () => InWhichCityGivenPlayerDied(rest))
                .Add("Which map was the deadliest in a season?", ()=> WhichMapGaveTheMostDeadlyExperience(rest))
                .Add("Who won a given season?", () => WhoWonGivenSeason(rest))
                .Add("When was the first game held at a given place?", () => InWhichSeasonFirstGameHeldInGivenPlace(rest))
                .Add("Which season was won by given player?", () => WhichSeasonWasWonByGivenPlayer(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var mainMenu = new ConsoleMenu()
                .Add("Create/Read/Update/Delete in Maps", () => mapMenu.Show())
                .Add("Create/Read/Update/Delete in Places", () => placeMenu.Show())
                .Add("Create/Read/Update/Delete in Players", () => playerMenu.Show())
                .Add("Create/Read/Update/Delete in Seasons", () => seasonMenu.Show())
                .Add("Some non-crud queries", () => noncrudMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            mainMenu.Show();

            Console.ReadKey();
        }

        //CRUD METHODS FOR THE MENU-----------------------------------------------------------------------------------------
        //CRUD METHODS FOR THE MAPMENU--------------------------------------------------------------------------------------
        #region MAPMENU_CRUD
        private static void CreateMap(RestService restService)
        {
            Console.WriteLine("Name of the new map: ");
            string name = Console.ReadLine();
            Console.WriteLine("Difficulty of the new map: ");
            int difficulty = int.Parse(Console.ReadLine());
            restService.Post(new Map
            {
                MapName = name,
                Difficulty=difficulty
            }, "maps");
            Console.WriteLine("The new map has been created");
            Console.ReadKey();
        }

        private static void GetAllMaps(RestService restService)
        {
            var maps = restService.Get<Map>("maps");
            foreach (var map in maps)
            {
                Console.WriteLine(map.MapName);
            }
            Console.ReadKey();
        }

        private static void GetMap(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the map: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Map map = restService.Get<Map>(id, "maps");
                Console.WriteLine($"The map with the given id is {map.MapName}, with difficulty level of {map.Difficulty}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void RenameMap(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the map you want to rename: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Map map = restService.Get<Map>(id, "maps");
                string oldName = map.MapName;
                Console.WriteLine("New name of the map: ");
                string newName = Console.ReadLine();
                map.MapName = newName;
                restService.Put(map, "maps");
                Console.WriteLine($"The map as been renamed from {oldName} to {newName}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void DeleteMap(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the map you want to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (restService.Get<Map>(id, "maps").Players.Count() == 0)
                {
                    restService.Delete(id, "maps");
                    Console.WriteLine($"The map with the id of ({id}) has been deleted.");
                }
                else
                {
                    Console.WriteLine("First you should delete all players who has died on this map.");
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }
#endregion
        //CRUD METHODS FOR THE PLACEMENU------------------------------------------------------------------------------------
        #region PLACEMENU_CRUD
        private static void CreatePlace(RestService restService)
        {
            Console.WriteLine("Name of the new place: ");
            string name = Console.ReadLine();
            Console.WriteLine("Country of the new place: ");
            string country = Console.ReadLine();
            restService.Post(new Place
            {
                PlaceName = name,
                Country = country
            }, "places");
            Console.WriteLine("The new place has been created");
            Console.ReadKey();
        }

        private static void GetAllPlaces(RestService restService)
        {
            var places = restService.Get<Place>("places");
            foreach (var place in places)
            {
                Console.WriteLine($"{place.PlaceName} in {place.Country}");
            }
            Console.ReadKey();
        }

        private static void GetPlace(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the place: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Place place = restService.Get<Place>(id, "places");
                Console.WriteLine($"The place with the given id is {place.PlaceName} in {place.Country}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void ChangePlace(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the place you want to change: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Place place = restService.Get<Place>(id, "places");
                string oldPlace = place.PlaceName;
                Console.WriteLine("New place in the same country: ");
                string newPlace = Console.ReadLine();
                place.PlaceName = newPlace;
                restService.Put(place, "places");
                Console.WriteLine($"The place has been changed from {oldPlace} to {newPlace}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void DeletePlace(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the place you want to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (restService.Get<Place>(id, "places").Seasons.Count() == 0)
                {
                    restService.Delete(id, "places");
                    Console.WriteLine($"The place with the id of ({id}) has been deleted.");
                }
                else
                {
                    Console.WriteLine("First you should delete all seasons which was played here.");
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }
        #endregion
        //CRUD METHODS FOR THE PLAYERMENU----------------------------------------------------------------------------------- DELETET MEGNÉÉÉÉÉÉÉÉÉÉÉÉÉÉÉÉZ
        #region PLAYERMENU_CRUD
        private static void CreatePlayer(RestService restService)
        {
            Console.WriteLine("Name of the new player: ");
            string name = Console.ReadLine();
            Console.WriteLine("Date of born:");
            DateTime born = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Id of season which s/he registers: ");
            int seasonId = Convert.ToInt32(Console.ReadLine());
            restService.Post(new Player
            {
                PlayerName = name,
                Born = born,
                SeasonId = seasonId
            }, "players"); ;
            Console.WriteLine($"The new player has been registered for season {seasonId}");
            Console.ReadKey();
        }

        private static void GetAllPlayers(RestService restService)
        {
            var players = restService.Get<Player>("players");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.PlayerName} played in season {player.SeasonId}");
            }
            Console.ReadKey();
        }

        private static void GetPlayer(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the player: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Player player = restService.Get<Player>(id, "players");
                Console.WriteLine($"The player with the given id is {player.PlayerName} from season {player.SeasonId}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void EliminatePlayer(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the player who got eliminated: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Player player = restService.Get<Player>(id, "players");
                if (player.EliminatedOnMap_MapId == null)
                {
                    Console.WriteLine("Id of the map where the player got eliminated: ");
                    int mapId = Convert.ToInt32(Console.ReadLine());
                    player.AliveOrDead = false;
                    //int linkedMapId = restService.Get<Map>("maps").Where(x => x.MapId == player.EliminatedOnMap_MapId).Select(x => x.MapId).FirstOrDefault();
                    player.EliminatedOnMap_MapId = mapId;
                    restService.Put(player, "players");
                    Console.WriteLine($"Player {player.PlayerId}, eliminated.");
                }
                else Console.WriteLine("The player already got eliminated.");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void DeletePlayer(RestService restService) //------------------------------------------------------------------------------
        {
            try
            {
                Console.WriteLine("Id of the player you want to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (restService.Get<Player>("players").Equals(null))
                {
                    Console.WriteLine("Player does not exist.");
                }
                else
                {
                    restService.Delete(id, "players");
                    Console.WriteLine($"Player {id} has been deleted.");
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }
        #endregion
        //CRUD METHODS FOR THE SEASONMENU-----------------------------------------------------------------------------------
        #region SEASONMENU_CRUD
        private static void CreateSeason(RestService restService)
        {
            Console.WriteLine("Name of the new season: ");
            string name = Console.ReadLine();
            Console.WriteLine("Id of the place where it is held");
            int placeId = Convert.ToInt32(Console.ReadLine());
            restService.Post(new Season
            {
                SeasonNickname=name,
                PlaceId=placeId
            }, "seasons") ;
            Console.WriteLine($"Season {restService.Get<Season>("seasons").Last().SeasonId} has been created.");
            Console.ReadKey();
        }

        private static void GetAllSeasons(RestService restService)
        {
            var seasons = restService.Get<Season>("seasons");
            foreach (var season in seasons)
            {
                Console.WriteLine($"Season {season.SeasonId}: {season.SeasonNickname}");
            }
            Console.ReadKey();
        }

        private static void GetSeason(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the season: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Season season = restService.Get<Season>(id, "seasons");
                Console.WriteLine($"Season {id} is called {season.SeasonNickname}.");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void RenameSeason(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the season you want to rename: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Season season = restService.Get<Season>(id, "seasons");
                string oldName = season.SeasonNickname;
                Console.WriteLine("New name of the season: ");
                string newName = Console.ReadLine();
                season.SeasonNickname = newName;
                restService.Put(season, "seasons");
                Console.WriteLine($"The season has been renamed from {oldName} to {newName}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void DeleteSeason(RestService restService)
        {
            try
            {
                Console.WriteLine("Id of the season you want to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (restService.Get<Season>(id, "seasons").Players.Count()==0)
                {
                    restService.Delete(id, "seasons");
                    Console.WriteLine($"Season {id} has been deleted.");
                }
                else
                {
                    Console.WriteLine("First you should delete all players linked to this season.");
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }
        #endregion
        //NON-CRUD METHODS
        //private static void InWhichCityGivenPlayerDied(RestService restService)
        //{
        //    Console.WriteLine("Id of the player who you are interested in: ");
        //    int id = Convert.ToInt32(Console.ReadLine());
        //    var cityName = restService.GetInWhichCityPlayerDied<Place>(id, "places", "seasons", "players");
        //    Console.WriteLine($"The player died in {cityName}");
        //    Console.ReadKey();
        //}

        private static void InWhichCityGivenPlayerDied(RestService restService)
        {
            Console.WriteLine("Id of the player who you are interested in: ");
            int playerId = Convert.ToInt32(Console.ReadLine());
            try
            {
                var place = restService.Get<Place>("places", "inWhichCityPlayerDied", playerId);
                Console.WriteLine($"The player died in {place.PlaceName}");
            }
            catch (Exception)
            {
                Console.WriteLine("The given id is invalid, either the player is still alive, or doesn't exist.");
            }
            Console.ReadKey();
        }

        private static void WhichMapGaveTheMostDeadlyExperience(RestService restService)
        {
            Console.WriteLine("Enter the name of a season to find out which map gave the most deadly experience: ");
            string seasonName = Console.ReadLine();
            try
            {
                var map = restService.Get<Map>("maps", "thekillermap", seasonName);
                Console.WriteLine($"The deadliest map in {seasonName} was {map.MapName}");
            }
            catch (Exception)
            {
                Console.WriteLine("No season with the given name exists");
            }
            Console.ReadKey();
        }

        private static void WhoWonGivenSeason(RestService restService)
        {
            Console.WriteLine("Id of the season, whose winner you're interested in: ");
            int seasonId = Convert.ToInt32(Console.ReadLine());
            try
            {
                var player = restService.Get<Player>("players", "whowongivenseason", seasonId);
                Console.WriteLine($"Season {player.SeasonId} was won by {player.PlayerName}");
            }
            catch (Exception)
            {
                Console.WriteLine("The given season does not exist.");
            }
            Console.ReadKey();
        }

        private static void InWhichSeasonFirstGameHeldInGivenPlace(RestService restService)
        {
            Console.WriteLine("Name of the place, that you want to find out when it gave place first for a game: ");
            string placeName = Console.ReadLine();
            try
            {
                var season = restService.Get<Season>("seasons", "inwhichseasonfirstgameingivenplace", placeName);
                Console.WriteLine($"The first game was held in Season {season.SeasonId} at {placeName}");
            }
            catch (Exception)
            {
                Console.WriteLine("The given place does not exist");
            }
            Console.ReadKey();
        }

        private static void WhichSeasonWasWonByGivenPlayer(RestService restService)
        {
            Console.WriteLine("Give the number of the player, who you're interested in: ");
            int playerId = Convert.ToInt32(Console.ReadLine());
            try
            {
                var season = restService.Get<Season>("seasons", "whichseasonwonbygivenplayer", playerId);
                Console.WriteLine($"Player number {playerId} won season {season.SeasonId}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Player {playerId} either does not exist, or already dead.");
            }
            Console.ReadKey();
        }
    }
}

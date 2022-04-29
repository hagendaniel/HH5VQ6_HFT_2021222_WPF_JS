using HH5VQ6_HFT_2021221.Data;
using HH5VQ6_HFT_2021221.Logic;
using HH5VQ6_HFT_2021221.Models;
using HH5VQ6_HFT_2021221.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Test
{
    [TestFixture]
    public class LogicTests
    {

        IMapRepository mapRepository;
        IPlaceRepository placeRepository;
        IPlayerRepository playerRepository;
        ISeasonRepository seasonRepository;

        MapLogic mapLogic;
        PlaceLogic placeLogic;
        PlayerLogic playerLogic;
        SeasonLogic seasonLogic;

        //non-crud tests

        [Test]
        public void inWhichCityPlayerDied()
        {
            PlaceLogic placeLogic = new PlaceLogic(placeRepository, playerRepository, seasonRepository);
            Assert.That(() => placeLogic.inWhichCityPlayerDied(3).PlaceName, Is.EqualTo("Choi"));
            Assert.That(() => placeLogic.inWhichCityPlayerDied(1).PlaceName, Throws.Exception);
        }

        [Test]
        public void WhichSeasonWonByGivenPlayer()
        {
            SeasonLogic seasonLogic = new SeasonLogic(seasonRepository, placeRepository, playerRepository);
            Assert.That(() => seasonLogic.whichSeasonWonByGivenPlayer(1).SeasonNickname, Is.EqualTo("Personal Bankruptcy"));
            Assert.That(() => seasonLogic.whichSeasonWonByGivenPlayer(218), Throws.Exception);
        }

        [Test]
        public void TheKillerMap()
        {
            MapLogic mapLogic = new MapLogic(mapRepository, playerRepository, seasonRepository);
            Assert.That(() => mapLogic.TheKillerMap("Personal Bankruptcy").MapName, Is.EqualTo("Tud-of-War"));
        }

        [Test]
        public void whichSeasonGame()
        {
            SeasonLogic seasonLogic = new SeasonLogic(seasonRepository, placeRepository, playerRepository);
            Assert.That(() => seasonLogic.whichSeasonFirstGameInGivenPlace("Buda").SeasonNickname, Is.EqualTo("Medieval Knockout"));
        }

        [TestCase(1)]
        public void WhoWonGivenSeason(int seasonId)
        {
            PlayerLogic logic = new PlayerLogic(playerRepository, seasonRepository);
            Assert.That(() => logic.whoWonGivenSeason(seasonId).PlayerId, Is.EqualTo(1));
        }

        //Some crud-tests

        [Test]
        public void GetSeasonByIdTest()
        {
            var got = seasonLogic.getSeasonById(1);
            Assert.That(got.SeasonNickname == ("Personal Bankruptcy"));
        }

        [Test]
        public void GetMapByIdTest()
        {
            var got = mapLogic.getMapById(1);
            Assert.That(got.MapName == ("Red Light Green Light"));
        }

        [Test]
        public void ChangePlayerStatus()
        {
            playerLogic.changeStatus(1, false, 3);
            Assert.That(playerLogic.getPlayerById(1).EliminatedOnMap_MapId, Is.Not.Null);
        }

        [Test]
        public void ChangePlace()
        {
            placeLogic.changePlace(2, "Pest");
            Assert.That(placeLogic.getPlaceById(2).PlaceName == "Pest");
        }


        [Test]
        public void AddMapTest()
        {
            Assert.That(() => mapLogic.addMap(new Map() { MapId = 5, MapName = "Trainsurfing through Hungary, and escaping from the guards", Difficulty = 15 }), Throws.InvalidOperationException);
        }

        [Test]
        public void AddPlaceTest()
        {
            Assert.That(() => placeLogic.addPlace(new Place() { PlaceId = 5, PlaceName = "Trainsurfing through Hungary, and escaping from the guards", Country="Hungary"}), Throws.InvalidOperationException);
        }

        [Test]
        public void AddSeasonTest()
        {
            Assert.That(() => seasonLogic.newSeason(new Season() { SeasonId = 5, SeasonNickname = "Trainsurfing through Hungary, and escaping from the guards", PlaceId=3 }), Throws.InvalidOperationException);
        }

        [SetUp]
        public void Setup()
        {
            List<Map> mapData =
                new List<Map>
                {
                    new Map() { MapId = 1, MapName = "Red Light Green Light", Difficulty = 6 },
                    new Map() { MapId = 2, MapName = "Ppopgi/Honeycomb Candy", Difficulty = 8 },
                    new Map() { MapId = 3, MapName = "Tud-of-War", Difficulty = 5 },
                    new Map() { MapId = 4, MapName = "Marbles", Difficulty = 2 }
                };
            List<Place> placeData =
                new List<Place>
                {
                    new Place() { PlaceId = 1, PlaceName = "Choi", Country = "South Korea" },
                    new Place() { PlaceId = 2, PlaceName = "Buda", Country = "Hungary" },
                    new Place() { PlaceId = 3, PlaceName = "Tokyo", Country = "Japan" }
                };
            List<Player> playerData =
                new List<Player>
                {
                    new Player() { PlayerId = 1, PlayerName = "Danca Kong", Born = Convert.ToDateTime("2000.07.23."), Debt = 0, Age = 21, AliveOrDead = true, SeasonId = 1 },
                    new Player() { PlayerId = 2, PlayerName = "Zsolszesz Fodi", Born = Convert.ToDateTime("2000.12.24."), Debt = 9000, Age = 21, AliveOrDead = false, SeasonId = 1 },
                    new Player() { PlayerId = 3, PlayerName = "Benedek James", Born = Convert.ToDateTime("2000.03.31."), Debt = 20000, Age = 21, AliveOrDead = false, EliminatedOnMap_MapId = 1, SeasonId = 1 },
                    new Player() { PlayerId = 4, PlayerName = "Ricsi 00", Born = Convert.ToDateTime("2000.09.17."), Debt = 20000, Age = 21, AliveOrDead = false, SeasonId = 1 },
                    new Player() { PlayerId = 5, PlayerName = "Cirno Fumo", Born = Convert.ToDateTime("2000.08.20."), Debt = 20000, Age = 21, AliveOrDead = false, EliminatedOnMap_MapId = 3, SeasonId = 1 },
                    new Player() { PlayerId = 6, PlayerName = "Gyurma", Born = Convert.ToDateTime("2000.08.21."), Debt = 20001, Age = 22, AliveOrDead = false, EliminatedOnMap_MapId = 3, SeasonId = 1 },
                    new Player() { PlayerId = 7, PlayerName = "Cirno Fumo", Born = Convert.ToDateTime("2000.08.20."), Debt = 20000, Age = 21, AliveOrDead = false, EliminatedOnMap_MapId = 3, SeasonId = 1 },
                    new Player() { PlayerId = 8, PlayerName = "Gyurma", Born = Convert.ToDateTime("2000.08.21."), Debt = 20001, Age = 22, AliveOrDead = false, EliminatedOnMap_MapId = 3, SeasonId = 1 }/*,
                    new Player() { PlayerId = 9, PlayerName = "Gyurma", Born = Convert.ToDateTime("2000.08.21."), Debt = 20001, Age = 22, AliveOrDead = false, EliminatedOnMap_MapId = 2, SeasonId = 1 },
                    new Player() { PlayerId = 10, PlayerName = "Gyurma", Born = Convert.ToDateTime("2000.08.21."), Debt = 20001, Age = 22, AliveOrDead = false, EliminatedOnMap_MapId = 2, SeasonId = 1 },
                    new Player() { PlayerId = 11, PlayerName = "Gyurma", Born = Convert.ToDateTime("2000.08.21."), Debt = 20001, Age = 22, AliveOrDead = false, EliminatedOnMap_MapId = 2, SeasonId = 1 },
                    new Player() { PlayerId = 12, PlayerName = "Gyurma", Born = Convert.ToDateTime("2000.08.21."), Debt = 20001, Age = 22, AliveOrDead = false, EliminatedOnMap_MapId = 2, SeasonId = 1 },
                    new Player() { PlayerId = 13, PlayerName = "Gyurma", Born = Convert.ToDateTime("2000.08.21."), Debt = 20001, Age = 22, AliveOrDead = false, EliminatedOnMap_MapId = 2, SeasonId = 1 }*/
                };
            List<Season> seasonData =
                new List<Season>
                {
                    new Season() { SeasonId = 1, SeasonNickname = "Personal Bankruptcy", PlaceId = 1 },
                    new Season() { SeasonId = 2, SeasonNickname = "Medieval Knockout", PlaceId = 2 },
                    new Season() { SeasonId = 3, SeasonNickname = "Tokyo Drift", PlaceId = 3 },
                    new Season() { SeasonId = 4, SeasonNickname = "Winter Knockout", PlaceId = 2 }
                };

            Mock<GameDbContext> contextMock = new Mock<GameDbContext>();

            Mock<DbSet<Map>> mapDbSetMock = new Mock<DbSet<Map>>();
            Mock<DbSet<Place>> placeDbSetMock = new Mock<DbSet<Place>>();
            Mock<DbSet<Player>> playerDbSetMock = new Mock<DbSet<Player>>();
            Mock<DbSet<Season>> seasonDbSetMock = new Mock<DbSet<Season>>();

            mapRepository = new MapRepository(contextMock.Object);
            placeRepository = new PlaceRepository(contextMock.Object);
            playerRepository = new PlayerRepository(contextMock.Object);
            seasonRepository = new SeasonRepository(contextMock.Object);

            IQueryable<Map> queMap = mapData.AsQueryable();
            IQueryable<Place> quePlace = placeData.AsQueryable();
            IQueryable<Player> quePlayer = playerData.AsQueryable();
            IQueryable<Season> queSeason = seasonData.AsQueryable();

            mapDbSetMock.As<IQueryable<Map>>()
                .Setup(mock => mock.Provider)
                .Returns(queMap.Provider);

            mapDbSetMock.As<IQueryable<Map>>()
                .Setup(mock => mock.Expression)
                .Returns(queMap.Expression);

            mapDbSetMock.As<IQueryable<Map>>()
                .Setup(mock => mock.ElementType)
                .Returns(queMap.ElementType);

            mapDbSetMock.As<IQueryable<Map>>()
                .Setup(mock => mock.GetEnumerator())
                .Returns(queMap.GetEnumerator());


            placeDbSetMock.As<IQueryable<Place>>()
                .Setup(mock => mock.Provider)
                .Returns(quePlace.Provider);

            placeDbSetMock.As<IQueryable<Place>>()
                .Setup(mock => mock.Expression)
                .Returns(quePlace.Expression);

            placeDbSetMock.As<IQueryable<Place>>()
                .Setup(mock => mock.ElementType)
                .Returns(quePlace.ElementType);

            placeDbSetMock.As<IQueryable<Place>>()
                .Setup(mock => mock.GetEnumerator())
                .Returns(quePlace.GetEnumerator());


            playerDbSetMock.As<IQueryable<Player>>()
                .Setup(mock => mock.Provider)
                .Returns(quePlayer.Provider);

            playerDbSetMock.As<IQueryable<Player>>()
                .Setup(mock => mock.Expression)
                .Returns(quePlayer.Expression);

            playerDbSetMock.As<IQueryable<Player>>()
                .Setup(mock => mock.ElementType)
                .Returns(quePlayer.ElementType);

            playerDbSetMock.As<IQueryable<Player>>()
                .Setup(mock => mock.GetEnumerator())
                .Returns(quePlayer.GetEnumerator());


            seasonDbSetMock.As<IQueryable<Season>>()
                .Setup(mock => mock.Provider)
                .Returns(queSeason.Provider);

            seasonDbSetMock.As<IQueryable<Season>>()
                .Setup(mock => mock.Expression)
                .Returns(queSeason.Expression);

            seasonDbSetMock.As<IQueryable<Season>>()
                .Setup(mock => mock.ElementType)
                .Returns(queSeason.ElementType);

            seasonDbSetMock.As<IQueryable<Season>>()
                .Setup(mock => mock.GetEnumerator())
                .Returns(queSeason.GetEnumerator());


            contextMock.Setup(mock => mock.Set<Map>()).Returns(mapDbSetMock.Object);
            contextMock.Setup(mock => mock.Set<Place>()).Returns(placeDbSetMock.Object);
            contextMock.Setup(mock => mock.Set<Player>()).Returns(playerDbSetMock.Object);
            contextMock.Setup(mock => mock.Set<Season>()).Returns(seasonDbSetMock.Object);

            mapLogic = new MapLogic(mapRepository, playerRepository, seasonRepository);
            seasonLogic = new SeasonLogic(seasonRepository, placeRepository, playerRepository);
            playerLogic = new PlayerLogic(playerRepository, seasonRepository);
            placeLogic = new PlaceLogic(placeRepository, playerRepository, seasonRepository);
        }
    }
}

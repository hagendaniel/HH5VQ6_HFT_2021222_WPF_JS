using HH5VQ6_HFT_2021221.Data;
using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(GameDbContext gameDbContext) : base(gameDbContext)
        {

        }
        public void changeStatus(int id, bool newStatus, int eliminatedOnMapId)
        {
            var player = GetOne(id);
            player.AliveOrDead = newStatus;
            //player.EliminatedOnMap_MapId = gameDbContext.Maps.Where(x => x.MapName == eliminatedOnMap).Select(x => x.MapId).FirstOrDefault();
            player.EliminatedOnMap_MapId = eliminatedOnMapId;
            gameDbContext.SaveChanges();
        }

        //public void customId(int id, int customId)
        //{
        //    var player = GetOne(id);
        //    player.PlayerId = customId;
        //    gameDbContext.SaveChanges();
        //}

        public void registerNewPlayer(Player newPlayer)
        {
            gameDbContext.Players.Add(newPlayer);
            gameDbContext.SaveChanges();
        }

        public void removePlayer(int id)
        {
            gameDbContext.Players.Remove(GetOne(id));
            gameDbContext.SaveChanges();
        }

        public override Player GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.PlayerId == id);
        }

        public override void Delete(int id)
        {
            Player player = GetOne(id);
            gameDbContext.Set<Player>().Remove(player);
            gameDbContext.SaveChanges();
        }
    }
}

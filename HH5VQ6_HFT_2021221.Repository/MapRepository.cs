using HH5VQ6_HFT_2021221.Data;
using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Repository
{
    public class MapRepository : Repository<Map>, IMapRepository
    {
        public MapRepository(GameDbContext gameDbContext) : base(gameDbContext)
        {

        }
        public void addMap(/*string mapName, int difficulty*/Map map)
        {
            gameDbContext.Maps.Add(/*new Map() { MapName = mapName, Difficulty = difficulty }*/map);
            gameDbContext.SaveChanges();
        }

        public void removeMap(int id)
        {
            gameDbContext.Maps.Remove(GetOne(id));
            gameDbContext.SaveChanges();
        }

        public void renameMap(int id, string newName)
        {
            var map = GetOne(id);
            map.MapName = newName;
            gameDbContext.SaveChanges();
        }

        public override Map GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.MapId == id);
        }

        public override void Delete(int id)
        {
            Map map = GetOne(id);
            gameDbContext.Set<Map>().Remove(map);
            gameDbContext.SaveChanges();
        }
    }
}

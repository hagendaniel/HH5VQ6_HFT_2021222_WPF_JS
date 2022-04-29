using HH5VQ6_HFT_2021221.Data;
using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Repository
{
    public class SeasonRepository : Repository<Season>, ISeasonRepository
    {
        public SeasonRepository(GameDbContext gameDbContext) : base(gameDbContext)
        {

        }
        public void newSeason(/*string seasonName, int placeId*/Season season)
        {
            gameDbContext.Seasons.Add(/*new Season() { SeasonNickname = seasonName, PlaceId = placeId }*/season);
            gameDbContext.SaveChanges();
        }

        public void removeSeason(int id)
        {
            gameDbContext.Seasons.Remove(GetOne(id));
            gameDbContext.SaveChanges();
        }

        public void changeName(int id, string newName)
        {
            var season = GetOne(id);
            season.SeasonNickname = newName;
            gameDbContext.SaveChanges();
        }

        public override Season GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.SeasonId == id);
        }

        public override void Delete(int id)
        {
            Season season = GetOne(id);
            gameDbContext.Set<Season>().Remove(season);
            gameDbContext.SaveChanges();
        }
    }
}

using HH5VQ6_HFT_2021221.Data;
using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Repository
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {
        public PlaceRepository(GameDbContext gameDbContext) : base(gameDbContext)
        {

        }
        public void addPlace(/*string placeName, string country*/Place place)
        {
            gameDbContext.Places.Add(/*new Place() { PlaceName = placeName, Country = country }*/place);
            gameDbContext.SaveChanges();
        }

        public void removePlace(int id)
        {
            gameDbContext.Places.Remove(GetOne(id));
            gameDbContext.SaveChanges();
        }

        public void changePlace(int id, string newPlace)
        {
            var place = GetOne(id);
            place.PlaceName = newPlace;
            gameDbContext.SaveChanges();
        }

        public override Place GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.PlaceId == id);
        }

        public override void Delete(int id)
        {
            Place place = GetOne(id);
            gameDbContext.Set<Place>().Remove(place);
            gameDbContext.SaveChanges();
        }
    }
}

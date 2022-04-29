using HH5VQ6_HFT_2021221.Models;

namespace HH5VQ6_HFT_2021221.Repository
{
    public interface IPlaceRepository : IRepository<Place>
    {
        void addPlace(/*string placeName, string country*/ Place place);
        void removePlace(int id);
        void changePlace(int id, string newPlace);
    }
}

using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    public interface IPlaceLogic
    {
        Place getPlaceById(int id);
        void addPlace(/*string placeName, string country*/Place place);
        void removePlace(int id);
        void changePlace(int id, string newPlace);
        //string inWhichCityPlayerDied(int playerId); //for non-crud
        Place inWhichCityPlayerDied(int playerId); //for non-crud
        IList<Place> getAllPlaces();
    }
}

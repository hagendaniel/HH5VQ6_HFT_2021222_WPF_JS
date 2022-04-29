using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    public interface ISeasonLogic
    {
        Season getSeasonById(int id);
        void newSeason(/*string seasonName, int placeId*/Season season);
        void removeSeason(int id);
        void changeName(int id, string newName);
        IList<Season> getAllSeasons();
        Season whichSeasonFirstGameInGivenPlace(string placeName);
        Season whichSeasonWonByGivenPlayer(int playerId);
    }
}

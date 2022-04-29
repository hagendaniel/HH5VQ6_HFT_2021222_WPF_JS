using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    public interface IPlayerLogic
    {
        Player getPlayerById(int id);
        void registerNewPlayer(/*string name, DateTime dateTime, int debt*/ Player player);
        void removePlayer(int id);
        void changeStatus(int id, bool newStatus, int eliminatedOnMapId);
        //void customId(int id, int customId);
        IList<Player> getAllPlayers();

        Player whoWonGivenSeason(int seasonId);
    }
}

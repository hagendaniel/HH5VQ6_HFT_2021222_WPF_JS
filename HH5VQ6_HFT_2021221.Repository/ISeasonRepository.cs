using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Repository
{
    public interface ISeasonRepository : IRepository<Season>
    {
        void newSeason(/*string seasonName, int placeId*/Season season);
        void removeSeason(int id);
        void changeName(int id, string newName);
    }
}

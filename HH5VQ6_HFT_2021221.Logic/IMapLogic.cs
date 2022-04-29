using HH5VQ6_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    public interface IMapLogic
    {
        Map getMapById(int id);
        void addMap(/*string mapName, int difficulty*/ Map map);
        void deleteMap(int id);
        void renameMap(int id, string newName);
        IList<Map> getAllMaps();

        Map TheKillerMap(string seasonName); //for non-crud
    }
}

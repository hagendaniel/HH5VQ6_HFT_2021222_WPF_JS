using HH5VQ6_HFT_2021221.Models;

namespace HH5VQ6_HFT_2021221.Repository
{
    public interface IMapRepository : IRepository<Map>
    {
        void addMap(/*string mapName, int difficulty*/Map map);
        void removeMap(int id);
        void renameMap(int id, string newName);
    }
}

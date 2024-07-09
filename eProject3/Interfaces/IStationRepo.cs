using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface IStationRepo
    {
        Task<IEnumerable<Station>> GetStations();
        Task<Station> GetStationById(int id);
        Task<Station> CreateStation(Station station);
        Task<Station> UpdateStation(Station station);
        Task<Station> DeleteStation(int id);
        /*Task<List<Station>> GetFromStations(int from);
        Task<List<Station>> GetToStations(int to);*/
    }
}

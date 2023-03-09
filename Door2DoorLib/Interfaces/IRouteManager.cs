﻿using Door2DoorLib.DataModels;

namespace Door2DoorLib.Interfaces
{
    public interface IRouteManager
    {
        Task<bool> AddRouteAsync(Route route, Admin admin);
        Task<bool> DeleteRouteAsync(Route route, Admin admin);
        Task<bool> UpdateRouteAsync(Route route, Admin admin);
        Task<Route> GetRouteAsync(string routename);
        Task<IEnumerable<Route>> GetRoutesAsync();
    }
}
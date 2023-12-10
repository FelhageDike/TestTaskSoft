using DAL.DbModels;

namespace BL.Interface;

public interface IWeatherForecastService
{
    public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate);
}
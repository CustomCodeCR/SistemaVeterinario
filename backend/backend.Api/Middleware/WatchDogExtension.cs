using WatchDog;
using WatchDog.src.Enums;

namespace backend.Api.Middleware;

public static class WatchDogExtension
{
    public static IServiceCollection AddWatchDog(this IServiceCollection services)
    {
        services.AddWatchDogServices(options =>
        {
            options.IsAutoClear = true;
            options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Quarterly;
        });

        return services;
    }
}
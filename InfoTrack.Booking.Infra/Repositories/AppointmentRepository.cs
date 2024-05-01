using InfoTrack.Booking.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace InfoTrack.Booking.Infra.Repositories;

public interface IRepository<TEntity>
{
    public TEntity Add(TEntity entity);
    public TEntity? GetById(string id);
    public TEntity[] GetAll();
}

public class AppointmentRepository(IMemoryCache _memoryCache) : IRepository<Appointment>
{
    private const string CacheKey = "AppointmentsCollection";
    private Func<ICacheEntry, Appointment[]> CacheFactory = (cache) => Array.Empty<Appointment>();

    public Appointment Add(Appointment appointment)
    {
        var list = _memoryCache.GetOrCreate(CacheKey, CacheFactory);
        _memoryCache.Set(CacheKey, list.Append(appointment).ToArray());
        return appointment;
    }

    public Appointment? GetById(string id)
    {
        var list = _memoryCache.GetOrCreate(CacheKey, CacheFactory);
        return list?.FirstOrDefault<Appointment>(x => string.Equals(x.Id, id, StringComparison.InvariantCulture));
    }

    public Appointment[] GetAll()
    {
        return _memoryCache.Get<Appointment[]>(CacheKey) ?? Array.Empty<Appointment>();
    }
}



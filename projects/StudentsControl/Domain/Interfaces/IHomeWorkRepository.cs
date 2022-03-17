namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface IHomeWorkRepository : IDisposable
    {
        IReadOnlyCollection<HomeWorkEntity> GetAll();
        HomeWorkEntity Get(Guid id);
        HomeWorkEntity Create(HomeWorkInputEntity homework);
        HomeWorkEntity Edit(HomeWorkInputEntity homework);
        bool Delete(Guid id);
    }
}
namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface IHomeWorkService
    {
        IReadOnlyCollection<HomeWorkModel> GetAll();
        HomeWorkModel Get(Guid id);
        HomeWorkModel Create(HomeWorkInputModel homework);
        HomeWorkModel Edit(HomeWorkInputModel homework);
        bool Delete(Guid id);
    }
}
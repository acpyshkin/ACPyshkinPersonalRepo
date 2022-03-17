namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILecturerRepository : IDisposable
    {
        IReadOnlyCollection<LecturerEntity> GetAll();
        LecturerEntity Get(Guid id);
        LecturerEntity Create(LecturerInputEntity lecturer);
        LecturerEntity Edit(LecturerInputEntity lecturer);
        public bool Delete(Guid id);
    }
}

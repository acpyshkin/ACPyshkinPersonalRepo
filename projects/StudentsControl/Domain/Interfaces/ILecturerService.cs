namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILecturerService
    {
        IReadOnlyCollection<LecturerModel> GetAll();
        LecturerModel Get(Guid id);
        LecturerModel Create(LecturerInputModel lecture);
        LecturerModel Edit(LecturerInputModel lecture);
        bool Delete(Guid id);
    }
}

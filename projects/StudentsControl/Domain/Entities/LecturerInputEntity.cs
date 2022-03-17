namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LecturerInputEntity
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public Guid Course { get; init; }
    }
}
namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LecturerModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public CourseModel Course { get; init; }
    }
}
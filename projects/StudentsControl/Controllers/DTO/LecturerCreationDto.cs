using System;

namespace Controllers
{
    public class LecturerCreationDto
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public Guid Course { get; init; }
    }
}
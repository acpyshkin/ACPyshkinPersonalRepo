namespace Controllers
{
    using System;
    public class LecturerDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; }
        public string Name { get; init; }
        public Guid Course { get; init; }
    }
}
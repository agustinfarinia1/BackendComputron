namespace BackendProyectoFinal.DTOs.User
{
    public class UserInsertDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool Eliminated { get; set; }
        public int RoleId { get; set; }
        public int AddressId { get; set; }
    }
}

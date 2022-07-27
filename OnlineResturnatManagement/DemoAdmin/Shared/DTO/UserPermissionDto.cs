namespace OnlineResturnatManagement.Shared.DTO
{
    public class UserPermissionDto
    {
        public int ID { get; set; }
        public int UsrId { get; set; }
        public int MenuId { get; set; }
        public bool HasPermission { get; set; }
    }
}

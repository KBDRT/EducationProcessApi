namespace Domain.Entities.Auth
{
    public enum PermissionAction
    {
        None,
        Read,
        Delete,
        Create,
        Update,
        Upload,
    }

    public class Permission
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public PermissionAction Action {  get; set; } = PermissionAction.None;

        public string TargetForAction { get; set; } = string.Empty;

        public List<Role> Roles { get; set; } = [];

    }
}

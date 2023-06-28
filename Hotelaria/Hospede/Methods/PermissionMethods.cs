namespace Hospede.Methods;

public static class PermissionMethods
{
    public static async Task<bool> CheckAndRequestFileStoragePermissions()
    {
        var read = await Permissions.RequestAsync<Permissions.StorageRead>();
        var write = await Permissions.RequestAsync<Permissions.StorageWrite>();

        return read == PermissionStatus.Granted && write == PermissionStatus.Granted;
    }
}

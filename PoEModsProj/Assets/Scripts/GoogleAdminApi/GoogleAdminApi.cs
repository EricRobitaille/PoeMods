using UnityEngine;
using UnityGoogleDrive;
using System.Threading.Tasks;

public class GoogleAdminApi : MonoBehaviour
{
    public async void Awake()
    {
        await ListAsync();
    }

    async Task ListAsync()
    {
        var files = await GoogleDriveFiles.List().Send();
        files.Files.ForEach(x => Debug.Log(x.Name));
    }
}

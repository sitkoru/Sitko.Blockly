using Sitko.Core.Storage;
using Sitko.Core.Storage.Remote;

namespace Sitko.Blockly.Demo.Client;

public class RemoteStorageOptions : StorageOptions, IRemoteStorageOptions
{
    public Uri RemoteUrl { get; set; }
    public Func<HttpClient>? HttpClientFactory { get; set; }
}

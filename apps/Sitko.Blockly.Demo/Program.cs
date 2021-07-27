using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Sitko.Blockly.Demo
{
    public class Program
    {
        public static async Task Main(string[] args) => await CreateApplication(args).RunAsync().ConfigureAwait(false);

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            CreateApplication(args).GetHostBuilder();

        private static BlocklyApplication CreateApplication(string[] args) => new(args);
    }
}

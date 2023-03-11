using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using NitroxModel.Discovery;
using NitroxModel.Helper;
using NitroxModel.Platforms.OS.Shared;
using NitroxModel.Platforms.OS.Windows.Internal;
using NitroxModel.Platforms.Store.Exceptions;
using NitroxModel.Platforms.Store.Interfaces;

namespace NitroxModel.Platforms.Store
{
    public sealed class Standalone : IGamePlatform
    {
        private static Standalone instance;
        public static Standalone Instance => instance ??= new Standalone();

        public string Name => "Standalone";
        public Platform Platform => Platform.NONE;

        public bool OwnsGame(string gameDirectory)
        {
            return File.Exists(Path.Combine(gameDirectory, "standalone.txt"));
        }

        public async Task<ProcessEx> StartPlatformAsync()
        {
            await Task.CompletedTask; // Suppresses async-without-await warning - can be removed.
            throw new NotImplementedException(); // Not necessary to implement.
        }

        public string GetExeFile()
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessEx> StartGameAsync(string pathToGameExe, string launchArguments)
        {
            return ProcessEx.Start(
                    pathToGameExe,
                    new[] { (NitroxUser.LAUNCHER_PATH_ENV_KEY, NitroxUser.LauncherPath) },
                    Path.GetDirectoryName(pathToGameExe),
                    launchArguments
            );
        }
    }
}

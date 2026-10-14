using System.Diagnostics;
using System.IO;
using UnityEngine;
using Process = System.Diagnostics.Process;

public class GameLauncher : MonoBehaviour
{
    public void LaunchWreckAndRun()
    {
        Launch("WreckAndRun");
    }

    public void LaunchMarlonsGame()
    {
        Launch("MarlonsGame");
    }

    void Launch(string folderName)
    {
        string folder = Path.Combine(Application.streamingAssetsPath, folderName);
        string exe = Directory.GetFiles(folder, "*.exe")[0];
        Process.Start(new ProcessStartInfo
        {
            FileName = exe,
            WorkingDirectory = folder,
            UseShellExecute = true
        });
    }
}

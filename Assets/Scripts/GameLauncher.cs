using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Process = System.Diagnostics.Process;

public class GameLauncher : MonoBehaviour
{
    void Awake()
    {
        if (FindAnyObjectByType<EventSystem>() == null)
        {
            var es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }
    }

    void Start()
    {
        foreach (Button button in GetComponentsInChildren<Button>(true))
        {
            string name = button.gameObject.name;
            if (name == "WreckAndRunButton")
                button.onClick.AddListener(LaunchWreckAndRun);
            if (name == "MarlonsGameButton")
                button.onClick.AddListener(LaunchMarlonsGame);
        }
    }

    public void LaunchWreckAndRun()
    {
        UnityEngine.Debug.Log("Wreck and Run clicked");
        Launch("WreckAndRun");
    }

    public void LaunchMarlonsGame()
    {
        UnityEngine.Debug.Log("Marlon's Game clicked");
        Launch("MarlonsGame");
    }

    void Launch(string folderName)
    {
        string folder = Path.Combine(Application.streamingAssetsPath, folderName);
        string[] exes = Directory.GetFiles(folder, "*.exe", SearchOption.AllDirectories);
        string exe = null;
        for (int i = 0; i < exes.Length; i++)
        {
            string fileName = Path.GetFileName(exes[i]).ToLowerInvariant();
            if (fileName.Contains("crashhandler"))
                continue;
            exe = exes[i];
            break;
        }
        UnityEngine.Debug.Log("Launching: " + exe);
        Process.Start(new ProcessStartInfo
        {
            FileName = exe,
            WorkingDirectory = Path.GetDirectoryName(exe),
            UseShellExecute = true
        });
    }
}

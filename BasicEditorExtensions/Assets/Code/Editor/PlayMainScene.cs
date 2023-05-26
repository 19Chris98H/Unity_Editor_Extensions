using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayMainScene : EditorWindow {

    [MenuItem("Play/Play Main Scene %T")]
    public static void Run() {
        if (EditorApplication.isPlaying)
            return;
        
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        EditorPrefs.SetString("LastScene", SceneManager.GetActiveScene().path);
        EditorPrefs.SetBool("ResetToLastScene", true);
        
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
        EditorApplication.isPlaying = true;
    }

    [InitializeOnLoadMethod]
    static void Init()
    {
        EditorApplication.quitting += () => EditorPrefs.SetBool("ResetToLastScene", false);
        
        EditorApplication.playModeStateChanged += (state) =>
        {
            if (state == PlayModeStateChange.EnteredEditMode && EditorPrefs.GetBool("ResetToLastScene", false))
            {
                EditorPrefs.SetBool("ResetToLastScene", false);
                EditorSceneManager.OpenScene(EditorPrefs.GetString("LastScene", EditorBuildSettings.scenes[0].path));
            }
        };
    }
}

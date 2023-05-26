using System.IO;
using UnityEditor;
using UnityEngine;

public class ClearData : EditorWindow
{
    [MenuItem("Edit/Clear Data/Clear All")]
    public static void ClearAll() 
    {
        ClearGameSave();
        ClearOptionSettings();
    }

    [MenuItem("Edit/Clear Data/Clear Game Save")]
    public static void ClearGameSave() 
    {
        //Adjust the filename for the game data here
        ClearFile("GameSave");
    }

    [MenuItem("Edit/Clear Data/Clear Option Settings")]
    public static void ClearOptionSettings() 
    {
        //Adjust the filename for the game settings here
        ClearFile("OptionSettings");
    }

    static void ClearFile(string fileName)
    {
        try
        {
            //Adjust the path here
            var path = Application.persistentDataPath + "/Data/" + fileName + ".json";

            if(!File.Exists(path))
            {
                Debug.LogWarning($"{fileName} already cleared");
                return;
            }

            File.Delete(path);
            Debug.Log($"{fileName} successfully cleared.");          
        }
        catch (IOException e)
        {
            Debug.LogError($"Something went wrong! {fileName} could not be cleared./nException: {e.Message}");
        }
    }
}

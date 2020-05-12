using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StansAssets.Foundation.Editor {

[InitializeOnLoad]
public class CustomPackageManagerResolver : AssetPostprocessor {
    
    /// <summary>
    /// Google scope registry
    /// </summary>
    public static readonly ScopeRegistry SCOPE_REGISTRY_GOOGLE = new ScopeRegistry(
        "Game Package Registry by Google",
        "https://unityregistry-pa.googleapis.com",
        new HashSet<string>
        {
            "com.google"
        }
    ); 
    
    /// <summary>
    /// Static constructor
    /// Init on load
    /// Creates modifier for manifest.json(set by default), checks for google scope registry and
    /// firebase dependency(commented), adds it if not exists, writes changes
    /// </summary>
    ///static CustomPackageManagerResolver()
    ///{
    ///    
    ///    ManifestModifier modifier = new ManifestModifier();
    ///    bool scopeExists = modifier.CheckIfRegistryExists(SCOPE_REGISTRY_GOOGLE);
    ///    bool dependencyExists = modifier.CheckIfDependencyExists("com.google.firebase.app", "6.14.0");
    ///    
    ///    if(scopeExists && dependencyExists) return;
    ///    
    ///    modifier.AddScopeRegistry(SCOPE_REGISTRY_GOOGLE);
    ///    modifier.AddDependency("com.google.firebase.app","6.14.0");
    ///    modifier.ApplyChanges();
    ///    
    ///}

    /// <summary>
    /// Displays GUI dialog to accept modifications
    /// </summary>
    /// <returns>True if ok button pressed, false otherwise</returns>
    private static bool ModificationConfirmed()
    {
        EditorApplication.Beep();
        return EditorUtility.DisplayDialog( 
            "Dependency required", 
            "Required registries and dependencies will be added to manifest",
            "Ok",
            "Cancel");
    }

   
    /// <summary>
    /// To see packages changes in console 
    /// </summary>
    /// <param name="importedAssets"></param>
    /// <param name="deletedAssets"></param>
    /// <param name="movedAssets"></param>
    /// <param name="movedFromAssetPaths"></param>
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            Debug.Log("Reimported Asset: " + str);
        }
        foreach (string str in deletedAssets)
        {
            Debug.Log("Deleted Asset: " + str);
        }
        foreach (string str in movedAssets)
        {
            Debug.Log("Moved Asset: " + str);
        }
        foreach (string str in movedFromAssetPaths)
        {
            Debug.Log("Moved from asset path Asset: " + str);
        }
         
    }

}
} 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OkLetsHackThisArtImport
{  
    [MenuItem("Cusotm Tools/Convert Art Prefabs to Real Ones")]
    public static void ConvertArtPrefabsToRealOnes()
    {
        GameObject root = Selection.activeGameObject;
        if(!root)
        {
            Debug.LogError("Hey select a root object first");
            return;
        }
        OkLetsHackThisArtImport hackerer = new OkLetsHackThisArtImport();
        for (int i = 0; i < root.transform.childCount; ++i)
        {
            hackerer.UpgradeGameObjectPrefab(root.transform.GetChild(i).gameObject);
        }
    }

    Dictionary<GameObject, GameObject> _prefabMap = new Dictionary<GameObject, GameObject>();

    void UpgradeGameObjectPrefab(GameObject g)
    {
        GameObject oldPrefab = PrefabUtility.GetPrefabParent(g as Object) as GameObject;
        if (oldPrefab)
        {
            GameObject prefabToSet = null;
            bool hasPrefab = _prefabMap.TryGetValue(oldPrefab, out prefabToSet);
            if (!hasPrefab)
            {
                prefabToSet = PrefabUtility.CreatePrefab("Assets/Prefabs/Environment/" + oldPrefab.name + ".prefab", g);
                _prefabMap.Add(oldPrefab, prefabToSet);
            }

            TransformExtensions.Transform_RTS rts = g.transform.ExtractRTS();
            GameObject newG = PrefabUtility.ConnectGameObjectToPrefab(g, prefabToSet);
            newG.transform.SetFromTransformRTS(rts);
        }
        else
        {
            for (int i = 0; i < g.transform.childCount; ++i)
            {
                this.UpgradeGameObjectPrefab(g.transform.GetChild(i).gameObject);
            }
        }
    }
}

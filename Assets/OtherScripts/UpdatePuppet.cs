using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UpdatePuppet : MonoBehaviour {

    public Material baseJointColor;
    public Material jointColor;
    public Material mainBodyColor;
    public Material secondaryBodyColor;
    public Material armourColor;
    public Material weaponColor;

    public GameObject[] baseJointsArray;
    public GameObject[] jointArray;
    public GameObject[] mainBodyArray;
    public GameObject[] secondaryBodyArray;
    public GameObject[] armourArray;
    public GameObject[] weaponArray;

    // Update is called once per frame
    void Update()
    {
        updateMeshArrayColor(baseJointsArray, baseJointColor);
        updateMeshArrayColor(jointArray, jointColor);
        updateMeshArrayColor(mainBodyArray, mainBodyColor);
        updateMeshArrayColor(secondaryBodyArray, secondaryBodyColor);
        updateMeshArrayColor(armourArray, armourColor);
        updateMeshArrayColor(weaponArray, weaponColor);
    }

    void updateMeshArrayColor(GameObject[] array, Material color)
    {

        if (array != null && array.Length > 0 && color != null)
        {
            var CountLength = array.Length;
            for (int count = 0; count < CountLength; count++)
            {
                MeshRenderer _render = array[count].GetComponent<MeshRenderer>();
                _render.material = color;
            }
        }        
    }
}

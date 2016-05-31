using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class MeshCombine : MonoBehaviour
{
    void Start()
    {
        Renderer cachedRenderer = null;
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 1;
        while (i < meshFilters.Length)
        {
            if (cachedRenderer == null)
            {
                cachedRenderer = meshFilters[i].GetComponent<Renderer>();
            }
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        SetMaterialOnCombinedMesh(cachedRenderer);

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
        DestroyImmediate(transform.gameObject.GetComponent<MeshCombine>());
    }

    private void SetMaterialOnCombinedMesh(Renderer cachedRenderer)
    {
        transform.GetComponent<Renderer>().material = cachedRenderer.material;
    }
}

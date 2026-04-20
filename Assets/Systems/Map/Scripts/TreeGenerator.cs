using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private Vector3 extents;

    [SerializeField] private int count;

    [SerializeField] private float minScaleMultiplier, maxScaleMultiplier;

    [ContextMenu("Generate Trees")]
    public void GenerateTrees()
    {
        DestroyChildren();

        for (int i = 0; i < count; i++)
        {
            Transform t = GameObject.Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], transform).transform;

            t.position = GetRandomPos();

            t.localRotation = Quaternion.Euler(0f, Random.Range(0, 360f), 0f);
            t.localScale *= Random.Range(minScaleMultiplier, maxScaleMultiplier);
        }
    }

    public Vector3 GetRandomPos()
    {
        float x = Random.Range(-extents.x, extents.x);
        float z = Random.Range(-extents.z, extents.z);

        Vector3 pos = new Vector3(x, 1000f, z);

        if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
        {
            return hit.point;
        }

        return pos.FlatY();
    }

    private void DestroyChildren()
    {
        int childCount = transform.childCount;

        for (int i = childCount - 1; i >= 0f; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, extents * 2f);
    }
}

using UnityEngine;

public class CliffGenerator : MonoBehaviour
{
    [SerializeField] private Vector3 bounds;

    [SerializeField] private int density = 25;

    [SerializeField] private GameObject prefab;

    [SerializeField] private Vector3 scale;
    [SerializeField] private float scaleMultiplierMin, scaleMultiplierMax;

    [ContextMenu("Build Cliffs")]
    public void BuildCliffs()
    {
        DestroyChildren();

        Vector3 topRight = new Vector3(bounds.x, 0f, bounds.z);
        Vector3 bottomRight = new Vector3(bounds.x, 0f, -bounds.z);
        Vector3 topLeft = new Vector3(-bounds.x, 0f, bounds.z);
        Vector3 bottomLeft = new Vector3(-bounds.x, 0f, -bounds.z);

        BuildStrip(topRight, bottomRight, Vector3.left);
        BuildStrip(bottomRight, bottomLeft, Vector3.forward);
        BuildStrip(bottomLeft, topLeft, Vector3.right);
        BuildStrip(topLeft, topRight, Vector3.back);
    }

    public void BuildStrip(Vector3 _from, Vector3 _to, Vector3 _forward)
    {
        for (int i = 0; i <= density; i++)
        {
            float t = Mathf.Clamp01((float)i / density);

            GameObject cliff = Instantiate(prefab);

            cliff.transform.parent = transform;
            cliff.transform.localPosition = Vector3.Lerp(_from, _to, t);
            cliff.transform.forward = _forward;

            Vector3 wantedScale = scale * Random.Range(scaleMultiplierMin, scaleMultiplierMax);
            wantedScale.x *= Mathf.Sign(Random.Range(-1, 1));
            cliff.transform.localScale = wantedScale;

            cliff.transform.Rotate(Vector3.up, Random.Range(-15, 15));
        }
    }

    private void DestroyChildren()
    {
        int childCount = transform.childCount;

        for (int i = childCount - 1; i >= 0f; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}

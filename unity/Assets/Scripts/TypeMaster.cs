using UnityEngine;
using System.Collections.Generic;

public class TypeMaster : MonoBehaviour {

    public GameObject targetTemplate;
    public int numTargets;

    private string[] wordList = { "beat", "blood", "pulse", "live" };
    private List<TypeTarget> targets;
    private float targetOffset;

	void Start () {
        targetOffset = targetTemplate.GetComponent<TextMesh>().fontSize / 10;
        for (int i = 0; i < numTargets; i++)
        {
            TypeTarget tt = ((GameObject)Instantiate(targetTemplate)).GetComponent<TypeTarget>();
            tt.transform.parent = transform;
            tt.transform.localPosition = new Vector3(0, -(i * targetOffset), 0);
            tt.text = wordList[i];
        }
	}

    // Update is called once per frame
    void Update()
    {
	}

    public void OnDrawGizmos()
    {
        targetOffset = targetTemplate.GetComponent<TextMesh>().fontSize / 10;
        Gizmos.color = targetTemplate.GetComponent<TypeTarget>().matchColor;
        Vector3 pos = transform.position;
        for (int i = 0; i < numTargets; i++)
        {
            Gizmos.DrawLine(pos,
                new Vector3(pos.x + targetOffset * 2, pos.y - .9f * targetOffset, pos.z));
            Gizmos.DrawLine(new Vector3(pos.x, pos.y - .9f * targetOffset, pos.z),
                new Vector3(pos.x + targetOffset * 2, pos.y, pos.z));
            pos.y -= targetOffset;
        }
    }
}

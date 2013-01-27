using UnityEngine;
using System.Collections;

public class FeedbackController : MonoBehaviour {

    public float beatPos;

    private PulseController pulse;
    private Material textMaterial, lineMaterial;
    private float moveAmount = 5, startTime;
    private Vector3 startPos, endPos;

	// Use this for initialization
	void Start () {
        TextMesh textMesh = GetComponent<TextMesh>();
        pulse = transform.parent.GetComponent<PulseController>();
        Color feedbackColor;
        float distance = pulse.GetBeatDistance(beatPos);
        if (distance < pulse.distanceGood)
        {
            textMesh.text = "Good";
            feedbackColor = Color.green;
        }
        else if (distance < pulse.distanceFair)
        {
            textMesh.text = "Fair";
            feedbackColor = Color.yellow;
        }
        else
        {
            textMesh.text = "Bad";
            feedbackColor = Color.red;
        }

        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(0, new Vector3(beatPos-20, 2, 0));

        lineMaterial = line.material;
        textMaterial = GetComponent<MeshRenderer>().material;
        lineMaterial.color = feedbackColor;
        startTime = Time.time;
        startPos = transform.position;
        endPos = startPos;
        endPos.y -= moveAmount;
	}
	
	// Update is called once per frame
	void Update () {
        if (startTime + pulse.beatDelay < Time.time)
            Destroy(gameObject);
        else
        {
            transform.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / pulse.beatDelay);
        }

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInvis_FOV : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Enemy";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;


    public GameObject player;

    private Transform _selection;

    private void Start()
    {
    }

    private void Update()
    {
        FieldOfView target = null;
        FieldOfView fow = (FieldOfView)target;
       // Color.white;
        //.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Debug.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Debug.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
        foreach (Transform visibleTarget in fow.visibleTargets)
		{
			Debug.DrawLine(fow.transform.position, visibleTarget.position);
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
            }

            var ray = gameObject;
            RaycastHit hit;
            float distanceOfRay = 100;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distanceOfRay))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRender = selection.GetComponent<Renderer>();
                    if (selectionRender != null)
                    {
                        selectionRender.material = highlightMaterial;
                    }
                    _selection = selection;
                }
            }
        }
    }
}

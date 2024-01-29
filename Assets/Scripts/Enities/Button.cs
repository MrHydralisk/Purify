using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Thing, IInteractable
{
    [SerializeField]
    private Door relatedObject;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void InteractionAction(GameObject interactionSource)
    {
        if (relatedObject != null){
            relatedObject.LockStateChange();
            StartCoroutine(LineDraw());
        }
    }

    public IEnumerator LineDraw()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, relatedObject.transform.position);
        yield return new WaitForSeconds(5);
        lineRenderer.positionCount = 0;
    }
}

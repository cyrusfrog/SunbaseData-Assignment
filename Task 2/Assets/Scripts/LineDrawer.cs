using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private Vector3 mousePosition;
    private bool isDrawing = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            FinishDrawing();
        }
    }

    private void StartDrawing()
    {
        isDrawing = true;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, GetMouseWorldPosition());
    }

    private void ContinueDrawing()
    {
        if (!isDrawing) return;

        int positionIndex = lineRenderer.positionCount;
        lineRenderer.positionCount = positionIndex + 1;
        lineRenderer.SetPosition(positionIndex, GetMouseWorldPosition());
    }

    private void FinishDrawing()
    {
        isDrawing = false;
        ClearCirclesInPath();
        lineRenderer.positionCount = 0;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    private void ClearCirclesInPath()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(lineRenderer.GetPosition(0), lineRenderer.GetPosition(lineRenderer.positionCount - 1));
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Circle"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}

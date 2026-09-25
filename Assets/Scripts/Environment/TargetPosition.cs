using UnityEditor;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] Color gizmoColor = Color.cyan;
    [SerializeField] float gizmoRadius = 0.3f;
    [SerializeField] bool alwaysDraw = true;

    private void OnDrawGizmos()
    {
        if (!alwaysDraw) return;
        DrawGizmo();
    }

    private void OnDrawGizmosSelected()
    {
        DrawGizmo(highlight: true);
    }

    private void DrawGizmo(bool highlight = false)
    {
        Gizmos.color = highlight ? Color.yellow : gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
        Gizmos.DrawSphere(transform.position, gizmoRadius * 0.3f);
    }
}

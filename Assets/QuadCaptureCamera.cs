using UnityEngine;

[RequireComponent(typeof(Camera))]
public class QuadCaptureCamera : MonoBehaviour
{
    // NOTE:
    // There is a way to generate quad & camera on runtime.
    // However, if need to set layer, material and any others are difficult when do it so.

    #region Field

    protected Camera quadCaptureCamera;

    public GameObject quad;

    protected Vector3 previousQuadScale;

    #endregion Field

    #region Method

    protected void Awake()
    {
        this.quadCaptureCamera = base.GetComponent<Camera>();
        InitializeSettings(this.quad.transform.localScale);
    }

    protected void Update()
    {
        Vector3 quadScale = this.quad.transform.localScale;

        if (this.previousQuadScale != quadScale)
        {
            InitializeSettings(quadScale);
        }

        this.quadCaptureCamera.cullingMask = 1 << this.quad.layer;
    }

    protected virtual void InitializeSettings(Vector3 quadScale)
    {
        this.previousQuadScale = quadScale;

        this.quadCaptureCamera.orthographic = true;
        this.quadCaptureCamera.rect = new Rect(0, 0, quadScale.x, quadScale.y);
        this.quadCaptureCamera.orthographicSize = this.quadCaptureCamera.rect.height / 2;
        this.quadCaptureCamera.aspect = quadScale.x / quadScale.y;
    }

    #endregion Method
}
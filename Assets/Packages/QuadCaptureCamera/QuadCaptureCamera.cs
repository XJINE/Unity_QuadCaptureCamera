using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class QuadCaptureCamera : MonoBehaviour
{
    // NOTE:
    // There is a way to generate quad & camera on runtime.
    // However, if need to set layer, material and any others are difficult when do it so.

    #region Field

    protected new Camera camera;

    public GameObject quad;

    protected Vector3 previousQuadScale;

    #endregion Field

    #region Method

    protected void Awake()
    {
        this.camera = base.GetComponent<Camera>();
        InitializeSettings(this.quad.transform.localScale);
    }

    protected void Update()
    {
        if (this.quad == null) 
        {
            return;
        }

        Vector3 quadScale = this.quad.transform.localScale;

        if (this.previousQuadScale != quadScale)
        {
            InitializeSettings(quadScale);
        }

        // NOTE:
        // Culling mask setting like following code is not good.
        // This class should be used in many case.
        // this.camera.cullingMask = 1 << this.quad.layer;
    }

    protected virtual void InitializeSettings(Vector3 quadScale)
    {
        this.previousQuadScale = quadScale;

        this.camera.orthographic = true;
        this.camera.rect = new Rect(0, 0, quadScale.x, quadScale.y);
        this.camera.orthographicSize = this.camera.rect.height / 2;
        this.camera.aspect = quadScale.x / quadScale.y;

        // WARNING:
        // Need to do following setup. If not, output image are not show in fullscreen. 

        if (this.camera.rect.width < 1 || this.camera.rect.height < 1)
        {
            this.camera.rect = new Rect(this.camera.rect.x, this.camera.rect.y, 1, 1);
        }
    }

    #endregion Method
}
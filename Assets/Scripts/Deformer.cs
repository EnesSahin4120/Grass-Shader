using UnityEngine;

public class Deformer : MonoBehaviour
{
    [SerializeField] private Rigidbody _ball;

    [SerializeField] private Material _trackMaterial;
    [SerializeField] private Material _grassMaterial;
    [SerializeField] private GameObject _grass;

    private RenderTexture _dentTexture;

    private void Awake()
    {
        _dentTexture = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _grassMaterial.SetTexture("_DentTex", _dentTexture);
    }

    private void Update()
    {
        HitControl();
    }

    private void HitControl()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-transform.up,out hit, 1f)){
            if (hit.collider.gameObject == _grass && Vector3.SqrMagnitude(_ball.angularVelocity) != 0){
                _trackMaterial.SetVector("_CurrentTrackCoord", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                RenderTexture temp = RenderTexture.GetTemporary(_dentTexture.width, _dentTexture.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_dentTexture, temp);
                Graphics.Blit(temp, _dentTexture, _trackMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }
}

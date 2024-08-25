using UnityEngine;

//public enum BackgroundType { Blue, Brown, Gray}

public class AnimatedBackground : MonoBehaviour
{
    [SerializeField] private Vector2 movementDir;
    private MeshRenderer mr;

    //[Header("Color")]
    //[SerializeField] private BackgroundType backgroundType;
    //[SerializeField] private Texture2D[] textures;


    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        //UpdateBackgroundTexture();
    }

    private void Update()
    {
        mr.material.mainTextureOffset += movementDir * Time.deltaTime;
    }

    //[ContextMenu("update background")]
    //private void UpdateBackgroundTexture() => mr.material.mainTexture = textures[(int)backgroundType];
    
}

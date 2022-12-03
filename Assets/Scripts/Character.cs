using UnityEngine;

public enum CharacterColor { White,Blue, Green, Red, Yellow }
public class Character : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private Material[] colorMats;
    private SkinnedMeshRenderer[] meshes;
    protected Animator animator;

    [HideInInspector] public Vector3 moveDirection;
    protected CharacterColor characterColor;

    protected virtual void Start()
    {
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected void SetColor(CharacterColor color)
    {
        characterColor = color;
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].material = colorMats[(int)color];
        }
    }

    protected virtual void Move()
    {
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
        Quaternion lerpedRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), Time.deltaTime * rotationSpeed);
        transform.rotation = lerpedRot;
    }
}

using UnityEngine;

public class AI : MonoBehaviour
{
    CharacterMovement characterMovement;
    [SerializeField] LayerMask obstacleLayer;
    private void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }
    private void Update()
    {
        if (characterMovement.IsMovingToNextPosition()) return;
        Vector3 _nextPos = transform.position + new Vector3(characterMovement.DirectionEye.x, characterMovement.DirectionEye.y, 0);
        if (!Physics2D.CircleCast(_nextPos, 0.1f, Vector3.forward, 1, obstacleLayer))
            characterMovement.NextPosition = _nextPos;
        else
            characterMovement.DirectionEye = -characterMovement.DirectionEye;

    }
}

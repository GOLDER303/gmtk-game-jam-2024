using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiningTower : MonoBehaviour
{
    [SerializeField]
    private Transform minableResource;

    [SerializeField]
    private Transform miningPart;

    [SerializeField]
    private float miningPartMovementSpeed = 10f;

    private MiningPartState miningPartState = MiningPartState.GoingTowardsResource;

    private Vector3 defaultMiningPartPosition;

    private void Start()
    {
        defaultMiningPartPosition = miningPart.position;

        minableResource = SceneManager
            .GetActiveScene()
            .GetRootGameObjects()
            .First(gameObject => gameObject.name == "MinableResource")
            .transform;
    }

    private void Update()
    {
        switch (miningPartState)
        {
            case MiningPartState.GoingTowardsResource:
            {
                Vector3 newPosition = Vector3.MoveTowards(
                    miningPart.position,
                    minableResource.position,
                    miningPartMovementSpeed * Time.deltaTime
                );

                miningPart.position = newPosition;

                if (Vector3.Distance(miningPart.position, minableResource.position) < .01f)
                {
                    miningPartState = MiningPartState.GoingBack;
                }

                break;
            }

            case MiningPartState.GoingBack:
            {
                Vector3 newPosition = Vector3.MoveTowards(
                    miningPart.position,
                    defaultMiningPartPosition,
                    miningPartMovementSpeed * Time.deltaTime
                );

                miningPart.position = newPosition;

                if (Vector3.Distance(miningPart.position, defaultMiningPartPosition) < .01f)
                {
                    miningPartState = MiningPartState.GoingTowardsResource;
                }
                break;
            }

            default:
                break;
        }
    }
}

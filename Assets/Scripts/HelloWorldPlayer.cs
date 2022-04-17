using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector2> Position = new NetworkVariable<Vector2>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
            }
        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                Position.Value = randomPosition;
            }
            else
            {
                Debug.Log("I'm a client");
                SubmitRandomPositionRequestServerRpc();
            }
        }
        
        public void MoveForward(Vector2 position)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                transform.position = position;
                Position.Value = position;
            }
            else
            {
                Debug.Log("I'm a client");
                SubmitPositionRequestServerRpc(transform.position);
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(Vector2 position, ServerRpcParams rpcParams = default)
        {
            Position.Value = position;
        }

        [ServerRpc]
        void SubmitRandomPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }

        static Vector2 GetRandomPositionOnPlane()
        {
            return new Vector2(Random.Range(-17f, 17f), Random.Range(-10f, 10f));
        }

        void Update()
        {
            transform.position = Position.Value;
        }
    }
}
using GameInput;
using Pool;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies
{
    public class Skeleton : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _ignoreMask;

        private FPInputHandler _player;
        private NavMeshAgent _agent;
        private SkeletonPool _pool;

        private bool _wasInited = false;
        private bool _playerInAttackRange;

        [Inject]
        private void Construct(FPInputHandler player, SkeletonPool pool)
        {
            _pool = pool;
            _player = player;
        }

        public void Init()
        {
            _agent = gameObject.AddComponent<NavMeshAgent>();
            _wasInited = true;
        }

        private void FixedUpdate()
        {
            if (_wasInited)
            {
                _playerInAttackRange = Physics.CheckSphere(_player.transform.position, _attackRange, _ignoreMask);

                if (_playerInAttackRange)
                {
                    Attack();
                }
                else
                {
                    Chase();
                }
            }
        }

        public void Attack()
        {
            _agent.isStopped = true;
            _animator.SetBool("IsRunning", false);
            _animator.SetTrigger("DoScreaming");
            _agent.transform.LookAt(_player.transform.position);
        }

        public void Chase()
        {
            _agent.isStopped = false;
            _animator.ResetTrigger("DoScreaming");
            _animator.SetBool("IsRunning", true);
            _agent.SetDestination(_player.transform.position);
            _agent.transform.LookAt(_player.transform.position);
        }

        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}
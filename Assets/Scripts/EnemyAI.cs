using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private Transform tankShoot;

    private float _attackRange = 3f;
    private float _rayDistance = 5.0f;
    private float _stoppingDistance = 1.5f;

    private Vector3 _destination;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private TankView _target;
    private TankState _currentState;


    private void Update()
    {
        switch (_currentState)
        {
            case TankState.Wander:
                {
                    if (NeedsDestination())
                    {
                        GetDestination();
                    }

                    transform.rotation = _desiredRotation;

                    transform.Translate(Vector3.forward * Time.deltaTime * 5f);

                    Color rayColor = IsPathBlocked() ? Color.red : Color.green;
                    Debug.DrawRay(transform.position, _direction * _rayDistance, rayColor);

                    while (IsPathBlocked())
                    {
                        Debug.Log("Path Blocked");
                        GetDestination();
                    }

                    var targetToAttack = CheckForPlayer();
                    if (targetToAttack != null)
                    {
                        _target = targetToAttack.GetComponent<TankView>();   
                        _currentState = TankState.Chase;
                    }

                    break;
                }
            case TankState.Chase:
                {
                    if (_target == null)
                    {
                        _currentState = TankState.Wander;
                        return;
                    }

                    transform.LookAt(_target.transform);
                    transform.Translate(Vector3.forward * Time.deltaTime * 5f);
                    float dist = Vector3.Distance(transform.position, _target.transform.position);
                    if (dist < _attackRange)
                    {
                        _currentState = TankState.Attack;
                    }
                    break;
                }
            case TankState.Attack:
                {
                    if (_target != null)
                    {
                        Debug.Log("shooting");
                        BulletService.Instance.InstantiateBullet(tankShoot);
                    }
                    else
                        _currentState = TankState.Wander;
                    break;
                }
        }
        Debug.Log(_currentState, gameObject);
        Debug.Log(_target, gameObject);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, _direction);
        var hitSomething = Physics.RaycastAll(ray, _rayDistance, _layerMask);
        return hitSomething.Any();
    }

    private void GetDestination()
    {
        Vector3 randomPoint = new Vector3(Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
        Vector3 testPosition = (transform.position + (transform.forward * 4f)) + randomPoint;
 

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    private bool NeedsDestination()
    {
        if (_destination == Vector3.zero)
            return true;

        var distance = Vector3.Distance(transform.position, _destination);
        if (distance <= _stoppingDistance)
        {
            return true;
        }

        return false;
    }



    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForPlayer()
    {
        float detectRange = 10f;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
/*        for (int i = 0; i < 24; i++)
        {
*/            if (Physics.Raycast(pos, direction, out hit, detectRange, _layerMask))
            {
                TankView tank = hit.collider.GetComponent<TankView>();
                if (tank != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return tank.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
                Debug.Log("Hit Info: " + hit.collider.gameObject.name);
                Debug.Log("Tank  "+ tank.gameObject.name);
            }
            else
            {
                Debug.DrawRay(pos, direction * detectRange, Color.white);
            }
            direction = stepAngle * direction;
/*        }
*/        return null;
    }
}

public enum TankState
{
    Wander,
    Chase,
    Attack
}

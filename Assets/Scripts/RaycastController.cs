using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private Camera _cam;
    private Vector2 _middlePoint = new(0.5f, 0.5f);

    [Header("Settings")]
    [SerializeField] private float _maxdistance = 200;
    [SerializeField] private LayerMask _layer;

    private GameObject _current;
    private float _maxLookTime = 1;
    private float _keepLookTime;

    public delegate void DelegateTimer(float time);
    public static DelegateTimer OnInteractionTimer;
    private void Awake()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        Ray r = _cam.ViewportPointToRay(_middlePoint);

        Debug.DrawLine(r.origin, r.direction * 200, Color.red, 1);

        if (Physics.Raycast(r, out RaycastHit hit, _maxdistance, _layer))
        {
            //check if the current look object is different than previous.
            if (_current != hit.collider.gameObject)
            {
                _keepLookTime = 0;
                _current = hit.collider.gameObject;
            }
            else
            {
                //if it is the same, then add time viewing.

                if (_keepLookTime >= _maxLookTime) //check if reaches the required time to do an interaction.
                {
                    _keepLookTime = 0;

                    hit.collider.TryGetComponent(out Cardboard.Interactions.Interactable i);

                    i.Interaction();
                }

                _keepLookTime += Time.deltaTime;
            }
        }
        else
            _keepLookTime = 0;

        OnInteractionTimer?.Invoke(_keepLookTime / _maxLookTime);
    }
}

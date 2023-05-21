using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoneUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI zoneName;
    [SerializeField] Image fontZone = null;
    [SerializeField] Transform finalPosition;
    [SerializeField] float animSpeed = 1.0f;
    [SerializeField] float timeDisplay = 1.0f;
    [SerializeField] Vector3 basePosition;
    Vector3 nextPosition;
    bool active = false;
    private void Start()
    {
        basePosition = fontZone.transform.position;
        ZoneManager.Instance.OnZoneChange += ZoneEnter;
    }
    private void Update()
    {
        if (!active) return;
        fontZone.transform.position = Vector3.Lerp(fontZone.transform.position, nextPosition, animSpeed * Time.deltaTime);
        if (Vector3.Distance(fontZone.transform.position, nextPosition) <= 0.25f)
        {
            active = false;
            Invoke(nameof(Turn), timeDisplay);
        }
    }
    public void Turn()
    {
        if (Vector3.Distance(nextPosition, basePosition) < 0.25f) return;
        nextPosition = basePosition;
        active = true;
    }
    void ZoneEnter(Zone _zone)
    {
        zoneName.text = _zone.ZoneName;
        fontZone.sprite = _zone.ZoneFont;
        fontZone.transform.position = basePosition;
        nextPosition = finalPosition.position;
        active = true;
    }
}

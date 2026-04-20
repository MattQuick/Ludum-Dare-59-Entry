using UnityEngine;

[System.Serializable]
public abstract class GameState_Base
{
    [SerializeField] private GameObject[] gameObjectsToActivate;

    public virtual void OnStateEnter()
    {
        ToggleObjects(true);
    }

    public virtual void OnStateExit()
    {
        ToggleObjects(false);
    }

    public virtual void OnStateUpdate() { }

    public virtual void OnStateFixedUpdate() { }

    public virtual void OnStateLateUpdate() { }

    private void ToggleObjects(bool _active)
    {
        for (int i = 0; i < gameObjectsToActivate.Length; i++)
        {
            gameObjectsToActivate[i].SetActive(_active);
        }
    }
}

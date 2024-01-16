using UnityEngine;
using UnityEngine.UI;


public class Selector : MonoBehaviour 
{
	private const int DEFAULT_TIME = 5;

	public GameObject popUpInfoPrefab;
	public GameObject popUpExitPrefab;
	public GameObject playIconPrefab;
	public Transform buttonsContainer;
	public float selectionTime;
	public Transform panelContainer;

	private Image triggerIcon;
	private Transform selectedButton;
	private float selectionCont;

	public enum PopUpType
    {
		INFO_POPUP,
		EXIT_POPUP
    }

	void Start () 
	{
		if(selectionTime <= 0)
        {
			selectionTime = DEFAULT_TIME;
		}
	}
	
	void Update () 
	{
		RaycastHit hit;

		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
		{
			if(selectedButton != hit.transform)// && popUp == null)
            {
				Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				Debug.Log("HIT: " + hit.transform.gameObject.name);

				selectedButton = hit.transform;

				if (triggerIcon != null)
					Destroy(triggerIcon.gameObject);

				triggerIcon = Instantiate(playIconPrefab, selectedButton.transform.position, Quaternion.identity).GetComponent<Image>();
				triggerIcon.fillAmount = 0;
				triggerIcon.transform.parent = buttonsContainer.parent;
				triggerIcon.transform.LookAt(Camera.main.transform);

				selectionCont = Time.time;
			}
			else if(triggerIcon != null)
            {
				triggerIcon.fillAmount =  (Time.time - selectionCont) / selectionTime;

				if((Time.time - selectionCont) >= selectionTime)
                {
					Debug.Log("TriggerButton");

					TriggerButton(selectedButton);
				}
			}
		}
		else if (triggerIcon != null)
		{
			CleanTimer();
        }
	}

	private void TriggerButton(Transform button)
    {
		var buttonData = button.transform.parent.GetComponent<ButtonTrigger>();

		if (buttonData != null)
		{
			if (buttonData.isPopUp)
			{
				GameObject prefab;
				switch(buttonData.popUpType)
                {
					case PopUpType.EXIT_POPUP:
						{
							prefab = popUpExitPrefab;
							break;
						}
					case PopUpType.INFO_POPUP:
					default:
						{
							prefab = popUpInfoPrefab;
							break;
						}
				}

				var popUp = Instantiate(prefab, panelContainer).GetComponent<PopUp>();
				popUp?.Init(buttonData.messageToDisplay);
			}
		}
        else
		{
			var ac = button.transform.parent.GetComponent<ButtonAction>();
			ac?.callback.Invoke();
		}

		//Destroy(selectedButton.gameObject);
		selectionCont = Time.time;

		CleanTimer();
	}

	private void CleanTimer()
    {
		Destroy(triggerIcon.gameObject);
		selectionCont = 0;
		selectedButton = null;
	}
}
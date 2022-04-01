using UnityEngine;

public class cashmoneyscript : MonoBehaviour
{
    public GameObject cashobj;
    public TicketMachine ticket;
	public AudioSource audios;
	public Color color;
	public TextLog textlog;
	bool used = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !used)
		{
            ticket.CompleteTask();
			textlog.LogText("You hoover up all of the money, totaling at 2 dollars and 43 cents (adjusted for inflation.)", color);
			Destroy(cashobj);
			audios.Play();
			used = true;
			Destroy(gameObject, 2);
        }
	}
}

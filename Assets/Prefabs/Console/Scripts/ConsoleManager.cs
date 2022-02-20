using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    private List<string> printingQueue = new List<string>();
    private List<Text> pastPrints = new List<Text>();
    

    [Header("Config")]
    [SerializeField]
    private Vector2 startPosition = new Vector2(0, -50);
    [SerializeField]
    private float upPushMagnitude = 25f;
    [SerializeField]
    private int maxNumberOfPrints = 20;
    [SerializeField]
    private bool allowInputs = true;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject messagePrefab;
    [Header("Scene Elements")]
    [SerializeField]
    private Image parent;
    [SerializeField]
    private InputField input;
    


    #region Private Variables
    private bool isPrinting = false;
    private float globalTimer = 0f;
    private string printLeft;

    private List<string> inputs = new List<string>();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isPrinting && printingQueue.Count > 0)
        {
            print("Printing new message!");
            this.PrintString(printingQueue[0]);
            printingQueue.RemoveAt(0);
            
        }
        if (Input.GetButtonDown("Submit"))
        {
            this.PrintToConsole(this.input.text);
            this.inputs.Add(this.input.text);
            this.input.text = "";
        }
        

        this.globalTimer += Time.deltaTime;
    }



    /// <summary>
    /// Send this message to the console. usr> [text] is the format of the message.
    /// </summary>
    /// <param name="text">The text being printed</param>
    public void PrintToConsole(string text)
    {
        this.printingQueue.Add("usr> "+text);
    }


    private void PrintString(string text)
    {
        this.isPrinting = true;
        this.printLeft = text;

        //Shift everything up
        foreach(Text print in pastPrints)
        {
            Vector2 printPos = print.gameObject.transform.position;
            print.gameObject.transform.position = new Vector2(printPos.x,printPos.y+this.upPushMagnitude);
        }

        //Then instantiate
        print("Instantiated!");
        GameObject newText = Instantiate(this.messagePrefab.gameObject,parent.transform);
        newText.transform.localPosition = startPosition;
        Text newTextElement = newText.GetComponent<Text>();
        this.pastPrints.Add(newTextElement);
        newTextElement.text = text;

        if (this.pastPrints.Count > this.maxNumberOfPrints)
        {
            Text firstPrint = this.pastPrints[0];
            this.pastPrints.RemoveAt(0);
            Destroy(firstPrint.gameObject);
        }

        this.isPrinting = false;
    }

    /// <summary>
    /// Returns whether console has input in queue.
    /// </summary>
    /// <returns>Boolean if there is input in queue.</returns>
    public bool hasInput()
    {
        return this.inputs.Count > 0;
    }
    /// <summary>
    /// Gets the next input in the queue.
    /// </summary>
    /// <returns>The input in it.</returns>
    public string getNextInput()
    {
        if (this.hasInput())
        {
            string ret = this.inputs[0];
            this.inputs.RemoveAt(0);
            return ret;
        }
        return null;
    }
    
}

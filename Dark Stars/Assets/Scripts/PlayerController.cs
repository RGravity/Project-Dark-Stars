using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    //speed stuff
    float speed;
    public int cruiseSpeed;
    float deltaSpeed;//(speed - cruisespeed)
    public int minSpeed;
    public int maxSpeed;
    float accel, decel;

    //turning stuff
    Vector3 angVel;
    Vector3 shipRot;
    public int sensitivity;
    public Vector3 cameraOffset;

    //HP
    private bool _hit = false;
    public int HP = 10;
    public bool Hit { set { _hit = value; } }

    //damageScreen
    private Color _damageScreenColor ;// 
    private float _alphaIncrease = 255 / 100;

    //Score
    private int _score = 0;
    private int _amountOfXenonite = 0;
    private int _amountOfHelionite = 0;
    private int _amountOfArgonite = 0;
    private int _amountOfNeonite = 0;
    private int _timeElapsed = 0;
    private int _amountOfKilledSpeeders = 0;
    private int _amountOfKilledAssailants = 0;
    private int _amountOfKilledBruisers = 0;
    private float _amountCrystalFill = 0;

    private bool _showDamage = false;
    private int _timer = 72;

    private Image _cristalFillImage;
    private Image cristalFillImage
    {
        get
        {
            if (_cristalFillImage == null)
                _cristalFillImage = GameObject.Find("crystalFill").GetComponent<Image>();
            return _cristalFillImage;
        }
    }

    public float AmountCrystalFill
    {
        get { return _amountCrystalFill; }
        set
        {
            cristalFillImage.fillAmount = value / 100;
            _amountCrystalFill = value;
        }
    }

    public int Score { get { return _score; } set { _score = value; } }
    public int AmountOfXenonite { get { return _amountOfXenonite; } set { _amountOfXenonite = value; } }
    public int AmountOfHelionite { get { return _amountOfHelionite; } set { _amountOfHelionite = value; } }
    public int AmountOfArgonite { get { return _amountOfArgonite; } set { _amountOfArgonite = value; } }
    public int AmountOfNeonite { get { return _amountOfNeonite; } set { _amountOfNeonite = value; } }
    public int TimeElapsed { get { return _timeElapsed; } set { _timeElapsed = value; } }
    public int AmountOfKilledSpeeders { get { return _amountOfKilledSpeeders; } set { _amountOfKilledSpeeders = value; } }
    public int AmountOfKilledAssailants { get { return _amountOfKilledAssailants; } set { _amountOfKilledAssailants = value; } }
    public int AmountOfKilledBruisers { get { return _amountOfKilledBruisers; } set { _amountOfKilledBruisers = value; } }


    void Start()
    {
        _damageScreenColor = GameObject.Find("DamageScreen").GetComponent<Image>().color;
        speed = cruiseSpeed;
        GameObject.Find("DamageScreen").GetComponent<Image>().enabled = false;
        GameObject.Find("HealthCounter").GetComponent<Text>().text = HP.ToString();
    }

    void FixedUpdate()
    {
        //Do all the stuff
        Movement();
        CheckHit();

        if (cristalFillImage.fillAmount >= 0.98)
        {
            gameWon();
        }
    }

    void Movement()
    {
        shipRot = transform.GetChild(1).localEulerAngles;

        if (shipRot.x > 180) shipRot.x -= 360;
        if (shipRot.y > 180) shipRot.y -= 360;
        if (shipRot.z > 180) shipRot.z -= 360;

        angVel.x += Input.GetAxis("Vertical") * Mathf.Abs(Input.GetAxis("Vertical")) * sensitivity * Time.fixedDeltaTime;

        float turn = Input.GetAxis("Horizontal") * Mathf.Abs(Input.GetAxis("Horizontal")) * sensitivity * Time.fixedDeltaTime;
        angVel.y += turn * .5f;
        angVel.z -= turn * .5f;

        if (Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Q))
        {
            angVel.y -= 20;
            angVel.z += 50;
            speed -= 5 * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.E))
        {
            angVel.y += 20;
            angVel.z -= 50;
            speed -= 5 * Time.fixedDeltaTime;
        }

        angVel /= 1 + deltaSpeed * .001f;

        angVel -= angVel.normalized * angVel.sqrMagnitude * .08f * Time.fixedDeltaTime;

        transform.GetChild(1).Rotate(angVel * Time.fixedDeltaTime);

        transform.GetChild(1).Rotate(-shipRot.normalized * .015f * (shipRot.sqrMagnitude + 500) * (1 + speed / maxSpeed) * Time.fixedDeltaTime);

        deltaSpeed = speed - cruiseSpeed;

        decel = speed - minSpeed;
        accel = maxSpeed - speed;
        
        bool rightTriggerHeld = Input.GetAxis("Mouse X") > 0.001f;
        bool leftTriggerHeld = Input.GetAxis("Mouse X") < 0;

        if (Input.GetKey(KeyCode.LeftShift))
            speed += accel * Time.fixedDeltaTime;
        else if (rightTriggerHeld)
            speed += accel * Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.Space))
            speed -= decel * Time.fixedDeltaTime;
        else if (leftTriggerHeld)
            speed -= decel * Time.fixedDeltaTime;

        else if (Mathf.Abs(deltaSpeed) > .1f)
            speed -= Mathf.Clamp(deltaSpeed * Mathf.Abs(deltaSpeed), -30, 100) * Time.fixedDeltaTime;

        transform.GetChild(0).localPosition = cameraOffset + new Vector3(0, 0, -deltaSpeed * .02f);


        float sqrOffset = transform.GetChild(1).localPosition.sqrMagnitude;
        Vector3 offsetDir = transform.GetChild(1).localPosition.normalized;


        transform.GetChild(1).Translate(-offsetDir * sqrOffset * 20 * Time.fixedDeltaTime);

        transform.Translate((offsetDir * sqrOffset * 50 + transform.GetChild(1).forward * speed) * Time.fixedDeltaTime, Space.World);

        transform.Rotate(shipRot.x * Time.fixedDeltaTime, (shipRot.y * Mathf.Abs(shipRot.y) * .02f) * Time.fixedDeltaTime, shipRot.z * Time.fixedDeltaTime);
    }

    void CheckHit()
    {
        if (_hit)
        {
            HP--;
            GameObject.Find("HealthCounter").GetComponent<Text>().text = HP.ToString();

            _showDamage = true;
            GameObject.Find("DamageScreen").GetComponent<Image>().enabled = true;


            Hit = false;
            if (HP <= 0)
            {
                GameObject.FindObjectOfType<YouFailScript>().YouDiedBool = true;
            }
        }
        if (_showDamage)
        {
            _timer--;
            if (_timer <= 0)
            {
                GameObject.Find("DamageScreen").GetComponent<Image>().enabled = false;
                _timer = 72;
            }
        }
    }

    void gameWon()
    {
        ScoreData scoreData = GameObject.FindObjectOfType<ScoreData>();

        scoreData.Xenonite = _amountOfXenonite;
        scoreData.Helionite = _amountOfHelionite;
        scoreData.Argonite = _amountOfArgonite;
        scoreData.Neonite = _amountOfNeonite;

        scoreData.Speeder = _amountOfKilledSpeeders;
        scoreData.Assailant = _amountOfKilledAssailants;
        scoreData.Bruiser = _amountOfKilledBruisers;

        Application.LoadLevel("Victory");
    }
}

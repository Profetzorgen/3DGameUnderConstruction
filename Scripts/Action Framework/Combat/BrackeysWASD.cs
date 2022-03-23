using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI;

//namespace Brackeys // Denna kod �r i grunden Brackeys fr�n YouTube, med massa till�gg utav mig
//{
    [
    //RequireComponent(typeof(NavMeshAgent)), // l�t gr�nt st�!
    //RequireComponent(typeof(Rigidbody)), // vissa beh�vs n�r jag testar funktioner i editor
    RequireComponent(typeof(Animator))
    ] // FELHANTERING: Dessa rader ovan ser till att det objekt du klistrar detta skript p�...
    // ... f�r alla komponenter som kr�vs fr�n start ifall de gl�mts l�ggas till i editorn.
    public class BrackeysWASD : MonoBehaviour
    {
        public Animator animator; // ny
        private NavMeshAgent navMeshAgent; // objekt som pratar med kartan, "kan jag g� h�r, ja/nej"
        public CharacterController controller; // styrningen av sj�lva gubben med WASD + collider
        //private Rigidbody rigidbody;
        public Transform cam; // Tredjepersons-kameran f�ljer spelaren via denna XYZ.
        public float speed = 6f; // Den faktiska hastigheten p� spelaren avg�rs h�r, �ven animator-trigger

        // Animator-relaterad triggerinformation
        [SerializeField] private string param_speed = "Speed"; // ny
        [SerializeField] private string param_attack = "Attack"; // ny
        [SerializeField] private string param_specialattack = "SpecialAttack"; // ny

        public float turnSmoothTime = 0.1f; // ryck mindre, sv�ng snyggare
        float turnSmoothVelocity;

        public void Awake() // Vid uppstart, FELHANTERING/Dubbelkolla att inget saknas:
        {
            animator = GetComponent<Animator>();
            //controller = GetComponent<CharacterController>();
            //rigidbody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            Assert.IsNotNull(animator);
            //Assert.IsNotNull(CharacterController);
            //Assert.IsNotNull(rigidbody);
            Assert.IsNotNull(navMeshAgent);
        }
        public void Run() // ny hela
        {
            if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift)) // sprinta!
            {
                speed = 12f; 
                animator.SetFloat(param_speed, speed); // visa att du sprintar!
            }
            else if (Input.GetKey("w")) // promenera!
            {
                speed = 6f;
                animator.SetFloat(param_speed, speed); // visa att du promenerar!
            }
            else speed = 0f;
            animator.SetFloat(param_speed, speed); // st� still och visa att du st�r still!
        }
        private void CheckForAttacks()
        {
            if (Input.GetMouseButtonDown(0)) // Vertikalt hugg
            {
                animator.SetTrigger(param_attack); // hej, jag h�gg, kan du visa det?
            }
            if (Input.GetMouseButtonDown(1)) // Horizontellt hugg ( eller tv�rt om, f�r se)
            {
                animator.SetTrigger(param_specialattack); // hej, jag h�gg jag med, kan du visa det ocks�?
            }
        }

        // H�r under �r ursprungskoden fr�n Brackeys,
        // f�rst�r den inte fullst�ndigt s� avst�r fr�n att kommentera detaljerat men du styr med musen samt WASD.
        // Byggde ihop denna med ovanst�ende kod fr�n 2 andra skript
        // Hade mycket problem med animering samt osynliga namespaces pga. Visual Studio 2022.
        /*'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''*/
        void Update() // Varje bildruta, g�r dessa grejer:
        {
            CheckForAttacks();
            Run();
            float horizontal = Input.GetAxisRaw("Horizontal"); // X
            float vertical = Input.GetAxisRaw("Vertical"); // Z
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Fram�t �r Fram�t! w+look direction = hit ska jag

            if (direction.magnitude >= 0.1f) // R�kna ut antal grader kvar tills du g�r rakt igen
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                    ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            } 
        }
    }
    
//}

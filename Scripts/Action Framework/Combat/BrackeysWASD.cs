using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI;

//namespace Brackeys // Denna kod är i grunden Brackeys från YouTube, med massa tillägg utav mig
//{
    [
    //RequireComponent(typeof(NavMeshAgent)), // låt grönt stå!
    //RequireComponent(typeof(Rigidbody)), // vissa behövs när jag testar funktioner i editor
    RequireComponent(typeof(Animator))
    ] // FELHANTERING: Dessa rader ovan ser till att det objekt du klistrar detta skript på...
    // ... får alla komponenter som krävs från start ifall de glömts läggas till i editorn.
    public class BrackeysWASD : MonoBehaviour
    {
        public Animator animator; // ny
        private NavMeshAgent navMeshAgent; // objekt som pratar med kartan, "kan jag gå här, ja/nej"
        public CharacterController controller; // styrningen av själva gubben med WASD + collider
        //private Rigidbody rigidbody;
        public Transform cam; // Tredjepersons-kameran följer spelaren via denna XYZ.
        public float speed = 6f; // Den faktiska hastigheten på spelaren avgörs här, även animator-trigger

        // Animator-relaterad triggerinformation
        [SerializeField] private string param_speed = "Speed"; // ny
        [SerializeField] private string param_attack = "Attack"; // ny
        [SerializeField] private string param_specialattack = "SpecialAttack"; // ny

        public float turnSmoothTime = 0.1f; // ryck mindre, sväng snyggare
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
            animator.SetFloat(param_speed, speed); // stå still och visa att du står still!
        }
        private void CheckForAttacks()
        {
            if (Input.GetMouseButtonDown(0)) // Vertikalt hugg
            {
                animator.SetTrigger(param_attack); // hej, jag högg, kan du visa det?
            }
            if (Input.GetMouseButtonDown(1)) // Horizontellt hugg ( eller tvärt om, får se)
            {
                animator.SetTrigger(param_specialattack); // hej, jag högg jag med, kan du visa det också?
            }
        }

        // Här under är ursprungskoden från Brackeys,
        // förstår den inte fullständigt så avstår från att kommentera detaljerat men du styr med musen samt WASD.
        // Byggde ihop denna med ovanstående kod från 2 andra skript
        // Hade mycket problem med animering samt osynliga namespaces pga. Visual Studio 2022.
        /*'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''*/
        void Update() // Varje bildruta, gör dessa grejer:
        {
            CheckForAttacks();
            Run();
            float horizontal = Input.GetAxisRaw("Horizontal"); // X
            float vertical = Input.GetAxisRaw("Vertical"); // Z
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Framåt är Framåt! w+look direction = hit ska jag

            if (direction.magnitude >= 0.1f) // Räkna ut antal grader kvar tills du går rakt igen
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

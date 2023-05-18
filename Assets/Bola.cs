using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Bola : Agent
{

    public Transform[] spawnsBall;
    public float fuerzaSalto = 20f;
    public float fuerzaAvanceAereo = 50f;
    private bool isJumping = false;
    private int jumpAction;
    Rigidbody cuerpazo;
    void Start()
    {
        cuerpazo = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log(isJumping);
    }

    public Transform Target; 
    public override void OnEpisodeBegin()
    {
        Target.position = new Vector3(-18.7999992f, 4f, 32.0999985f);
        isJumping = false;
        if (this.transform.localPosition.y < 0)
        {
            this.cuerpazo.angularVelocity = Vector3.zero;
            this.cuerpazo.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(-6.4000001f, 3.99000001f, 31.5900002f);
        }

    }
    // programamos los sensores
    public override void CollectObservations(VectorSensor sensor)//ayuda a programar los sensores
    {
        //El agente sa consciente de la posición del objetivo
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        //Velocidad del agente
        sensor.AddObservation(cuerpazo.velocity.x);
        sensor.AddObservation(cuerpazo.velocity.z);
        sensor.AddObservation(isJumping ? 1 : 0);
    }

    //programamos los actuadores
    public float fuerza = 10;

    public override void OnActionReceived(ActionBuffers actions)
    {
        //acciones en 2 vectores
        Vector3 controlS = Vector3.zero;
        controlS.x = actions.ContinuousActions[0];
        controlS.z = actions.ContinuousActions[1];
        controlS.y = actions.ContinuousActions[2];
        cuerpazo.AddForce(controlS * fuerza);

        //politicas
        float TargetDistance = Vector3.Distance(this.transform.localPosition, Target.localPosition); //que el agente calcule su distancia con la del objetivo
        float GroundDistance = Vector3.Distance(this.transform.localPosition, Target.localPosition); //que el agente calcule su distancia con la del objetivo
        //recompensas
        
        
        if (GroundDistance <= 15f)
        {
            isJumping = true;
            SetReward(1.0f);
            
        }


        //penalización
        //penalizacion por si se parte la madre
        else if (this.transform.localPosition.y < 0)
        {
            SetReward(-2.0f);
            EndEpisode();
        }

        jumpAction = actions.DiscreteActions[0];

        if (jumpAction == 1 && !isJumping)
        {
            isJumping = true;
            cuerpazo.AddForce(Vector3.up * fuerzaSalto);
            
        }
    }
    
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Fance"))
        {
            SetReward(-1f);
            
        }
        
        if (col.gameObject.CompareTag("Ground2"))
        {
            jumpAction = 1;
            SetReward(1f);
           
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reward"))
        {
            SetReward(4f);
            Target.position = new Vector3(-30.7700005f, 7.61999989f, 32.0999985f);
            

        }
    }
    //ayuda me exploto la cabeza
    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }
}
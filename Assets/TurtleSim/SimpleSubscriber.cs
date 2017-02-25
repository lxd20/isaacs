﻿using ROSBridgeLib;
//using ROSBridgeLib.std_msgs;
using ROSBridgeLib.turtlesim;
using System.Collections;
using SimpleJSON;
using UnityEngine;

/**
 * This is a toy example of the Unity-ROS interface talking to the TurtleSim 
 * tutorial (circa Groovy). Note that due to some changes since then this will have
 * to be slightly re-written, but as its a test ....
 * 
 * This defines the callback that links the pose message. It moves the Dalek with
 * the turtlesim
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 **/

public class SimpleSubscriber : ROSBridgeSubscriber {
	
	public new static string GetMessageTopic() {
		return "/turtle1/pose";
	}  
	
	public new static string GetMessageType() {
		return "turtlesim/Pose";
	}
	
	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new PoseMsg(msg);
	}
	
	public new static void CallBack(ROSBridgeMsg msg) {
		GameObject robot = GameObject.Find ("Dalek");
        GameObject debug = GameObject.Find("Debug");

        if (robot == null)
			Debug.Log ("Can't find the robot???");
            
		else {
			PoseMsg pose = (PoseMsg) msg;
			robot.transform.position = new Vector3((pose.GetX () - 6f) /30f, (pose.GetY() - 6f)/ 30f, 0.9f);
            debug.GetComponent<TextMesh>().text = robot.transform.position.x + " " + robot.transform.position.y + " " + robot.transform.position.z; 

            robot.transform.rotation = Quaternion.AngleAxis (-pose.GetTheta() * 180.0f / 3.1415f, Vector3.forward);
		}
	}
}
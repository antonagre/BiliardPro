using System;
using System.Collections; 
using System.Collections.Generic; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;

// TCP code: https://gist.github.com/danielbierwirth/0636650b005834204cb19ef5ae6ccedb
// JSON Unity: https://github.com/tawnkramer/sdsandbox/tree/master &
// https://assetstore.unity.com/packages/tools/input-management/json-object-710 &
// https://github.com/mtschoen/JSONObject

public class NetworkSocket : MonoBehaviour {  	
	#region private members 	
	/// <summary> 	
	/// TCPListener to listen for incomming TCP connection 	
	/// requests. 	
	/// </summary> 	
	private TcpListener tcpListener; 
	/// <summary> 
	/// Background thread for TcpServer workload. 	
	/// </summary> 	
	private Thread tcpListenerThread;  	
	/// <summary> 	
	/// Create handle to connected tcp client. 	
	/// </summary> 	
	private TcpClient connectedTcpClient; 	
	#endregion

	private bool stop = false;
	private Dictionary<string, string> msg;
		
	// Use this for initialization
	void Start () { 		
		// Start TcpServer background thread 		
		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests)); 		
		tcpListenerThread.IsBackground = true; 		
		tcpListenerThread.Start(); 
			
		///inirializzation message
		msg=new Dictionary<string, string>();
		msg["unity"] = String.Format("msg-- UNITY TEST");
	}  	


	/// <summary> 	
	/// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
	/// </summary> 	
	private void ListenForIncommingRequests () { 		
		try { 			
			// Create listener on localhost port 8052. 			
			tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080); 			
			tcpListener.Start();              
			Debug.Log("Server is listening");              
			Byte[] bytes = new Byte[1024];  			
			while (true) { 				
				using (connectedTcpClient = tcpListener.AcceptTcpClient()) { 					
					// Get a stream object for reading 					
					using (NetworkStream stream = connectedTcpClient.GetStream()) { 						
						int length; 						
						// Read incomming stream into byte arrary. 						
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 							
							var incommingData = new byte[length]; 							
							Array.Copy(bytes, 0, incommingData, 0, length);  							
							// Convert byte array to JSON message. 							
							String clientMessage = Encoding.UTF8.GetString(incommingData);
							// Added: convert string to JSON
							JSONObject clientMessage_json = new JSONObject(clientMessage);
							Debug.Log("client message received as: " + clientMessage_json);
							SendMessage(msg);
						} 					
					} 				
				} 			
			} 		
		} 		
		catch (SocketException socketException) { 			
			Debug.Log("SocketException " + socketException.ToString()); 		
		}     
	}


	/// <summary> 	
	/// Send message to client using socket connection. 	
	/// </summary> 	
	private void SendMessage(Dictionary<string, string> serverMessage) { 		
		if (connectedTcpClient == null) {             
			return;         
		}  		
		
		try { 			
			// Get a stream object for writing. 			
			NetworkStream stream = connectedTcpClient.GetStream(); 			
			if (stream.CanWrite) {
				// Added: create dict to be a JSON object
				JSONObject serverMessage_json = new JSONObject(serverMessage);
				String serverMessage_string = serverMessage_json.ToString();
              
				byte[] serverMessageAsByteArray = Encoding.UTF8.GetBytes(serverMessage_string);  // serverMessage				
				// Write byte array to socketConnection stream.               
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);               
				Debug.Log("Server sent his message - should be received by client");
				stop = true;
			}       
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		} 	
	} 
}
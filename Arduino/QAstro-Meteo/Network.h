	//Network Details
	#define SECRET_SSID "<Your SSID>"
	#define SECRET_PASS "<Your wifi Password>"

	#define STATICIP 1    //Set to 0 if you use DHCP

	#ifdef STATICIP
		IPAddress myIP(192, 168, 1, 200);	//When using a Static IP address, make sure you use commas and not dots for the next 4 rows.
		IPAddress myGW(192, 168, 1, 254);
		IPAddress mySN(255, 255, 255, 0);
		IPAddress myDNS(192, 168, 1, 254);
	#endif
	
	#define ALLOWED_CONNECTION_TIME 180000 //60000 = 1 minute. 
	#define TELNET_PORT 23

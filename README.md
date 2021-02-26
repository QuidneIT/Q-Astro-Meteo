# Q-Astro-Meteo
Q-Astro Meteo Project Arduino &amp; ASCOM

Please see here for details: http://www.q-astro.com/?#/ascom-weather-station/

#Uploading the Arduino code
If you use serial you do not need to update any Arduino file (Arduino Nano).
But if you are planning to use an Arduino Nano 33 IoT and use it over wifi you will need to update Network.h and fill in the specifics for you wifi environment

After you connect via serial or telnet, you can issue to following command.

i# <enter> - returns Q-Astro Meteo ver. x.y.z.

mm# <enter> - returns a string with all the relevant data the unit collects, separated by _

The data returned:
o = observatory temp, 
a = altitude, 
d = dew point, 
h = humidity %, 
p = pressure, 
s = sky state, 
r = Raining or not D = dry, M = modest, H = heavy, 
v = raining rate 0 = dry, 60 = modest, etc, 
c = cloudcover %, 
t = sky temp, 
q = sky quality, 
b = lux (Sky brightness)

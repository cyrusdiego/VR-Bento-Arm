# Virtual Prosthesis Systems Simulator
<p>
    <img align="left" width="225" height="120" src="https://github.com/Gnarlywhale/VR-Bento-Arm/blob/master/img/logo.png"></img>
    <img align="right" width="225" height="80" src="https://github.com/Gnarlywhale/VR-Bento-Arm/blob/master/img/blinclogo.png">
</p>

## Description
Implementation of a simulated [Bento Arm](https://blincdev.ca/the-bento-arm/overview/) to carry 
out tasks in a virtual reality environment. 

## Table of Contents 
[Installation](#installation)<br/>
[Usage](#usage)<br/>
[Credits](#credits)<br/>
[License](#licesne)<br/>
## Installation
**Required Software:**
- Steam
- SteamVR
- UnityHub
- Visual Studio (for development) 
## Usage 
### Startup
1) Launch the VIPER Project in Unity 
2) Launch brachIOplexus through Visual Studio 
    - Launch the application by clicking "Start" 
3) Connect 1 of the 3 input devices 
    - Open the applicable profile for the input device chosen 
4) Connect to Unity
    - Do not change the IP address or the ports
    - Select all degrees of freedom 
5) In Unity, go to the VIPER_INIT scene 
6) Press "play" 
7) In brachIOplexus, go to the Unity tab 
    - The task selection menu should be enabled (if it is still greyed out, wait a few seconds for the connection to be established) 
8) Choose a task with the specified parameters and press launch 

### Shutdown
1) Click the "play" button in Unity 
2) Press the "Disconnect" button in brachIOplexus in the Input/Output tab 
3) Close brachIOplexus and Unity 

### Switching Tasks 
1) Press the "End Task" button in the Unity tab 
2) Unity should switch back to the VIPER_INIT scene 
3) Choose your new task with the specified parameters 

For additional guides to use the project refer to the wiki. 

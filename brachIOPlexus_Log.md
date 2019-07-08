# Changes made to BrachIOplexus 
# June 27, 2019 
## Output Devices
- Created new section for the Unity Bento Arm Setup 
- All components of the section start with keyword "unity" in mainForm.Designer.cs 

## Connect / Disconnect 
- Currently functions like a toggle (one on other off) 
    - There is no "check" to ensure that the Unity project is running / no functionality 
    to *launch* the Unity project as it has not been built yet 

## IP Address / TX and RX port 
- Editable text fields but default is set, BrachIOplexus will parse through these fields 
to create the port and IP address 

## Check list 
- Enables / Disables DOF in Unity Bento Arm 
- No set profile created so upon launch of BrachIOplexus these are left disabled 
- Will need to create some sort of default profile so that these are enabled upon launch
    - also doesn't function **exactly** like the check list for the actual Bento Arm 
- Select All / Clear All buttons are functioning 

### Hand Open / Close
- The actual Bento Arm has a constraint where they both have to be on / both have to be off
- This constraint has **not** been implemented

# June 28,2019

- Created new unity Tab
- Holds reset and arm shell toggle

- Also displays name of active scene 

- **Note** brachioplexus MUST connect first then Unity can be turned on 

# July 2, 2019
- Create section for camera control 
- created a list<string> to hold the names of the camera presets
- pop up window to edit names and delete presets
**Features**
- Save camera position: pop up window to name the camera position and sends signal to unity
- Switch to Next position: loops thru list to go to next one and sends signal to do the same in unity
- Clear Presets: clears list and sends signal to clear in unity
- Edit Presets: pop up window to delete specific preset or rename 
- Display Current posiiton
*Question:* does brachIOplexus clear everything once its shutdown (after building the software)? 
    - this affects the saved camera presets and how they will be displayed

# July 4, 2019
- Created folder in `C:\Users\Trillian\Documents\VR-Bento-Arm\brachIOplexus\Example1\resources\unityCameraPositions` to store camera positions as json files 

# July 5, 2019 
- Implemented: save / load camera profiles 
- Removed: delete / rename specific camera positions 

# July 8, 2019 
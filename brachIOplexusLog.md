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
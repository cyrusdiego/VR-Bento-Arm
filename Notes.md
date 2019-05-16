# Project Notes 

## Sites Used:

## General Progress
**May 7**
- imported bento arm into Unity 
- Created simple rotation script
    - For limb? (idk the terminology lol) wrap in empty game object and rotate that game object 

**May 8**
- Fixed constraint bug for shoulder 
    - i think this should still be improved (not the best solution to the constraint)
- Got basic rotation for the elbow 
- Got Wrist rotation
*Rotations*
    - Toggle Pivot 
    - Create/move empty game object to pivot location
    - nest object to rotate about that pivot 
    - apply rotaton to empty game object 

*Camera Movement*
- W/D == forward/backward
- A/D == left/right
- Up/Down Arrows == up/down

*Camera Rotation*
- Use Left/Right Arrows
HOLD:
    - X == rotate about x-axis
    - Y == rotate about y-axis
    - Z == rotate about z-axis

- Next step/goal: increase accuracy for the rotational axes of the arm 
- learned: found a script to set pivot easily but it still manual maybe i can alter it to specify where?? but also, can go to solidworks or blender to set pivot point there and re import into unity and that would work. 
    - also learned that you can create menu items in the editor... may be useful when setting up with the emg stuff later on to interface with another software 
    - quick google search yielded this: [link](https://answers.unity.com/questions/973452/does-unity-allow-interfacing-with-external-program.html)

3D modelling notes:
- for the shoulder rotation, is the circular plate connected to the stand supposed to rotate too??? 
- may be better if the 3D objects are split into smaller models?? so there's more rotating pieces 

**May 10**
- i have proper pivot points now
- now moving into more realistic physics by changing everything to rigidbody instead of using transform 
- i need to know actual mass of objects and the amount of torque required for the parts of the arm to rotate 
    - also need to put in restrictions for movement 

- need to figure out/ ask how they want movement to be based on:
    - do i have to make a script that models the servo motor as close as possible?
        - i think so lol 
    - so we move the arm in one rotation (using slider for now) then the script (motor) will apply apporpriate torque to maximum velocity 

FOR NEXT DAY:
    - rank / organize requirements into doc
    - read hackernews lol
    - get constraints in oml
    - have arm move proerly using motor
    - mention PID controller modelling vs joint motor in unity 

**May 13**
TODO for today:
    - Comment code (DONE)
    - watch about hinge and motor (DONE)
    - organize requirements for project (DONE)
    - Start constraints and read Rory's email regarding constrains and the motor functionality (WORK in PROGRESS)
    - be able to hide extras and add the colours for table (DONE)
    
- PID C# scripts in downloads folder in case [link](http://www.technologicalutopia.com/servo.htm)

- Figured out how to use motor / joints to create motor like controlls 
    - for joints, will need to use configurable joints to have the joints retain the displacement even when switdching between modes 

- got elbow and shoulder motor scripts but i think i need to have specific paramaters like rotational axis save in a text file. 

TODO for next couple days?? / plan: 
    - finish basic rotation of the arm using the motor / joint scripting 
    - create a json file for the information 
    - look into interfacing with brachiOplexus 
    - i think i may be getting confused / not prioritziing correct thing so this will come up in meeting 

- i think in the future i need to evaluate the memory allocation i do, i use a quite a bit of "new" : **C# and unity has automatic garbage collection** 

**May 14**
TODO for today:
    - Continue with the motor functionality and constraints, i think this should be done by today (ANGULAR Constraints are done, basic level but rigid body collision will take a bit longer)
    - read into software interfacing, sockets, and network programming 
        - maybe try making your own C# classes to learn 

QUESTIONS: 
    - for the collisions with itself, are trigger events sufficient to just stop the torque applied? 

- currently the arm can interact with objects BUT it doesnt interact well with itself, it needs angular restriction 

**May 15**
EVENTS today:
    - 3D printing training 
    - meeting with quinn, riley, and ahmed 
TODO: 
    - Read about networking, Sockets, and C# programming 
        - I'll need to know how brachiOplexus was made
        - is is UWP of WPF? 
FOR TMRW:
    - figured out a way to use joints and mesh colliders BUT unity only allows for convex mesh colliders not concave so i need a work around 


**May 16**
- added some box colliders to the main part of the arm 
    - I need to create the scripts for each section of the arm to stop when they collide with one another 

- FOR BOX COLLIDERS TO WORK: have parent or itself have rigid body, turn on isTrigger and use the trigget event method 

TODO for TMRW:
    - Finish scripting the box colliders (DONE TODAY)
    - Thoroughly test the edge cases for the collisions 
    - edit the sizes or add any more box colliders or remove if necessary 
    - Create box colliders for the stand and desk and the arm shells
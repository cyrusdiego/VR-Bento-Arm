# Project Notes 

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
    - Thoroughly test the edge cases for the collisions (NOT DONE, Look below)
    - edit the sizes or add any more box colliders or remove if necessary (I think i just need to add more the for the stand, but the arm should be good? )
    - Create box colliders for the desk and the arm shells (I NEED THE ANGULAR SECTION FOR THE STAND)
    - have more realistic angular restrictions like the motors 

EDGE CASES:
    - rotating one part of arm to collide works BUT, for example rotate the rist to hit the forearm, you can no longer rotate the shoulder properly
        - one collision will restrict all forms of rotation 
        - so need to have an if statement or something to check what mode should be restricted 

**May 17**
TODO:
    - Test and fix edge cases (I think it is done, double check)
    - Create box collider for the table and desktop (DONE)
    - fix angular restrictions for motors (did not get to do)
    - Comment all files with consistent style (didnt do all the files but i did it for RotationScript)

RESULTS:
    - keyboard is now working with the H,J,K,L buttons (this will make it easier to do button mapping for the VR controllers)
    - started doing acer headset stuff:
        - theres a bit of delay so cannot use start() for init or use a delay?
    - vr controller input is good just need to map what each button needs to do

TODO for NEXT DAY:
    - create script that sets up input manager (not really needed rn with the acer)

**May 21**
TODO:
    - VR controller mapping and accurate movement / rotation of arm (DONE, i think, keep testing i suppose but i think its good)
    - camera movement using controller 
    - Arm shell bounding boxes (to both scenes) (DONE for vr controllers, i dont think i need it for keyboard)
RESULTS:
    - vr controllers mapped to rotate arm joints and have good box collider approx's 
    - basic camera movement in vr is done but prolly want something else maybe??

QUESTIONS FOR MEETING:
    - I have the basic VR controller buttons mapped and the arm will rotate appropriatley. Also I have basic camera movement (kinda bad though)
    - is this what we want for right now? or would it be better to have something like this? [link](https://www.youtube.com/watch?v=atoUeEPLVqQ) ?? 
        - that is nothing like the current implentation 
        - for the VR project we arent trying to do IK right? its more mechanical control of each motor using various inputs?
    - if we are correct, in terms of the "next step" would it be using the VR controller rotation / location to control the arm? similar to use the MYO arm band, 
    it can switch between modes and rotations / flexions of the controller will move the arm? 

**May 22** 
TODO:
    - VR controller rotation / movement to control arm? 
    - better angular rotation control 
    - Maybe better camera movement? or teleport?? ask in meeting 
RESULTS:
    - I ended up just reading about UDP / TCP/IP programming to familiarize 
    - Meeting with Riley/Quinn resulted in a few outcomes to work on in the next week:
        - fix collision detection algorithm 
        - placement of camera 
        - cup / sphere in scene 
MEETING with RORY:
    - Learned about BrachiOplexus, Development Logs, and discussed uses for the project 
    - OUTCOME: need to be better with my Dev Log for this project (citing websites i used for resources in this document)

**May 23**
TODO:
    - Collision Detection Algorithm 
    - increase accuracy / realism of the rotation of arm: rotating controller will rotate arm at same rate (apply torque)  
- currently, sometimes the box colliders do not work. I do not thing it is my algorithm but the physics engine not being able to "catch up" with the movements of the arm
    - looking into raycasting to detect it, might work better than box colliders?
- REMEMBER: box colliders need a parent (or self) rigid body to work (non kinematic) and use continuous dynamic 
RESOURCES:
    - retrieve index of element in array[link](https://docs.microsoft.com/en-us/dotnet/api/system.array.indexof?view=netframework-4.8)
    - clear console through code[link](https://answers.unity.com/questions/578393/clear-console-through-code-in-development-build.html)
    - `out` keyword in C#[link](https://answers.unity.com/questions/257054/what-is-the-use-of-out-in-variable-fields-example.html)
    - collisions [link](https://gamedev.stackexchange.com/questions/151670/unity-how-to-detect-collision-occuring-on-child-object-from-a-parent-script)
    - raycasting for collision detection (possible solution but im not sure )[link](http://wiki.unity3d.com/index.php?title=DontGoThroughThings)

**May 24**
TODO:
    - Collision Detection Algorithm 

Physics Engine Adjustments:
    - changes Time -> Fixed Time Step, originally 0.02 changed to 0.001, 0.0002 made the game too slow. 
    - enabled adaptive force 
- Note that Continuous Dynamic collision detection has a high toll and usually can be solved with raycasting 
    - Changed all the rigid bodies to discrete 

- Above physics engine adjustments didnt do anything
- 2nd test: add rigidbody (is kinematic true) and use trigger
    - have parent with rigidbody: is Kinematic, discrete then have children with trigger 
    - trigger is having a "collision" detected but no actual physical impact, only a notification
    - set fixed time step back to 0.02
- 3rd test: find angle they collide and set that as the new angular restriction
    - the angles are sending over properly BUT they are not setting correctly

RESOURCES:
    - physics engine limitations / adjustments[link](https://gamedev.stackexchange.com/questions/99180/unity-rigidbody-gets-pushed-through-collider-by-another-rigidbody)
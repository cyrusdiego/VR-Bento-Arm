# Project Notes 
**V**irtual **P**rosthesis Syst**E**ms Simulato**R** (VIPER) 
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

*FOR NEXT DAY*:
- rank / organize requirements into doc
- read hackernews lol
- get constraints in oml
- have arm move proerly using motor
- mention PID controller modelling vs joint motor in unity 

**May 13**
*TODO* for today:
- Comment code (DONE)
- watch about hinge and motor (DONE)
- organize requirements for project (DONE)
- Start constraints and read Rory's email regarding constrains and the motor functionality (WORK in PROGRESS)
- be able to hide extras and add the colours for table (DONE)

- PID C# scripts in downloads folder in case [link](http://www.technologicalutopia.com/servo.htm)

- Figured out how to use motor / joints to create motor like controlls 
- for joints, will need to use configurable joints to have the joints retain the displacement even when switdching between modes 

- got elbow and shoulder motor scripts but i think i need to have specific paramaters like rotational axis save in a text file. 

*TODO* for next couple days?? / plan: 
- finish basic rotation of the arm using the motor / joint scripting 
- create a json file for the information 
- look into interfacing with brachiOplexus 
- i think i may be getting confused / not prioritziing correct thing so this will come up in meeting 

- i think in the future i need to evaluate the memory allocation i do, i use a quite a bit of "new" : **C# and unity has automatic garbage collection** 

**May 14**
*TODO* for today:
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
*TODO*: 
- Read about networking, Sockets, and C# programming 
    - I'll need to know how brachiOplexus was made
    - is is UWP of WPF? 
FOR TMRW:
- figured out a way to use joints and mesh colliders BUT unity only allows for convex mesh colliders not concave so i need a work around 


**May 16**
- added some box colliders to the main part of the arm 
- I need to create the scripts for each section of the arm to stop when they collide with one another 

- FOR BOX COLLIDERS TO WORK: have parent or itself have rigid body, turn on isTrigger and use the trigget event method 

*TODO* for TMRW:
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
*TODO*:
- Test and fix edge cases (I think it is done, double check)
- Create box collider for the table and desktop (DONE)
- fix angular restrictions for motors (did not get to do)
- Comment all files with consistent style (didnt do all the files but i did it for RotationScript)

*RESULTS*:
- keyboard is now working with the H,J,K,L buttons (this will make it easier to do button mapping for the VR controllers)
- started doing acer headset stuff:
    - theres a bit of delay so cannot use start() for init or use a delay?
- vr controller input is good just need to map what each button needs to do

*TODO* for NEXT DAY:
- create script that sets up input manager (not really needed rn with the acer)

**May 21**
*TODO*:
- VR controller mapping and accurate movement / rotation of arm (DONE, i think, keep testing i suppose but i think its good)
- camera movement using controller 
- Arm shell bounding boxes (to both scenes) (DONE for vr controllers, i dont think i need it for keyboard)
*RESULTS*:
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
*TODO*:
- VR controller rotation / movement to control arm? 
- better angular rotation control 
- Maybe better camera movement? or teleport?? ask in meeting 
*RESULTS*:
- I ended up just reading about UDP / TCP/IP programming to familiarize 
- Meeting with Riley/Quinn resulted in a few outcomes to work on in the next week:
    - fix collision detection algorithm 
    - placement of camera 
    - cup / sphere in scene 
MEETING with RORY:
- Learned about BrachiOplexus, Development Logs, and discussed uses for the project 
- *OUTCOME*: need to be better with my Dev Log for this project (citing websites i used for resources in this document)

**May 23**
*TODO*:
- Collision Detection Algorithm 
- increase accuracy / realism of the rotation of arm: rotating controller will rotate arm at same rate (apply torque)  
- currently, sometimes the box colliders do not work. I do not thing it is my algorithm but the physics engine not being able to "catch up" with the movements of the arm
- looking into raycasting to detect it, might work better than box colliders?
- REMEMBER: box colliders need a parent (or self) rigid body to work (non kinematic) and use continuous dynamic 
*RESOURCES*:
- retrieve index of element in array[link](https://docs.microsoft.com/en-us/dotnet/api/system.array.indexof?view=netframework-4.8)
- clear console through code[link](https://answers.unity.com/questions/578393/clear-console-through-code-in-development-build.html)
- `out` keyword in C#[link](https://answers.unity.com/questions/257054/what-is-the-use-of-out-in-variable-fields-example.html)
- collisions [link](https://gamedev.stackexchange.com/questions/151670/unity-how-to-detect-collision-occuring-on-child-object-from-a-parent-script)
- raycasting for collision detection (possible solution but im not sure )[link](http://wiki.unity3d.com/index.php?title=DontGoThroughThings)

**May 24**
*TODO*:
- Collision Detection Algorithm 
- 1st test: 
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
- difficult to deal with and isnt the most ideal solution imo
4th test: switch to collisions instead of triggers and when they hit each other either apply equal forces to cancel torque or try to fix position and shut off the applied torque 

*RESOURCES*:
- physics engine limitations / adjustments[link](https://gamedev.stackexchange.com/questions/99180/unity-rigidbody-gets-pushed-through-collider-by-another-rigidbody)
- collider affects hinge joints [link](https://forum.unity.com/threads/collider-affects-hinge-joint.156502/)
- rigidbody and colliders [link](https://forum.unity.com/threads/rigidbody-collider-root-or-each-gameobject.269904/)
- parenting and collisions [link](https://gamedev.stackexchange.com/questions/151670/unity-how-to-detect-collision-occuring-on-child-object-from-a-parent-script)

**May 27** 
*TODO*: 
- Collision Detection Algorithm (DONE)

TRIALS:
- Rigidbody: Continuous OR interpolated 
- Might re-design the rotation method i have set slightly. Instead of using the joint "motor" im just gonna use the rigidbody.AddTorque(x,y,z) method 
- Placing configurable joints at the specific axes will set the rotational axis. using `rigidbody.AddTorque(float x, float y, float z)` will add a torque at that axis 
- then using `rb.AngularVelocity = Vector3.zero` will stop the rigidbody from rotating (must be in fixed update, or else the momentum of the rb will continue rotation) 
- changing the axis in configurable joint ((1,0,0) -> (0,1,0)) will not affect Vector3 for torque, if you want to rotated it about y axis, you still use the y / 2nd value not x 
- placing the box colliders as components of the arm would help with the physics engine from not being dumb (b/c i used transform.position with a rigidbody essentially which is a no no)
- placing multiple colliders in one gameobject will NOT collide with itself BUT will collide with outside gameobjects (parented AND not-parented)
- Possible solution: make each model a rigid body + configurable joint and have a parent wrapper still, this will allow for each thing to have continuous dynamic collision detection

GAMEPLAN 1: 
1) move box colliders to the bento arm model as components 
a) collision == continuous 
b) change collision code 
2) Re-format the rotations of the arm (w/o box colliders) -> change it in the script (lower priority as the current method works fine, just need to "stop" the rotation completely)
a) configurable joints are already placed in correct points in the rigidbodies
b) change the script to use above methods instead 

*RESOURCES*:
- objects going through colliders [link](https://gamedev.stackexchange.com/questions/115449/ball-passing-through-box-collider)
- Explains Rigidbodys in Unity [link](http://digitalopus.ca/site/using-rigid-bodies-in-unity-everything-that-is-not-in-the-manual/)
- Unity Resources [link](http://digitalopus.ca/site/links-to-some-interesting-game-resources-and-information/)
- 2 colliders one rigidbody [link](https://answers.unity.com/questions/64740/2-colliders-on-1-gameobject.html)
- trigger vs colliders [link](https://bladecast.pro/unity-tutorial/fix-my-collision-complete-guide-collision-trigger-detection-unity)
- UDP Services [link](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/using-udp-services)

*OUTCOME*: 
- Seems to work fine now (may 27 2:00pm "seems to work perfectly now")
- solution: rigid body at parent and setting continuous dynamic for better collision detection
- put box colliders in model gameobjects instead of seperate entity 

**May 28** 
*TODO*: 
- Testing of arm 
- Create Flowchart for VRrotations 
- Debugging further 
- Double Check mapping for controllers omg 

- Removing all Rigidbodies and configurable joints from each rotation 
- changing the array to Gameobjects instead of rigidbodies 
- Adding rigidbody and configurable joints on the spot and set kinematic and collision detection 
    - got this working but doesnt fix the problem can keep it the way it is or revert doesnt really matter 
- going to try using collision instead of trigger 

SOLUTION TO COLLISION DETECTION:
- mixture of issues
    1) my controller mapping was slightly wrong (angular detection and whichever method was wrong)
    2) collision detection needed to be more percise 
*RESOURCES*
- collision [link](https://forum.unity.com/threads/weapon-passing-through-colliders.418840/)

CURRENTLY:
- RIGHT Controller:
    - Trigger (Hold) stops rotation of the arm 
    - Tilting Left / Right will rotate the arm accordingly 
    - Trigger (Hold) + Touchpad (Press) cycles rotational mode
    - Grip (Press) will toggle between arm shells vs bare bento arm 
- when it cycles open hand to shoulder, it carries over the collision in open hand to shoulder 

**May 29**
- Fixed the weird cycling problem but in the weiredest solution
- What i think was happening, during the transition from hand to shoulder, the hand still tries to send a signal of a collision but the mode changes and it tries to stop
shoulder rotation even though the shoulder isnt actually doing it. 
- So to solve it: 2 things
    1) add tags to the bento arm so that in the shoulder rotation, if it hits itself (impossible but it does it here for some reason) it wont send a msg
    2) the jointCollision dictionary will only update if the mode isnt shoulder OR the msg isnt from open hand 
        - so this prevents messages when its is both shoulder mode AND the message is from open hand 
- PHYSICS:
- i think the scale of the world is in mm so i had to convert 9.81 to 981

- BUG:
- end effectors will go thru itself 

PROJECT NAME:
- (VIPER) VIrtual Prosthesis systEm simulatoR   

- fixed box collider for elbow rotation

- COLLISION NOTES: 
[link](https://www.youtube.com/watch?v=RxG7YYEdmVE) colliders vs triggers 
- colliders: what physics engine uses for physics interactions and event calling 
- triggers: only event calling 

`OnCollisionEnter()` is only called when both objects have colliders attached (box collider) and at least one has rigidbody (non kinematic)
- trigger doesnt do physics interaction ie) no "bumping" no moving/dragging objects ONLY send messages to scripts 

- REASON FOR RIGIDBODY CYCLING: if i have rigidbodies on each segment "on" at each time, it will not move. instead i only have a rigidbody for the segments that MOVE at that point in time I will need to change this 
eventually to enable movement at all times  `isKinematic` prevents object from moved / oushed / rotated by other objects (will still interact with other objects tho)
- two `isKinematic` objects will not push / move objects with each other 

- Colliders can be added to an object without a Rigidbody component to create floors, walls and other motionless elements of a scene. These are referred to as static colliders.
- Colliders on an object that does have a Rigidbody are known as dynamic colliders. Static colliders can interact with dynamic colliders but since they don’t have a Rigidbody, they will not move in response to collisions.
- colliders affects torque / force applied to objects [link](https://answers.unity.com/questions/250233/rigidbody-behaves-differently-when-i-add-a-collide.html)

- configurable joints [link](https://forum.unity.com/threads/configurable-joints-in-depth-documentation-tutorial-for-dummies.389152/)

**May 30** 
*TODO*
    - colliders and rotations 
- problem yesterday was that when u add box colliders (with multiple parenting) the mass distributes instead of a point. so adding a torque and such become difficult. to circumvent, the children must have 
rigidbody with isKinematic on. 
- so limititation is that the box colliders will need to be a trigger. 
- but outside interaction can still happen b/c only one needs to have a non kinematic rigidbody 
- so it seems the last week or so was a little useless b/c by having right combination of rigidbody (kinematic or not) plus position and rotation constraints, the arm will stop rotating anyways
- jokes, i still need my scripts b/c of the isKinematic setting, colliders wont actually "feel" the collisions so i need to use triggers. but outside objects w/o a position restriction will! 

- cannot make everything colliders!

- in Open Hand mode, set "Left" game object to `!isKinematic` with all constraints frozen, then the msg will be caught and use that msg to stop rotation

PROBLEMS:
- when it cycles it stops working properly (fixed)

- set gravity to 981 b/c current units are in mm
- added collided lists to each part to keep track of what is currently colliding with it 

*RESOURCES*:
- For new Unity users: Use the Edit -> Projects Settings -> Physics to identify which layers can interact with which other layer. For example you can still have a ray-cast target, without picking up player body collisions. Also great to define play areas but permit shooting. [link](https://forum.unity.com/threads/item-pick-up-colliders-question.520291/)
- collider infor[link](https://gamedev.stackexchange.com/questions/99180/unity-rigidbody-gets-pushed-through-collider-by-another-rigidbody?rq=1)
- grippers [link](https://answers.unity.com/questions/214900/grip-a-rigidbody-up-with-friction-is-this-possible.html)
- grippers pt 2 [link](https://forum.unity.com/threads/need-advice-suggestions-how-best-to-grab-a-rigidbody.41038/)

- having trouble picking up objects currently, objects tend to go thru the left chopstick for some reasone 

**May 31**
*TODO*:
- Grippers, grab an object 
- Debug for no shells! 
- Seperate VRRotations script to the individual scripts to allow for multiple movement of joints at the same time 
- got basic grabbing, need to clean up. 
- rotations are very buggy at times, soemtimes they lock up. i think i need to split up the rotation script now or monday !!

- got object interaction but will require further testing maybe like 80% confident rn
- DO NOT HAVE WMR APP RUNNING WHILE RUNNING UNITY!!!

**June 3**
*TODO*:
- debug collisions
- add another object interaction
- ensure object interaction works
- have the "hide" shells thing restart the scene 

*RESOURCES*:
- enum from other script / object [link](https://answers.unity.com/questions/991759/how-do-i-call-an-enum-from-another-script.html)
- plastic cup [link](https://free3d.com/3d-model/plastic-cup-high-poly-version-79161.html)
- removing gameobject as child of another [link](https://answers.unity.com/questions/787390/de-attach-child-from-parent-and-remove-from-animat.html)

- one weird interaction is if u push down on the interactables with the end effectors, it doesnt break but 
the arm will keep pushing until it basically goes thru the table. i think i need to hard code where
if pushing down and hitting an interactable: stop
- fixed it by adding the following in GripperTrigger.cs 
```C#
    if(rotations.GetComponent<VRrotations>().mode == "Elbow")
    {
        return;
    }
```
- limitation with the current grabber method is that the object MUST be a kinematic object so it WILL 
go thru the table unles i add it to the script as well to stop rotations 

*TODO* FOR TMRW:
- switch moddle toggle to grip 
- fix open hand collisions (goes thru itslef and the table)
- camera position 

**June 4**
- switchting to the grip seems to do the same thing where it will miss the button press every now and 
then. will probably need to figure out a better place to put the function call? 
- got the collisions wokring better with the end effectors. going to continue with testing it 
to ensure that even when one limb is at the edge case, the other rotations still work fine. 
- need to continue testing the object interaction as well tho 
- havent yet tried the camera position. 

- camera position is being fixed now: scaled mixed reality playspace by 1000

*RESOURCES*:
- wmr dev guide [link](https://blogs.msdn.microsoft.com/uk_faculty_connection/2017/10/09/mixed-reality-immersive-a-beginners-guide-to-building-for-mr/)

- fixed the unresponsive trigger, moved it to Update instead 

FUTURE IMPROVEMENTS:
- only have the grabber trigger active when the hand is open to a certain degree! 

*INSTRUCTIONS FOR DEMO*
- Ensure objects are within the demo area 
- place headset on table and the RIGHT controller 
- double check in WMR that the room boundaries are set if not:
    - set room boundaries using the tape on the floor 
    - Test demo to ensure objects are in correct positions if not, move objects in Unity 
- Press "play" -> subject should be in seat with headset on. 

*Progress: 1st Inchstone tomorrow*
- A demo-able version is complete with a simple task created 
    - primitive version of end-effector Unity grippers are working with select cases
    - further imporvements need to be made to be used with other objects and interact at a more realistic manner 
    - Arm rotates as it should using thumbstick and trigger to move / change modes 
    - box colliders are in place and are detecting collision at 90% of time, more testing at weird edge 
    cases need to be made 
    - VR room is set up (primitively) but needs improvements:
        1) cover reflective floor with a carpet OR a dull covering 
        2) Glass cabinet casings need to be removed 

**June 5**
- `FixedUpdate()` vs `Update()` 
    - `Update()` runs once per frame 
    - `FixedUpdate()` can run once, zero, or several times per frame depending on 
    how many physics frames per second are set in time settings 
    - `FixedUpdate()` should be used when applying forces , torques or other physics related 
    function so that they will sync with the physics engine 

**June 6**
*TODO*:
    - research on configurable joints and rigid bodies to try and find a better way 
    for rotations and collision detection 

*RESEARCH*
    - the drives for joints are propertional derivative drives [link](https://docs.nvidia.com/gameworks/content/gameworkslibrary/physx/guide/Manual/Joints.html)
    - Axis / Secondary Axis: maybe we can change this every frame to maintain local axes per 
    motor ?? 
    - config joint help [link](https://answers.unity.com/questions/1276071/configurablejoint-target-positionrotation-issues-1.html)
    - position spring and damper [link](https://answers.unity.com/questions/29625/configurable-joint-what-does-position-spring-and-d.html)
    - position sping and damper [link](https://forum.unity.com/threads/cant-set-a-joints-drive-mode-positional-velocity-in-5-5.452666/)

**June 7**
- Worked with Quinn and Riley to figure out the physics engine 
- Got configurable joints working and a full (?) understanding of their drive system
- Each Rigidbody has their mass defined and their degree of motion 
- The Wrist and Elbow do have a bug where it has weird behaviour passed 90 degrees 

**June 10** 
*TODO* 
    - Fix elbow and wrist bug (DONE)
    - Improve Arm movement 
        - basically figuring out spring value so that the arm segments will move realisticaly if say the chopsticks were rubbing against the table would the shoulder move with it?? 
    
- Naming convention of files / gameobjects are with the equivalent arm part 
*RESOURCES*
    - explaining quaternions [link](https://answers.unity.com/questions/645903/please-explain-quaternions.html)
    - how to get inspector values [link](https://answers.unity.com/questions/1589025/how-to-get-inspector-rotation-values.html)
    - rigidbody help [link](https://www.3dgep.com/physics-in-unity-3-5/)
    - to get forces applied on rigidbody use onCollisionstay and `collision.impulse` 
    - friction [link](https://forum.unity.com/threads/solved-friction-force-and-acceleration.505260/)
- 10:49am -> this is what the rotations look like [link](https://answers.unity.com/questions/1299082/transformeulerangles-x-issues.html)
but flipped about x axis, so, bottom half is 270 - 360 

**June 11**
*TODO*
    - Friction 
        - Increasing mass of the grippers (BOTH Chopsticks wrapper AND the Hand wrapper) to 1 provided enough force to lift the box with gravity ON, 
        BUT it is very buggy, the grippers seem to go through the box and slight movement will let the box go
    - Arm movement
        - Arm still moves very rubbery? very hard to control for fine movement 

*PROBLEMS + RESOURCES*
    - objects "sink" though the table UNLESS I shut off gravity for the table [link](https://answers.unity.com/questions/453248/rigidbodies-sinking-through-surfaces.html)
    - phyics behind the end effectors [link](https://en.wikipedia.org/wiki/Robot_end_effector)
    - PHYSX for surgery sim, theres a gripper tool [link](https://www.ncbi.nlm.nih.gov/pmc/articles/PMC2810833/)

*OPTIONS TO FRICTION*
        - increase mass of end effectors (and/or) decrease mass of object
        - attatch a script to end effectors such that on contact (and while in contact) w/ 
        appropriate object tag, they will grab the object information (mass) and use
        `rigidbody.addForce(Vector3)` to the normal of the grippers in the direction 
        of the contact points 
            - cons: this can potentially be buggy still and again does not rely on Unity's physics built in physics engine to handle the work
        - create a very un-noticeable box collider in the gripper that acts like a tooth to pick up objects 
- changed bounce threshold was 2 originally
- Messaged someone on unity forums with similar problem, should check inbox every now and then

- the grippers need to limit the amount of force / torque is applied to objects:
    - basically need to stop rotating when it has an object in hand 
    - this will prevent it from going through the primitive and from the object from tunnelling under [link](https://forum.unity.com/threads/collision-problems-with-robotic-jaw.103010/)

*RESOURCES*
    - damp value for configurable joint cannot be 0 [link](https://issuetracker.unity3d.com/issues/configurablejoints-target-velocity-does-not-move-object-unless-position-damper-greater-than-0)
    - config joint, target velocity [link](https://forum.unity.com/threads/configurable-joint-target-velocity.309359/)
    - PID [link](https://answers.unity.com/questions/236144/rotate-using-physics.html)

- Tried creating a wrapper class to do the rotations for but it didnt work as well??? for some reason i think the rotating one segment affected the others 
- gravity for sure works ==> tested with dropping a ball 
- torque i am not sure if it is correct with the current model of the joint, might want to make PID model 

- torque i am not sure if it is correct with the current model of the joint, might want to make PID model for more control?

**June 12**
*RESOURCES* 
    - rigidbody and collisions [link](https://www.reddit.com/r/Unity3D/comments/1ee65w/having_wonky_collisions_rigidbodies_being_weird/)

- table doesnt need a rigidbody because its not moving !
- pressing touchpad on right controller will toggle arm shells (resetting scene) 
- looked into deformable bodies a little, it is quite difficult to implement as id need to learn about deforming meshes and its colliders 

- will be moving on to a new physics engine learning how it works now:
    - DOTS based project (PhysX vs DOTS)[link](https://forum.unity.com/threads/unity-physics-discussion.646486/#post-4336105)
    - do I have to use ECS to use new physics system? [link](https://forum.unity.com/threads/do-i-have-to-use-ecs-to-use-the-new-physics-system.647035/)
    - leveraging DOTS powered physics [link](https://www.youtube.com/watch?v=yuqM-Z-NauU)
    - intro to ECS [link](https://www.youtube.com/watch?v=WLfhUKp2gag&list=PLX2vGYjWbI0S4yHZwjDI1boIrYStpBCdN)

**June 13**
*RESOURCES*
Leveraging DOTS powered physics:

*DOTS System* 
*Burst Compiler*
    - optimized assembly 
    - C# code to be as fast as C++ 
    - all physics jobs use burst 
        - siumulation query jobs
    - implications: supports subsets of C# no classes means no inheritance
    - collider implements own virtual table
*Entity component system* 
    - design: layout components linearly in memory
        - but physics needs random access 
    - `EntityManager.GetCOmponent()` is slow
    - instead design: uses ICompnentData's to define entity physics properties and mirrors ECS world into physics of NativeArrays
    - Physics Collider (collision world) describes geometry and stored as blittable blob
    - physivs velcoity (dynamics world) linear and angular
    - physics mass (dynamic world) mass and inertia property 
    - physicsjoint (dynamics world) joints definition and pair of entities 
    - as a user, would only deal with components (in entity component system) and deal with them (ie rigidbody or joint) 
*Job System*
    - Broad Phase -> narrow phase -> solve 
    - *Broad Phase* determine / detect which rigidbodies that are in contact or might be in contact
        - output is list of rigidbody pairs 
    - *Narrow Phase* low level collision detection algorithms and produces contact points 
        - outputs to jacobians : mathematical model of all of them 
    - *Solve* takes all the jacobians and determines output
        - this last part has a lot of dependencies behind it and multithreading optimizes this 
- multithreading the simulation is critical 
    - maximize threads 
- collision detection is highly parallelizable 
    - granularity is per body pair 
- look for pair of rigidbodies that can be solves in paralel 
    - take these pairs and puts them into phases 

*DEMO*
    - Add "components" within the inspector 
    1) add "physics shape" 
        - this describes the shape of the objects -> basically wraps rigidbody and collider in one 
    2) add "physics body"
        - needed if you want it to move
            - dynamic: it can move and others can move it, kinematic: it can move and push others but others cant move it, static: 
    - Queries: "view" on the physics world by showing the ray casts in the scene 

Unity Physics Documentation [link](https://docs.unity3d.com/Packages/com.unity.physics@0.1/manual/index.html)

*Unity Physics* 
Design:
    - Stateless: most physics engines have large amounts of cached state to achieve high performance. cost: added complexity in simulation pipeline 
    - modular: core algorithms are deliberatley decoupled from jobs and ECS to encourage their reuse 
    - high performant
Getting started:
    - not having "physics body" will assume its static (anything not meant to move)

    - `unsafe` code enable [link](https://stackoverflow.com/questions/39132079/how-to-use-unsafe-code-unity)
**I had to add the DOTSJoints folder in assets AND a mcs.rsp file to allow it to compile**
- meshes are incredibly expensive i guess because changing the cup to a mesh and the end effectors caused unity to stop responding 

*TODO*
    - redo controller scripts for the arm 
    - redo the shell toggling
    - use unity physics for the scene 

- *Error* (Entities 0.0.12-preview.5 C# 6.0 Error) entity component system [link](https://github.com/Unity-Technologies/EntityComponentSystemSamples/issues/31)
- to use Unity physics, follow the installation guide in unity github (for ecs project) 

- if i switch to unity physics: would have to reimplement the arm using unity physics "components", no available joint drives so would need to create another script to model a motor (a PID) and this won't guarantee 
it would fix the problem with friction

- i dont think i need to switch to unity physics
    - so if i hold down the "close" button on the hand itll actually keep the ball up
    - so there IS enough torque to lift the ball
    - the problem was that there was no "continuous" force to keep the friction on the ball
    - ball still falls when theres rapid stop (like hitting an edge case of the arm) or when moving side to side (probably the torque of one direction (elbow) unbalances the torque applied by the hand)
    - does this make sense: w/ the real bento arm, when it picks up an objects the motor doesnt apply any more torque so what supplies the normal force? the object and the end effector but theres also grippers that provide extra frictional forces and contact points. i raised the mass of the endd effector to 0.2 and it was able to raise the object easily. object does jitter and the arm is "weak" to raise it ? but it does raise it 
- problems with the config joint at edge cases (push elbow up -> move shoulder left and right) it gets stiff sometimes i dont know why ... (FIXED)

**June 14**
- still have mcs.rsp file in assets 
- talked with quinn and riley about the arm further, agreed its okay for now (the arm is still kinda springy and this should be fixed)
- talked with rory to set up UDP connection with Unity (for now it will just be one way proof of concept)

**June 17**
*TODO*
    - going to read thru the BrachIOplexus code (UDP Connection) and try to connect to unity 

- Currently the UDPRecieve.cs in Assets/Scripts/old AND UDPTest.cs in Assets/Scripts + the UDPClient in Documents/Cyrus/UDPClient programs work together
    - UDPClient can send to unity and print the text to the console
- UDPClient in Documents/Cyrus/UDPClient + UDPtoACE works as well:
    ```C#
    string text = Encoding.UTF8.GetString(bytes);
    Console.Write(text);
    ```

- Basically I can send data to brachIOplexus but i can recieve rn, i need to fix the UDPtoScript button

*UDP Comms Protocol* 
- Required: position (?), Velocity, load (?), checksum, which "joystick" or button is pressed 
- similar to how brachIOplexus has it organized, create an input map that will be updated from udp connections

- was able to get a "proof of concept" of having brachioplexus sending a string to unity 
- used UDPtest.cs and added 
```C#
            udpClientTX = new UdpClient();
            ipEndPointTX = new IPEndPoint(localAddr, portTX);
            string text = "yooooo";
            byte[] packet = Encoding.UTF8.GetBytes(text);
            udpClientTX.Send(packet, packet.Length, ipEndPointTX);
```
in `KB_conncect_Click()` because the surprise demo button was greyed out and i just need a place to put it in.

- if "W" is pressed from brachIOplexus, will move the elbow :)
- i think im having problems with the main and sub threads running, it doesnt refresh on screen

**June 18** 
- fixed the earlier issue with the arm not moving up and down. had to comment out the interaction 
with the vr controllers 
- brachIOplexus broke with vs studio lol i cant open the designer so i cannot modify the GUI for unity display 

**June 19**
*TODO*
- create a sample byte array to send on click and see if unity gets it 
- the continuous array is used to control the arm, 
    - so there needs to be a number of bytes expected, count them out and determine what to move from there. 
    - probably just 5 bytes to move each arm for now. 

*Changes to BrachIOplexus*
    - added packet array in Initialization
    - added UDP connection setup in Keyboard controller
    - added keyboard logging in Main loop 

*Things Learned from BrachIOplexus*
- Right now it's easy to just send to Unity what input is being pressed, simple just reading through the `InputMap` matrix 
    - that would be simple way of controlling arm 
    - cannot send over target position and velocity stuff to the virtual bento arm 
    - idk if it's needed to be sent to unity due to the design of the rotaton as it is
- if we did want to send in everything (positon (current, target, min, max), velocity(current, target, min, max), torque stuff, and what is being pressed)
would need to redesign the rotation class to accomodate for those values / would need to redesign the class OR again, potentially creating custon 
script to handle drive motor. 
    - custom drive motor script would be easy, and brachIOplexus can send over the gain values for the PID model 
    - would require more work as to sending the values properly, mapping from brachIOplexus space to Unity space (positions and velocities would be a pain to map)
    - using Robot + Motor classes to get values for each servo motor 
- im not sure what needs to be sent TO brachIOplexus for rn but certain values can be sent back in conjuction with the ID class 
- Another thing is, im not sure what the end goal is with usage but, need  to figure out a way to accomodate for the vr controllers 
    - if using brachIOplexus inputs then input from the controllers would need to be blocked
    - idk if it would be better to connect vr controller to brachIOplexus to have it all there OR just have a detection condition in UNity 
    to see if any data is being recieved from UDP -> if not then use controller else use brachIOplexus 
- tested out the example 1 check sum thing, worked fine. not sure if i need it tho??? 
- the output package to the servo motors are in the Dynamixel region, basically uses the robot class which holds certain values and writes the packet to the motors using dynamixel sdk 
- the `readDyna()` function can read feedback from the dynamixel, maybe can immitiate this for the vr bento arm 
- need to figure out further how much control brachIOplexus will have over Unity 

- illustrates how windows form application works [link](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.application?view=netframework-4.8)
- InitializeComponent() is in AboutBox1.Designer.cs

**June 20**
- working on velocity ramp for the arm
- sorta works but it seems one direction is going faster than other direction
- also elbow moves when its not supposed to 
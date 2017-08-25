## HoloLens - "LandscapeAdventure" Application for MixedReality
Inside of this project you'll be able to find all realized functionality from next "Holographic Academy" chapters: 101, 210, 211, 212 and 230 which are updated daily as fast as an owner adds new functionality.
#### (image #1: Unity's area)
![5](https://user-images.githubusercontent.com/26745790/28887901-7c65411e-77c7-11e7-9715-84f76c30a053.png)

## Functionality
The project works in conjunction with the "HoloToolkit-Unity" and "MRDesign_Labs" resources to enhance the speed of developing. Perfom "Tapped_Event" to assign a needed recognizer or to record a user's message through the Menu. All details will be described in the part which is called "Used classes (structures) in conjunction with scripts".

## Clever Holograms: the easiest way to move them from one location to another.
Holograms can follow the user instead. This tag-along hologram position itself relative to the user when you'll say "Follow Me",
no matter where they walk. You may even choose to bring a hologram with you for a while after that say "Stop Follow Me" and then place it on the wall once you get to another room based on Spatial Mapping Data, so your hologram won't be placed behind the wall.
#### (video example in the "DEMO" folder of this project: https://github.com/Go2bad/HoloLens_LandscapeAdventure/blob/master/DEMO/video.gif)

## UI Menu (circle buttons + text) with "Tagalong" script
Every button is unique and perfoms its own appropriate command like "Open Recording Menu" or "Change recognizer from "Scale" to "Rotate"" and etc. You can assign any icon and text for those buttons and what's more, the menu moves around the user smoothly like a Tagalong and always exist in the user's field of view. And as a consequence of it, it won't be frustrating experience for the end user.
Tap on "Record" button on the user's voice will be recorded and while you will be speaking the user's speech will be shown on the top of the menu, so the user will be able to see how the HoloLens understands his.
#### (image #2: UI Menu: 1) text (user's speech, description of actions), 2) buttons)
![3](https://user-images.githubusercontent.com/26745790/28887936-93c4d202-77c7-11e7-9956-a167fdbfa953.png)

## Directional Indicator
That is the best way to find the hologram if you've losted it. The indicator will guide you where exactly this or that hologram is existing. In the "DirectionalIndicator" script you can play with some values to understand how it actually works.
#### (image #3: Visual feedback: Directional Indicator)
![2](https://user-images.githubusercontent.com/26745790/28887962-a26786f6-77c7-11e7-97a6-e96f4ea50c77.png)

## Hologram's description with "BillBoard" script
There you will be able to find the information about the hologram and two "enable" and "disbale visual meshes" buttons to show and hide the mesh respectively. Also the description is always looking at the user so you will be able to read the text on it like all the time.
#### (image #4: UI Text Menu with rectangle buttons (Enable / Disable Meshes))
![4](https://user-images.githubusercontent.com/26745790/28887980-adae300a-77c7-11e7-8c16-d795c0dd6d40.png)

## Gestures and visual feedback
In terms of manipulating with a hologram you can scale or rotate any object around. You can change the recognizer by tapping on the appropriate button. Notice that when you are gazing at a hologram you can see a visual feedback which will help to understand what gesture is actually turned on at the moment and will be recognized by the HoloLens. 
#### (image #5: Rotate / Scale Prefab near the cursor: two kinds of gestures)
![1](https://user-images.githubusercontent.com/26745790/28888026-cf31b3fa-77c7-11e7-9b0f-32334fcdd1f9.png)

## Keywords (assignment through the Inspector Panel)
If you want to assign additional keywords except "Enable / Disable Mesh" it is going to be very easy because in this application you can assign the appropriate keywords' events for "KeywordRecognizer" through the Inspector Panel in the "KeywordManager" script.

## SpatialMapping (constantly scanning of the environment)
In this project the room's mesh is updating every 4 seconds and you can decide between to enable or disbale that mesh. You can also upload your custom room and add the mesh to it.
#### (image #6: Spatial Mapping Data: constantly scanning every 4 seconds)
![6](https://user-images.githubusercontent.com/26745790/28958482-2fadf2f6-78ff-11e7-9d1d-f3f7e00afd0e.png)

## Used classes (structures) in conjunction with scripts
Here' is the list of them:
- GestureRecognizer
- KeywordRecognizer
- DictationRecognizer
- MicrophoneManager
- GeometryUtility
- Tagalong
- BillBoard
- DirectionIndicator

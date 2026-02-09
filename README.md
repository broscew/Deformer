# Deformer

## Description
This is a simple mesh deformer done on Unity with a custom plane.obj

## Build/ Run Instructions 
- Clone the git
- Open the project
- Build and Run

## Architecture
The architecture is designed to have scripts be as self contained as possible with each one only handling a specific task.

### Input Reader
This scriptable object is designed to keep the input of the deformer editor in a self contained file, it also provides the
flexibility down the line if there are new inputs being added to the deformer editor.

### Deformer
This is a component that manages the deformation of the mesh. Job system is used for the deformation as it is better at 
managing large numbers of vertex than a single-threaded implementation . It offers two types of mesh manipulation, a deform 
action and a reset action.

### Deformer Model
This manages the interaction between Input, View Model, and the deformer objects. It provides the logic for grabbing the 
necessary information before pass it to the object's deformer component to handle the actual deformation well also handling
user defined changes on the UI through the event updates from the View Model

### User Interface (UI)
The UI architecture is setup to using a simple Model View View Model (MVVM) pattern to better split the actual model logic 
from UI related code. The respective canvas objects (FPS Canvas, Deformer Panel Canvas) has a View related component that 
manages the UI Objects tied to the canvas; which includes value change events and widget level behaviour(e.g. Stepped Slider)

Each View Model is created by the View and serves as a bridge between the UI and the model logic. This helps remove the
dependency between the two since the UI isn't blocked by the need to wait for a particular logic to be implemented. The
View Models do not directly update the View but it is handled through a simple event bindings this allows for easier
UI test without the need of an actual UI and also allows for it to be used in other Canvas screens as well.

There are two model components in the current project, FrameRateCounter Model and Deformer Model. Each one purely handling
their respective logics and have no dependencies on UI in general. Structurally, this allows the project to support different
UI systems (e.g. UI ToolKit) or implementation without it being a major risk.

## Improvements
Due to the time, the approach for the models are through singleton due to the ease of implementation, this isn't ideal as it 
can potentially lead to hidden dependencies. Therefore if there is a need to expand on it, a service locator pattern would 
have been a better solution.

The ResetMeshs uses FindObjectsByType which can be slow if there are a lot of game objects in the scene. Caching or pooling
the Game Objects will reduce the need for calling this function.



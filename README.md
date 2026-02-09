# Deformer

## Description
This is a simple mesh deformer created in Unity using a custom plane.obj

## Build/ Run Instructions 
- Clone the git
- Open the project
- Open MainScene
- Build and Run

## Architecture
The architecture is designed to have scripts be as self-contained as possible, with each one handling a specific task.

### Input Reader
This scriptable object is designed to keep the input of the deformer editor in a self-contained file; it also provides the
flexibility down the line if there are new inputs being added to the deformer editor.

### Deformer
This component manages the deformation of the mesh. The job system is used for deformation as it is better at
managing large numbers of vertices than a single-threaded implementation. It offers two types of mesh manipulation: a deform
action and a reset action.

### Deformer Model
This manages the interaction between the input, the view model, and the deformer objects. It provides the logic for grabbing the
necessary information before passing it to the object’s deformer component to handle the actual deformation, as well as handling
user-defined changes on the UI through the event updates from the View Model

### User Interface (UI)
The UI architecture is set up to use a simple Model View ViewModel (MVVM) pattern to better split the actual model logic
from UI related code. The respective canvas objects (FPS Canvas, Deformer Panel Canvas) have a View-related component that
manages the UI Objects tied to the canvas, which includes value change events and widget-level behaviour(e.g. Stepped Slider)

Each View Model is created by the View and serves as a bridge between the UI and the model logic. This helps remove the
dependency between the two since the UI isn’t blocked by the need to wait for a particular logic to be implemented. The
View Models do not directly update the View but it is handled through simple event bindings. This allows for easier
UI test without the need of an actual UI and also allows for it to be used in other Canvas screens.

There are two model components in the current project, FrameRateCounter Model and Deformer Model. Each one purely handles
its respective logic and has no hard dependencies on UI in general. Structurally, this allows the project to support different
UI systems (e.g. UI Toolkit) or implementation without it being a major risk.

## Improvements
Due to the time, the approach for the models is through a singleton due to the ease of implementation, but this isn’t ideal, as it
can potentially lead to hidden dependencies. Therefore, if there is a need to expand on it, a service locator pattern would
have been a better solution.

The ResetMesh uses FindObjectsByType, which can be slow if there are a lot of game objects in the scene. Caching or pooling the Game 
Objects will reduce the need for calling this function.


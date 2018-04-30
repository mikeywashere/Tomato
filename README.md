# Tomato

Simple command line Pomodoro timer and work item list.

I decided to use dotnet core 2.1 since I already had the tools installed. In hindsight that might have been a mistake since a command line program is run using

dotnet todo.console.dll add -t "Todo list item number one"

which is a bit cumbersome

You can run this command to generate a lot of list items

FOR /L %f IN (1,1,100) DO dotnet Todo.Console.dll add -t "Item %f"

That command generates 100 items - you can easily generate more. by changing the value of 100 to something rediculous.

## Projects

### Todo.Console
- The todo app console runner.
- Saves the todo list in a file names data.list
- TODO: Add the ability to specify a filename. Easy to add as a command line parameter
- Has full command line options and help
```
Todo 1.0.0
Copyright (C) 2018 Todo.Console

  add        Add a todo item

  list       Show the items.

  remove     Remove a todo item.

  move       Move a todo item.

  help       Display more information on a specific command.

  version    Display version information.
```

### Todo.Core
- Contains TodoItem - just a data holder - and TodoItemSortedList
- TodoItemSortedList
  - Allows for easy reordering of todo items.
  - Internally it is a SortedList - the SortedList index is kept internal and unseen and neat and orderly

### ToDo.Extensions
- Easy to use methods for saving and loading the todo list from a file
- Sure, a full on repository would have been nice...

### Tomato.Console
- Pomodoro console runner
- Featureless. It runs the pomodoro timer one time through four iterations with breaks.

- Output example

```
Starting Pomodoro Timer
Work Time 1: 0: 25
Work Time 1: 1: 25
Work Time 1: 2: 25
Work Time 1: 3: 24
```

Where the data is colon separated. 
The first block "Work Time 1" is the name of the ProcessStep.
Following that is the Percentage complete and then the number of minutes remaining.

### Tomato.Core
- Pomodoro: The actual configured timer.
- - Takes a IPropertyRepository to store the names of the steps
- PomodoroProgress is just a data container
- WaitTimeStep: The timer that runs and fires events.
  - Used for all the process steps as that's all a pom timer is

### Tomato.Interface
- Interfaces that may need to be shared

### Tomato.Process
- Process: The code that processes steps
  - Since a Pom timer is really just a sequence of
      - Work time
      - Short Break Time
      - Work time
      - Short Break Time
      - Work time
      - Short Break Time
      - Work time
      - Long Break Time
  - I implemented it as a sequence of IProcessStep objects
  - Pros:
    - Allows for reuse for other projects that might require a processing sequence
    - Allows for flexibiliy and configurability
  - Cons:
    - More complicated than just implementing a timer with state (maybe)
- PercentageProgressArgs is just a data container to pass to events

### Tomato.Repository
- InMemoryRepository
  - Stores key value pairs in memory, uses ConcurrentDictionary just in case we ever need to have thread safety.
- PropertyRepository
  - Stores a property collection for objects that you may not want to tack more properties onto
  - This is how Names of process steps get stored.
  - This does require a guid to identify the object. So IProcessStep has "Guid Id"
  - The guid was just the easy way to do this. I could have also used an object reference, which I considered momentarily.

### Tomato.Settings
- Settings:
  - A stupid easy way to implement settings that is testable

### Todo.Core.Test
- Test TodoItemSortedList
  - Can we add, move, remove items

### Todo.Process.Test
- Test Process object
  - Adding steps
  - Progressing steps
  - Wrapping steps when RotateToStartWhenDone is true
  - OnLastStep returns the correct value
  - Current returns the correct value

### Tomato.Repository.Test
- Test the InMemoryRepository
  - Not a lot to test here - can we put and get?

### Tomato.Test
- Lot's of event testing going on here

## Summary
I had fun. This is interesting.

I wish I had time to create a real UI. 
I usually create test projects and work in a TDD fashion but sometimes I like to create a command line app to do testing.
It allows for a more "developer" experience of using the objects you are creating.
Writing tests is great, it allows you to know that the objects you are creating are testable.
Writing an app lets you know that they are usable and are providing the functionallity that a developer will need to use them in the real world.

I wanted to create a web UI but since I went down this road of "DotNet Core 2.1" that didn't allow me to create the cheesy web UI I was planning.

Typically I would have used more IoC - I definitely had the Interfaces necessary to use Autofac (I am failry familiar with that toolset) but I didn't see the necessity in this project. I think you can see that I was aiming for testable, autofacable would be next and if there was a pretty web UI it would have had to have happened.

I prefer to commit early and often with very small commits. I didn't really do that here. Might have something to do with working mostly between 5pm and 5 am. :-)



# Tomato

Simple command line Pomodoro timer and work item list.

I decided to use dotnet core 2.1 since I already had the tools installed. In hindsight that might have been a mistake since a command line program is run using

dotnet todo.console.dll add -t "Todo list item number one"

which is a bit cumbersome

You can run this command to generate a lot of list items

FOR /L %f IN (1,1,100) DO dotnet Todo.Console.dll add -t "Item %f"

That command generates 100 items - you can easily generate more. by changing the value of 100 to something rediculous.

#Projects
Todo.Console
	The todo app console runner.
	Saves the todo list in a file names data.list
	TODO: Add the ability to specify a filename
		  Easy to add as a command line parameter

Todo.Core
	Contains TodoItem - just a data holder - and 



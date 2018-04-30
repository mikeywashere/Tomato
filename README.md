# Tomato

Simple command line Pomodoro timer and work item list.

I decided to use dotnet core 2.1 since I already had the tools installed. In hindsight that might have been a mistake since a command line program is run using

dotnet todo.console.dll -a -t "Todo list item number one"

which is a bit cumbersome
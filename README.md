# Periodic Reminder

Displays a timed alert at an interval. Use it for reminding you of a task that must be done repeatedly, like hourly eyedrops or daily system checks.

## Command line arguments

You can make a bat file or shortcut to start a reminder with a custom time and info text. Put it in the Startup folder to run it when logging into Windows.

| Command | Function                                                                        |
|---------|---------------------------------------------------------------------------------|
|/m       | perform first task at the H:m mark, e.g. if it's 11:50, /m 34 happens at 12:34  |
|/t       | perform first task at the specified time                                        |
|/i       | set interval between alerts                                                     |
|/start   | auto start the timer when the program starts                                    |
|         | Info text (no slash command)                                                    |

Examples:
```
PeriodicReminder.exe /m 00 /start /i 120 Take your medication
PeriodicReminder.exe Take your medication
PeriodicReminder.exe Go to bed /t 23:30 /i 1440
```

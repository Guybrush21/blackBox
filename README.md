# blackBox
The purpose of this project is to start and stop some windows program commanded by some input received from the outside, (in my case from the Serial Port which will have an Arduino attached).

This is accomplished with a WPF GUI which manages the configuration of program that should start along with the actual Serial Port listener. Beacuse I'm not a great architect nor a WPF expert (this is my first attmept to WPF) this is probably a bad example of how this should be done. 
But at least it works, if someone wants to help me improve this I will do my best in order to manage the pull request. (No idea of of all of this works)

My Arduino project consist of 6 simple ON\OFF switch and a bunch of eyecandy led which will just send the switch "number" along with its state through the Serial on every ON\OFF.  

Cheers. 
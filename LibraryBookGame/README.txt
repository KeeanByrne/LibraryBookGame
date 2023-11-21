






																										Keean  Byrne
																										 ST10238118
																										  Prog7312
Link for GitHub Repository: https://github.com/KeeanByrne/LibraryBookGame
		
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//		
				On startup, the application allows the user to choose between three tasks:
				a. Replacing books.
				b. Identifying areas.
				c. Finding call numbers.

				For Part 2, Replacing Books / Identifying Areas have been enabled and Finding Call Numbers remains disabled.

				When the user selects Identifying Areas, the application runs a match the column game. The data is stored in a dictionary. The data stored is high level call numbers ranging from 000 - 900 and their respective descriptions. 
				The user can click on a call number and then click on whichever description they think is the correct one associated with the call number. The game will randomly alternate between matching call numbers to their descriptions and 
				then matching descriptions to call numbers. There is a timer of 30 seconds and the user can try get as many matches correct before the time runs out. For every correct match the user earns 10 points and for every incorrect 
				match the loser loses 5 points. There will only ever be 4 questions (either call numbers or their descriptions) and then 7 possible answers. 

				The gamification features added were that of the points system and a timer. 

				For Part 3, Replacing Books / Identifying Areas and Finding Call Numbers have all been enabled. 

				When the user selects Finding Call Numbers, they need to click the Start button to be presented with the quiz. The ListViews are populated with data taken from the DataCallNumbers.txt file and are stored in a tree in memory.
				The user is presented with a 3rd level call number and needs to match this call number to its 1st and 2nd level categories.

				The gamification features added were that of the points system and a timer. For each correct match the user is awared 5 points and for each incorrect match the user loses 10 points.

				Features changed have been working on functionality of part 1 and 2. Part 1s timer has been corrected. Better structure and File layout/coordination was followed. 


//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

Unzip the folder titled ST10238118_Prog7312_Poe_Part3. 

Preferably use Visual Studio 2022 and .NET Framework 4.8 to run this application. 

To run this application, you need to start the application. Upon starting the application you will be met with a starting screen. Clicking the "Get Started?" will take you to the main page where you will be presented with 
the "Replacing Books" game. You can then click on Identifying Areas or Finding Call Numbers and the program will redirect you to the respective usercontrol. Click the "How To Play" button to learn how to play the game. 
Click start when you are ready to beat the timer! Beware, for each incorrect choice, you do lose 10 points.

//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
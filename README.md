# LibraryBookGame


Task 2: IMPLEMENTATION
Finding the correct place for a book on the shelves in the library requires librarians to be able to 
sort call numbers in numerical and then alphabetical order. The call number for the prescribed 
book for this module is 005.73 JAM – the numbers indicate the topic of the book, and the letters 
are the first three letters of the author’s surname.
Write a C# software application that fulfils the following requirements:
1. On startup, the application shall allow the user to choose between three tasks:
a. Replacing books.
b. Identifying areas.
c. Finding call numbers.
2. For this first task, only Replacing books will be implemented – disable the other two 
options for now.
3. When the user selects Replacing books, the application shall randomly generate 10
different call numbers, and display them to the user.
4. The application shall allow the user to reorder the call numbers in ascending order, and the 
application shall check whether the user got the ordering right.
5. Implement the gamification feature that you identified to motivate users to keep learning.
Technical requirements:
1. Make use of a list to store the generated call numbers.
2. Choose an appropriate sorting algorithm to sort the call numbers to check the order that 
the user put them in

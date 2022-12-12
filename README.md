# Developer Technical Assessment


**Andreas Nikolaidis submission**

**Project contents**

- Shared class library
- Blazor hosted web project for a front-end management of student records
- Web Api project to read and write from json files (json files moved to this project for ease of access - I hope this is OK)
- A unit test project (I am still new to unit testing and eager to develop these skills so I appreciate they may not follow best practice)

**Improvements that should be made in production or if I had more time**
- I would move direect api calls in razor page to an api service, so it can be re-used by other front ends(mobile, MVC)
- I would use separate viewmodels for the frontend with validation attributes to ensure proper separation
- Refactoring of some of the logic around Ids of course assignments and student Id
- Improvements to unit testing
- I have had to make some assumtions about some properties of the objects (Course assignment details etc)
- Validation of the models in blazor needs some work
- Deletion of course assigments/students was not in the scope of the requirements but would be useful
- I would improve the logic around adding duplicate courses but this could be desired behaviour
- Styling/formatting improvements
- Refactoring for optimisation


Please let me know if there are any difficulties running the project, or if you have any questions

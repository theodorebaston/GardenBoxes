## Garden Boxes (Instruction)

Write a program that will take in a users garden box size, then let them pick from a list of plants and tell them how many they can plant in that space.

Please create a database that will hold plants. You don't need to add more than 2 or 3 plants into the database for testing. Please make sure the database or a description of the database is included in your repo.

## Database
The database contains a single table named 'Plants'. It consists of three columns. 
- The first column is named 'Id' of type int and has the IDENTITY property
- The second column is named 'plantName' of type nvarchar(50)
    - This is the name of the plant
- The third column is named 'plantsPerSqFt' of type decimal(10,4)
    - This is the number of plants that can be planted within 1 square foot
